using ERP.Models;
using ERP.DTOs;
namespace ERP.Services
{
    public interface IAssignedWorkForceRepo
    {
        bool SaveChanges();
        IEnumerable<AssignedWorkForce> GetAllAssignedWorkForces();
        AssignedWorkForce GetAssignedWorkForce(int id);
        AssignedWorkForce CreateAssignedWorkForce(AssignedWorkForceCreateDto assignedWork);
        public void DeleteAssignedWorkForce(int id);
        void UpdateAssignedWorkForce(int id, AssignedWorkForceCreateDto updatedAssignedWorkForce);
    }
}
