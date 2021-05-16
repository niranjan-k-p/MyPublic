using BillSplitApp.Contract;
using BillSplitApp.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace BillSplitApp.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            return services.AddSingleton<ITripExpenseParser, TripExpenseParser>()
                .AddSingleton<IExpenseCalculator, ExpenseCalculator>()
                .AddSingleton<IFileReader, FileReader>()
                .AddSingleton<IFileWriter, FileWriter>()
                .AddSingleton<IBillsPublisherService, BillsPublisherService>();
        }
    }
}
