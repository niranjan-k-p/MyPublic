using System.Linq;
using BillSplitApp.Contract;
using BillSplitApp.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace BillSplitTests
{
    public class FileReaderTests
    {
        private IFileReader _fileReader;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ITripExpenseParser, TripExpenseParser>();
            services.AddSingleton<IFileReader, FileReader>();
            
            var serviceProvider = services.BuildServiceProvider();

            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
        }

        [Test]
        public void ReadFile_ValidFileName_VerifyCampingTripCount()
        {
            const string fileName = "TestExpensesData.txt";

            var campingTrips = _fileReader.ReadFile(fileName).ToList();

            Assert.That(campingTrips.Count == 2);
        }

        [Test]
        public void ReadFile_InValidFileName_VerifyCampingTripCountIsZero()
        {
            const string fileName = "TestExpensesData_INVALID.txt";

            var campingTrips = _fileReader.ReadFile(fileName).ToList();

            Assert.That(campingTrips.Count == 0);
        }
    }
}