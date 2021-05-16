using System.Collections.Generic;
using System.Linq;
using BillSplitApp.Contract;
using BillSplitApp.Implementation;
using BillSplitApp.Model;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace BillSplitTests
{
    public class BillsPublisherServiceTests
    {
        private IBillsPublisherService _billsPublisherService;

        private Mock<IFileReader> _fileReaderMock;
        private Mock<IFileWriter> _fileWriterMock;

        [SetUp]
        public void Setup()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _fileWriterMock = new Mock<IFileWriter>();

            _billsPublisherService = new BillsPublisherService(_fileReaderMock.Object, _fileWriterMock.Object);
        }

        [Test]
        public void PublishBills_ValidFileName_WriteToFileIsCalled()
        {
            const string fileName = "TestExpensesData.txt";
            var trips = new List<CampingTrip> { new CampingTrip(2) };

            _fileReaderMock.Setup(m => m.ReadFile(fileName)).Returns(trips);

            _billsPublisherService.PublishBills(fileName);

            _fileWriterMock.Verify(m => m.WriteToFile(fileName, trips), Times.Once);
        }

        [Test]
        public void PublishBills_InValidFileName_WriteToFileIsNotCalled()
        {
            const string fileName = "TestExpensesData_INVALID.txt";

            _fileReaderMock.Setup(m => m.ReadFile(fileName)).Returns(Enumerable.Empty<CampingTrip>());

            _billsPublisherService.PublishBills(fileName);

            _fileWriterMock.Verify(m => m.WriteToFile(fileName, It.IsAny<List<CampingTrip>>()), Times.Never);
        }
    }
}