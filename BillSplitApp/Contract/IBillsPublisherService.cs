namespace BillSplitApp.Contract
{
    public interface IBillsPublisherService
    {
        string PublishBills(string inputFileName);
    }
}
