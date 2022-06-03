using ERP.Models;
using ERP.DTOs;


namespace ERP.Services
{
    public interface IWeeklyRequirementRepo
    {
        bool SaveChanges();

        IEnumerable<WeeklyRequirement> GetAllWeeklyRequirement();
        WeeklyRequirement GetWeeklyRequirements(int id);
        WeeklyRequirement CreateWeeklyRequirement(WeeklyRequirementCreateDto weeklyRequirement);
        void DeleteWeeklyRequirements(int id);
        void UpdateWeeklyRequirement(int id, WeeklyRequirementCreateDto updatedWeeklyRequirement);
    }
}
