using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface ILaborDetailRepo
    {

        bool SaveChanges();

        IEnumerable<LaborDetail> GetAllLaborDetails();
        LaborDetail GetLaborDetail(int id);
        LaborDetail CreateLaborDetail(LaborDetailCreateDto laborDetail);
        void DeleteLaborDetails(int id);
        void UpdateLaborDetail(int id, LaborDetailCreateDto updatedLaborDetail);
    }
}
