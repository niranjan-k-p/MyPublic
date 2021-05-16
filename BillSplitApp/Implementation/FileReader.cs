using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using BillSplitApp.Contract;
using BillSplitApp.Model;

namespace BillSplitApp.Implementation
{
    internal sealed class FileReader : IFileReader
    {
        private readonly ITripExpenseParser _tripExpenseParser;

        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public FileReader(ITripExpenseParser tripExpenseParser)
        {
            _tripExpenseParser = tripExpenseParser;
        }

        public IEnumerable<CampingTrip> ReadFile(string fileName)
        {
            _lock.EnterReadLock();

            try
            {
                var fileContents = ReadFileContents(fileName);

                if (fileContents != null && fileContents.Any())
                {
                    return _tripExpenseParser.ParseCampingTrips(fileContents.ToList());
                }

                return Enumerable.Empty<CampingTrip>();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        
        private string[] ReadFileContents(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (!File.Exists(path))
            {
                return null;
            }

            return File.ReadAllLines(path);
        }
    }
}
