using ERP.Models;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class AllocatedResourcesRepo: IAllocatedResourcesRepo
    {
        private readonly AppDbContext _context;


        public AllocatedResourcesRepo(AppDbContext context)
        {
            _context = context;

        }

        public void CreateAllocatedResources(AllocatedResources allocatedResources)
        {
            if (allocatedResources == null)
            {
                throw new ArgumentNullException();
            }         
           // allocatedResources.allocatedResourcesNo =// Guid.NewGuid().ToString();

            _context.AllocatedResources.Add(allocatedResources);
        }

        public IEnumerable<AllocatedResources> GetAllAllocatedResources()
        {
            return _context.AllocatedResources.ToList();
        }


        public AllocatedResources GetAllocatedResources(int id)
        {
            return _context.AllocatedResources.FirstOrDefault(c => c.allocatedResourcesNo == id);
        }


        public void DeleteAllocatedResource(int id)
        {
            var allocatedResources = _context.AllocatedResources.FirstOrDefault(c => c.allocatedResourcesNo == id);
            if (allocatedResources == null){
                throw new ArgumentNullException();

            }
            _context.AllocatedResources.Remove(allocatedResources);
        }

        public void UpdateAllocatedResource(int id,AllocatedResources updatedallocatedResources)
        {

            if (updatedallocatedResources ==  null)
            {
                throw new ArgumentNullException();
            }

            AllocatedResources allocatedResources = _context.AllocatedResources.FirstOrDefault(c => c.allocatedResourcesNo == id);
            if (allocatedResources == null)
                throw new ItemNotFoundException($"Allocated budget not found with allocatedbudgetId={id}");
            allocatedResources.date = updatedallocatedResources.date;
            allocatedResources.itemId = updatedallocatedResources.itemId;
            allocatedResources.projId = updatedallocatedResources.projId;
            allocatedResources.remark = updatedallocatedResources.remark;
            allocatedResources.unit = updatedallocatedResources.unit;
            _context.AllocatedResources.Update(allocatedResources);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
