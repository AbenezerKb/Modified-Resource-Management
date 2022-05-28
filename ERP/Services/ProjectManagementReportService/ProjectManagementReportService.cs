using ERP.Context;
using ERP.Models;
using Microsoft.EntityFrameworkCore;
using ERP.Helpers;
using ERP.Exceptions;

namespace ERP.Services.ProjectManagementReportService
{
    public class ProjectManagementReportService : IProjectManagementReportService
    {
        private readonly DataContext dbContext;
        public ProjectManagementReportService(DataContext DataContext)
        {
            dbContext = DataContext;

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
            var projectTasks = await dbContext.Tasks.Where(t => (t.StartDate >= StartDate && t.EndDate <= EndDate) && t.ProjectId == projectId)
                                                     .Include(t => t.SubTasks).ToListAsync();


            var pendingTasks = projectTasks.Where(t => !t.IsSubContractorWork && !t.IsCompleted()).Select(t => new
            {
                TaskId = t.Id,
                TaskName = t.Name,
                Progress = (float)t.Progress,
                Status = "Pending"
            });
            var completedTasks = projectTasks.Where(t => !t.IsSubContractorWork && t.IsCompleted()).Select(t => new
            {
                TaskId = t.Id,
                TaskName = t.Name,
                Progress = (float)t.Progress,
                Status = "Completed"
            });
            List<object> budgetsummery = new();
            projectTasks.ForEach(t =>
            {
                budgetsummery.AddRange(t.SubTasks.Select(s => new
                {
                    SubTaskId = s.Id,
                    SubTaskName = s.Name,
                    Budget = s.Budget
                }).ToList());
            });

            var subContractorWorks = projectTasks.Where(t => t.IsSubContractorWork)
                                             .Select(t => new
                                             {
                                                 TaskName = t.Name,
                                                 Progress = t.Progress,
                                                 Status = t.IsCompleted() ? "Completed" : "Pending"
                                             }).ToList();

            var incidents = await dbContext.Incidents.Where(i => i.proID == projectId).ToListAsync();

            var consultants = await dbContext.Consultants.Where(c => c.projectId == projectId).ToListAsync();
            return new
            {

                Tasks = new
                {
                    Completed = completedTasks,
                    Pending = pendingTasks,
                    SubContractorWorks = subContractorWorks,
                    ProjectId = projectId
                },
                Budgets = new
                {
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
                Consultants = consultants.Select(c => new
                {

                }),
                Workforces = new
                {
                    // list those who worked on the project
                },
                Incidents = incidents.Select(i => new
                {
                    Id = i.incidentNo,
                    IncidentName = i.incidentName,
                    Description = i.Description
                }),
                ReportDate = DateTime.Now
            };

        }

        public async Task<object> GetGeneralReportWith(DateTime StartDate, DateTime EndDate, List<int> projectsIds)
        {
            var unknownIds = await Utils.GetDifference(projectsIds, await dbContext.Projects.Select(p => p.Id).ToListAsync());

            if (unknownIds.Count != 0) throw new ItemNotFoundException($"Employee(s) not found with id=[{string.Join(',', unknownIds)}]");

            List<object> projectsSummery = new();
            projectsIds.ForEach(async pId =>
            {
                var project = await dbContext.Projects.AsNoTracking().Where(p => p.Id == pId)
                .Include(p => p.Tasks)
                .ThenInclude(p => p.SubTasks)
                .FirstOrDefaultAsync();

                projectsSummery.Add(new
                {
                    Progress = project!.GetProgress(),
                    Budget = project!.GetTotalBudget(),
                    Resource = new
                    {
                        Material = 0,
                        Equipment = 0
                    }

                });


            });
            return projectsSummery;

        }
    }
}