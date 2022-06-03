using ERP.Context;
using ERP.Exceptions;
using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public class DailyLaborRepo: IDailyLaborRepo
    {

        private readonly DataContext _context;
        
        public DailyLaborRepo(DataContext context)
        {
            _context = context;            
        }

        public DailyLabor CreateDailyLabor(DailyLaborCreateDto dailyLaborCreateDto)
        {
            if (dailyLaborCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            DailyLabor dailylabor = new DailyLabor();

            var project = _context.Projects.FirstOrDefault(c => c.Id == dailyLaborCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {dailyLaborCreateDto.projectId} not found");

            //_context.LaborDetails.FirstOrDefault(c => c.id == dailyLabor.LaborerID);

            dailylabor.approvedById = dailyLaborCreateDto.approvedById;

            var approvedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == dailyLaborCreateDto.approvedById);
            if (approvedBy == null)
                throw new ItemNotFoundException($"DailyLabor with Id {dailyLaborCreateDto.approvedById} not found");



            dailylabor.approvedBy = approvedBy;
            dailylabor.date = dailyLaborCreateDto.date;
            dailylabor.fullName = dailyLaborCreateDto.fullName;
            dailylabor.jobTitle = dailyLaborCreateDto.jobTitle;
            dailylabor.name = dailyLaborCreateDto.name;
            dailylabor.project = project;
            dailylabor.projectId = dailyLaborCreateDto.projectId;
            dailylabor.remarks = dailyLaborCreateDto.remarks;
            dailylabor.status = dailyLaborCreateDto.remarks;
            dailylabor.wagePerhour = dailyLaborCreateDto.wagePerhour;
            _context.DailyLabors.Add(dailylabor);

            _context.Notifications.Add(new Notification
            {
                Title = "New Labor is registered.",
                Content = $"New {dailylabor.jobTitle} is registered. ",
                Type = NOTIFICATIONTYPE.LaborRegistered,
                SiteId = project.Site.SiteId,
                EmployeeId = dailylabor.approvedById,    
                ActionId = dailylabor.LaborerID,
                Status = 0

            });

            return dailylabor;

        }

        public DailyLabor GetDailyLabor(int dailyLaborId)
        {
            return _context.DailyLabors.FirstOrDefault(c => c.LaborerID == dailyLaborId);

        }

        public IEnumerable<DailyLabor> GetAllDailyLabors()
        {
            return _context.DailyLabors.ToList();

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


        public void DeleteDailyLabor(int id)
        {
            var dailyLabor = _context.DailyLabors.FirstOrDefault(c => c.LaborerID == id);
            if (dailyLabor == null)
                throw new ItemNotFoundException($"DailyLabor not found with DailyLabor Id={id}");
            _context.DailyLabors.Remove(dailyLabor);
            _context.SaveChanges();
        }



        public void UpdateDailyLabor(int id, DailyLaborCreateDto dailyLaborCreateDto)
        {

            if (dailyLaborCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            DailyLabor dailylabor = _context.DailyLabors.FirstOrDefault(c => c.LaborerID == id);
            if (dailylabor == null)
                throw new ItemNotFoundException($"DailyLabor not found with DailyLabor Id={id}");

            var project = _context.Projects.FirstOrDefault(c => c.Id == dailyLaborCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {dailyLaborCreateDto.projectId} not found");

            //_context.LaborDetails.FirstOrDefault(c => c.id == dailyLabor.LaborerID);

            dailylabor.approvedById = dailyLaborCreateDto.approvedById;

            var approvedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == dailyLaborCreateDto.approvedById);
            if (approvedBy == null)
                throw new ItemNotFoundException($"DailyLabor with Id {dailyLaborCreateDto.approvedById} not found");



            dailylabor.approvedBy = approvedBy;
            dailylabor.date = dailyLaborCreateDto.date;
            dailylabor.fullName = dailyLaborCreateDto.fullName;
            dailylabor.jobTitle = dailyLaborCreateDto.jobTitle;
            dailylabor.name = dailyLaborCreateDto.name;
            dailylabor.project = project;
            dailylabor.projectId = dailyLaborCreateDto.projectId;
            dailylabor.remarks = dailyLaborCreateDto.remarks;
            dailylabor.status = dailyLaborCreateDto.remarks;
            dailylabor.wagePerhour = dailyLaborCreateDto.wagePerhour;

            _context.DailyLabors.Update(dailylabor);
            _context.SaveChanges();

            _context.Notifications.Add(new Notification
            {
                Title = "New Labor is registered.",
                Content = $"New {dailylabor.jobTitle} is registered. ",
                Type = NOTIFICATIONTYPE.LaborRegistered,
                SiteId = project.Site.SiteId,
                EmployeeId = dailylabor.approvedById,
                ActionId = dailylabor.LaborerID,
                Status = 0

            });

        }



    }
}
