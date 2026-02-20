namespace FactoryMethod.Interfaces
{
    public interface IBeverage
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
        string GetBeverageType();
        void DisplayInfo();
        decimal CalculateTotal();
    }
}
