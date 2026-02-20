namespace FactoryMethod.Interfaces
{
    public interface IBeverageRepository
    {
        IBeverage Create(IBeverage beverage);
        List<IBeverage> GetAll();
        IBeverage GetById(int id);
        bool Update(int id, IBeverage updatedBeverage);
        bool Delete(int id);
    }
}
