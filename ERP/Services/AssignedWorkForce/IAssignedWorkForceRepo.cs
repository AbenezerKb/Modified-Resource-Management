using ERP.Models;
namespace ERP.Services
{
    public interface IAssignedWorkForceRepo
    {
        bool SaveChanges();
        IEnumerable<AssignedWorkForce> GetAllAssignedWorkForces();
        AssignedWorkForce GetAssignedWorkForce(int id);
        void CreateAssignedWorkForce(AssignedWorkForce assignedWork);
        public void DeleteAssignedWorkForce(int id);
        void UpdateAssignedWorkForce(int id,AssignedWorkForce updatedAssignedWorkForce);
    }
}
