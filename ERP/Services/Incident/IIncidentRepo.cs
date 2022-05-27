using ERP.Models;
namespace ERP.Services
{
    public interface IIncidentRepo
    {
        bool SaveChanges();
        public void CreateIncident(Incident incident);
        public Incident GetIncident(int incidentNo);
        public IEnumerable<Incident> GetAllIncidents();
        void DeleteIncident(int id);
        void UpdateIncident(int id,Incident updatedIncident);
    }
}
