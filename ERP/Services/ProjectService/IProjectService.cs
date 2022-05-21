using ERP.Models;
using ERP.DTOs.Project;
namespace ERP.Services.ProjectService
{
    public interface IProjectService
    {
        Task<Project> Add(ProjectDto projectDto);
        Task<Project> Remove(int id);
        Task<Project> Update(int id, ProjectDto projectDto);
        Task<List<Project>> GetAll();
        Task<Project> GetById(int id);
        Task<List<Project>> GetBySiteId(string siteId);
        Task<List<Project>> GetByName(string name);
        Task<List<Project>> GetByNameAndSiteId(string name, string siteId);
    }
}
