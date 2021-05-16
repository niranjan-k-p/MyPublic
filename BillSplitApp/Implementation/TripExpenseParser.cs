using System;
using System.Collections.Generic;
using BillSplitApp.Contract;
using BillSplitApp.Model;

namespace BillSplitApp.Implementation
{
    internal class TripExpenseParser : ITripExpenseParser
    {
        public IList<CampingTrip> ParseCampingTrips(IList<string> lines)
        {
            int row = 0;
            var campingTrips = new List<CampingTrip>();

            while(Convert.ToInt32(lines[row]) != 0)
            {
                var numOfParticipants = Convert.ToInt32(lines[row]);

                var campingTrip = new CampingTrip(numOfParticipants);

                row = FillTripExpenses(campingTrip, lines, numOfParticipants, row);

                campingTrips.Add(campingTrip);

                row++;
            }

            return campingTrips;
        }

        private int FillTripExpenses(CampingTrip campingTrip, IList<string> lines, int participantCounter, int row)
        {
            for (int i = 1; i <= participantCounter; i++)
            {
                row++;

                var numberOfReceipts = Convert.ToInt32(lines[row]);

                var expense = new Expense(i, numberOfReceipts);

                row = FillReceipts(expense, lines, numberOfReceipts, row);

                campingTrip.Expenses.Add(expense);
            }

            return row;
        }

        private int FillReceipts(Expense expense, IList<string> lines, int receiptCounter, int row)
        {
            for (int i = 1; i <= receiptCounter; i++)
            {
                row++;

                var receipt = new Receipt(i, Convert.ToDecimal(lines[row]));

                expense.Receipts.Add(receipt);
            }

            return row;
        }
    }
}
