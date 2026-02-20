using Adapter.Interfaces;
using Adapter.ThirdPartyServices;

namespace Adapter.Adapters
{
    // Adapter for ZaloPay Gateway
    public class ZaloPayAdapter : IPaymentProcessor
    {
        private readonly ZaloPayGateway _zaloPayGateway;
        private readonly Dictionary<int, long> _orderChargeMap;

        public string PaymentMethodName => "ZaloPay";

        public ZaloPayAdapter()
        {
            _zaloPayGateway = new ZaloPayGateway();
            _orderChargeMap = new Dictionary<int, long>();
        }

        public bool ProcessPayment(int orderId, decimal amount, string customerName)
        {
            string userAccount = GenerateZaloAccount(customerName);
            decimal amountVND = amount * 24000m; // Convert USD to VND (simplified)

            long chargeId = _zaloPayGateway.InitiatePayment(amountVND, userAccount);
            _orderChargeMap[orderId] = chargeId;

            return chargeId > 0;
        }

        public bool RefundPayment(int orderId)
        {
            if (!_orderChargeMap.ContainsKey(orderId))
            {
                Console.WriteLine($"  ? No charge found for Order #{orderId}");
                return false;
            }

            return _zaloPayGateway.CancelPayment(_orderChargeMap[orderId]);
        }

        public string GetPaymentStatus(int orderId)
        {
            if (!_orderChargeMap.ContainsKey(orderId))
                return "NOT_FOUND";

            bool isSuccess = _zaloPayGateway.VerifyPayment(_orderChargeMap[orderId]);
            return isSuccess ? "Paid" : "Refunded";
        }

        public decimal GetTransactionFee(decimal amount)
        {
            decimal amountVND = amount * 24000m;
            return _zaloPayGateway.CalculateFee(amountVND) / 24000m;
        }

        private string GenerateZaloAccount(string customerName)
        {
            return $"{customerName.Replace(" ", "").ToLower()}{new Random().Next(100, 999)}";
        }
    }
}
