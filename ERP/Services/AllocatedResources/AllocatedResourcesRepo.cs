using ERP.Models;
using ERP.DTOs;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class AllocatedResourcesRepo: IAllocatedResourcesRepo
    {
        private readonly DataContext _context;


        public AllocatedResourcesRepo(DataContext context)
        {
            _context = context;

        }

        public AllocatedResources CreateAllocatedResources(AllocatedResourcesCreateDto allocatedResourcesCreateDto)
        {
            if (allocatedResourcesCreateDto == null)
            {
                throw new ArgumentNullException();
            }
            AllocatedResources allocatedResources = new AllocatedResources();
            allocatedResources.date = allocatedResourcesCreateDto.date;
            allocatedResources.itemId = allocatedResourcesCreateDto.itemId;

            Item item = _context.Items.FirstOrDefault(c => c.ItemId == allocatedResourcesCreateDto.itemId);
            if (item == null)
                throw new ItemNotFoundException($"Allocated budget not found with allocatedbudgetId={ allocatedResourcesCreateDto.itemId}");

            allocatedResources.item = item;
            allocatedResources.projectId = allocatedResourcesCreateDto.projectId;

            Project project = _context.Projects.FirstOrDefault(c => c.Id == allocatedResourcesCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project not found with Id={ allocatedResourcesCreateDto.projectId}");

            allocatedResources.project = project;
            allocatedResources.remark = allocatedResourcesCreateDto.remark;
            allocatedResources.unit = allocatedResources.unit;
            
            _context.AllocatedResources.Add(allocatedResources);
            return allocatedResources;
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

        public void UpdateAllocatedResource(int id, AllocatedResourcesCreateDto allocatedResourcesCreateDto)
        {

            if (allocatedResourcesCreateDto ==  null)
            {
                throw new ArgumentNullException();
            }

            AllocatedResources allocatedResources = _context.AllocatedResources.FirstOrDefault(c => c.allocatedResourcesNo == id);
            allocatedResources.date = allocatedResourcesCreateDto.date;
            allocatedResources.itemId = allocatedResourcesCreateDto.itemId;

            Item item = _context.Items.FirstOrDefault(c => c.ItemId == allocatedResourcesCreateDto.itemId);
            if (item == null)
                throw new ItemNotFoundException($"Allocated budget not found with allocatedbudgetId={ allocatedResourcesCreateDto.itemId}");

            allocatedResources.item = item;
            allocatedResources.projectId = allocatedResourcesCreateDto.projectId;

            Project project = _context.Projects.FirstOrDefault(c => c.Id == allocatedResourcesCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project not found with Id={ allocatedResourcesCreateDto.projectId}");

            allocatedResources.project = project;
            allocatedResources.remark = allocatedResourcesCreateDto.remark;
            allocatedResources.unit = allocatedResources.unit;

            _context.AllocatedResources.Update(allocatedResources);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
