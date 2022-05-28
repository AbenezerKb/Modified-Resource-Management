using ERP.Context;
using ERP.Exceptions;
using ERP.Models;

namespace ERP.Services
{
    public class DailyLaborRepo: IDailyLaborRepo
    {

        private readonly DataContext _context;
        
        public DailyLaborRepo(DataContext context)
        {
            _context = context;            
        }

        public void CreateDailyLabor(DailyLabor dailyLabor)
        {
            if (dailyLabor == null)
            {
                throw new ArgumentNullException();
            }
         
            _context.DailyLabors.Add(dailyLabor);

            var project = _context.Projects.FirstOrDefault(c => c.Id == dailyLabor.projectId);
            var dailyProject = _context.LaborDetails.FirstOrDefault(c => c.id == dailyLabor.LaborerID);            

            _context.Notifications.Add(new Notification
            {
                Title = "New Labor is registered.",
                Content = $"New {dailyLabor.jobTitle} is registered. ",
                Type = NOTIFICATIONTYPE.LaborRegistered,
                SiteId = project.Site.SiteId,
                EmployeeId = dailyLabor.apprvedBy,    
                ActionId = dailyProject.id,
                Status = 0

            });

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



        public void UpdateDailyLabor(int id,DailyLabor updatedDailyLabor)
        {

            if (updatedDailyLabor == null)
            {
                throw new ArgumentNullException();
            }

            DailyLabor dailyLabor = _context.DailyLabors.FirstOrDefault(c => c.LaborerID == id);
            if (dailyLabor == null)
                throw new ItemNotFoundException($"DailyLabor not found with DailyLabor Id={id}");

            dailyLabor.date = updatedDailyLabor.date;
            dailyLabor.fullName = updatedDailyLabor.fullName;
            dailyLabor.jobTitle = updatedDailyLabor.jobTitle;
            dailyLabor.name = updatedDailyLabor.name;
            dailyLabor.remarks = updatedDailyLabor.remarks;
            dailyLabor.wagePerhour = updatedDailyLabor.wagePerhour;
            
            _context.DailyLabors.Update(dailyLabor);
            _context.SaveChanges();
        }



    }
}
