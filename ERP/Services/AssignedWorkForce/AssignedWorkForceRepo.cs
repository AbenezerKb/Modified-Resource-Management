using ERP.Context;
using ERP.Exceptions;
using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public class AssignedWorkForceRepo : IAssignedWorkForceRepo
    {
        private readonly DataContext _context;

        public AssignedWorkForceRepo(DataContext context)
        {
            _context = context;

        }
        public AssignedWorkForce CreateAssignedWorkForce(AssignedWorkForceCreateDto assignedWorkForceCreateDto)
        {
            if (assignedWorkForceCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            AssignedWorkForce assignedWorkForce = new AssignedWorkForce();
            assignedWorkForce.date = assignedWorkForceCreateDto.date;
            assignedWorkForce.projectId = assignedWorkForceCreateDto.projectId;
            assignedWorkForce.remark = assignedWorkForceCreateDto.remark;
            assignedWorkForce.ProfessionWithWork = assignedWorkForceCreateDto.ProfessionWithWork;

            var project = _context.Projects.FirstOrDefault(c => c.Id == assignedWorkForceCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {assignedWorkForceCreateDto.projectId} not found");

            assignedWorkForce.project = project;
            assignedWorkForce.remark = assignedWorkForceCreateDto.remark;          

            //GetCoordinator here from assigned proffessionals

            _context.Notifications.Add(new Notification
            {
                Title = "Wokrforce assigned to project.",
                Content = $"Wokrforce has been assigned to project.",
                Type = NOTIFICATIONTYPE.WorkForceAssigned,
                SiteId = project.Site.SiteId,
                // EmployeeId = project.CoordinatorId,
                ActionId = assignedWorkForce.assigneWorkForceNo,
                Status = 0

            });

            _context.AssignedWorkForces.Add(assignedWorkForce);

            return assignedWorkForce;

        }

        public IEnumerable<AssignedWorkForce> GetAllAssignedWorkForces()
        {
            var assignedList = _context.AssignedWorkForces.ToList();
            var workwithForceList = _context.WorkForces.ToList();
            for (int i = 0; i < assignedList.Count; i++)
            {
                foreach (WorkForce aw in workwithForceList)
                {
                    if (aw.assigneWorkForceNo == assignedList[i].assigneWorkForceNo)
                    {
                        assignedList[i].ProfessionWithWork.Add(aw);
                    }
                }

            }



            return assignedList;
        }


        public AssignedWorkForce GetAssignedWorkForce(int id)
        {
            //return 
            var assignedList = _context.AssignedWorkForces.FirstOrDefault(c => c.assigneWorkForceNo == id);
            var workwithForceList = _context.WorkForces.ToList();
            foreach (WorkForce aw in workwithForceList)
            {
                if (aw.assigneWorkForceNo == assignedList.assigneWorkForceNo)
                {
                    assignedList.ProfessionWithWork.Add(aw);
                }
            }
            return assignedList;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


        public void DeleteAssignedWorkForce(int id)
        {
            var assignedWorkForce = _context.AssignedWorkForces.FirstOrDefault(c => c.assigneWorkForceNo == id);
            if (assignedWorkForce == null)
            {
                throw new ArgumentNullException();

            }
            _context.AssignedWorkForces.Remove(assignedWorkForce);
        }


        public void UpdateAssignedWorkForce(int id, AssignedWorkForceCreateDto updatedAssignedWorkForce)
        {
            if (updatedAssignedWorkForce == null)
            {
                throw new ArgumentNullException();
            }

            AssignedWorkForce assignedWorkForce = _context.AssignedWorkForces.FirstOrDefault(c => c.assigneWorkForceNo == id);
            if (updatedAssignedWorkForce == null)
                throw new ItemNotFoundException($"Allocated budget not found with allocatedbudgetId={id}");
       

            assignedWorkForce.date = updatedAssignedWorkForce.date;
            assignedWorkForce.projectId = updatedAssignedWorkForce.projectId;
            assignedWorkForce.remark = updatedAssignedWorkForce.remark;
            assignedWorkForce.ProfessionWithWork = updatedAssignedWorkForce.ProfessionWithWork;

            var project = _context.Projects.FirstOrDefault(c => c.Id == updatedAssignedWorkForce.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {updatedAssignedWorkForce.projectId} not found");

            assignedWorkForce.project = project;
            assignedWorkForce.remark = updatedAssignedWorkForce.remark;
            
            _context.AssignedWorkForces.Update(assignedWorkForce);
            _context.SaveChanges();
            
        }
    }
}
