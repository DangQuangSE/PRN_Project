namespace Adapter.Interfaces
{
    public interface IPaymentProcessor
    {
        string PaymentMethodName { get; }
        bool ProcessPayment(int orderId, decimal amount, string customerName);
        bool RefundPayment(int orderId);
        string GetPaymentStatus(int orderId);
        decimal GetTransactionFee(decimal amount);
    }
}
