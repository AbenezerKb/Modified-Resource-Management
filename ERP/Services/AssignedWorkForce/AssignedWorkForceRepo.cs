using ERP.Context;
using ERP.Exceptions;
using ERP.Models;

namespace ERP.Services
{
    public class AssignedWorkForceRepo: IAssignedWorkForceRepo
    {
        private readonly DataContext _context;
      
        public AssignedWorkForceRepo(DataContext context)
        {
            _context = context;

        }
        public void CreateAssignedWorkForce(AssignedWorkForce assignedWorkForce)
        {
            if (assignedWorkForce == null)
            {
                throw new ArgumentNullException();
            }
         //  
            foreach (WorkForce w in assignedWorkForce.ProfessionWithWork) { 
               

               w.assigneWorkForceNo = 0 ;               
                _context.WorkForces.Add(w);
            }
            //assignedWorkForce.assigneWorkForceNo
            _context.AssignedWorkForces.Add(assignedWorkForce);
            AssignedWorkForce assignedList = _context.AssignedWorkForces.Last();
            for (int i = 0; i < assignedList.ProfessionWithWork.Count; i++)
            {
                if (assignedWorkForce.ProfessionWithWork[i].assigneWorkForceNo == 0)
                {
                    assignedWorkForce.ProfessionWithWork[i].assigneWorkForceNo = assignedWorkForce.assigneWorkForceNo;
                    _context.WorkForces.First(q => q.assigneWorkForceNo == 0).assigneWorkForceNo = assignedWorkForce.ProfessionWithWork[i].assigneWorkForceNo;
                    _context.SaveChanges();
                }

                   
            }


            var project = _context.Projects.FirstOrDefault(c => c.Id == assignedList.projId);
            

            _context.Notifications.Add(new Notification
            {
                Title = "Wokrforce assigned to project.",
                Content = $"Wokrforce has been assigned to project.",
                Type = NOTIFICATIONTYPE.WorkForceAssigned,
                SiteId = project.Site.SiteId,
                EmployeeId = project.CoordinatorId,
                ActionId = assignedWorkForce.assigneWorkForceNo,
                Status = 0

            });


    
        }

        public IEnumerable<AssignedWorkForce> GetAllAssignedWorkForces()
        {
           var assignedList = _context.AssignedWorkForces.ToList();
           var  workwithForceList = _context.WorkForces.ToList();
            for (int i=0; i < assignedList.Count; i++)
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
            if (assignedWorkForce == null) { 
                throw new ArgumentNullException();

            }
            _context.AssignedWorkForces.Remove(assignedWorkForce);
        }


        public void UpdateAssignedWorkForce(int id,AssignedWorkForce updatedAssignedWorkForce)
        {
            if (updatedAssignedWorkForce == null)
            {
                throw new ArgumentNullException();
            }

            AssignedWorkForce assignedWorkForce = _context.AssignedWorkForces.FirstOrDefault(c => c.assigneWorkForceNo == id);
            if (updatedAssignedWorkForce == null)
                throw new ItemNotFoundException($"Allocated budget not found with allocatedbudgetId={id}");

            for (int i = 0; i < assignedWorkForce.ProfessionWithWork.Count; i++)
            {
                for (int j = 0; j < updatedAssignedWorkForce.ProfessionWithWork.Count; j++)
                {
                    if (assignedWorkForce.ProfessionWithWork[j].WokrkForceID == updatedAssignedWorkForce.ProfessionWithWork[j].WokrkForceID)
                        assignedWorkForce.ProfessionWithWork[j].EmployeeId = updatedAssignedWorkForce.ProfessionWithWork[j].EmployeeId;

                }
            }




            assignedWorkForce.date = updatedAssignedWorkForce.date;            
            assignedWorkForce.projId = updatedAssignedWorkForce.projId;
            assignedWorkForce.remark = updatedAssignedWorkForce.remark;            
            
            _context.AssignedWorkForces.Update(assignedWorkForce);
            _context.SaveChanges();
        }
    }
}
