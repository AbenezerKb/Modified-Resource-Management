using ERP.Models;
using ERP.DTOs;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class WeeklyRequirementRepo : IWeeklyRequirementRepo
    {


        private readonly DataContext _context;

        public WeeklyRequirementRepo(DataContext context)
        {
            _context = context;
        }


        public WeeklyRequirement CreateWeeklyRequirement(WeeklyRequirementCreateDto weeklyRequirementCreateDto)
        {
            if (weeklyRequirementCreateDto == null)
            {
                throw new ArgumentNullException();
            }
            WeeklyRequirement weeklyRequirement = new WeeklyRequirement();
            weeklyRequirement.date = weeklyRequirementCreateDto.date;

            var projectCoordinator = _context.Employees.FirstOrDefault(c => c.EmployeeId == weeklyRequirementCreateDto.projectCoordinator);
            if (projectCoordinator == null)
                throw new ItemNotFoundException($"Grander with Id {weeklyRequirementCreateDto.projectCoordinator} not found");

            weeklyRequirement.projectCoordinator = projectCoordinator;

            var projectManager = _context.Employees.FirstOrDefault(c => c.EmployeeId == weeklyRequirementCreateDto.projectManager);
            if (projectManager == null)
                throw new ItemNotFoundException($"Grander with Id {weeklyRequirementCreateDto.projectManager} not found");



            weeklyRequirement.projectManager = projectManager;
            weeklyRequirement.projectId = weeklyRequirementCreateDto.projectId;
            weeklyRequirement.remark = weeklyRequirementCreateDto.remark;
            weeklyRequirement.specialRequest = weeklyRequirementCreateDto.specialRequest;
            weeklyRequirement.projectManagerId = weeklyRequirementCreateDto.projectManager;
            weeklyRequirement.remark = weeklyRequirementCreateDto.remark;
            weeklyRequirement.specialRequest = weeklyRequirementCreateDto.specialRequest;
            weeklyRequirement.status = weeklyRequirementCreateDto.specialRequest;
            weeklyRequirement.projectCoordinatorId = weeklyRequirementCreateDto.projectCoordinator;
            weeklyRequirement.material = weeklyRequirementCreateDto.material;
            weeklyRequirement.labor = weeklyRequirementCreateDto.labor;
            weeklyRequirement.equipment = weeklyRequirementCreateDto.equipment;
            
            //weeklyRequirement.Id = Guid.NewGuid().ToString();
            /*
                        foreach (WeeklyMaterial s in weeklyRequirement.material)
                        {
                            //s.materialId =// Guid.NewGuid().ToString();
                        }
                        foreach (WeeklyLabor r in weeklyRequirement.labor)
                        {
                          //  r.laborId =// Guid.NewGuid().ToString();
                        }

                        foreach (WeeklyEquipment w in weeklyRequirement.equipment)
                        {
                          //  w.equipmentId =// Guid.NewGuid().ToString();
                        }
            */
            _context.WeeklyRequirements.Add(weeklyRequirement);




            var project = _context.Projects.FirstOrDefault(c => c.Id == weeklyRequirement.Id);

            _context.Notifications.Add(new Notification
            {
                Title = "Weekly requirement requested.",
                Content = $"Weekly requirement request has been sent from {project.Name} project.",
                Type = NOTIFICATIONTYPE.WorkForceAssigned,
                SiteId = project.Site.SiteId,
                // EmployeeId = project.CoordinatorId,
                ActionId = weeklyRequirement.Id,
                Status = 0

            });


            return weeklyRequirement;
        }



        public WeeklyRequirement GetWeeklyRequirements(int id)
        {
            var weeklyRequirement = _context.WeeklyRequirements.FirstOrDefault(c => c.Id == id);
            
            /*
            var material = _context.WeeklyMaterials.ToList();
            var equipment = _context.WeeklyEquipments.ToList();
            var labor = _context.Labors.ToList();



            foreach (WeeklyMaterial scp in material)
            {
                if (scp.WeeklyRequirementFK == weeklyRequirement.Id)
                {
                    weeklyRequirement.material.Add(scp);
                }
            }


            foreach (WeeklyEquipment scp in equipment)
            {
                if (scp.WeeklyRequirementFK == weeklyRequirement.Id)
                {
                    weeklyRequirement.equipment.Add(scp);
                }
            }


            foreach (WeeklyLabor scp in labor)
            {
                if (scp.WeeklyRequirementFK == weeklyRequirement.Id)
                {
                    weeklyRequirement.labor.Add(scp);
                }
            }
            */
            return weeklyRequirement;
        }

        public IEnumerable<WeeklyRequirement> GetAllWeeklyRequirement()
        {
            var weeklyRequirement = _context.WeeklyRequirements.ToList();
           
            /*
            var material = _context.WeeklyMaterials.ToList();
            var equipment = _context.WeeklyEquipments.ToList();
            var labor = _context.Labors.ToList();

            for (int i = 0; i < weeklyRequirement.Count; i++)
            {
                foreach (WeeklyMaterial scp in material)
                {
                    if (scp.WeeklyRequirementFK == weeklyRequirement[i].Id)
                    {
                        weeklyRequirement[i].material.Add(scp);
                    }
                }


                foreach (WeeklyEquipment scp in equipment)
                {
                    if (scp.WeeklyRequirementFK == weeklyRequirement[i].Id)
                    {
                        weeklyRequirement[i].equipment.Add(scp);
                    }
                }


                foreach (WeeklyLabor scp in labor)
                {
                    if (scp.WeeklyRequirementFK == weeklyRequirement[i].Id)
                    {
                        weeklyRequirement[i].labor.Add(scp);
                    }
                }


            }
            */

            return weeklyRequirement;















            //return _context.WeeklyRequirements.ToList();
        }

        public void DeleteWeeklyRequirements(int id)
        {
            var weeklyRequirement = _context.WeeklyRequirements.FirstOrDefault(c => c.Id == id);
            if (weeklyRequirement == null)
                throw new ItemNotFoundException($"WeeklyRequirement not found with WeeklyRequirement Id={id}");
            _context.WeeklyRequirements.Remove(weeklyRequirement);
            _context.SaveChanges();
        }


        public void UpdateWeeklyRequirement(int id, WeeklyRequirementCreateDto weeklyRequirementCreateDto)
        {

            if (weeklyRequirementCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            WeeklyRequirement weeklyRequirement = _context.WeeklyRequirements.FirstOrDefault(c => c.Id == id);
            if (weeklyRequirement == null)
                throw new ItemNotFoundException($"WeeklyRequirement not found with WeeklyRequirement Id={id}");

            /*
            for (int i = 0; i < weeklyRequirements.material.Count; i++)
            {
                for (int j = 0; j < updatedWeeklyRequirement.material.Count; j++)
                {
                    if (updatedWeeklyRequirement.material[j].materialId == weeklyRequirements.material[j].materialId)
                        weeklyRequirements.material[j].amount = updatedWeeklyRequirement.material[j].amount;
                    weeklyRequirements.material[j].budget = updatedWeeklyRequirement.material[j].budget;
                    weeklyRequirements.material[j].unit = updatedWeeklyRequirement.material[j].unit;
                }
            }



            for (int i = 0; i < weeklyRequirements.equipment.Count; i++)
            {
                for (int j = 0; j < updatedWeeklyRequirement.equipment.Count; j++)
                {
                    if (updatedWeeklyRequirement.equipment[j].equipmentId == weeklyRequirements.equipment[j].equipmentId)
                        weeklyRequirements.equipment[j].amount = updatedWeeklyRequirement.equipment[j].amount;
                    weeklyRequirements.equipment[j].budget = updatedWeeklyRequirement.material[j].budget;
                    weeklyRequirements.equipment[j].unit = updatedWeeklyRequirement.equipment[j].unit;

                }
            }


            for (int i = 0; i < weeklyRequirements.labor.Count; i++)
            {
                for (int j = 0; j < updatedWeeklyRequirement.labor.Count; j++)
                {
                    if (updatedWeeklyRequirement.labor[j].laborId == weeklyRequirements.labor[j].laborId)
                        weeklyRequirements.labor[j].number = updatedWeeklyRequirement.labor[j].number;
                    weeklyRequirements.labor[j].budget = updatedWeeklyRequirement.labor[j].budget;
                    weeklyRequirements.labor[j].laborId = updatedWeeklyRequirement.labor[j].laborId;

                }
            }
            */

            weeklyRequirement.date = weeklyRequirementCreateDto.date;

            var projectCoordinator = _context.Employees.FirstOrDefault(c => c.EmployeeId == weeklyRequirementCreateDto.projectCoordinator);
            if (projectCoordinator == null)
                throw new ItemNotFoundException($"Grander with Id {weeklyRequirementCreateDto.projectCoordinator} not found");

            weeklyRequirement.projectCoordinator = projectCoordinator;

            var projectManager = _context.Employees.FirstOrDefault(c => c.EmployeeId == weeklyRequirementCreateDto.projectManager);
            if (projectManager == null)
                throw new ItemNotFoundException($"Grander with Id {weeklyRequirementCreateDto.projectManager} not found");



            weeklyRequirement.projectManager = projectManager;
            weeklyRequirement.projectId = weeklyRequirementCreateDto.projectId;
            weeklyRequirement.remark = weeklyRequirementCreateDto.remark;
            weeklyRequirement.specialRequest = weeklyRequirementCreateDto.specialRequest;
            weeklyRequirement.projectManagerId = weeklyRequirementCreateDto.projectManager;
            weeklyRequirement.remark = weeklyRequirementCreateDto.remark;
            weeklyRequirement.specialRequest = weeklyRequirementCreateDto.specialRequest;
            weeklyRequirement.status = weeklyRequirementCreateDto.specialRequest;
            weeklyRequirement.projectCoordinatorId = weeklyRequirementCreateDto.projectCoordinator;
            weeklyRequirement.material = weeklyRequirementCreateDto.material;
            weeklyRequirement.labor = weeklyRequirementCreateDto.labor;
            weeklyRequirement.equipment = weeklyRequirementCreateDto.equipment;



            _context.WeeklyRequirements.Update(weeklyRequirement);
            _context.SaveChanges();
        }















        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }





    }
}
