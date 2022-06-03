using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public interface IGranderRepo
    {
        bool SaveChanges();
        public Grander CreateGrander(GranderCreateDto grander);
        public Grander GetGrander(int granderNo);
        public IEnumerable<Grander> GetAllGranders();

        void DeleteGrander(int id);
        void UpdateGrander(int id, GranderCreateDto updatedGrander);        
    }
}
