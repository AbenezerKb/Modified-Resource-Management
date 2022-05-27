using ERP.Models;

namespace ERP.Services
{
    public interface ITimeCardRepo
    {


        bool SaveChanges();

        IEnumerable<TimeCard> GetAllTimeCards();
        TimeCard GetTimeCard(int id);
        void CreateTimeCard(TimeCard timeCard);
        void DeleteTimeCard(int id);
        void UpdateTimeCard(int id, TimeCard updatedTimeCard);
    }
}
