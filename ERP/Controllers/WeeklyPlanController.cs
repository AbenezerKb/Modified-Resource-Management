using ERP.DTOs.Others;
using ERP.DTOs.WeeklyPlan;
using ERP.Exceptions;
using ERP.Models;
using ERP.Services.WeeklyPlanService;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    [ApiController]
    [Route("api/weeklyPlans")]
    public class WeeklyPlanController : ControllerBase
    {
        private readonly IWeeklyPlanService weeklyPlanService;

        public WeeklyPlanController(IWeeklyPlanService service)
        {
            weeklyPlanService = service;
        }
        [HttpPost]
        public async Task<ActionResult<CustomApiResponse>> CreateNewPlan([FromBody] WeeklyPlanDto weeklyPlanDto)
        {
            try
            {
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await weeklyPlanService.Add(weeklyPlanDto)
                });

            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse
                {
                    Message = infe.Message,

                });
            }
            catch (ItemAlreadyExistException iaee)
            {
                return Conflict(
                    new CustomApiResponse
                    {
                        Message = iaee.Message
                    }
                );

            }
        }

        [HttpGet]
        public async Task<ActionResult<CustomApiResponse>> GetPlansWith([FromQuery] int projectId, [FromQuery] int? week, [FromQuery] int? month, [FromQuery] int? year)
        {
            try
            {
                if (week != null && month != null)
                {
                    return BadRequest(
                        new CustomApiResponse
                        {
                            Message = "Both year and month can't be set at the same time, please remove one of them"
                        }
                    );
                }
                else if (week == null && year == null)
                {
                    return Ok(new CustomApiResponse
                    {
                        Message = "Success",

                        Data = await weeklyPlanService.GetByProjectId(projectId)
                    });
                }
                else if (month != null && year != null)
                {
                    return Ok(new CustomApiResponse
                    {
                        Message = "Success",

                        Data = await weeklyPlanService.GetByMonthYear(month.Value, year.Value, projectId)
                    });
                }

                else if (week != null && year != null)
                {

                    return Ok(new CustomApiResponse
                    {
                        Message = "Success",
                        Data = new List<WeeklyPlan>{
                    await weeklyPlanService.GetByWeekAndYear(week.Value, year.Value,projectId)}
                    });

                }

                else
                {
                    return BadRequest(new CustomApiResponse
                    {
                        Message = "Can't filter Weekly Plans only by week or year, Either pass both week and year or omit both of them"
                    });
                }
            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse
                {
                    Message = infe.Message
                });

            }

        }

        [HttpPost("{weeklyPlanId}/tasks")]
        public async Task<ActionResult<CustomApiResponse>> AddTaskToWeeklyPlan(int weeklyPlanId, [FromQuery] int subTaskId, [FromQuery] int performedBy)
        {
            try
            {
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await weeklyPlanService.AddTask(subTaskId, performedBy, weeklyPlanId)
                });

            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse
                {
                    Message = infe.Message
                });
            }
            catch (ItemAlreadyExistException iaex)
            {
                return Conflict(
                    new CustomApiResponse
                    {
                        Message = iaex.Message
                    }
                );
            }
        }
        [HttpDelete("{weeklyPlanId:int}")]
        public async Task<ActionResult<CustomApiResponse>> RemoveWeeklyPlan(int weeklyPlanId)
        {
            try
            {
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await weeklyPlanService.Remove(weeklyPlanId)
                });
            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse
                {
                    Message = infe.Message
                });
            }

        }
        [HttpDelete("tasks/{subTaskId:int}")]
        public async Task<ActionResult<CustomApiResponse>> RemovePlannedTask(int subTaskId, [FromQuery] int weeklyPlanId)
        {
            try
            {
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await weeklyPlanService.RemovePlannedTask(subTaskId, weeklyPlanId)
                });
            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse
                {
                    Message = infe.Message
                });
            }

        }
    }
}