using ERP.Context;
using ERP.Models;
using Microsoft.EntityFrameworkCore;
namespace ERP.Services.ProjectManagementReportService
{
    public class ProjectManagementReportService : IProjectManagementReportService
    {
        private readonly DataContext dbContext;
        public ProjectManagementReportService(DataContext appDbContext)
        {
            dbContext = appDbContext;

        }
        private double getTotalBudget(List<ProjectTask> tasks)
        {
            double total = 0.0;
            tasks.ForEach(t =>
            {
                total += t.GetTotalBudget();

            });
            return total;
        }
        public async Task<Object> GetReportWith(DateTime StartDate, DateTime EndDate, int projectId)
        {
            if (!await dbContext.Projects.AnyAsync(p => p.Id == projectId))
            {
                throw new Exceptions.ItemNotFoundException($"Project not found with projectId={projectId}");
            }

            // ProjectManagementReportDto report = new();
            var projectTasks = await dbContext.Tasks.Where(t => (t.CreatedAt.Date.Day >= StartDate.Day && t.CreatedAt.Day <= EndDate.Day) && t.ProjectId == projectId)
                                                     .Include(t => t.SubTasks).ToListAsync();


            var pendingTasks = projectTasks.Where(t => !t.IsCompleted()).Select(t => new
            {
                TaskId = t.Id,
                TaskName = t.Name,
                Progress = (float)t.GetTaskProgress(),
                Status = "Pending"
            });
            var completedTasks = projectTasks.Where(t => t.IsCompleted()).Select(t => new
            {
                TaskId = t.Id,
                TaskName = t.Name,
                Progress = (float)t.GetTaskProgress(),
                Status = "Completed"
            });
            List<object> budgetsummery = new();
            projectTasks.ForEach(t =>
            {
                budgetsummery.AddRange(t.SubTasks.Select(s => new
                {
                    SubTaskName = s.Name,
                    Budget = s.Budget
                }).ToList());
            });
            // report.MaterialsUsed = 0;
            // report.EquipementsUsed = 0;

            // report.SubContractorWorksCompleted = 0;
            // report.BudgetExpensed = (float)getTotalBudget(projectTasks); ;

            // report.ReportDate = DateTime.Now;

            // report.ProjectId = projectId;
            return new
            {
                Tasks = new
                {
                    Completed = completedTasks,
                    Pending = pendingTasks,
                    ProjectId = projectId
                },
                Budgets = new
                {
                    // which tasks, how much budget
                    Summery = budgetsummery
                },
                Resources = new
                {
                    //Materials assigned
                    MaterialsUsed = new { },
                    //Equipmenets assigned
                    EquipmentsUsed = new { }
                },
                SubContractors = new
                {
                    //Works Completed By the subcontractor
                    CompletedTasks = new { }
                },
                Consultants = new
                {
                    //Uknown
                },
                Workforces = new
                {
                    // list those who worked on the project
                },
                Incidents = new
                {
                    // list all incidents occured on the project between the specified Dates

                }
            };

        }


    }
}