using FactoryMethod.Interfaces;

namespace FactoryMethod.Models
{
    public class SoftDrink : IBeverage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public int Volume { get; set; } // ml

        public string GetBeverageType() => "Soft Drink";

        public void DisplayInfo()
        {
            Console.WriteLine($"  [Soft Drink] ID: {Id} | {Name} ({Brand}) | {Volume}ml");
            Console.WriteLine($"     Price: {Price:C} | Qty: {Quantity} | Total: {CalculateTotal():C}");
        }

        public decimal CalculateTotal() => Price * Quantity;
    }
}
