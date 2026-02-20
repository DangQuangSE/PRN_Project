using Adapter.Adapters;
using Adapter.Interfaces;
using Adapter.Repositories;
using Adapter.Services;

namespace Adapter
{
    class Program
    {
        private static IOrderRepository _orderRepository;
        private static OrderService _orderService;
        private static IPaymentProcessor _momoAdapter;
        private static IPaymentProcessor _zaloPayAdapter;
        private static IPaymentProcessor _bankingAdapter;
        private static IPaymentProcessor _currentPaymentProcessor;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Initialize dependencies
            _orderRepository = new OrderRepository();
            _momoAdapter = new MoMoPaymentAdapter();
            _zaloPayAdapter = new ZaloPayAdapter();
            _bankingAdapter = new BankingPaymentAdapter();
            _currentPaymentProcessor = _momoAdapter; // Default payment method

            _orderService = new OrderService(_orderRepository, _currentPaymentProcessor);

            bool exit = false;
            while (!exit)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateOrderMenu();
                        break;
                    case "2":
                        ViewAllOrders();
                        break;
                    case "3":
                        ViewOrderById();
                        break;
                    case "4":
                        UpdateOrderMenu();
                        break;
                    case "5":
                        CancelOrderMenu();
                        break;
                    case "6":
                        ChangePaymentMethodMenu();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("\nGoodbye! Thanks for shopping with us.");
                        break;
                    default:
                        Console.WriteLine("\n[ERROR] Invalid choice! Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("       ADAPTER PATTERN - PAYMENT SYSTEM DEMO");
            Console.WriteLine("              Beverage E-Commerce Shop");
            Console.WriteLine(new string('=', 60) + "\n");
            Console.WriteLine($"  Current Payment Method: {_currentPaymentProcessor.PaymentMethodName}");
            Console.WriteLine("  " + new string('-', 58));
            Console.WriteLine("  MAIN MENU:");
            Console.WriteLine("  " + new string('-', 58));
            Console.WriteLine("  1. Create New Order");
            Console.WriteLine("  2. View All Orders");
            Console.WriteLine("  3. Search Order by ID");
            Console.WriteLine("  4. Update Order");
            Console.WriteLine("  5. Cancel Order (Refund)");
            Console.WriteLine("  6. Change Payment Method");
            Console.WriteLine("  0. Exit");
            Console.WriteLine("  " + new string('-', 58));
            Console.Write("\n  Enter your choice: ");
        }

        static void CreateOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                  CREATE NEW ORDER");
            Console.WriteLine(new string('=', 60) + "\n");
            Console.WriteLine($"  Payment Method: {_currentPaymentProcessor.PaymentMethodName}");
            Console.WriteLine("  " + new string('-', 58) + "\n");

            Console.Write("  Customer Name: ");
            string customerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("\n[ERROR] Customer name cannot be empty!");
                return;
            }

            Console.WriteLine("\n  Available Beverages:");
            Console.WriteLine("  1. Coca Cola - $1.50");
            Console.WriteLine("  2. Pepsi - $1.50");
            Console.WriteLine("  3. Orange Juice - $2.50");
            Console.WriteLine("  4. Apple Juice - $2.30");
            Console.WriteLine("  5. Milk Tea - $3.50");
            Console.WriteLine("  6. Matcha Milk Tea - $3.80");
            Console.WriteLine("  7. Water - $1.00");
            Console.WriteLine("  8. Other (Custom)");

            Console.Write("\n  Select beverage (1-8): ");
            string beverageChoice = Console.ReadLine();

            string beverageName;
            decimal price;

            switch (beverageChoice)
            {
                case "1":
                    beverageName = "Coca Cola";
                    price = 1.50m;
                    break;
                case "2":
                    beverageName = "Pepsi";
                    price = 1.50m;
                    break;
                case "3":
                    beverageName = "Orange Juice";
                    price = 2.50m;
                    break;
                case "4":
                    beverageName = "Apple Juice";
                    price = 2.30m;
                    break;
                case "5":
                    beverageName = "Milk Tea";
                    price = 3.50m;
                    break;
                case "6":
                    beverageName = "Matcha Milk Tea";
                    price = 3.80m;
                    break;
                case "7":
                    beverageName = "Water";
                    price = 1.00m;
                    break;
                case "8":
                    Console.Write("  Enter beverage name: ");
                    beverageName = Console.ReadLine();
                    Console.Write("  Enter price ($): ");
                    if (!decimal.TryParse(Console.ReadLine(), out price))
                    {
                        Console.WriteLine("\n[ERROR] Invalid price!");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("\n[ERROR] Invalid choice!");
                    return;
            }

            Console.Write("  Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("\n[ERROR] Invalid quantity!");
                return;
            }

            _orderService.CreateOrder(customerName, beverageName, price, quantity);
        }

        static void ViewAllOrders()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                    ALL ORDERS LIST");
            Console.WriteLine(new string('=', 60));

            _orderService.DisplayAllOrders();
            _orderService.DisplayOrdersSummary();
        }

        static void ViewOrderById()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                  SEARCH ORDER BY ID");
            Console.WriteLine(new string('=', 60) + "\n");

            Console.Write("  Enter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[ERROR] Invalid ID!");
                return;
            }

            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                Console.WriteLine($"\n[ERROR] Order #{id} not found!");
                return;
            }

            Console.WriteLine("\n[SUCCESS] Found:");
            Console.WriteLine("  " + new string('-', 65));
            order.DisplayInfo();
        }

        static void UpdateOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                    UPDATE ORDER");
            Console.WriteLine(new string('=', 60) + "\n");

            Console.Write("  Enter Order ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[ERROR] Invalid ID!");
                return;
            }

            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                Console.WriteLine($"\n[ERROR] Order #{id} not found!");
                return;
            }

            Console.WriteLine("\n  Current Order Info:");
            order.DisplayInfo();

            Console.WriteLine("\n  " + new string('-', 65));
            Console.WriteLine("  Enter new information (press Enter to keep current value):\n");

            Console.Write($"  Customer Name [{order.CustomerName}]: ");
            string customerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(customerName)) customerName = order.CustomerName;

            Console.Write($"  Beverage Name [{order.BeverageName}]: ");
            string beverageName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(beverageName)) beverageName = order.BeverageName;

            Console.Write($"  Price [{order.Amount}]: ");
            string priceInput = Console.ReadLine();
            decimal price = string.IsNullOrWhiteSpace(priceInput) ? order.Amount : decimal.Parse(priceInput);

            Console.Write($"  Quantity [{order.Quantity}]: ");
            string qtyInput = Console.ReadLine();
            int quantity = string.IsNullOrWhiteSpace(qtyInput) ? order.Quantity : int.Parse(qtyInput);

            Console.WriteLine();
            _orderService.UpdateOrder(id, customerName, beverageName, price, quantity);
        }

        static void CancelOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                 CANCEL ORDER (REFUND)");
            Console.WriteLine(new string('=', 60) + "\n");

            Console.Write("  Enter Order ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[ERROR] Invalid ID!");
                return;
            }

            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                Console.WriteLine($"\n[ERROR] Order #{id} not found!");
                return;
            }

            Console.WriteLine("\n  Order to cancel:");
            order.DisplayInfo();

            Console.Write("\n[WARNING] Are you sure you want to cancel and refund this order? (y/n): ");
            string confirm = Console.ReadLine()?.ToLower();

            if (confirm == "y")
            {
                // Switch to correct payment processor for refund
                IPaymentProcessor refundProcessor = order.PaymentMethod switch
                {
                    "MoMo Wallet" => _momoAdapter,
                    "ZaloPay" => _zaloPayAdapter,
                    "Bank Transfer" => _bankingAdapter,
                    _ => _currentPaymentProcessor
                };

                _orderService.SetPaymentProcessor(refundProcessor);
                _orderService.CancelOrder(id);
                
                // Switch back to user's selected payment method
                _orderService.SetPaymentProcessor(_currentPaymentProcessor);
            }
            else
            {
                Console.WriteLine("\n[INFO] Cancellation aborted.");
            }
        }

        static void ChangePaymentMethodMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                CHANGE PAYMENT METHOD");
            Console.WriteLine(new string('=', 60) + "\n");
            Console.WriteLine($"  Current: {_currentPaymentProcessor.PaymentMethodName}");
            Console.WriteLine("  " + new string('-', 58) + "\n");
            Console.WriteLine("  Available Payment Methods:");
            Console.WriteLine("  1. MoMo Wallet (Fee: 1%)");
            Console.WriteLine("  2. ZaloPay (Fee: 1.5%)");
            Console.WriteLine("  3. Bank Transfer (Fee: 0.5% or fixed)");
            Console.WriteLine("  0. Back to Main Menu");
            Console.WriteLine("  " + new string('-', 58));
            Console.Write("\n  Select payment method: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _currentPaymentProcessor = _momoAdapter;
                    _orderService.SetPaymentProcessor(_momoAdapter);
                    break;
                case "2":
                    _currentPaymentProcessor = _zaloPayAdapter;
                    _orderService.SetPaymentProcessor(_zaloPayAdapter);
                    break;
                case "3":
                    _currentPaymentProcessor = _bankingAdapter;
                    _orderService.SetPaymentProcessor(_bankingAdapter);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\n[ERROR] Invalid choice!");
                    return;
            }

            Console.WriteLine($"\n[SUCCESS] Payment method changed to: {_currentPaymentProcessor.PaymentMethodName}");
        }
    }
}