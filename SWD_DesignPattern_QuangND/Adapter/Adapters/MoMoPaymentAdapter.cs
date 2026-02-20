using Adapter.Interfaces;
using Adapter.ThirdPartyServices;

namespace Adapter.Adapters
{
    // Adapter for MoMo Payment Service
    public class MoMoPaymentAdapter : IPaymentProcessor
    {
        private readonly MoMoPaymentService _momoService;
        private readonly Dictionary<int, string> _orderTransactionMap;

        public string PaymentMethodName => "MoMo Wallet";

        public MoMoPaymentAdapter()
        {
            _momoService = new MoMoPaymentService();
            _orderTransactionMap = new Dictionary<int, string>();
        }

        public bool ProcessPayment(int orderId, decimal amount, string customerName)
        {
            string phoneNumber = GeneratePhoneNumber(customerName);
            double amountVND = (double)amount * 24000; // Convert USD to VND (simplified)

            string transactionId = _momoService.CreateTransaction(phoneNumber, amountVND);
            _orderTransactionMap[orderId] = transactionId;

            return !string.IsNullOrEmpty(transactionId);
        }

        public bool RefundPayment(int orderId)
        {
            if (!_orderTransactionMap.ContainsKey(orderId))
            {
                Console.WriteLine($"  ? No transaction found for Order #{orderId}");
                return false;
            }

            return _momoService.RollbackTransaction(_orderTransactionMap[orderId]);
        }

        public string GetPaymentStatus(int orderId)
        {
            if (!_orderTransactionMap.ContainsKey(orderId))
                return "NOT_FOUND";

            string status = _momoService.QueryTransactionStatus(_orderTransactionMap[orderId]);
            return MapMoMoStatus(status);
        }

        public decimal GetTransactionFee(decimal amount)
        {
            double amountVND = (double)amount * 24000;
            return (decimal)_momoService.GetServiceFee(amountVND) / 24000m;
        }

        private string GeneratePhoneNumber(string customerName)
        {
            return $"0{new Random().Next(900000000, 999999999)}";
        }

        private string MapMoMoStatus(string momoStatus)
        {
            return momoStatus switch
            {
                "SUCCESS" => "Paid",
                "REFUNDED" => "Refunded",
                "NOT_FOUND" => "Not Found",
                _ => "Unknown"
            };
        }
    }
}
