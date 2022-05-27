
namespace ERP.Services.ProjectManagementReportService
{
    public interface IProjectManagementReportService
    {
        Task<object> GetReportWith(DateTime StartDate, DateTime EndDate, int projectId);
        Task<object> GetGeneralReportWith(DateTime StartDate, DateTime EndDate, List<int> projectsIds);

    }

}