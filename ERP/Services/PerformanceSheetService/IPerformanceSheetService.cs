using ERP.Models;

namespace ERP.Services.PerformanceSheetService
{
    public interface IPerformanceSheetService
    {
        Task<List<PerformanceSheet>> GetAllByProjectId(int projectId);
        Task<PerformanceSheet> RemoveSheet(int performanceSheetId);
        Task<List<PerformanceSheet>> GetAllByProjectIdAndEmployeeId(int employeeId, int projectId);

    }

}