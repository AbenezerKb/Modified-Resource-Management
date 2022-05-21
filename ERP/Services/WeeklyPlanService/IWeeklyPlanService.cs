using ERP.Models;
using ERP.DTOs.WeeklyPlan;

namespace ERP.Services.WeeklyPlanService
{
    public interface IWeeklyPlanService
    {
        Task<WeeklyPlan> Add(WeeklyPlanDto weeklyPlanDto);
        Task<WeeklyPlan> GetById(int weeklyPlanId);
        Task<List<WeeklyPlan>> GetByProjectId(int projectId);
        Task<WeeklyPlan> GetByWeekAndYear(int week, int year, int projectId);
        Task<List<WeeklyPlan>> GetByMonthYear(int month, int year, int projectId);

        Task<WeeklyPlan> Remove(int weeklyPlanId);
        Task<WeeklyPlanValue> AddTask(int subTaskId, int performedBy, int weeklyPlanId);
        Task<WeeklyPlanValue> RemovePlannedTask(int subTaskId, int weeklyPlanId);
    }

}