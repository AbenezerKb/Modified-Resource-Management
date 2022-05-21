using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ERP.Exceptions;
using ERP.Context;
using ERP.Models;
using ERP.DTOs.WeeklyPlan;

namespace ERP.Services.WeeklyPlanService
{

    public class WeeklyPlanService : IWeeklyPlanService
    {
        private readonly DataContext dbContext;

        public WeeklyPlanService(DataContext context)
        {
            dbContext = context;
        }

        private Task<List<int>> GetDifference(List<int> sample, List<int> all)
        {
            return Task.FromResult(sample.Where(s => !all.Contains(s)).ToList());
        }
        public async Task<WeeklyPlan> Add(WeeklyPlanDto weeklyPlanDto)
        {
            var planExists = await dbContext.WeeklyPlans
            .AnyAsync(p =>
             p.WeekNo == weeklyPlanDto.WeekNo && p.Year == weeklyPlanDto.Year && p.ProjectId == weeklyPlanDto.ProjectId);

            if (planExists) throw new ItemAlreadyExistException($"Weekly Plan already created for week={weeklyPlanDto.WeekNo}, year={weeklyPlanDto.Year} and ProjectId={weeklyPlanDto.ProjectId}");


            if (!await dbContext.Projects.AnyAsync(p => p.Id == weeklyPlanDto.ProjectId)) throw new ItemNotFoundException($"Project not found with projectId={weeklyPlanDto.ProjectId}");

            var unknownIds = await GetDifference(
                  weeklyPlanDto.PlanValues.Select(pv => pv.SubTaskId).ToList(),
                          await dbContext.SubTasks.Select(s => s.Id).ToListAsync()
                    );

            if (unknownIds.Count != 0) throw new ItemNotFoundException($"Task(s) not found with id=[{string.Join(',', unknownIds)}]");
            //TODO: Check for invalid employee Id as well

            List<WeeklyPlanValue> planValues = new();

            weeklyPlanDto.PlanValues.ForEach(wp =>
            {

                planValues.Add(new WeeklyPlanValue
                {
                    PerformedBy = wp.PerformedBy,
                    SubTaskId = wp.SubTaskId,

                });
            });

            var newPlan = new WeeklyPlan
            {
                WeekNo = weeklyPlanDto.WeekNo,
                Year = weeklyPlanDto.Year,
                Remark = weeklyPlanDto.Remark,
                ProjectId = weeklyPlanDto.ProjectId,
                PlanValues = planValues,

            };

            await dbContext.WeeklyPlans.AddAsync(newPlan);
            await dbContext.Notifications.AddAsync(new Notification
            {


            });
            //TODO: Add 'Weekly Plan Sent' Notification Here
            await dbContext.SaveChangesAsync();

            return newPlan;

        }

        public async Task<WeeklyPlanValue> AddTask(int subTaskId, int performedBy, int weeklyPlanId)
        {
            var weeklyPlan = await GetById(weeklyPlanId);
            var subTask = await dbContext.SubTasks.FindAsync(subTaskId);

            if (subTask == null) throw new ItemNotFoundException($"SubTask not found with subTaskId={subTaskId}");
            if (weeklyPlan.PlanValues.Any(t => t.SubTaskId == subTaskId))
                throw new ItemAlreadyExistException($"SubTask with id={subTaskId} has already been added to weekly plan");
            //TODO: check if employee exists with the given id
            var planValue = new WeeklyPlanValue
            {
                SubTaskId = subTaskId,
                PerformedBy = performedBy,
                WeeklyPlanId = weeklyPlanId
            };
            weeklyPlan.PlanValues.Add(planValue);
            await dbContext.SaveChangesAsync();

            return planValue;
        }


        public async Task<WeeklyPlan> GetById(int weeklyPlanId)
        {
            var weeklyPlan = await dbContext.WeeklyPlans.Where(wp => wp.Id == weeklyPlanId)
                                                        .Include(wp => wp.PlanValues)
                                                        .ThenInclude(wpv => wpv.SubTask)
                                                        .FirstOrDefaultAsync();
            if (weeklyPlan == null) throw new ItemNotFoundException($"Weekly plan not found with WeeklyPlanId={weeklyPlanId}");
            return weeklyPlan;
        }

        public async Task<List<WeeklyPlan>> GetByMonthYear(int month, int year, int projectId)
        {

            var yearlyPlans = await dbContext.WeeklyPlans.Where(wp => wp.ProjectId == projectId && wp.Year == year)
                                                         .Include(p => p.PlanValues)
                                                         .ThenInclude(wpv => wpv.SubTask)
                                                         .ToListAsync();

            var monthlyPlans = yearlyPlans.Where(wp => ISOWeek.ToDateTime(year, wp.WeekNo, DayOfWeek.Monday).Month
            == month)
                                          .ToList();
            if (monthlyPlans == null || monthlyPlans.Count == 0) throw new ItemNotFoundException($"Monthly plan not found with week={month} and year={year}");
            return monthlyPlans;
        }

        public async Task<List<WeeklyPlan>> GetByProjectId(int projectId)
        {
            List<WeeklyPlan> weeklyPlans = await dbContext.WeeklyPlans.Where(wp => wp.ProjectId == projectId)
                                                                      .Include(wp => wp.PlanValues)
                                                                      .ThenInclude(wpv => wpv.SubTask)
                                                                      .ToListAsync();
            if (weeklyPlans == null || !weeklyPlans.Any()) throw new ItemNotFoundException($"Weekly Plans not found with ProjectId={projectId}");
            return weeklyPlans;
        }

        public async Task<WeeklyPlan> GetByWeekAndYear(int week, int year, int projectId)
        {
            var weeklyPlan = (await dbContext.WeeklyPlans.Where(p => p.ProjectId == projectId && p.WeekNo == week && p.Year == year)
                                                        .Include(p => p.PlanValues)
                                                        .ThenInclude(wpv => wpv.SubTask)
                                                        .ToListAsync())
                                                        .FirstOrDefault();

            if (weeklyPlan == null) throw new ItemNotFoundException($"Weekly plan not found with week={week} and year={year}");
            return weeklyPlan;
        }

        public async Task<WeeklyPlan> Remove(int weeklyPlanId)
        {
            var weeklyPlan = await GetById(weeklyPlanId);
            dbContext.WeeklyPlans.Remove(weeklyPlan);
            await dbContext.SaveChangesAsync();
            return weeklyPlan;
        }

        public async Task<WeeklyPlanValue> RemovePlannedTask(int subTaskId, int weeklyPlanId)
        {
            var weeklyPlan = await GetById(weeklyPlanId);
            var planValue = weeklyPlan.PlanValues.ToList().Find(wp => wp.SubTaskId == subTaskId);


            if (planValue == null) throw new ItemNotFoundException($"WeeklyPlanValue not found by subTaskId={subTaskId}");

            weeklyPlan.PlanValues.Remove(planValue);
            await dbContext.SaveChangesAsync();
            return planValue;
        }


    }
}