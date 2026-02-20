using FactoryMethod.Interfaces;

namespace FactoryMethod.Factories
{
    public abstract class BeverageFactory
    {
        protected readonly IBeverageRepository _repository;

        protected BeverageFactory(IBeverageRepository repository)
        {
            _repository = repository;
        }

        // Factory Method
        public abstract IBeverage CreateBeverageInstance();

        // Template method for CRUD operations
        public IBeverage CreateBeverage(string name, decimal price, int quantity, Dictionary<string, object> additionalData)
        {
            var beverage = CreateBeverageInstance();
            beverage.Name = name;
            beverage.Price = price;
            beverage.Quantity = quantity;

            ConfigureAdditionalProperties(beverage, additionalData);

            var created = _repository.Create(beverage);
            Console.WriteLine($"[SUCCESS] {beverage.GetBeverageType()} '{name}' created successfully! (ID: {created.Id})");
            return created;
        }

        protected abstract void ConfigureAdditionalProperties(IBeverage beverage, Dictionary<string, object> data);

        public List<IBeverage> GetAllBeverages() => _repository.GetAll();

        public IBeverage GetBeverageById(int id) => _repository.GetById(id);

        public bool UpdateBeverage(int id, string name, decimal price, int quantity, Dictionary<string, object> additionalData)
        {
            var existing = _repository.GetById(id);
            if (existing == null)
            {
                Console.WriteLine($"[ERROR] Beverage with ID {id} not found!");
                return false;
            }

            var updated = CreateBeverageInstance();
            updated.Name = name;
            updated.Price = price;
            updated.Quantity = quantity;
            ConfigureAdditionalProperties(updated, additionalData);

            if (_repository.Update(id, updated))
            {
                Console.WriteLine($"[SUCCESS] Beverage ID {id} updated successfully!");
                return true;
            }

            return false;
        }

        public bool DeleteBeverage(int id)
        {
            var beverage = _repository.GetById(id);
            if (beverage == null)
            {
                Console.WriteLine($"[ERROR] Beverage with ID {id} not found!");
                return false;
            }

            if (_repository.Delete(id))
            {
                Console.WriteLine($"[SUCCESS] {beverage.GetBeverageType()} '{beverage.Name}' (ID: {id}) deleted successfully!");
                return true;
            }

            return false;
        }
    }
}
