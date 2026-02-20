namespace Adapter.ThirdPartyServices
{
    // Adaptee 2 - ZaloPay Payment System (Third Party API)
    public class ZaloPayGateway
    {
        private readonly Dictionary<long, ChargeInfo> _charges = new();
        private long _nextChargeId = 100000;

        public class ChargeInfo
        {
            public long ChargeId { get; set; }
            public bool IsSuccess { get; set; }
            public decimal Amount { get; set; }
            public string UserAccount { get; set; }
        }

        public long InitiatePayment(decimal amountVND, string userAccount)
        {
            long chargeId = _nextChargeId++;
            
            _charges[chargeId] = new ChargeInfo
            {
                ChargeId = chargeId,
                IsSuccess = true,
                Amount = amountVND,
                UserAccount = userAccount
            };

            Console.WriteLine($"  [ZaloPay API] Payment initiated: Charge #{chargeId}");
            Console.WriteLine($"  [ZaloPay API] Account: {userAccount} | Amount: {amountVND:N0} VND");
            
            return chargeId;
        }

        public bool CancelPayment(long chargeId)
        {
            if (!_charges.ContainsKey(chargeId))
            {
                Console.WriteLine($"  [ZaloPay API] Charge not found: #{chargeId}");
                return false;
            }

            _charges[chargeId].IsSuccess = false;
            Console.WriteLine($"  [ZaloPay API] Payment cancelled: Charge #{chargeId}");
            return true;
        }

        public bool VerifyPayment(long chargeId)
        {
            if (!_charges.ContainsKey(chargeId))
                return false;

            return _charges[chargeId].IsSuccess;
        }

        public decimal CalculateFee(decimal amountVND)
        {
            return amountVND * 0.015m; // 1.5% fee
        }
    }
}
