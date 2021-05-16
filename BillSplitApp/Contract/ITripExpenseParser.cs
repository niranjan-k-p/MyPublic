using System.Collections.Generic;
using BillSplitApp.Model;

namespace BillSplitApp.Contract
{
    public interface ITripExpenseParser
    {
        IList<CampingTrip> ParseCampingTrips(IList<string> lines);
    }
}
