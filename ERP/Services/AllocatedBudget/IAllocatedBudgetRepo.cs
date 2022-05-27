using ERP.Models;


namespace ERP.Services
{
    public interface IAllocatedBudgetRepo
    {
        bool SaveChanges();
        IEnumerable<AllocatedBudget> GetAllAllocatedBudgets();
        AllocatedBudget GetAllocatedBudget(int id);
        void CreateAllocatedBudget(AllocatedBudget allocatedBudget);
        void DeleteAllocatedBudget(int id);
        void UpdateAllocatedBudget(int id,AllocatedBudget allocatedBudget);
    }
}
