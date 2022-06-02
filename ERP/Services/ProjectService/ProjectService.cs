using ERP.Context;
using ERP.Models;
using ERP.Exceptions;
using ERP.DTOs.Project;
using ERP.Models.Others;
using Microsoft.EntityFrameworkCore;
using ERP.DTOs.TaskProgressSheet;
using ERP.DTOs.TaskSchedule;

namespace ERP.Services.ProjectService
{

    public class ProjectService : IProjectService
    {
        private readonly DataContext dbContext;
        public ProjectService(DataContext DataContext)
        {
            dbContext = DataContext;

        }

        public async Task<Project> ApproveProject(int projectId, ProjectStatus status)
        {
            var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null) throw new ItemNotFoundException($"Project not found with ProjectId={projectId}");
            project.Status = status;
            await dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<List<TaskScheduleDto>> GetActualSchedule(int projectId)
        {
            List<TaskScheduleDto> actualSchedule = new();
            var weeklyPlans = await dbContext.WeeklyPlans.AsNoTracking().Where(wp => wp.ProjectId == projectId)
                                                   .Include(wp => wp.PlanValues)
                                                   .ThenInclude(wpv => wpv.SubTask)
                                                   .ToListAsync();
            weeklyPlans.ForEach(wp =>
            {
                var weekEndDate = wp.WeekStartDate.AddDays(7);
                var weeklySchedule = wp.PlanValues.Select(wpv => new TaskScheduleDto
                {
                    TaskName = wpv.SubTask!.Name,
                    Priority = wpv.SubTask.Priority,
                    StartDate = wp.WeekStartDate,
                    EndDate = weekEndDate
                });

                actualSchedule.AddRange(weeklySchedule);

            });


            return actualSchedule;
        }

        public async Task<List<Project>> GetByName(string name)
        {
            var projects = await dbContext.Projects.Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Include(p => p.Site)
                .ToListAsync();
            if (!projects.Any())
            {
                throw new ItemNotFoundException($"Projects not found with name={name}");
            }
            return projects;

        }

        public async Task<List<Project>> GetByNameAndSiteId(string name, int siteId)
        {
            var projects = await dbContext.Projects.Where(p => p.SiteId == siteId && p.Name.ToLower().Contains(name.ToLower()))
                 .Include(p => p.Site)
                .ToListAsync();
            if (!projects.Any())
            {
                throw new ItemNotFoundException($"Projects not found with name={name} and siteId={siteId}");
            }
            return projects;
        }

        public async Task<List<Project>> GetBySiteId(int siteId)
        {
            var projects = await dbContext.Projects.Where(p => p.SiteId == siteId)
                .Include(p => p.Site)
                .ToListAsync();
            if (!projects.Any())
            {
                throw new ItemNotFoundException($"Projects not found with siteId={siteId}");
            }
            return projects;
        }

        public async Task<List<TaskScheduleDto>> GetCrashSchedule(int projectId)
        {
            List<SubTask> subTasks = await dbContext.SubTasks.AsNoTracking()
                                                             .Include(st => st.ProjectTask)
                                                             .Where(st => st.ProjectTask!.ProjectId == projectId)
                                                             .OrderBy(st => st.StartDate)
                                                             .ToListAsync();

            List<TaskScheduleDto> crashSchedule = subTasks.Select(st =>
              new TaskScheduleDto
              {
                  TaskName = st.Name,
                  Priority = st.Priority,
                  StartDate = st.StartDate,
                  EndDate = st.EndDate
              }).ToList();

            return crashSchedule;

        }

        public async Task<List<TaskProgressSheetDto>> GetTaskProgressSheet(int projectId)
        {
            var project = await dbContext.Projects.Where(p => p.Id == projectId)
                                                .Include(p => p.Tasks)
                                                .ThenInclude(p => p.SubTasks)
                                                .FirstOrDefaultAsync();
            if (project == null)
            {
                throw new ItemNotFoundException($"Project not found with ProjectId={projectId}");
            }
            List<TaskProgressSheetDto> taskProgressSheets = project.Tasks.Select(t =>
            new TaskProgressSheetDto
            {
                MainTaskId = t.Id,
                MainTaskName = t.Name,
                Progress = (float)t.Progress

            }).ToList();

            return taskProgressSheets;
        }

        async Task<Project> IProjectService.Add(ProjectDto projectDto)
        {

            var site = await dbContext.Sites.FirstOrDefaultAsync(s => s.SiteId == projectDto.SiteId);
            if (site == null)
            {
                throw new ItemNotFoundException($"Site not found with SiteId={projectDto.SiteId}");
            }
            var isSiteAssigned = await dbContext.Projects.AnyAsync(p => p.SiteId == projectDto.SiteId);

            if (isSiteAssigned) throw new ItemAlreadyExistException($"Site already assigned to a project");
            // var manager = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == projectDto.ManagerId);
            // if (manager == null)
            // {
            //     throw new ItemNotFoundException($"Manager not found with ManagerId={projectDto.ManagerId}");
            // }
            //TODO: fetch the following from abenezer
            // var isManagerAssigned = await dbContext.Projects.AnyAsync(p => p.ManagerId == projectDto.ManagerId);
            // if (isManagerAssigned)
            // {
            //     throw new ItemAlreadyExistException($"Manager already assigned to a project with ManagerId={projectDto.ManagerId}");
            // }

            // var coordinator = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == projectDto.CoordinatorId);
            // if (coordinator == null)
            // {
            //     throw new ItemNotFoundException($"Coordinator not found with CoordinatorId={projectDto.CoordinatorId}");
            // }

            var project = new Project
            {
                Name = projectDto.Name,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                SiteId = projectDto.SiteId,

                Status = ProjectStatus.Pending
            };

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();

            project.Site = site;

            return project;

        }

        async Task<List<Project>> IProjectService.GetAll()
        {
            return await dbContext.Projects

                .Include(p => p.Site)
                .ToListAsync();
        }

        async Task<Project> IProjectService.GetById(int id)
        {
            var project = await dbContext.Projects.Where(p => p.Id == id)
                .Include(p => p.Site)
                .FirstOrDefaultAsync();
            if (project == null)
            {
                throw new ItemNotFoundException($"Project not found with id={id}");
            }
            return project;
        }

        async Task<Project> IProjectService.Remove(int id)
        {
            var project = await dbContext.Projects.FindAsync(id);
            if (project == null)
            {
                throw new ItemNotFoundException($"Project not found with id={id}");
            }
            try
            {
                dbContext.Projects.Remove(project);
                await dbContext.SaveChangesAsync();

            }
            catch (DbUpdateException duex)
            {
                throw new InvalidOperationException(message: "Cannot delete the selected project because its already associated with other entities");
            }

            return project;
        }

        async Task<Project> IProjectService.Update(int id, ProjectDto projectDto)
        {
            var project = await dbContext.Projects.FindAsync(id);

            if (project == null)
            {
                throw new ItemNotFoundException($"Project not found with id={id}");
            }
            if (project.Status == ProjectStatus.Approved)
            {
                throw new InvalidOperationException(message: "Project cannot be updated because its already been approved!");
            }

            project.Name = projectDto.Name;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.SiteId = projectDto.SiteId;

            dbContext.Update(project);
            await dbContext.SaveChangesAsync();

            return project;

        }
    }
}
