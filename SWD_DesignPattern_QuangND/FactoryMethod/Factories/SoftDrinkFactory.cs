using FactoryMethod.Interfaces;
using FactoryMethod.Models;

namespace FactoryMethod.Factories
{
    public class SoftDrinkFactory : BeverageFactory
    {
        public SoftDrinkFactory(IBeverageRepository repository) : base(repository) { }

        public override IBeverage CreateBeverageInstance() => new SoftDrink();

        protected override void ConfigureAdditionalProperties(IBeverage beverage, Dictionary<string, object> data)
        {
            if (beverage is SoftDrink softDrink)
            {
                softDrink.Brand = data.GetValueOrDefault("Brand", "Unknown").ToString();
                softDrink.Volume = Convert.ToInt32(data.GetValueOrDefault("Volume", 330));
            }
        }
    }
}
