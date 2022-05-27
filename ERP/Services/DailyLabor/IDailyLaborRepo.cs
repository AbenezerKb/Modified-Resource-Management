using ERP.Models;


namespace ERP.Services
{
    public interface IDailyLaborRepo
    {
        bool SaveChanges();
        public void CreateDailyLabor(DailyLabor grander);
        public DailyLabor GetDailyLabor(int granderNo);
        public IEnumerable<DailyLabor> GetAllDailyLabors();
        public void DeleteDailyLabor(int id);
        public void UpdateDailyLabor(int id,DailyLabor updatedDailyLabor);
    }
}
