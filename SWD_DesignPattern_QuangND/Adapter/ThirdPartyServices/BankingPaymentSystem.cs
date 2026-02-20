namespace Adapter.ThirdPartyServices
{
    // Adaptee 3 - Banking Payment System (Third Party API)
    public class BankingPaymentSystem
    {
        private readonly Dictionary<string, BankTransaction> _transactions = new();

        public class BankTransaction
        {
            public string ReferenceNumber { get; set; }
            public string Status { get; set; }
            public decimal Amount { get; set; }
            public string AccountNumber { get; set; }
        }

        public string TransferMoney(string accountNumber, decimal amount, string description)
        {
            string referenceNumber = $"BNK{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
            
            _transactions[referenceNumber] = new BankTransaction
            {
                ReferenceNumber = referenceNumber,
                Status = "COMPLETED",
                Amount = amount,
                AccountNumber = accountNumber
            };

            Console.WriteLine($"  [Banking API] Transfer executed: {referenceNumber}");
            Console.WriteLine($"  [Banking API] Account: {accountNumber} | Amount: {amount:N0} VND");
            Console.WriteLine($"  [Banking API] Description: {description}");
            
            return referenceNumber;
        }

        public bool ReversalTransaction(string referenceNumber)
        {
            if (!_transactions.ContainsKey(referenceNumber))
            {
                Console.WriteLine($"  [Banking API] Transaction not found: {referenceNumber}");
                return false;
            }

            _transactions[referenceNumber].Status = "REVERSED";
            Console.WriteLine($"  [Banking API] Transaction reversed: {referenceNumber}");
            return true;
        }

        public string CheckTransactionStatus(string referenceNumber)
        {
            if (!_transactions.ContainsKey(referenceNumber))
                return "NOT_FOUND";

            return _transactions[referenceNumber].Status;
        }

        public decimal GetBankFee(decimal amount)
        {
            if (amount < 500000)
                return 1100; // Fixed fee for small transactions
            return amount * 0.005m; // 0.5% for larger transactions
        }
    }
}
