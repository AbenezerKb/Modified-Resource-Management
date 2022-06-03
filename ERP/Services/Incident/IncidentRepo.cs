using ERP.Context;
using ERP.Exceptions;
using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public class IncidentRepo:IIncidentRepo
    {
        private readonly DataContext _context;
        public IncidentRepo(DataContext context)
        {
            _context = context;

        }

        public Incident CreateIncident(IncidentCreateDto incidentCreateDto)
        {
            if (incidentCreateDto == null)
            {
                throw new ArgumentNullException();
            }
            var project = _context.Projects.FirstOrDefault(c => c.Id == incidentCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {incidentCreateDto.projectId} not found");
            //var site = _context.LaborDetails.FirstOrDefault(c => c.id == dailyLabor.LaborerID);


            Incident incident = new Incident();
            incident.date = incidentCreateDto.date;
            incident.Description = incidentCreateDto.Description;
            incident.EmployeeId = incidentCreateDto.EmployeeId;

            Employee employee = _context.Employees.FirstOrDefault(c => c.EmployeeId == incidentCreateDto.EmployeeId);
            if (employee == null)
                throw new ItemNotFoundException($"Employee with Id {incidentCreateDto.EmployeeId} not found");

            incident.employee = employee;
            incident.incidentName = incidentCreateDto.incidentName;
            incident.projectID = incidentCreateDto.projectId;
            incident.project = project;
            _context.Incidents.Add(incident);
            _context.Notifications.Add(new Notification
            {
                Title = "New incident has occurd.",
                Content = $"New incident,{incident.incidentName}, has occurd. ",
                Type = NOTIFICATIONTYPE.IncidentOccured,
                SiteId = project.Site.SiteId,
                EmployeeId = incident.EmployeeId,
                ActionId = incident.incidentNo,
                Status = 0

            });

            return incident;

        }

        public Incident GetIncident(int incidentNo)
        {
            // return _context.Incidents.FirstOrDefault(c => c.incidentNo == incidentNo);

            Incident incident = _context.Incidents.FirstOrDefault(c => c.incidentNo == incidentNo);
            if (incident == null)
                throw new ItemNotFoundException($"Incident not found with Incident Id={incidentNo}");
            return incident;

        }

        public IEnumerable<Incident> GetAllIncidents()
        {
            return _context.Incidents.ToList();

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


        public void DeleteIncident(int id)
        {
            var incident = _context.Incidents.FirstOrDefault(c => c.incidentNo == id);
            if (incident == null)
                throw new ItemNotFoundException($"Incident not found with Incident Id={id}");
            _context.Incidents.Remove(incident);
        }



        public void UpdateIncident(int id, IncidentCreateDto incidentCreateDto)
        {

            if (incidentCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            Incident incident = _context.Incidents.FirstOrDefault(c => c.incidentNo == id);
            if (incident==null)
                throw new ItemNotFoundException($"Incident not found with Incident Id={id}");

            var project = _context.Projects.FirstOrDefault(c => c.Id == incidentCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {incidentCreateDto.projectId} not found");


            incident.date = incidentCreateDto.date;
            incident.Description = incidentCreateDto.Description;
            incident.EmployeeId = incidentCreateDto.EmployeeId;

            Employee employee = _context.Employees.FirstOrDefault(c => c.EmployeeId == incidentCreateDto.EmployeeId);
            if (employee == null)
                throw new ItemNotFoundException($"Employee with Id {incidentCreateDto.EmployeeId} not found");

            incident.employee = employee;
            incident.incidentName = incidentCreateDto.incidentName;
            incident.projectID = incidentCreateDto.projectId;
            incident.project = project;
            _context.Incidents.Update(incident);
            _context.SaveChanges();
        }




    }
}

