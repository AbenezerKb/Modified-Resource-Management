using ERP.Models;
using ERP.DTOs;
namespace ERP.Services
{
    public interface IIncidentRepo
    {
        bool SaveChanges();
        public Incident CreateIncident(IncidentCreateDto incident);
        public Incident GetIncident(int incidentNo);
        public IEnumerable<Incident> GetAllIncidents();
        void DeleteIncident(int id);
        void UpdateIncident(int id, IncidentCreateDto updatedIncident);
      //  void UpdateIncident(int id, IncidentCreateDto updatedIncident);
    }
}
