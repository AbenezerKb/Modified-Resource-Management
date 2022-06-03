using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface ISubContractWorkRepo
    {

        bool SaveChanges();

        IEnumerable<SubContractWork> GetAllSubContractWorks();
        SubContractWork GetSubContractWork(int id);
        SubContractWork CreateSubContractWork(SubContractWorkCreateDto contract);
        void DeleteSubContractWorks(int id);
        void UpdateSubContractWork(int id, SubContractWorkCreateDto contract);
        
    }
}
