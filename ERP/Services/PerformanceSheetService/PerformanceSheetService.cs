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
            List<PerformanceSheet> sheets = await dbContext.PerformanceSheets
                                                          .Where(ps => ps.ProjectId == projectId)

                                                          .ToListAsync();
            if (!sheets.Any()) throw new ItemNotFoundException($"Performance sheets not found with ProjectId={projectId}");

            return sheets;
        }
        public async Task<List<PerformanceSheet>> GetAllByProjectIdAndEmployeeId(int employeeId, int projectId)
        {
            List<PerformanceSheet> sheets = await dbContext.PerformanceSheets
                                                          .Where(ps => ps.ProjectId == projectId && ps.EmployeeId == employeeId)

                                                          .ToListAsync();
            if (!sheets.Any()) throw new ItemNotFoundException($"Performance sheets not found with EmployeeId={employeeId} and ProjectId={projectId}");

            return sheets;
        }

        public async Task<PerformanceSheet> RemoveSheet(int performanceSheetId)
        {
            var sheet = await dbContext.PerformanceSheets.Where(ps => ps.Id == performanceSheetId).FirstOrDefaultAsync();
            if (sheet == null) throw new ItemNotFoundException($"Performance sheet not found with Id={performanceSheetId}");
            dbContext.PerformanceSheets.Remove(sheet);
            await dbContext.SaveChangesAsync();
            return sheet;
        }
    }

}