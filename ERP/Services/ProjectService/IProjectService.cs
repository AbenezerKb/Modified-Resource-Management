using ERP.Models;
using ERP.DTOs.Project;
using ERP.DTOs.TaskProgressSheet;
using ERP.DTOs.TaskSchedule;
using ERP.Models.Others;

namespace ERP.Services.ProjectService
{
    public interface IProjectService
    {
        Task<Project> Add(ProjectDto projectDto);
        Task<Project> ApproveProject(int projectId, ProjectStatus approvalStatus);
        Task<Project> Remove(int id);
        Task<Project> Update(int id, ProjectDto projectDto);
        Task<List<Project>> GetAll();
        Task<Project> GetById(int id);
        Task<List<Project>> GetBySiteId(int siteId);
        Task<List<Project>> GetByName(string name);
        Task<List<Project>> GetByNameAndSiteId(string name, int siteId);
        Task<List<TaskProgressSheetDto>> GetTaskProgressSheet(int projectId);
        Task<List<TaskScheduleDto>> GetCrashSchedule(int projectId);
        Task<List<TaskScheduleDto>> GetActualSchedule(int projectId);

    }
}
