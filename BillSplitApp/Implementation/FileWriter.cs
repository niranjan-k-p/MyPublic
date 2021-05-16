using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BillSplitApp.Contract;
using BillSplitApp.Model;

namespace BillSplitApp.Implementation
{
    internal class FileWriter : IFileWriter
    {
        private readonly IExpenseCalculator _expenseCalculator;
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        
        public FileWriter(IExpenseCalculator expenseCalculator)
        {
            _expenseCalculator = expenseCalculator;
        }
        
        public string WriteToFile(string fileName, IList<CampingTrip> trips)
        {
            _lock.EnterWriteLock();

            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), fileName + ".out");
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    foreach (CampingTrip trip in trips)
                    {
                        UpdateExpenses(trip, textWriter);
                        textWriter.WriteLine();
                    }
                }

                return path;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void UpdateExpenses(CampingTrip trip, TextWriter textWriter)
        {
            for (int participantId = 1; participantId <= trip.NumberOfParticipants; participantId++)
            {
                var amountOwed = _expenseCalculator.GetAmountOwed(participantId, trip);

                textWriter.WriteLine(amountOwed >= 0 ? $"${amountOwed}" : $"(${Math.Abs(amountOwed)})");
            }
        }
    }
}
