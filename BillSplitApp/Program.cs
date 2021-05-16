using System;
using Microsoft.Extensions.DependencyInjection;
using BillSplitApp.Extension;
using BillSplitApp.Contract;

namespace BillSplitApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddDependencies().BuildServiceProvider();

            var publisherService = serviceProvider.GetRequiredService<IBillsPublisherService>();

            Console.WriteLine("Enter file name: ");

            var fileName = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Please enter a valid file name!");
            }
            else
            {
                var outputFilePath = publisherService.PublishBills(fileName);

                if(string.IsNullOrWhiteSpace(outputFilePath))
                {
                    Console.WriteLine($"Failed to create output file!");
                }
                else
                {
                    Console.WriteLine($"Bills published to {outputFilePath}");
                }
            }
        }
    }
}
