using System.IO;
using System.Linq;
using BillSplitApp.Contract;
using BillSplitApp.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace BillSplitTests
{
    public class FileWriterTests
    {
        private IFileWriter _fileWriter;
        private IFileReader _fileReader;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IExpenseCalculator, ExpenseCalculator>();
            services.AddSingleton<ITripExpenseParser, TripExpenseParser>();
            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton<IFileWriter, FileWriter>();
            
            var serviceProvider = services.BuildServiceProvider();

            _fileWriter = serviceProvider.GetRequiredService<IFileWriter>();
            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
        }

        [Test]
        public void WriteToFile_ValidFileName_VerifyPathContainsOutputFile()
        {
            const string fileName = "TestExpensesData.txt";

            var campingTrips = _fileReader.ReadFile(fileName).ToList();

            var path = _fileWriter.WriteToFile(fileName, campingTrips);

            Assert.That(!string.IsNullOrWhiteSpace(path), Is.True);
            Assert.That(File.Exists(path), Is.True);
        }
    }
}