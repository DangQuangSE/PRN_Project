using FactoryMethod.Interfaces;

namespace FactoryMethod.Models
{
    public class MilkTea : IBeverage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; } // S, M, L
        public string Topping { get; set; }

        public string GetBeverageType() => "Milk Tea";

        public void DisplayInfo()
        {
            Console.WriteLine($"  [Milk Tea] ID: {Id} | {Name} (Size: {Size})");
            Console.WriteLine($"     Topping: {Topping} | Price: {Price:C} | Qty: {Quantity} | Total: {CalculateTotal():C}");
        }

        public decimal CalculateTotal() => Price * Quantity;
    }
}
