using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface IAllocatedBudgetRepo
    {
        bool SaveChanges();
        IEnumerable<AllocatedBudget> GetAllAllocatedBudgets();
        AllocatedBudget GetAllocatedBudget(int id);
        AllocatedBudget CreateAllocatedBudget(AllocatedBudgetCreateDto allocatedBudget);
        void DeleteAllocatedBudget(int id);
        void UpdateAllocatedBudget(int id, AllocatedBudgetCreateDto allocatedBudget);
    }
}
