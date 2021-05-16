using System;
using System.Collections.Generic;

namespace BillSplitApp.Model
{
    public class Expense
    {
        public Expense(int participantId, int numOfReceipts)
        {
            ParticipantId = participantId;
            NumberOfReceipts = numOfReceipts;
            Receipts = new List<Receipt>();
        }

        public int ParticipantId { get; set; }

        public int NumberOfReceipts { get; set; }

        public IList<Receipt> Receipts { get; set; }
    }
}
