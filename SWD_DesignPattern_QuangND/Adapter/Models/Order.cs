namespace Adapter.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string BeverageName { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }

        public void DisplayInfo()
        {
            Console.WriteLine($"  Order #{Id}");
            Console.WriteLine($"     Customer: {CustomerName}");
            Console.WriteLine($"     Beverage: {BeverageName} x{Quantity}");
            Console.WriteLine($"     Total: {TotalAmount:C}");
            Console.WriteLine($"     Payment: {PaymentMethod} - {PaymentStatus}");
            Console.WriteLine($"     Date: {OrderDate:dd/MM/yyyy HH:mm}");
        }

        public decimal CalculateTotal() => Amount * Quantity;
    }
}
