using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface ITimeCardRepo
    {


        bool SaveChanges();

        IEnumerable<TimeCard> GetAllTimeCards();
        TimeCard GetTimeCard(int id);
        TimeCard CreateTimeCard(TimeCardCreateDto timeCard);
        void DeleteTimeCard(int id);
        void UpdateTimeCard(int id, TimeCardCreateDto updatedTimeCard);
    }
}
