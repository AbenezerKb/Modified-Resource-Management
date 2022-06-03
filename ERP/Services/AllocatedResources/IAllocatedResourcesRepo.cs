using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface IAllocatedResourcesRepo
    {
        bool SaveChanges();
        IEnumerable<AllocatedResources> GetAllAllocatedResources();
        AllocatedResources GetAllocatedResources(int id);
        AllocatedResources CreateAllocatedResources(AllocatedResourcesCreateDto allocatedResources);
        void DeleteAllocatedResource(int id);
        void UpdateAllocatedResource(int id, AllocatedResourcesCreateDto allocatedResources);
    }
}

