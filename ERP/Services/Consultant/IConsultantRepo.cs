using ERP.Models;


namespace ERP.Services
{
    public interface IConsultantRepo
    {       
            bool SaveChanges();
            IEnumerable<Consultant> GetAllConsultant();
            Consultant GetConsultant(int id);
            void CreateConsultant(Consultant consultant);
            void DeleteConsultant(int id);
            void UpdateConsultant(int id,Consultant updatedConsultant);
        }
    }

