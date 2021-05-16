using System;
using System.Linq;
using System.Runtime.CompilerServices;
using BillSplitApp.Contract;

[assembly: InternalsVisibleTo("BillSplitTests")]
namespace BillSplitApp.Implementation
{
    internal sealed class BillsPublisherService : IBillsPublisherService
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;

        public BillsPublisherService(IFileReader fileReader, IFileWriter fileWriter)
        {
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        public string PublishBills(string inputFileName)
        {
            try
            {
                var campingTrips = _fileReader.ReadFile(inputFileName).ToList();

                if (campingTrips.Any())
                {
                    return _fileWriter.WriteToFile(inputFileName, campingTrips);
                }

                Console.WriteLine($"No camping trips found in file '{inputFileName}'. Please ensure that the file name is valid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to publish bills due to the following error: {ex.Message}");
            }

            return string.Empty;
        }
    }
}
