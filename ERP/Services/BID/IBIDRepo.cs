using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface IBIDRepo
    {
        bool SaveChanges();

        IEnumerable<BID> GetAllBIDs();
        BID GetBID(int id);
        BID CreateBID(BIDCreateDto bid);
        void DeleteBID(int id);
        void UpdateBID(int id, BIDCreateDto updateBID);
    }
}
