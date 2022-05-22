using Microsoft.EntityFrameworkCore;
using ERP.Services.WeeklyResultService;
using ERP.Context;
using ERP.Models;
using ERP.DTOs.WeeklyResult;
using ERP.Exceptions;

namespace ERP.Services.WeeklyResultService
{
    public class WeeklyResultService : IWeeklyResultService
    {
        public readonly DataContext dbContext;
        public WeeklyResultService(DataContext context)
        {
            dbContext = context;

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
            };

            var weeklyPlan = dbContext
            .WeeklyPlans.Where(wp => wp.Id == weeklyResultDto.WeeklyPlanId)
                        .Include(wp => wp.PlanValues)
                        .ThenInclude(pv => pv.SubTask)
                        .FirstOrDefault();

            if (weeklyPlan == null) throw new ItemNotFoundException($"Weekly plan not found with WeeklyPlanId={weeklyResultDto.WeeklyPlanId}");
            weeklyResultDto.Results.ForEach(wr =>
            {
                var subTask = weeklyPlan.PlanValues.Where(wpv => wpv.SubTaskId == wr.SubTaskId).Select(wpv => wpv.SubTask).FirstOrDefault();
                if (subTask == null) throw new ItemNotFoundException($"SubTask not found with SubTaskId={wr.SubTaskId}");

                subTask.Progress = wr.Value;

                weeklyResult.Results.Add(new WeeklyResultValue
                {
                    SubTaskId = wr.SubTaskId,
                    Value = wr.Value
                });

            });

            await dbContext.WeeklyResults.AddAsync(weeklyResult);

            //Generate PerformanceSheet for each professional for their weekly planned work
            var performedTasksByEmployee = weeklyPlan.PlanValues.GroupBy(pv => pv.PerformedBy)
            .Select(pvg => new
            {
                PerformedBy = pvg.Key,
                SubTasks = pvg.Select(pvg => pvg.SubTask)
            }).ToList();
            performedTasksByEmployee.ForEach(async ptbe =>
            {

                await dbContext.PerformanceSheets.AddAsync(
                               new PerformanceSheet
                               {
                                   EmployeeId = ptbe.PerformedBy,
                                   Date = DateTime.Now,
                                   PerformancePoint = (float)ptbe.SubTasks.Average(st => st!.Progress),
                                   ProjectId = weeklyPlan.ProjectId
                               }
                           );
            });

            await dbContext.SaveChangesAsync();
            return weeklyResult;
        }

        // public async Task<WeeklyResult> UpdateWeeklyResultApproval(int weeklyResultId, int employeeId, bool isApproved)
        // {
        //     var weeklyResult = await dbContext.WeeklyResults.Where(wr => wr.Id == weeklyResultId)
        //                                                     .Include(wr => wr.Results)
        //                                                     .ThenInclude(wr => wr.PerformanceSheet)
        //                                                     .Include(wr => wr.Results)
        //                                                     .ThenInclude(wr => wr.SubTask)
        //                                                     .FirstOrDefaultAsync();

        //     if (weeklyResult == null) throw new ItemNotFoundException($"Weekly Result not found with WeeklyResultId= {weeklyResultId}");

        //     weeklyResult.Staus = isApproved ? Models.Others.Status.Approved : Models.Others.Status.Declined;

        //     weeklyResult.ApprovedBy = employeeId;

        //     if (isApproved)
        //     {
        //         weeklyResult.Results.ForEach(wrv =>
        //         {
        //             if (wrv.SubTask != null)
        //             {
        //                 wrv.SubTask!.Progress = wrv.Value;
        //             }
        //             wrv.PerformanceSheet.PerformancePoint = wrv.Value;
        //         });


        //     }
        //     dbContext.WeeklyResults.Update(weeklyResult);

        //     await dbContext.SaveChangesAsync();
        //     return weeklyResult;
        // }

        public Task<List<WeeklyResult>> GetAll()
        {
            return dbContext.WeeklyResults.Include(wr => wr.Results).ToListAsync();
        }

        public async Task<WeeklyResult> GetById(int weeklyResultId)
        {
            var weeklyResult = await dbContext.WeeklyResults.Where(wr => wr.Id == weeklyResultId).Include(wr => wr.Results).FirstOrDefaultAsync();

            if (weeklyResult == null) throw new ItemNotFoundException($"Weekly Result not found with WeeklyResultId= {weeklyResultId}");

            return weeklyResult;
        }

        public async Task<List<WeeklyResult>> GetByProjectId(int projectId)
        {
            var weeklyResults = await dbContext
                                    .WeeklyResults.Include(wr => wr.WeeklyPlan)
                                                .Where(wr => wr.WeeklyPlan!.ProjectId == projectId)
                                                .Include(wr => wr.Results)
                                                .ToListAsync();

            if (weeklyResults == null) throw new ItemNotFoundException($"Weekly Results not found with ProjectId= {projectId}");
            return weeklyResults;
        }

        public async Task<WeeklyResult> GetByWeeklyPlanId(int weeklyPlanId)
        {
            var weeklyResult = await dbContext
            .WeeklyResults.Where(wr => wr.WeeklyPlanId == weeklyPlanId)
                          .Include(wr => wr.Results)
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
            var weeklyResultValue = await dbContext.WeeklyResultValues.FindAsync(weeklyResultValueId);
            if (weeklyResultValue == null) throw new ItemNotFoundException($"Weekly result value not found with WeeklyResultValueId={weeklyResultValueId}");

            weeklyResultValue.Value = newValue;
            dbContext.WeeklyResultValues.Update(weeklyResultValue);
            await dbContext.SaveChangesAsync();
            return weeklyResultValue;
        }
    }

}