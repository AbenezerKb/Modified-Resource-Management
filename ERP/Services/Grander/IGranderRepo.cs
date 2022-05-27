using ERP.Models;

namespace ERP.Services
{
    public interface IGranderRepo
    {
        bool SaveChanges();
        public void CreateGrander(Grander grander);
        public Grander GetGrander(int granderNo);
        public IEnumerable<Grander> GetAllGranders();

        void DeleteGrander(int id);
        void UpdateGrander(int id,Grander updatedGrander);        
    }
}
