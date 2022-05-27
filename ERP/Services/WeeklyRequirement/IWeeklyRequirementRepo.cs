using ERP.Models;


namespace ERP.Services
{
    public interface IWeeklyRequirementRepo
    {
        bool SaveChanges();

        IEnumerable<WeeklyRequirement> GetAllWeeklyRequirement();
        WeeklyRequirement GetWeeklyRequirements(int id);
        void CreateWeeklyRequirement(WeeklyRequirement weeklyRequirement);
        void DeleteWeeklyRequirements(int id);
        void UpdateWeeklyRequirement(int id,WeeklyRequirement updatedWeeklyRequirement);
    }
}
