using ERP.Models;

namespace ERP.Services.PerformanceSheetService
{
    public interface IPerformanceSheetService
    {
        Task<List<PerformanceSheet>> GetAllByProjectId(int projectId);
        Task<List<PerformanceSheet>> GetAllByTaskIdAndProjectId(int taskId, int projectId);
        Task<List<PerformanceSheet>> GetAllByProjectIdAndEmployeeId(int employeeId, int projectId);

    }

}