using ERP.DTOs;
using ERP.DTOs.Others;
using ERP.Exceptions;
using ERP.Services.PerformanceSheetService;
using Microsoft.AspNetCore.Mvc;
namespace ERP.Controllers
{
    [Route("api/performanceSheet")]
    [ApiController]
    public class PerformanceSheetController : ControllerBase
    {

        private IPerformanceSheetService performanceSheetService;
        public PerformanceSheetController(IPerformanceSheetService sheetService)
        {
            performanceSheetService = sheetService;
        }
        [HttpGet]
        public async Task<ActionResult<CustomApiResponse>> GetAllPerformanceSheets([FromQuery] int? taskId, [FromQuery] int? employeeId, [FromQuery] int projectId)
        {
            try
            {
                if (taskId == null && employeeId == null)
                {
                    return Ok(new CustomApiResponse
                    {
                        Data = await performanceSheetService.GetAllByProjectId(projectId)
                    });
                }

                if (taskId != null && employeeId == null)
                {
                    return Ok(new CustomApiResponse
                    {
                        Data = await performanceSheetService.GetAllByTaskIdAndProjectId(taskId.Value, projectId)
                    });
                }
                if (employeeId != null && taskId == null)
                {
                    return Ok(new CustomApiResponse
                    {
                        Data = await performanceSheetService.GetAllByProjectIdAndEmployeeId(employeeId.Value, projectId)
                    });

                }

                //if both employeeId and taskId are set

                return BadRequest(new CustomApiResponse
                {
                    Message = "Both taskId and employeeId cannot be set at one request, Please use only one of them"
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