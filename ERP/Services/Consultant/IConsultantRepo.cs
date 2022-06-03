using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface IConsultantRepo
    {       
            bool SaveChanges();
            IEnumerable<Consultant> GetAllConsultant();
        Consultant GetConsultant(int id);
        Consultant CreateConsultant(ConsultantCreateDto consultant);
            void DeleteConsultant(int id);
            void UpdateConsultant(int id, ConsultantCreateDto updatedConsultant);
        }
    }


