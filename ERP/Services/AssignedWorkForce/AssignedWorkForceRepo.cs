using ERP.Context;
using ERP.Exceptions;
using ERP.Models;

namespace ERP.Services
{
    public class AssignedWorkForceRepo: IAssignedWorkForceRepo
    {
        private readonly AppDbContext _context;
      
        public AssignedWorkForceRepo(AppDbContext context)
        {
            _context = context;

        }
        public void CreateAssignedWorkForce(AssignedWorkForce assignedWorkForce)
        {
            if (assignedWorkForce == null)
            {
                throw new ArgumentNullException();
            }
         //   assignedWorkForce.assigneWorkForceNo =// Guid.NewGuid().ToString();
            foreach (WorkForce w in assignedWorkForce.ProfessionWithWork) { 
                //w.WokrkForceID =// Guid.NewGuid().ToString();

               w.assigneWorkForceNo = 0 ;               
                _context.WorkForces.Add(w);
            }
            //assignedWorkForce.assigneWorkForceNo
            _context.AssignedWorkForces.Add(assignedWorkForce);
            foreach (WorkForce w in assignedWorkForce.ProfessionWithWork)
            {
                //w.WokrkForceID =// Guid.NewGuid().ToString();

                w.assigneWorkForceNo = assignedWorkForce.assigneWorkForceNo;
                _context.WorkForces.Update(w);
            }

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
