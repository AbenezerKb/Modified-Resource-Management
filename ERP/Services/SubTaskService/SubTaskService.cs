using ERP.Context;
using ERP.DTOs.SubTask;
using ERP.Exceptions;
using ERP.Models;
using Microsoft.EntityFrameworkCore;
namespace ERP.Services.SubTaskService
{

    public class SubTaskService : ISubTaskService
    {
        private readonly DataContext dbContext;

        public SubTaskService(DataContext context)
        {
            dbContext = context;
        }

        public async Task<SubTask> Add(SubTaskDto subTaskDto)
        {
            var task = await dbContext.Tasks.FindAsync(subTaskDto.TaskId);
            if (task == null) throw new ItemNotFoundException($"Task not found with TaskId={subTaskDto.TaskId}");
            var subTask = new SubTask
            {
                Name = subTaskDto.Name,
                Priority = subTaskDto.Priority,
                Budget = subTaskDto.Budget,
                StartDate = subTaskDto.StartDate,
                EndDate = subTaskDto.EndDate,
                Progress = 0,
                Remark = subTaskDto.Remark,

            };

            task.SubTasks.Add(subTask);
            await dbContext.SaveChangesAsync();
            return subTask;
        }

        public async Task<List<SubTask>> GetAll()
        {
            return await dbContext.SubTasks.ToListAsync();
        }

        public async Task<SubTask> GetById(int id)
        {
            var subTask = await dbContext.SubTasks.FindAsync(id);

            if (subTask == null) throw new ItemNotFoundException($"SubTask not found with Id=${id}");
            return subTask;
        }

        public async Task<List<SubTask>> GetByTaskId(int taskId)
        {
            var task = await dbContext.Tasks.Where(t => t.Id == taskId).Include(t => t.SubTasks).FirstOrDefaultAsync();
            if (task == null) throw new ItemNotFoundException($"SubTasks not found with TaskId=${taskId}");

            return task.SubTasks;
        }

        public async Task<List<SubTask>> GetUpComming(int deadlineNotificationDay)
        {
            List<SubTask> subTasks = await dbContext.SubTasks.Where(st => EF.Functions.DateDiffDay(st.EndDate, DateTime.Now) <= deadlineNotificationDay && st.Progress != 100)
                                                             .Include(st => st.ProjectTask)
                                                             .ThenInclude(t => t.Project)
                                                             .ToListAsync();
            return subTasks;
        }

        async public Task<SubTask> Remove(int id)
        {
            var subTask = await dbContext.SubTasks.FindAsync(id);
            if (subTask == null) throw new ItemNotFoundException($"SubTask not found with Id=${id}");
            try
            {
                dbContext.SubTasks.Remove(subTask);
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException(message: "Cannot delete the selected subtask because its already associated with other entities");
            }
            return subTask;
        }

        public async Task<SubTask> Update(int id, SubTaskDto subTaskDto)
        {
            var subTask = await dbContext.SubTasks.FindAsync(id);

            if (subTask == null) throw new ItemNotFoundException($"SubTask not found with Id=${id}");

            subTask.Name = subTaskDto.Name;
            subTask.Priority = subTaskDto.Priority;
            //if the subTask was not completed before and it is now completed
            if (!subTask.isCompleted() && subTaskDto.Progress == 100)
            {
                //check if the main taskis completed
                var mainTask = await dbContext.Tasks.Where(t => t.Id == subTask.TaskId)
                    .Include(t => t.Project)
                    .Include(t => t.SubTasks)
                    .FirstOrDefaultAsync();

                dbContext.Notifications.Add(new Notification
                {
                    Title = "Main Activity Completed",
                    Content = $"{mainTask!.Name} is completed from project '{mainTask.Project!.Name}'",
                    Type = NOTIFICATIONTYPE.MainTaskCompleted,
                    SiteId = mainTask.Project.SiteId,
                    ActionId = subTask.Id,
                    Status = 0

                });


            }

            subTask.Progress = subTaskDto.Progress;
            subTask.Budget = subTaskDto.Budget;
            subTask.StartDate = subTaskDto.StartDate;
            subTask.EndDate = subTaskDto.EndDate;
            subTask.Remark = subTaskDto.Remark;

            dbContext.SubTasks.Update(subTask);

            await dbContext.SaveChangesAsync();

            return subTask;
        }
    }
}