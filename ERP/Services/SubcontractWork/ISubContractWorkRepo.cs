using ERP.Models;


namespace ERP.Services
{
    public interface ISubContractWorkRepo
    {

        bool SaveChanges();

        IEnumerable<SubContractWork> GetAllSubContractWorks();
        SubContractWork GetSubContractWork(int id);
        void CreateSubContractWork(SubContractWork contract);
        void DeleteSubContractWorks(int id);
        void UpdateSubContractWork(int id, SubContractWork updatedSubContractWork);
    }
}
