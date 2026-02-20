using FactoryMethod.Interfaces;
using FactoryMethod.Models;

namespace FactoryMethod.Factories
{
    public class JuiceFactory : BeverageFactory
    {
        public JuiceFactory(IBeverageRepository repository) : base(repository) { }

        public override IBeverage CreateBeverageInstance() => new Juice();

        protected override void ConfigureAdditionalProperties(IBeverage beverage, Dictionary<string, object> data)
        {
            if (beverage is Juice juice)
            {
                juice.Fruit = data.GetValueOrDefault("Fruit", "Mixed").ToString();
                juice.IsOrganic = Convert.ToBoolean(data.GetValueOrDefault("IsOrganic", false));
            }
        }
    }
}
