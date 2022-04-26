using ERP.DTOs;
using ERP.DTOs.Report;
using ERP.Models;

namespace ERP.Services.ReportServices
{
    public interface IReportService
    {
        Task<ReportReturnDTO<Borrow>> GetBorrowReport(BorrowReportDTO borrowDTO);
        Task<GeneralReportReturnDTO> GetGeneralReport(GeneralReportDTO reportDTO);
        Task<ReportReturnDTO<Issue>> GetIssueReport(IssueReportDTO issueDTO);
        Task<ReportReturnDTO<Maintenance>> GetMaintenanceReport(MaintenanceReportDTO maintenanceDTO);
        Task<ReportReturnDTO<Transfer>> GetTansferReport(TransferReportDTO transferDTO);
    }
}
