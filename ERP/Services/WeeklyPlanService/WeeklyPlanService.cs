using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ERP.Exceptions;
using ERP.Context;
using ERP.Models;
using ERP.DTOs.WeeklyPlan;
using ERP.Helpers;
namespace ERP.Services.WeeklyPlanService
{

    public class WeeklyPlanService : IWeeklyPlanService
    {
        private readonly DataContext dbContext;

        public WeeklyPlanService(DataContext context)
        {
            dbContext = context;
        }



        private async Task<bool> PlanExists(WeeklyPlanDto weeklyPlanDto)
        {
            // var weekEndDate = weeklyPlanDto.WeekStartDate.AddDays(7);
            var plans = await dbContext.WeeklyPlans
                      .AsNoTracking().Where(p => p.ProjectId == weeklyPlanDto.ProjectId)
                      .ToListAsync();
            // return plans.Any(p => p.WeekStartDate >= weeklyPlanDto.WeekStartDate
            //            && p.WeekStartDate.AddDays(7) <= weekEndDate);
            return plans.Any(p => ISOWeek.GetWeekOfYear(p.WeekStartDate) == ISOWeek.GetWeekOfYear(weeklyPlanDto.WeekStartDate));
        }

        private async Task validateRequest(WeeklyPlanDto weeklyPlanDto)
        {
            if (await PlanExists(weeklyPlanDto)) throw new ItemAlreadyExistException($"Weekly Plan already exist with WeekStartDate=${weeklyPlanDto.WeekStartDate} and ProjectId={weeklyPlanDto.ProjectId}");


            if (!await dbContext.Projects.AnyAsync(p => p.Id == weeklyPlanDto.ProjectId)) throw new ItemNotFoundException($"Project not found with projectId={weeklyPlanDto.ProjectId}");

            var unknownSubTaskIds = await Utils.GetDifference(
                  weeklyPlanDto.PlanValues.Select(pv => pv.SubTaskId).ToList(),
                          await dbContext.SubTasks.Select(s => s.Id).ToListAsync()
                    );

            if (unknownSubTaskIds.Count != 0) throw new ItemNotFoundException($"Task(s) not found with id=[{string.Join(',', unknownSubTaskIds)}]");
            var unknownEmployeeIds = await Utils.GetDifference(weeklyPlanDto.PlanValues
                .Where(pv => pv.PerformedBy != null)
                .Select(pv => pv.PerformedBy!.Value)
                .ToList(),

            await dbContext.Employees.Select(e => e.EmployeeId).ToListAsync()
            );
            if (unknownEmployeeIds.Count != 0) throw new ItemNotFoundException($"Employee(s) not found with id=[{string.Join(',', unknownEmployeeIds)}]");
            var unknownSubcontractorIds = await Utils.GetDifference(
                weeklyPlanDto.PlanValues
            .Where(pv => pv.SubContractorId != null)
            .Select(pv => pv.SubContractorId!.Value).ToList(),
           await dbContext.SubContractors.Select(s => s.SubId).ToListAsync()
            );

            if (unknownSubcontractorIds.Count != 0) throw new ItemNotFoundException($"Subcontractor(s) not found with id=[{string.Join(',', unknownSubcontractorIds)}]");

        }
        public async Task<WeeklyPlan> Add(WeeklyPlanDto weeklyPlanDto)
        {

            var project = await dbContext.Projects.FindAsync(weeklyPlanDto.ProjectId);
            if (project == null) throw new ItemNotFoundException($"Project not found with PrjectId={weeklyPlanDto.ProjectId}");
            await validateRequest(weeklyPlanDto);

            List<WeeklyPlanValue> planValues = new();

            weeklyPlanDto.PlanValues.ForEach(wp =>
            {
                WeeklyPlanValue value = new WeeklyPlanValue
                {
                    SubTaskId = wp.SubTaskId,
                };
                if (wp.PerformedBy != null)
                {
                    value.EmployeeId = wp.PerformedBy;
                }
                else
                {
                    value.SubContractorId = wp.SubContractorId;
                }

                planValues.Add(value);
            });

            var newPlan = new WeeklyPlan
            {
                WeekNo = ISOWeek.GetWeekOfYear(weeklyPlanDto.WeekStartDate),
                WeekStartDate = weeklyPlanDto.WeekStartDate,
                Remark = weeklyPlanDto.Remark,
                ProjectId = weeklyPlanDto.ProjectId,
                PlanValues = planValues,

            };

            await dbContext.WeeklyPlans.AddAsync(newPlan);

            await dbContext.Notifications.AddAsync(new Notification
            {
                Title = $"Weekly Plan - {project.Name}",
                Content = $"Weekly plan added for {newPlan.WeekStartDate} - {newPlan.WeekStartDate.AddDays(7)}",
                Type = NOTIFICATIONTYPE.WeeklyTaskPlanSent,
                Status = 0,
                SiteId = project.SiteId
                // addCoordinator Id here
                //EmployeeId=

            });
            await dbContext.SaveChangesAsync();

            return await dbContext.WeeklyPlans.Where(wp => wp.Id == newPlan.Id)
            .Include(wp => wp.PlanValues)
            .ThenInclude(pv => pv.Employee)
            .Include(wp => wp.PlanValues)
            .ThenInclude(pv => pv.SubTask)
            .FirstAsync();

        }

        public async Task<WeeklyPlanValue> AddTask(int weeklyPlanId, WeeklyPlanValueDto weeklyPlanValueDto)
        {
            var weeklyPlan = await GetById(weeklyPlanId);
            var subTask = await dbContext.SubTasks.FindAsync(weeklyPlanValueDto.SubTaskId);

            if (subTask == null) throw new ItemNotFoundException($"SubTask not found with subTaskId={weeklyPlanValueDto.SubTaskId}");
            if (weeklyPlan.PlanValues.Any(t => t.SubTaskId == weeklyPlanValueDto.SubTaskId))
                throw new ItemAlreadyExistException($"SubTask with id={weeklyPlanValueDto.SubTaskId} has already been added to weekly plan");
            //TODO: check if employee exists with the given id
            var planValue = new WeeklyPlanValue
            {
                SubTaskId = weeklyPlanValueDto.SubTaskId,
                WeeklyPlanId = weeklyPlanId
            };
            if (weeklyPlanValueDto.PerformedBy != null)
            {
                planValue.EmployeeId = weeklyPlanValueDto.PerformedBy;
            }
            else
            {
                planValue.SubContractorId = weeklyPlanValueDto.SubContractorId;
            }

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

            var plans = await dbContext.WeeklyPlans.Where(wp => wp.ProjectId == projectId && wp.WeekStartDate.Month == month && wp.WeekStartDate.Year == year)
                                                         .Include(p => p.PlanValues)
                                                         .ThenInclude(wpv => wpv.SubTask)
                                                         .Include(wp => wp.PlanValues)
                                                         .ThenInclude(wpv => wpv.Employee)
                                                         .OrderBy(wp => wp.WeekStartDate)
                                                         .ToListAsync();

            if (plans == null || plans.Count == 0) throw new ItemNotFoundException($"Monthly plan not found with week={month} and year={year}");
            return plans;
        }

        public async Task<List<WeeklyPlan>> GetByProjectId(int projectId)
        {
            List<WeeklyPlan> weeklyPlans = await dbContext.WeeklyPlans.Where(wp => wp.ProjectId == projectId)
                                                                      .Include(wp => wp.PlanValues)
                                                                      .ThenInclude(wpv => wpv.SubTask)
                                                                      .Include(wp => wp.PlanValues)
                                                                      .ThenInclude(wpv => wpv.Employee)
                                                                      .ToListAsync();
            if (weeklyPlans == null || !weeklyPlans.Any()) throw new ItemNotFoundException($"Weekly Plans not found with ProjectId={projectId}");
            return weeklyPlans;
        }

        public async Task<WeeklyPlan> GetByWeekAndYear(int week, int year, int projectId)
        {
            var weeklyPlan = (await dbContext.WeeklyPlans.Where(p => p.ProjectId == projectId && p.WeekNo == week && p.WeekStartDate.Year == year)
                                                        .Include(p => p.PlanValues)
                                                        .ThenInclude(wpv => wpv.SubTask)
                                                         .Include(wp => wp.PlanValues)
                                                         .ThenInclude(wpv => wpv.Employee)
                                                        .ToListAsync())
                                                        .FirstOrDefault();

            if (weeklyPlan == null) throw new ItemNotFoundException($"Weekly plan not found with week={week} and year={year}");
            return weeklyPlan;
        }

        public async Task<WeeklyPlan> Remove(int weeklyPlanId)
        {
            var weeklyPlan = await GetById(weeklyPlanId);
            try
            {
                dbContext.WeeklyPlans.Remove(weeklyPlan);
                await dbContext.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException(message: "Cannot delete the selected weekly plan because its already associated with other entities");
            }
            return weeklyPlan;
        }

        public async Task<WeeklyPlanValue> RemovePlannedTask(int subTaskId, int weeklyPlanId)
        {
            var weeklyPlan = await GetById(weeklyPlanId);
            var planValue = weeklyPlan.PlanValues.ToList().Find(wp => wp.SubTaskId == subTaskId);


            if (planValue == null) throw new ItemNotFoundException($"WeeklyPlanValue not found by subTaskId={subTaskId}");
            if (DateTime.Now.Subtract(weeklyPlan.WeekStartDate).Days > 2)
                throw new InvalidOperationException(message: "You cannot delete a planned task after 2 days of the weekly plan start date");
            weeklyPlan.PlanValues.Remove(planValue);
            await dbContext.SaveChangesAsync();
            return planValue;
        }

        public async Task<WeeklyPlan> Update(int planId, WeeklyPlanDto weeklyPlanDto)
        {
            var project = await dbContext.Projects.FindAsync(weeklyPlanDto.ProjectId);
            if (project == null) throw new ItemNotFoundException($"Project not found with PrjectId={weeklyPlanDto.ProjectId}");
            var weeklyPlan = await GetById(planId);

            if (weeklyPlan.WeekStartDate != weeklyPlanDto.WeekStartDate)
            {
                weeklyPlan.WeekStartDate = weeklyPlanDto.WeekStartDate;
                weeklyPlan.WeekNo = ISOWeek.GetWeekOfYear(weeklyPlanDto.WeekStartDate);

            }
            weeklyPlan.Remark = weeklyPlanDto.Remark;
            await dbContext.SaveChangesAsync();
            return weeklyPlan;
        }
    }
}