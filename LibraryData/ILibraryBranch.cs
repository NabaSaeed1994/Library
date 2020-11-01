using System.Collections.Generic;

using LibraryData.Models;

namespace Library.Data
{
    public interface ILibraryBranch
    {
        IEnumerable<LibraryBranch> GetAll();
        IEnumerable<Patron> GetPatrons(int branchId);
        IEnumerable<LibraryAsset> GetAssets(int branchId);
        IEnumerable<string> GetBranchHours(int branchId);
        LibraryBranch Get(int branchId);
        void Add(LibraryBranch newBranch);
        bool IsBranchOpen(int branchId);


        //int GetAssetCount(int branchId);
        //int GetPatronCount(int branchId);
        //decimal GetAssetsValue(int id);
    }
}