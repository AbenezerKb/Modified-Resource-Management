using ERP.Models;
using ERP.DTOs;


namespace ERP.Services
{
    public interface IDailyLaborRepo
    {
        bool SaveChanges();
        public DailyLabor CreateDailyLabor(DailyLaborCreateDto grander);
        public DailyLabor GetDailyLabor(int granderNo);
        public IEnumerable<DailyLabor> GetAllDailyLabors();
        public void DeleteDailyLabor(int id);
        public void UpdateDailyLabor(int id, DailyLaborCreateDto updatedDailyLabor);
    }
}
