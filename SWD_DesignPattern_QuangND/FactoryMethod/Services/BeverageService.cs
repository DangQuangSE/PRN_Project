using FactoryMethod.Interfaces;

namespace FactoryMethod.Services
{
    public class BeverageService
    {
        private readonly IBeverageRepository _repository;

        public BeverageService(IBeverageRepository repository)
        {
            _repository = repository;
        }

        public void DisplayAllBeverages()
        {
            var beverages = _repository.GetAll();
            if (!beverages.Any())
            {
                Console.WriteLine("  No beverages in stock.");
                return;
            }

            Console.WriteLine($"\n  Total Beverages: {beverages.Count}");
            Console.WriteLine("  " + new string('?', 60));

            foreach (var beverage in beverages)
            {
                beverage.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void DisplayInventorySummary()
        {
            var beverages = _repository.GetAll();
            if (!beverages.Any()) return;

            var totalValue = beverages.Sum(b => b.CalculateTotal());
            var totalItems = beverages.Sum(b => b.Quantity);

            Console.WriteLine($"  Total Inventory Value: {totalValue:C}");
            Console.WriteLine($"  Total Items: {totalItems}");
        }
    }
}
