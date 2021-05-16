using System;
using System.Collections.Generic;

namespace BillSplitApp.Model
{
    public class CampingTrip
    {
        public CampingTrip(int numOfParticipants)
        {
            NumberOfParticipants = numOfParticipants;
            Expenses = new List<Expense>();
        }

        public int NumberOfParticipants { get; private set; }

        public IList<Expense> Expenses { get; set; }
    }
}
