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
        public async Task<ActionResult<CustomApiResponse>> GetAllPerformanceSheets([FromQuery] int? employeeId, [FromQuery] int projectId)
        {
            try
            {
                if (employeeId == null)
                {
                    return Ok(new CustomApiResponse
                    {
                        Data = await performanceSheetService.GetAllByProjectId(projectId)
                    });
                }
                else
                {
                    return Ok(new CustomApiResponse
                    {
                        Data = await performanceSheetService.GetAllByProjectIdAndEmployeeId(employeeId.Value, projectId)
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CustomApiResponse>> RemovePerformanceSheet(int id)
        {
            try
            {

                return Ok(
                    new CustomApiResponse
                    {
                        Message = "Success",
                        Data = await performanceSheetService.RemoveSheet(id)
                    }
                );
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