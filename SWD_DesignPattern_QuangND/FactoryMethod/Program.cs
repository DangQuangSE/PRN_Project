using FactoryMethod.Factories;
using FactoryMethod.Interfaces;
using FactoryMethod.Repositories;
using FactoryMethod.Services;

namespace FactoryMethod
{
    class Program
    {
        private static IBeverageRepository _repository;
        private static BeverageService _beverageService;
        private static SoftDrinkFactory _softDrinkFactory;
        private static JuiceFactory _juiceFactory;
        private static MilkTeaFactory _milkTeaFactory;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Initialize dependencies
            _repository = new BeverageRepository();
            _beverageService = new BeverageService(_repository);
            _softDrinkFactory = new SoftDrinkFactory(_repository);
            _juiceFactory = new JuiceFactory(_repository);
            _milkTeaFactory = new MilkTeaFactory(_repository);

            bool exit = false;
            while (!exit)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateBeverageMenu();
                        break;
                    case "2":
                        ViewAllBeverages();
                        break;
                    case "3":
                        ViewBeverageById();
                        break;
                    case "4":
                        UpdateBeverageMenu();
                        break;
                    case "5":
                        DeleteBeverageMenu();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("\nGoodbye! Thanks for using Beverage Management System.");
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
            Console.WriteLine("     FACTORY METHOD PATTERN - BEVERAGE MANAGEMENT");
            Console.WriteLine(new string('=', 60) + "\n");
            Console.WriteLine("  MAIN MENU:");
            Console.WriteLine("  " + new string('-', 58));
            Console.WriteLine("  1. Create New Beverage");
            Console.WriteLine("  2. View All Beverages");
            Console.WriteLine("  3. Search Beverage by ID");
            Console.WriteLine("  4. Update Beverage");
            Console.WriteLine("  5. Delete Beverage");
            Console.WriteLine("  0. Exit");
            Console.WriteLine("  " + new string('-', 58));
            Console.Write("\n  Enter your choice: ");
        }

        static void CreateBeverageMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                  CREATE NEW BEVERAGE");
            Console.WriteLine(new string('=', 60) + "\n");
            Console.WriteLine("  Select Beverage Type:");
            Console.WriteLine("  " + new string('-', 58));
            Console.WriteLine("  1. Soft Drink");
            Console.WriteLine("  2. Juice");
            Console.WriteLine("  3. Milk Tea");
            Console.WriteLine("  0. Back to Main Menu");
            Console.WriteLine("  " + new string('-', 58));
            Console.Write("\n  Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateSoftDrink();
                    break;
                case "2":
                    CreateJuice();
                    break;
                case "3":
                    CreateMilkTea();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\n[ERROR] Invalid choice!");
                    break;
            }
        }

        static void CreateSoftDrink()
        {
            Console.WriteLine("\n  === CREATE SOFT DRINK ===\n");

            Console.Write("  Name: ");
            string name = Console.ReadLine();

            Console.Write("  Price ($): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("\n[ERROR] Invalid price!");
                return;
            }

            Console.Write("  Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("\n[ERROR] Invalid quantity!");
                return;
            }

            Console.Write("  Brand: ");
            string brand = Console.ReadLine();

            Console.Write("  Volume (ml): ");
            if (!int.TryParse(Console.ReadLine(), out int volume))
            {
                Console.WriteLine("\n[ERROR] Invalid volume!");
                return;
            }

            var data = new Dictionary<string, object>
            {
                { "Brand", brand },
                { "Volume", volume }
            };

            Console.WriteLine();
            _softDrinkFactory.CreateBeverage(name, price, quantity, data);
        }

        static void CreateJuice()
        {
            Console.WriteLine("\n  === CREATE JUICE ===\n");

            Console.Write("  Name: ");
            string name = Console.ReadLine();

            Console.Write("  Price ($): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("\n[ERROR] Invalid price!");
                return;
            }

            Console.Write("  Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("\n[ERROR] Invalid quantity!");
                return;
            }

            Console.Write("  Fruit: ");
            string fruit = Console.ReadLine();

            Console.Write("  Is Organic? (y/n): ");
            bool isOrganic = Console.ReadLine()?.ToLower() == "y";

            var data = new Dictionary<string, object>
            {
                { "Fruit", fruit },
                { "IsOrganic", isOrganic }
            };

            Console.WriteLine();
            _juiceFactory.CreateBeverage(name, price, quantity, data);
        }

        static void CreateMilkTea()
        {
            Console.WriteLine("\n  === CREATE MILK TEA ===\n");

            Console.Write("  Name: ");
            string name = Console.ReadLine();

            Console.Write("  Price ($): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("\n[ERROR] Invalid price!");
                return;
            }

            Console.Write("  Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("\n[ERROR] Invalid quantity!");
                return;
            }

            Console.Write("  Size (S/M/L): ");
            string size = Console.ReadLine()?.ToUpper();

            Console.Write("  Topping: ");
            string topping = Console.ReadLine();

            var data = new Dictionary<string, object>
            {
                { "Size", size },
                { "Topping", topping }
            };

            Console.WriteLine();
            _milkTeaFactory.CreateBeverage(name, price, quantity, data);
        }

        static void ViewAllBeverages()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                  ALL BEVERAGES LIST");
            Console.WriteLine(new string('=', 60));
            
            _beverageService.DisplayAllBeverages();
            _beverageService.DisplayInventorySummary();
        }

        static void ViewBeverageById()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                SEARCH BEVERAGE BY ID");
            Console.WriteLine(new string('=', 60) + "\n");

            Console.Write("  Enter Beverage ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[ERROR] Invalid ID!");
                return;
            }

            var beverage = _repository.GetById(id);
            if (beverage == null)
            {
                Console.WriteLine($"\n[ERROR] Beverage with ID {id} not found!");
                return;
            }

            Console.WriteLine("\n[SUCCESS] Found:");
            Console.WriteLine("  " + new string('-', 58));
            beverage.DisplayInfo();
        }

        static void UpdateBeverageMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                  UPDATE BEVERAGE");
            Console.WriteLine(new string('=', 60) + "\n");

            Console.Write("  Enter Beverage ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[ERROR] Invalid ID!");
                return;
            }

            var beverage = _repository.GetById(id);
            if (beverage == null)
            {
                Console.WriteLine($"\n[ERROR] Beverage with ID {id} not found!");
                return;
            }

            Console.WriteLine("\n  Current Info:");
            beverage.DisplayInfo();

            Console.WriteLine("\n  " + new string('-', 58));
            Console.WriteLine("  Enter new information (press Enter to keep current value):\n");

            Console.Write($"  Name [{beverage.Name}]: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = beverage.Name;

            Console.Write($"  Price [{beverage.Price}]: ");
            string priceInput = Console.ReadLine();
            decimal price = string.IsNullOrWhiteSpace(priceInput) ? beverage.Price : decimal.Parse(priceInput);

            Console.Write($"  Quantity [{beverage.Quantity}]: ");
            string qtyInput = Console.ReadLine();
            int quantity = string.IsNullOrWhiteSpace(qtyInput) ? beverage.Quantity : int.Parse(qtyInput);

            Dictionary<string, object> data = new Dictionary<string, object>();

            // Get specific properties based on type
            string beverageType = beverage.GetBeverageType();
            if (beverageType == "Soft Drink")
            {
                var sd = beverage as Models.SoftDrink;
                Console.Write($"  Brand [{sd.Brand}]: ");
                string brand = Console.ReadLine();
                data["Brand"] = string.IsNullOrWhiteSpace(brand) ? sd.Brand : brand;

                Console.Write($"  Volume [{sd.Volume}]: ");
                string volInput = Console.ReadLine();
                data["Volume"] = string.IsNullOrWhiteSpace(volInput) ? sd.Volume : int.Parse(volInput);

                Console.WriteLine();
                _softDrinkFactory.UpdateBeverage(id, name, price, quantity, data);
            }
            else if (beverageType == "Juice")
            {
                var j = beverage as Models.Juice;
                Console.Write($"  Fruit [{j.Fruit}]: ");
                string fruit = Console.ReadLine();
                data["Fruit"] = string.IsNullOrWhiteSpace(fruit) ? j.Fruit : fruit;

                Console.Write($"  Is Organic [{(j.IsOrganic ? "Yes" : "No")}] (y/n): ");
                string orgInput = Console.ReadLine();
                data["IsOrganic"] = string.IsNullOrWhiteSpace(orgInput) ? j.IsOrganic : orgInput.ToLower() == "y";

                Console.WriteLine();
                _juiceFactory.UpdateBeverage(id, name, price, quantity, data);
            }
            else if (beverageType == "Milk Tea")
            {
                var mt = beverage as Models.MilkTea;
                Console.Write($"  Size [{mt.Size}]: ");
                string size = Console.ReadLine();
                data["Size"] = string.IsNullOrWhiteSpace(size) ? mt.Size : size.ToUpper();

                Console.Write($"  Topping [{mt.Topping}]: ");
                string topping = Console.ReadLine();
                data["Topping"] = string.IsNullOrWhiteSpace(topping) ? mt.Topping : topping;

                Console.WriteLine();
                _milkTeaFactory.UpdateBeverage(id, name, price, quantity, data);
            }
        }

        static void DeleteBeverageMenu()
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                  DELETE BEVERAGE");
            Console.WriteLine(new string('=', 60) + "\n");

            Console.Write("  Enter Beverage ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[ERROR] Invalid ID!");
                return;
            }

            var beverage = _repository.GetById(id);
            if (beverage == null)
            {
                Console.WriteLine($"\n[ERROR] Beverage with ID {id} not found!");
                return;
            }

            Console.WriteLine("\n  Item to delete:");
            beverage.DisplayInfo();

            Console.Write("\n[WARNING] Are you sure you want to delete this item? (y/n): ");
            string confirm = Console.ReadLine()?.ToLower();

            if (confirm == "y")
            {
                Console.WriteLine();
                _repository.Delete(id);
                Console.WriteLine($"[SUCCESS] Beverage ID {id} deleted successfully!");
            }
            else
            {
                Console.WriteLine("\n[INFO] Delete cancelled.");
            }
        }
    }
}
