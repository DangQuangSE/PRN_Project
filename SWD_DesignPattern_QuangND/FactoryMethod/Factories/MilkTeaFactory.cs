using FactoryMethod.Interfaces;
using FactoryMethod.Models;

namespace FactoryMethod.Factories
{
    public class MilkTeaFactory : BeverageFactory
    {
        public MilkTeaFactory(IBeverageRepository repository) : base(repository) { }

        public override IBeverage CreateBeverageInstance() => new MilkTea();

        protected override void ConfigureAdditionalProperties(IBeverage beverage, Dictionary<string, object> data)
        {
            if (beverage is MilkTea milkTea)
            {
                milkTea.Size = data.GetValueOrDefault("Size", "M").ToString();
                milkTea.Topping = data.GetValueOrDefault("Topping", "None").ToString();
            }
        }
    }
}
