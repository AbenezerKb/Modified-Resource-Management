using ERP.Models;

namespace ERP.Services
{
    public interface IAllocatedResourcesRepo
    {
        bool SaveChanges();
        IEnumerable<AllocatedResources> GetAllAllocatedResources();
        AllocatedResources GetAllocatedResources(int id);
        void CreateAllocatedResources(AllocatedResources allocatedResources);
        void DeleteAllocatedResource(int id);
        void UpdateAllocatedResource(int id,AllocatedResources allocatedResources);
    }
}

