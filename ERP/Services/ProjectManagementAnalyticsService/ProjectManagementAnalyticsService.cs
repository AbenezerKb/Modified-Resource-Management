using ERP.Context;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services.ProjectManagementAnalyticsService
{
    public class ProjectManagementAnalyticsService : IProjectManagementAnalyticsService
    {
        DataContext dbContext;
        IGranderRepo granderRepo;
        public ProjectManagementAnalyticsService(DataContext dbContext, IGranderRepo repo)
        {
            this.dbContext = dbContext;
            this.granderRepo = repo;
        }
        public async Task<object> GetAnalytics(int projectId)
        {
            if (!await dbContext.Projects.AnyAsync(p => p.Id == projectId))
            {
                throw new Exceptions.ItemNotFoundException($"Project not found with id={projectId}");
            }
            var projectTasks = await dbContext.Tasks.Where(t => t.ProjectId == projectId)
                                                    .Include(t => t.SubTasks)
                                                    .ToListAsync();
            var tasksCompletedCount = projectTasks.Count(t => t.IsCompleted());
            // var grander = await dbContext.Granders.Where(g => g.ProjectId == projectId).FirstOrDefaultAsync();
            // if (grander == null)
            // {
            //     throw new Exceptions.ItemNotFoundException($"Grander not found for projectId={projectId}");

            // }
            // grander = granderRepo.GetGrander(grander.GranderId);

            return new
            {
                tasks = new
                {
                    completed = tasksCompletedCount,
                    pending = projectTasks.Count - tasksCompletedCount
                },
                professionals = new
                {
                    planned = 0,//grander.WorkForcePlans.Count,
                    used = dbContext.AssignedWorkForces.Where(awf => awf.projId == projectId).Count()
                },
                subContractors = new
                {
                    completed = projectTasks.Where(t => t.IsCompleted() && t.IsSubContractorWork).Count(),
                    pending = projectTasks.Where(t => !t.IsCompleted() && t.IsSubContractorWork).Count()
                },

                resources = new
                {
                    planned = 0,//grander.ResourcePlans.Count,
                    used = dbContext.AssignedWorkForces.Count(awf => awf.projId == projectId),
                    damaged = 0
                },
                budget = new
                {
                    used = projectTasks.Sum(t => t.GetTotalBudget()),
                    planned = 0//grander.TotalEstiamtedReqtBudget
                }
            };
        }
    }
}