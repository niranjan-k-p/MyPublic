using System;
using System.Linq;
using BillSplitApp.Contract;
using BillSplitApp.Model;

namespace BillSplitApp.Implementation
{
    internal sealed class ExpenseCalculator : IExpenseCalculator
    {
        public decimal GetAmountOwed(int participantId, CampingTrip campingTrip)
        {
            return decimal.Round(GetAverageExpense(campingTrip) - GetExpenseByParticipant(participantId, campingTrip), 2, MidpointRounding.AwayFromZero);
        }

        private decimal GetAverageExpense(CampingTrip campingTrip)
        {
            return campingTrip.Expenses.SelectMany(e => e.Receipts).Sum(r => r.Amount) / campingTrip.NumberOfParticipants;
        }

        private decimal GetExpenseByParticipant(int participantId, CampingTrip campingTrip)
        {
            return campingTrip.Expenses.Where(e => e.ParticipantId == participantId).SelectMany(e => e.Receipts).Sum(r => r.Amount);
        }
    }
}
