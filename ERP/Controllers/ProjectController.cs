﻿using Microsoft.AspNetCore.Mvc;
using ERP.Services.ProjectService;
using ERP.Services.ProjectManagementReportService;
using ERP.Services.ProjectManagementAnalyticsService;
using ERP.DTOs.Others;
using ERP.DTOs.Project;
using ERP.Exceptions;
using ERP.Models;
using ERP.Helpers;
using ERP.Services.ProjectTaskService;
using Microsoft.AspNetCore.Authorization;
using ERP.Models.Others;

namespace ERP.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IProjectTaskService projectTaskService;
        private readonly IProjectManagementReportService projectManagementReportService;
        private readonly IProjectManagementAnalyticsService projectManagementAnalyticsService;
        public ProjectController(IProjectService service,
         IProjectManagementReportService projectManagementReportService,
          IProjectManagementAnalyticsService projectManagementAnalyticsService, IProjectTaskService projectTaskService)
        {
            projectService = service;
            this.projectManagementReportService = projectManagementReportService;
            this.projectManagementAnalyticsService = projectManagementAnalyticsService;
            this.projectTaskService = projectTaskService;
        }

        [HttpPost("{id:int}/approval")]
        async public Task<ActionResult<CustomApiResponse>> Approval(int projectId, [FromBody] ProjectStatus status)
        {
            try
            {

                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await projectService.ApproveProject(projectId, status)
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
        [HttpPost]
        [Authorize(Roles = "OfficeEngineer,Coordinator,Admin")]
        async public Task<ActionResult<CustomApiResponse>> AddProject([FromBody] ProjectDto projectDto)
        {
            try
            {

                if (!Utils.isValidDateRange(projectDto.StartDate, projectDto.EndDate))
                {
                    return BadRequest(new CustomApiResponse
                    {
                        Message = "Invalid Date Range, StartDate must be less than EndDate and EndDate must be greater than StartDate",

                    });
                }
                return Ok(new CustomApiResponse
                {
                    Data = await projectService.Add(projectDto),
                    Message = "Success"
                });
            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse
                {
                    Message = infe.Message
                });

            }
            catch (ItemAlreadyExistException infe)
            {
                return NotFound(new CustomApiResponse
                {
                    Message = infe.Message
                });

            }

        }

        [Authorize(Roles = "Admin,OfficeEngineer,Coordinator,ProjectManager,Manager")]
        [HttpGet]
        public async Task<ActionResult<CustomApiResponse>> GetProjects([FromQuery] int? siteId, [FromQuery] string? name)
        {
            try
            {
                List<Project> projects = await FilterProjectsBy(name, siteId);

                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = projects
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

        [HttpGet("{id:int}")]
        [Authorize(Roles = "OfficeEngineer,Coordinator,ProjectManager,Admin")]
        public async Task<ActionResult<CustomApiResponse>> GetProjectById(int id)
        {
            try
            {
                return Ok(new CustomApiResponse
                {
                    Data = await projectService.GetById(id),
                    Message = "Success"
                });
            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse { Message = infe.Message });
            }
        }

        [HttpGet("{id:int}/subContractorWorks")]
        [Authorize(Roles = "ProjectManager,OfficeEngineer,Admin")]
        public async Task<ActionResult<ProjectTask>> GetSubContractingWorks(int id)
        {
            try
            {
                return Ok(new CustomApiResponse
                {
                    Data = await projectTaskService.GetSubContractorWorks(id),
                    Message = "Success"
                });
            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse { Message = infe.Message });
            }

        }
        [HttpGet("report")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<CustomApiResponse>> GetProjectsReport([FromHeader] List<int> Ids, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                Console.WriteLine("IDs from controller: " + Ids.Count());
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await projectManagementReportService.GetGeneralReportWith(StartDate, EndDate, Ids)

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

        [HttpGet("{id:int}/report")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<CustomApiResponse>> GetReport(int id, [FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate)
        {

            try
            {
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await projectManagementReportService.GetReportWith(StartDate, EndDate, id)

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
        [HttpGet("{id:int}/analytics")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult<CustomApiResponse>> GetAnalytics(int id)
        {
            try
            {
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await projectManagementAnalyticsService.GetAnalytics(id)

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
        [HttpGet("{id:int}/taskProgressSheet")]
        public async Task<ActionResult<CustomApiResponse>> GetTaskProgressSheet(int id)
        {
            try
            {
                return Ok(
                    new CustomApiResponse
                    {
                        Message = "Success",
                        Data = await projectService.GetTaskProgressSheet(id)
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
        [HttpGet("{id:int}/crashSchedule")]
        [Authorize(Roles = "OfficeEngineer,Coordinator,ProjectManager,Admin")]
        public async Task<ActionResult<CustomApiResponse>> GetCrashSchedule(int id)
        {
            try
            {
                return Ok(
                    new CustomApiResponse
                    {
                        Message = "Success",
                        Data = await projectService.GetCrashSchedule(id)
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
        [HttpGet("{id:int}/actualSchedule")]
        [Authorize(Roles = "ProjectManager,Admin")]
        public async Task<ActionResult<CustomApiResponse>> GetActualSchedule(int id)
        {
            try
            {
                return Ok(
                    new CustomApiResponse
                    {
                        Message = "Success",
                        Data = await projectService.GetActualSchedule(id)
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

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "OfficeEngineer,Admin")]
        public async Task<ActionResult<CustomApiResponse>> DeleteProject(int id)
        {

            try
            {
                Project project = await projectService.Remove(id);
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = project
                });
            }
            catch (ItemNotFoundException infe)
            {
                return NotFound(new CustomApiResponse { Message = infe.Message });
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(new CustomApiResponse { Message = ioe.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "OfficeEngineer,Admin")]
        public async Task<ActionResult<CustomApiResponse>> UpdateProject(int id, [FromBody] ProjectDto projectDto)
        {

            try
            {
                Project project = await projectService.Update(id, projectDto);

                return Ok(new CustomApiResponse
                {
                    Data = project,
                    Message = "Success"
                });

            }
            catch (ItemNotFoundException infe)
            {

                return NotFound(new CustomApiResponse { Message = infe.Message });
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(new CustomApiResponse { Message = ioe.Message });
            }

        }


        private async Task<List<Project>> FilterProjectsBy(string? name, int? siteId)
        {
            List<Project> projects = new List<Project>();
            if (siteId != null && name != null)
            {
                projects = await projectService.GetByNameAndSiteId(name, siteId.Value);
            }
            else if (siteId != null)
            {
                projects = await projectService.GetBySiteId(siteId.Value);

            }
            else if (name != null)
            {
                projects = await projectService.GetByName(name);
            }
            else
            {
                projects = await projectService.GetAll();
            }
            return projects;
        }
    }
}