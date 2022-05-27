using ERP.Models;


namespace ERP.Services
{
    public interface ILaborDetailRepo
    {

        bool SaveChanges();

        IEnumerable<LaborDetail> GetAllLaborDetails();
        LaborDetail GetLaborDetail(int id);
        void CreateLaborDetail(LaborDetail laborDetail);
        void DeleteLaborDetails(int id);
        void UpdateLaborDetail(int id, LaborDetail updatedLaborDetail);
    }
}
