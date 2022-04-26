using ERP.Context;
using ERP.DTOs;
using ERP.DTOs.Report;
using ERP.Models;
using ERP.Services.ReportServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class ReportController : Controller
    {
        private readonly DataContext context;
        private readonly IReportService _reportService;

        public ReportController(DataContext context, IReportService reportService)
        {
            this.context = context;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Site>>> Get()
        {
            var sites = await context.Sites.ToListAsync();

            return Ok(sites);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult<ReportReturnDTO<Transfer>>> GetTransfers(TransferReportDTO transferDTO)
        {
           var result = await _reportService.GetTansferReport(transferDTO);

            return Ok(result);
        }

        [HttpPost("issue")]
        public async Task<ActionResult<ReportReturnDTO<Transfer>>> GetIssues(IssueReportDTO issueDTO)
        {
            var result = await _reportService.GetIssueReport(issueDTO);

            return Ok(result);
        }

        [HttpPost("borrow")]
        public async Task<ActionResult<ReportReturnDTO<Transfer>>> GetBorrows(BorrowReportDTO borrowDTO)
        {
            var result = await _reportService.GetBorrowReport(borrowDTO);

            return Ok(result);
        }

        [HttpPost("maintenance")]
        public async Task<ActionResult<ReportReturnDTO<Transfer>>> GetMaintenances(MaintenanceReportDTO maintenanceDTO)
        {
            var result = await _reportService.GetMaintenanceReport(maintenanceDTO);

            return Ok(result);
        }

    }
}
