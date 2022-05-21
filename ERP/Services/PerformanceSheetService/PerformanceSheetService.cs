using ERP.Context;
using ERP.Exceptions;
using ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services.PerformanceSheetService
{
    public class PerformanceSheetService : IPerformanceSheetService
    {
        public readonly DataContext dbContext;
        public PerformanceSheetService(DataContext context)
        {

            dbContext = context;
        }

        public async Task<List<PerformanceSheet>> GetAllByProjectId(int projectId)
        {
            List<PerformanceSheet> sheets = await dbContext.PerformanceSheets.Include(ps => ps.ProjectTask)
                                                          .Where(ps => ps.ProjectTask!.ProjectId == projectId)
                                                          .Include(ps => ps.WeeklyResultValue)
                                                          .ToListAsync();
            if (!sheets.Any()) throw new ItemNotFoundException($"Performance sheets not found with ProjectId={projectId}");

            return sheets;
        }
        public async Task<List<PerformanceSheet>> GetAllByProjectIdAndEmployeeId(int employeeId, int projectId)
        {
            List<PerformanceSheet> sheets = await dbContext.PerformanceSheets
                                                          .Where(ps => ps.ProjectTask!.ProjectId == projectId && ps.EmployeeId == employeeId)
                                                          .Include(ps => ps.WeeklyResultValue)
                                                          .ToListAsync();
            if (!sheets.Any()) throw new ItemNotFoundException($"Performance sheets not found with ProjectId={projectId}");

            return sheets;
        }

        public async Task<List<PerformanceSheet>> GetAllByTaskIdAndProjectId(int taskId, int projectId)
        {
            List<PerformanceSheet> sheets = await dbContext.PerformanceSheets.Include(ps => ps.ProjectTask)
                                                                             .Where(ps => ps.ProjectTaskId == taskId && ps.ProjectTask!.ProjectId == projectId)
                                                                             .ToListAsync();
            if (!sheets.Any()) throw new ItemNotFoundException($"Performance sheets not found with TaskId={taskId}");

            return sheets;

        }
    }

}