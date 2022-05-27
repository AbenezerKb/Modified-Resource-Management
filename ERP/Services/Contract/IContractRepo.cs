using ERP.Models;

namespace ERP.Services
{
    public interface IContractRepo
    {
        bool SaveChanges();
        IEnumerable<Contract> GetAllContract();
        Contract GetContract(int id);
        void CreateContract(Contract contract);
        void DeleteContract(int id);
        void UpdateContract(int id,Contract updatedContract);
    }

}
