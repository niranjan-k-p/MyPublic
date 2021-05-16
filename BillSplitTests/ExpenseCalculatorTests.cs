using System.Linq;
using BillSplitApp.Contract;
using BillSplitApp.Implementation;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BillSplitTests
{
    public class ExpenseCalculatorTests
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
        public void GetAmountOwed_WhenParticipantOwes_ReturnsPositiveAmountElseNegativeAmount()
        {
            const string fileName = "TestExpensesData.txt";

            var trips = _fileReader.ReadFile(fileName);

            var campingTripTwo = trips.Last();

            var firstParticipantExpenses = campingTripTwo.Expenses.Where(e => e.ParticipantId == 1).SelectMany(e => e.Receipts).Sum(r => r.Amount);

            var secondParticipantExpenses = campingTripTwo.Expenses.Where(e => e.ParticipantId == 2).SelectMany(e => e.Receipts).Sum(r => r.Amount);

            var expenseCalculator = new ExpenseCalculator();

            var amountOwedByFirstParticipant = expenseCalculator.GetAmountOwed(1, campingTripTwo);
            var amountOwedBySecondParticipant = expenseCalculator.GetAmountOwed(2, campingTripTwo);

            Assert.That(amountOwedByFirstParticipant > 0, Is.True);
            Assert.That(amountOwedBySecondParticipant < 0, Is.True);
        }
    }
}