using Microsoft.EntityFrameworkCore;
using ERP.Services.WeeklyResultService;
using ERP.Context;
using ERP.Models;
using ERP.DTOs.WeeklyResult;
using ERP.Exceptions;
using ERP.Services.User;

namespace ERP.Services.WeeklyResultService
{
    public class WeeklyResultService : IWeeklyResultService
    {
        private readonly IUserService userService;
        public readonly DataContext dbContext;
        public WeeklyResultService(DataContext context, IUserService service)
        {
            dbContext = context;
            this.userService = service;

        }
        public async Task<WeeklyResult> Add(WeeklyResultDto weeklyResultDto)
        {
            var resultExists = await dbContext.WeeklyResults
            .AnyAsync(wr => wr.WeeklyPlanId == weeklyResultDto.WeeklyPlanId);

            if (resultExists) throw new ItemAlreadyExistException($"Weekly Result already exists for a weekly plan with WeeklyPlanId={weeklyResultDto.WeeklyPlanId}");

            var weeklyResult = new WeeklyResult
            {
                Remark = weeklyResultDto.Remark,
                WeeklyPlanId = weeklyResultDto.WeeklyPlanId,
                ApprovedById = userService.Employee.EmployeeId
            };

            var weeklyPlan = dbContext
            .WeeklyPlans.Where(wp => wp.Id == weeklyResultDto.WeeklyPlanId)
                        .Include(wp => wp.PlanValues)
                        .ThenInclude(pv => pv.SubTask)
                        .FirstOrDefault();

            if (weeklyPlan == null) throw new ItemNotFoundException($"Weekly plan not found with WeeklyPlanId={weeklyResultDto.WeeklyPlanId}");
            weeklyResultDto.Results.ForEach(async wr =>
            {
                var subTask = weeklyPlan.PlanValues.Where(wpv => wpv.SubTaskId == wr.SubTaskId).Select(wpv => wpv.SubTask).FirstOrDefault();
                if (subTask == null) throw new ItemNotFoundException($"SubTask not found with SubTaskId={wr.SubTaskId}");

                subTask.Progress = wr.Value;
                await AddNotificationIfMainTaskCompleted(subTask);

                weeklyResult.Results.Add(new WeeklyResultValue
                {
                    SubTaskId = wr.SubTaskId,
                    Value = wr.Value

                });

            });

            await dbContext.WeeklyResults.AddAsync(weeklyResult);
            await dbContext.AddRangeAsync(GeneratePerformanceSheet(weeklyPlan));

            await dbContext.SaveChangesAsync();
            return weeklyResult;
        }
        private async Task AddNotificationIfMainTaskCompleted(SubTask subTask)
        {

            var mainTask = await dbContext.Tasks.Where(t => t.Id == subTask.TaskId)
                .Include(t => t.SubTasks)
                .Include(t => t.Project)
                .FirstOrDefaultAsync();
            if (mainTask!.IsCompleted())
            {

                await dbContext.Notifications.AddAsync(new Notification
                {
                    Title = "Main Activity Completed",
                    Content = $"{mainTask.Name} is completed from project '{mainTask.Project!.Name}'",
                    Type = NOTIFICATIONTYPE.MainTaskCompleted,
                    SiteId = mainTask.Project.SiteId,
                    ActionId = subTask.Id,
                    Status = 0

                });
            }
        }

        private List<PerformanceSheet> GeneratePerformanceSheet(WeeklyPlan weeklyPlan)
        {
            List<PerformanceSheet> performanceSheets = new();
            //Generate PerformanceSheet for each professional for their weekly planned work
            var tasksPerformedByEmployees = weeklyPlan.PlanValues.Where(p => !p.IsAssignedForSubContractor()).GroupBy(pv => pv.EmployeeId)
            .Select(pvg => new
            {
                PerformedBy = pvg.Key,
                SubTasks = pvg.Select(pvg => pvg.SubTask)
            }).ToList();

            var tasksPerformedBySubcontractors = weeklyPlan.PlanValues.Where(p => p.IsAssignedForSubContractor()).GroupBy(pv => pv.SubContractorId)
            .Select(pvg => new
            {
                PerformedBy = pvg.Key,
                SubTasks = pvg.Select(pvg => pvg.SubTask)
            }).ToList();


            //generate employees performance sheet
            tasksPerformedByEmployees.ForEach(async tpbe =>
            {

                performanceSheets.Add(new PerformanceSheet
                {
                    EmployeeId = tpbe.PerformedBy!.Value,
                    Date = DateTime.Now,
                    PerformancePoint = (float)tpbe.SubTasks.Average(st => st!.Progress),
                    ProjectId = weeklyPlan.ProjectId,
                    Remark = $"Tasks Assigned: {string.Join(',', tpbe.SubTasks.Select(t => t!.Name))}"
                });
            });
            //generate sucontractors performance sheet
            tasksPerformedBySubcontractors.ForEach(async tpbs =>
         {
             performanceSheets.Add(new PerformanceSheet
             {
                 SubContractorId = tpbs!.PerformedBy!.Value,
                 Date = DateTime.Now,
                 PerformancePoint = (float)tpbs.SubTasks.Average(st => st!.Progress),
                 ProjectId = weeklyPlan.ProjectId,
                 Remark = $"Tasks Assigned: {string.Join(',', tpbs.SubTasks.Select(t => t!.Name))}"
             });
         });

            return performanceSheets;
        }
        public Task<List<WeeklyResult>> GetAll()
        {
            return dbContext.WeeklyResults.Include(wr => wr.Results)
                .ThenInclude(wrv => wrv.SubTask)
                .ToListAsync();
        }

        public async Task<WeeklyResult> GetById(int weeklyResultId)
        {
            var weeklyResult = await dbContext.WeeklyResults.Where(wr => wr.Id == weeklyResultId)
                .Include(wr => wr.Results)
                .ThenInclude(wrv => wrv.SubTask)
                .FirstOrDefaultAsync();

            if (weeklyResult == null) throw new ItemNotFoundException($"Weekly Result not found with WeeklyResultId= {weeklyResultId}");

            return weeklyResult;
        }

        public async Task<List<WeeklyResult>> GetByProjectId(int projectId)
        {
            var weeklyResults = await dbContext
                                    .WeeklyResults.Include(wr => wr.WeeklyPlan)
                                                .Where(wr => wr.WeeklyPlan!.ProjectId == projectId)
                                                .Include(wr => wr.Results)
                                                .ThenInclude(wrv => wrv.SubTask)
                                                .ToListAsync();

            if (weeklyResults == null) throw new ItemNotFoundException($"Weekly Results not found with ProjectId= {projectId}");
            return weeklyResults;
        }

        public async Task<WeeklyResult> GetByWeeklyPlanId(int weeklyPlanId)
        {
            var weeklyResult = await dbContext
            .WeeklyResults.Where(wr => wr.WeeklyPlanId == weeklyPlanId)
                          .Include(wr => wr.Results)
                          .ThenInclude(wrv => wrv.SubTask)
                          .FirstOrDefaultAsync();

            if (weeklyResult == null) throw new ItemNotFoundException($"Weekly Results not found with WeeklyPlanId= {weeklyPlanId}");
            return weeklyResult;
        }

        public async Task<WeeklyResult> Remove(int weeklyResultId)
        {
            var weeklyResult = await dbContext.WeeklyResults.FindAsync(weeklyResultId);
            if (weeklyResult == null) throw new ItemNotFoundException($"Weekly result not found with WeeklyResultId={weeklyResultId}");
            dbContext.WeeklyResults.Remove(weeklyResult);
            await dbContext.SaveChangesAsync();
            return weeklyResult;

        }

        public async Task<WeeklyResultValue> UpdateResult(int weeklyResultValueId, int newValue)
        {
            var weeklyResultValue = await dbContext.WeeklyResultValues.Where(wrv => wrv.Id == weeklyResultValueId)
            .Include(wrv => wrv.SubTask)
            .FirstOrDefaultAsync();
            if (weeklyResultValue == null) throw new ItemNotFoundException($"Weekly result value not found with WeeklyResultValueId={weeklyResultValueId}");

            weeklyResultValue.Value = newValue;
            weeklyResultValue.SubTask!.Progress = newValue;

            dbContext.WeeklyResultValues.Update(weeklyResultValue);
            await dbContext.SaveChangesAsync();
            await AddNotificationIfMainTaskCompleted(weeklyResultValue.SubTask);
            await dbContext.SaveChangesAsync();

            return weeklyResultValue;

        }
    }

}