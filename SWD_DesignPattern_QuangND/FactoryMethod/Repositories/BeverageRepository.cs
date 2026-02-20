using FactoryMethod.Interfaces;

namespace FactoryMethod.Repositories
{
    public class BeverageRepository : IBeverageRepository
    {
        private readonly List<IBeverage> _beverages = new List<IBeverage>();
        private int _nextId = 1;

        public IBeverage Create(IBeverage beverage)
        {
            beverage.Id = _nextId++;
            _beverages.Add(beverage);
            return beverage;
        }

        public List<IBeverage> GetAll() => _beverages.ToList();

        public IBeverage GetById(int id) => _beverages.FirstOrDefault(b => b.Id == id);

        public bool Update(int id, IBeverage updatedBeverage)
        {
            var index = _beverages.FindIndex(b => b.Id == id);
            if (index == -1) return false;

            updatedBeverage.Id = id;
            _beverages[index] = updatedBeverage;
            return true;
        }

        public bool Delete(int id)
        {
            var beverage = GetById(id);
            if (beverage == null) return false;

            _beverages.Remove(beverage);
            return true;
        }
    }
}
