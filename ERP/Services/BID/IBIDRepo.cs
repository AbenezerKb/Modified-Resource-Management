using ERP.Models;

namespace ERP.Services
{
    public interface IBIDRepo
    {
        bool SaveChanges();

        IEnumerable<BID> GetAllBIDs();
        BID GetBID(int id);
        void CreateBID(BID bid);
        void DeleteBID(int id);
        void UpdateBID(int id,BID updateBID);
    }
}
