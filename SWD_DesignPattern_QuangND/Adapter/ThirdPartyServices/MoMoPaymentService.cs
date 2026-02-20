namespace Adapter.ThirdPartyServices
{
    // Adaptee 1 - MoMo Payment System (Third Party API)
    public class MoMoPaymentService
    {
        private readonly Dictionary<string, PaymentTransaction> _transactions = new();

        public class PaymentTransaction
        {
            public string TransactionId { get; set; }
            public string Status { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public string CreateTransaction(string phoneNumber, double amountVND)
        {
            string transactionId = $"MOMO-{Guid.NewGuid().ToString()[..8].ToUpper()}";
            
            _transactions[transactionId] = new PaymentTransaction
            {
                TransactionId = transactionId,
                Status = "SUCCESS",
                Amount = (decimal)amountVND,
                CreatedAt = DateTime.Now
            };

            Console.WriteLine($"  [MoMo API] Transaction created: {transactionId}");
            Console.WriteLine($"  [MoMo API] Phone: {phoneNumber} | Amount: {amountVND:N0} VND");
            
            return transactionId;
        }

        public bool RollbackTransaction(string transactionId)
        {
            if (!_transactions.ContainsKey(transactionId))
            {
                Console.WriteLine($"  [MoMo API] Transaction not found: {transactionId}");
                return false;
            }

            _transactions[transactionId].Status = "REFUNDED";
            Console.WriteLine($"  [MoMo API] Transaction rolled back: {transactionId}");
            return true;
        }

        public string QueryTransactionStatus(string transactionId)
        {
            if (!_transactions.ContainsKey(transactionId))
                return "NOT_FOUND";

            return _transactions[transactionId].Status;
        }

        public double GetServiceFee(double amountVND)
        {
            return amountVND * 0.01; // 1% fee
        }
    }
}
