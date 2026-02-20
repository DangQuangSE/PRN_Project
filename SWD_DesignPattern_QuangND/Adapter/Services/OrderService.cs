using Adapter.Interfaces;
using Adapter.Models;

namespace Adapter.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private IPaymentProcessor _paymentProcessor;

        public OrderService(IOrderRepository orderRepository, IPaymentProcessor paymentProcessor)
        {
            _orderRepository = orderRepository;
            _paymentProcessor = paymentProcessor;
        }

        public void SetPaymentProcessor(IPaymentProcessor processor)
        {
            _paymentProcessor = processor;
            Console.WriteLine($"\n[INFO] Payment method changed to: {processor.PaymentMethodName}");
        }

        public Order CreateOrder(string customerName, string beverageName, decimal amount, int quantity)
        {
            decimal totalAmount = amount * quantity;
            decimal fee = _paymentProcessor.GetTransactionFee(totalAmount);
            decimal finalAmount = totalAmount + fee;

            var order = new Order
            {
                CustomerName = customerName,
                BeverageName = beverageName,
                Amount = amount,
                Quantity = quantity,
                TotalAmount = finalAmount,
                PaymentMethod = _paymentProcessor.PaymentMethodName,
                PaymentStatus = "Processing"
            };

            var createdOrder = _orderRepository.Create(order);

            Console.WriteLine($"\n[INFO] Creating Order #{createdOrder.Id}...");
            Console.WriteLine($"     Subtotal: {totalAmount:C} | Fee: {fee:C} | Total: {finalAmount:C}");

            bool paymentSuccess = _paymentProcessor.ProcessPayment(
                createdOrder.Id, 
                finalAmount, 
                customerName
            );

            if (paymentSuccess)
            {
                createdOrder.PaymentStatus = _paymentProcessor.GetPaymentStatus(createdOrder.Id);
                _orderRepository.Update(createdOrder.Id, createdOrder);
                Console.WriteLine($"[SUCCESS] Order #{createdOrder.Id} created successfully!");
                return createdOrder;
            }

            _orderRepository.Delete(createdOrder.Id);
            Console.WriteLine($"[ERROR] Payment failed! Order cancelled.");
            return null;
        }

        public List<Order> GetAllOrders() => _orderRepository.GetAll();

        public Order GetOrderById(int id) => _orderRepository.GetById(id);

        public bool UpdateOrder(int id, string customerName, string beverageName, decimal amount, int quantity)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                Console.WriteLine($"[ERROR] Order #{id} not found!");
                return false;
            }

            order.CustomerName = customerName;
            order.BeverageName = beverageName;
            order.Amount = amount;
            order.Quantity = quantity;
            order.TotalAmount = order.CalculateTotal();

            if (_orderRepository.Update(id, order))
            {
                Console.WriteLine($"[SUCCESS] Order #{id} updated successfully!");
                return true;
            }

            return false;
        }

        public bool CancelOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                Console.WriteLine($"[ERROR] Order #{id} not found!");
                return false;
            }

            Console.WriteLine($"\n[INFO] Processing refund for Order #{id}...");

            bool refundSuccess = _paymentProcessor.RefundPayment(id);
            
            if (refundSuccess)
            {
                order.PaymentStatus = _paymentProcessor.GetPaymentStatus(id);
                _orderRepository.Update(id, order);
                _orderRepository.Delete(id);
                Console.WriteLine($"[SUCCESS] Order #{id} cancelled and refunded!");
                return true;
            }

            Console.WriteLine($"[ERROR] Refund failed!");
            return false;
        }

        public void DisplayAllOrders()
        {
            var orders = _orderRepository.GetAll();
            if (!orders.Any())
            {
                Console.WriteLine("  No orders found.");
                return;
            }

            Console.WriteLine($"\n  Total Orders: {orders.Count}");
            Console.WriteLine("  " + new string('-', 65));

            foreach (var order in orders)
            {
                order.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void DisplayOrdersSummary()
        {
            var orders = _orderRepository.GetAll();
            if (!orders.Any()) return;

            var totalRevenue = orders.Sum(o => o.TotalAmount);
            var totalOrders = orders.Count;

            Console.WriteLine($"  Total Revenue: {totalRevenue:C}");
            Console.WriteLine($"  Total Orders: {totalOrders}");
        }
    }
}
