using ERP.Models;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class LaborDetailRepo: ILaborDetailRepo
    {


        private readonly AppDbContext _context;

        public LaborDetailRepo(AppDbContext context)
        {
            _context = context;
        }


        public void CreateLaborDetail(LaborDetail laborDetail)
        {
            if (laborDetail == null)
            {
                throw new ArgumentNullException();
            }

            //laborDetail.Id = Guid.NewGuid().ToString();           

            _context.LaborDetails.Add(laborDetail);
        }



        public LaborDetail GetLaborDetail(int id)
        {
            return _context.LaborDetails.FirstOrDefault(c => c.id == id);
        }

        public IEnumerable<LaborDetail> GetAllLaborDetails()
        {
            return _context.LaborDetails.ToList();
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteLaborDetails(int id)
        {
            var laborDetail = _context.LaborDetails.FirstOrDefault(c => c.id == id);
            _context.LaborDetails.Remove(laborDetail);
        }



        public void UpdateLaborDetail(int id,LaborDetail updatedLaborDetail)
        {

            if (updatedLaborDetail == null)
            {
                throw new ArgumentNullException();
            }

            LaborDetail laborDetail = _context.LaborDetails.FirstOrDefault(c => c.id == id);
            if (laborDetail == null)
                throw new ItemNotFoundException($"LaborDetail not found with LaborDetail Id={id}");
            laborDetail.afternoonSession = updatedLaborDetail.afternoonSession;
            laborDetail.dateOfWork = updatedLaborDetail.dateOfWork;
            laborDetail.dateType = updatedLaborDetail.dateType;
            laborDetail.eveningSession = updatedLaborDetail.eveningSession;
            laborDetail.morningSession = updatedLaborDetail.morningSession;
            laborDetail.NoOfHrsPerSession = updatedLaborDetail.NoOfHrsPerSession;
            laborDetail.weekNo = updatedLaborDetail.weekNo;

            _context.LaborDetails.Update(laborDetail);
            _context.SaveChanges();
        }




    }
}
