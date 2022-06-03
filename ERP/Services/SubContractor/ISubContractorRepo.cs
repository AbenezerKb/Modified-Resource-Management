using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface ISubContractorRepo
    {
        bool SaveChanges();

        IEnumerable<SubContractor> GetAllSubContractors();
        SubContractor GetSubContractor(int id);
        SubContractor CreateSubContractor(SubContractorCreateDto contract);
        void DeleteSubContractor(int id);
        void UpdateSubContractor(int id, SubContractorCreateDto updatedSubContractor);
    }
}
