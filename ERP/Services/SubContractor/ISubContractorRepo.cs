using ERP.Models;

namespace ERP.Services
{
    public interface ISubContractorRepo
    {
        bool SaveChanges();

        IEnumerable<SubContractor> GetAllSubContractors();
        SubContractor GetSubContractor(int id);
        void CreateSubContractor(SubContractor contract);
        void DeleteSubContractor(int id);
        void UpdateSubContractor(int id,SubContractor updatedSubContractor);
    }
}
