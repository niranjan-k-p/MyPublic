using System.Collections.Generic;
using BillSplitApp.Model;

namespace BillSplitApp.Contract
{
    public interface IFileReader
    {
        IEnumerable<CampingTrip> ReadFile(string fileName);
    }
}
