using System;
namespace BillSplitApp.Model
{
    public class Receipt
    {
        public Receipt(int receiptId, decimal amount)
        {
            ReceiptId = receiptId;
            Amount = amount;
        }

        public int ReceiptId { get; private set; }

        public decimal Amount { get; private set; }
    }
}
