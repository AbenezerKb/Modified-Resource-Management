using Microsoft.AspNetCore.Mvc;
using ERP.Services.ProjectTaskService;
using ERP.Services.SubTaskService;
using ERP.DTOs.Others;
using ERP.Helpers;
using ERP.Models;
using ERP.Exceptions;
using ERP.DTOs.ProjectTask;
using ERP.DTOs.SubTask;
using Microsoft.AspNetCore.Authorization;

namespace ERP.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService tasksService;
        private readonly ISubTaskService subTaskService;
        public ProjectTaskController(IProjectTaskService tasksService, ISubTaskService subTaskService)
        {
            this.tasksService = tasksService;
            this.subTaskService = subTaskService;
        }
        [Authorize(Roles = "ProjectManager,OfficeEngineer,Admin")]
        [HttpGet]
        public async Task<ActionResult<CustomApiResponse>> GetTasks(string? name, int? projectId)
        {
            try
            {

                List<ProjectTask> tasks = await FilterProjectTasks(name, projectId);
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = tasks
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
        [Authorize(Roles = "ProjectManager,OfficeEngineer,Admin")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomApiResponse>> GetTaskById(int id)
        {
            try
            {
                ProjectTask task = await tasksService.GetById(id);
                return Ok(
                    new CustomApiResponse
                    {
                        Message = "Success",
                        Data = task
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

        [Authorize(Roles = "OfficeEngineer,Admin")]
        [HttpPost]
        async public Task<ActionResult<CustomApiResponse>> AddTask([FromBody] ProjectTaskDto projectTaskDto)
        {

            if (!Utils.isValidDateRange(projectTaskDto.StartDate, projectTaskDto.EndDate))
            {
                return BadRequest(new CustomApiResponse
                {
                    Message = "Invalid Date Range, StartDate must be less than EndDate and EndDate must be greater than StartDate",

                });
            }

            try
            {
                return Ok(new CustomApiResponse
                {
                    Message = "Success",
                    Data = await tasksService.Add(projectTaskDto)
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

        [Authorize(Roles = "OfficeEngineer,Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CustomApiResponse>> DeleteTask(int id)
        {
            try
            {
                ProjectTask project = await tasksService.Remove(id);
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
        [Authorize(Roles = "OfficeEngineer,Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CustomApiResponse>> UpdateTask(int id, [FromBody] ProjectTaskDto projectTaskDto)
        {
            try
            {
                var project = await tasksService.Update(id, projectTaskDto);

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

        }

        private async Task<List<ProjectTask>> FilterProjectTasks(string? name, int? projectId)
        {
            List<ProjectTask> projectTasks = new List<ProjectTask>();

            if (projectId != null && name != null)
            {
                projectTasks = await tasksService.GetByNameAndProjectId(name, projectId.Value);
            }
            else if (projectId != null)
            {
                projectTasks = await tasksService.GetByProjectId(projectId.Value);

            }
            else if (name != null)
            {
                projectTasks = await tasksService.GetByName(name);
            }
            else
            {
                projectTasks = await tasksService.GetAll();
            }
            return projectTasks;
        }

        #region SubTasks 

        [HttpGet("{taskId:int}/subTasks")]
        [Authorize(Roles = "OfficeEngineer,ProjectManager,Admin")]

        async public Task<ActionResult<CustomApiResponse>> GetAllSubTasks(int taskId)
        {
            try
            {
                return Ok(new CustomApiResponse
                {

                    Message = "Success",
                    Data = await subTaskService.GetByTaskId(taskId)
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
        [HttpGet("{taskId:int}/subTasks/{subtaskId:int}")]
        [Authorize(Roles = "OfficeEngineer,ProjectManager,Admin")]

        async public Task<ActionResult<CustomApiResponse>> GetSubTask(int subtaskId)
        {
            try
            {
                return Ok(new CustomApiResponse
                {

                    Message = "Success",
                    Data = await subTaskService.GetById(subtaskId)
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

        [HttpPost("{taskId:int}/subTasks")]
        [Authorize(Roles = "OfficeEngineer,Admin")]
        async public Task<ActionResult<CustomApiResponse>> AddSubTask(int taskId, [FromBody] SubTaskDto subTaskDto)
        {
            try
            {
                if (subTaskDto.Priority <= 0 || subTaskDto.Priority > 5)
                {
                    return BadRequest(
                        new CustomApiResponse
                        {
                            Message = $"Invalid sub-task priority, it has to be between 1 and {TaskConstant.MAXPRIORITYVALUE}"
                        }
                    );
                }

                subTaskDto.TaskId = taskId;
                return Ok(new CustomApiResponse
                {

                    Message = "Success",
                    Data = await subTaskService.Add(subTaskDto)
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
        [HttpPut("{taskId:int}/subTasks/{subTaskId:int}")]
        [Authorize(Roles = "OfficeEngineer,Admin")]
        async public Task<ActionResult<CustomApiResponse>> UpdateSubTask(int taskId, int subTaskId, [FromBody] SubTaskDto subTaskDto)
        {
            try
            {
                subTaskDto.TaskId = taskId;

                return Ok(
                    new CustomApiResponse
                    {
                        Message = "Success",
                        Data = await subTaskService.Update(subTaskId, subTaskDto)

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
        [HttpDelete("{taskId:int}/subTasks/{subTaskId:int}")]
        [Authorize(Roles = "OfficeEngineer,Admin")]
        async public Task<ActionResult<CustomApiResponse>> DeleteSubTask(int taskId, int subTaskId)
        {
            try
            {

                return Ok(
                    new CustomApiResponse
                    {
                        Message = "Success",
                        Data = await subTaskService.Remove(subTaskId)

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
            catch (InvalidOperationException ioe)
            {
                return BadRequest(new CustomApiResponse { Message = ioe.Message });
            }
        }


        #endregion
    }
}