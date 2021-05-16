using BillSplitApp.Model;

namespace BillSplitApp.Contract
{
    public interface IExpenseCalculator
    {
        decimal GetAmountOwed(int participantId, CampingTrip campingTrip);
    }
}
