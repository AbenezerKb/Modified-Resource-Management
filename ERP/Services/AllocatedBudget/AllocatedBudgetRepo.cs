using ERP.Context;
using ERP.Exceptions;
using ERP.Models;
using ERP.DTOs;
namespace ERP.Services
{
    public class AllocatedBudgetRepo : IAllocatedBudgetRepo
    {
        private readonly DataContext _context;


        public AllocatedBudgetRepo(DataContext context)
        {
            _context = context;

        }

        public AllocatedBudget CreateAllocatedBudget(AllocatedBudgetCreateDto allocatedBudgetCreateDto)
        {
            if (allocatedBudgetCreateDto == null)
            {
                throw new ArgumentNullException();
            }
            AllocatedBudget allocatedBudget = new AllocatedBudget();
            allocatedBudget.activity = allocatedBudgetCreateDto.activity;
            allocatedBudget.amount = allocatedBudgetCreateDto.amount;

            Employee ApprovedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == allocatedBudgetCreateDto.ApprovedBy);
            if (ApprovedBy == null)
                throw new ItemNotFoundException($"Employee with {allocatedBudgetCreateDto.ApprovedBy} Id not found ");


            allocatedBudget.ApprovedBy = ApprovedBy;
            allocatedBudget.ApprovedById = allocatedBudgetCreateDto.ApprovedBy;
            allocatedBudget.contingency = allocatedBudget.contingency;
            allocatedBudget.date = allocatedBudgetCreateDto.date;

            Employee preparedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == allocatedBudgetCreateDto.ApprovedBy);
            if (preparedBy == null)
                throw new ItemNotFoundException($"Employee with {allocatedBudgetCreateDto.preparedBy} Id not found ");


            allocatedBudget.preparedBy = preparedBy;
            allocatedBudget.preparedById = allocatedBudgetCreateDto.preparedBy;

            Project project = _context.Projects.FirstOrDefault(c => c.Id == allocatedBudgetCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with {allocatedBudgetCreateDto.projectId} Id not found ");


            allocatedBudget.project = project;
            allocatedBudget.projectId = allocatedBudgetCreateDto.projectId;
            
             _context.AllocatedBudgets.Add(allocatedBudget);
            return allocatedBudget;
        }

        public IEnumerable<AllocatedBudget> GetAllAllocatedBudgets()
        {

           var list =   _context.AllocatedBudgets.ToList();
            return list;
        }


        public AllocatedBudget GetAllocatedBudget(int id)
        {
           var allocatedBudget = _context.AllocatedBudgets.FirstOrDefault(c => c.Id == id);
            if (allocatedBudget == null)
                throw new ItemNotFoundException($"Allocated budget not found with allocatedbudgetId={id}");
            return allocatedBudget;
        }

       public void DeleteAllocatedBudget(int id)
        {
            var allocatedBudget =  _context.AllocatedBudgets.FirstOrDefault(c => c.Id == id);
            if (allocatedBudget == null)
                throw new ItemNotFoundException($"Allocated budget not found with allocatedbudgetId={id}");
            _context.AllocatedBudgets.Remove(allocatedBudget);
        }

        
        public void UpdateAllocatedBudget(int id, AllocatedBudgetCreateDto updateAllocatedBudget)
        {
            if (updateAllocatedBudget ==  null)
            {
                throw new ArgumentNullException();
            }

            AllocatedBudget allocatedBudget = _context.AllocatedBudgets.FirstOrDefault(c => c.Id == id);
            allocatedBudget.activity = updateAllocatedBudget.activity;
            allocatedBudget.amount = updateAllocatedBudget.amount;

            Employee ApprovedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == updateAllocatedBudget.ApprovedBy);
            if (ApprovedBy == null)
                throw new ItemNotFoundException($"Employee with {updateAllocatedBudget.ApprovedBy} Id not found ");


            allocatedBudget.ApprovedBy = ApprovedBy;
            allocatedBudget.ApprovedById = updateAllocatedBudget.ApprovedBy;
            allocatedBudget.contingency = updateAllocatedBudget.contingency;
            allocatedBudget.date = updateAllocatedBudget.date;

            Employee preparedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == updateAllocatedBudget.ApprovedBy);
            if (preparedBy == null)
                throw new ItemNotFoundException($"Employee with {updateAllocatedBudget.preparedBy} Id not found ");


            allocatedBudget.preparedBy = preparedBy;
            allocatedBudget.preparedById = updateAllocatedBudget.preparedBy;

            Project project = _context.Projects.FirstOrDefault(c => c.Id == updateAllocatedBudget.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with {updateAllocatedBudget.projectId} Id not found ");


            allocatedBudget.project = project;
            allocatedBudget.projectId = updateAllocatedBudget.projectId;


            _context.AllocatedBudgets.Update(allocatedBudget);
            
            _context.SaveChanges();
            

        }



        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

