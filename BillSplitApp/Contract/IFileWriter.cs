using System.Collections.Generic;
using BillSplitApp.Model;

namespace BillSplitApp.Contract
{
    public interface IFileWriter
    {
        string WriteToFile(string fileName, IList<CampingTrip> trips);
    }
}
