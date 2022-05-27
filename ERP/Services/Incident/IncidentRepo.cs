using ERP.Context;
using ERP.Exceptions;
using ERP.Models;

namespace ERP.Services
{
    public class IncidentRepo:IIncidentRepo
    {
        private readonly DataContext _context;
        public IncidentRepo(DataContext context)
        {
            _context = context;

        }

        public void CreateIncident(Incident incident )
        {
            if (incident == null)
            {
                throw new ArgumentNullException();
            }
         

            _context.Incidents.Add(incident);

        }

        public Incident GetIncident(int incidentNo)
        {
            return _context.Incidents.FirstOrDefault(c => c.incidentNo == incidentNo);

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
            _context.Incidents.Remove(incident);
        }



        public void UpdateIncident(int id,Incident updatedIncident)
        {

            if (updatedIncident == null)
            {
                throw new ArgumentNullException();
            }

            Incident incident = _context.Incidents.FirstOrDefault(c => c.incidentNo == id);
            if (incident==null)
                throw new ItemNotFoundException($"Incident not found with Incident Id={id}");

            incident.date = updatedIncident.date;
            incident.Description = updatedIncident.Description;
            incident.empName = updatedIncident.empName;
            incident.incidentName = updatedIncident.incidentName;
            incident.proID = updatedIncident.proID;
            _context.Incidents.Update(incident);
            _context.SaveChanges();
        }




    }
}
