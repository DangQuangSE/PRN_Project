using Adapter.Interfaces;
using Adapter.ThirdPartyServices;

namespace Adapter.Adapters
{
    // Adapter for Banking Payment System
    public class BankingPaymentAdapter : IPaymentProcessor
    {
        private readonly BankingPaymentSystem _bankingSystem;
        private readonly Dictionary<int, string> _orderReferenceMap;

        public string PaymentMethodName => "Bank Transfer";

        public BankingPaymentAdapter()
        {
            _bankingSystem = new BankingPaymentSystem();
            _orderReferenceMap = new Dictionary<int, string>();
        }

        public bool ProcessPayment(int orderId, decimal amount, string customerName)
        {
            string accountNumber = GenerateAccountNumber();
            decimal amountVND = amount * 24000m; // Convert USD to VND (simplified)
            string description = $"Payment for Order #{orderId} by {customerName}";

            string referenceNumber = _bankingSystem.TransferMoney(accountNumber, amountVND, description);
            _orderReferenceMap[orderId] = referenceNumber;

            return !string.IsNullOrEmpty(referenceNumber);
        }

        public bool RefundPayment(int orderId)
        {
            if (!_orderReferenceMap.ContainsKey(orderId))
            {
                Console.WriteLine($"  ? No transaction found for Order #{orderId}");
                return false;
            }

            return _bankingSystem.ReversalTransaction(_orderReferenceMap[orderId]);
        }

        public string GetPaymentStatus(int orderId)
        {
            if (!_orderReferenceMap.ContainsKey(orderId))
                return "NOT_FOUND";

            string status = _bankingSystem.CheckTransactionStatus(_orderReferenceMap[orderId]);
            return MapBankingStatus(status);
        }

        public decimal GetTransactionFee(decimal amount)
        {
            decimal amountVND = amount * 24000m;
            return _bankingSystem.GetBankFee(amountVND) / 24000m;
        }

        private string GenerateAccountNumber()
        {
            return $"{new Random().Next(100000000, 999999999):D9}";
        }

        private string MapBankingStatus(string bankStatus)
        {
            return bankStatus switch
            {
                "COMPLETED" => "Paid",
                "REVERSED" => "Refunded",
                "NOT_FOUND" => "Not Found",
                _ => "Unknown"
            };
        }
    }
}
