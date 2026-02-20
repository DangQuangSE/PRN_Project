using FactoryMethod.Interfaces;

namespace FactoryMethod.Models
{
    public class Juice : IBeverage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Fruit { get; set; }
        public bool IsOrganic { get; set; }

        public string GetBeverageType() => "Juice";

        public void DisplayInfo()
        {
            string organic = IsOrganic ? "Organic" : "Regular";
            Console.WriteLine($"  [Juice] ID: {Id} | {Name} ({Fruit} - {organic})");
            Console.WriteLine($"     Price: {Price:C} | Qty: {Quantity} | Total: {CalculateTotal():C}");
        }

        public decimal CalculateTotal() => Price * Quantity;
    }
}
