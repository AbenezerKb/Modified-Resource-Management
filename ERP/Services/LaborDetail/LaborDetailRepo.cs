using ERP.Models;
using ERP.Context;
using ERP.Exceptions;
using ERP.DTOs;

namespace ERP.Services
{
    public class LaborDetailRepo: ILaborDetailRepo
    {


        private readonly DataContext _context;

        public LaborDetailRepo(DataContext context)
        {
            _context = context;
        }


        public LaborDetail CreateLaborDetail(LaborDetailCreateDto laborDetailCreateDto)
        {
            if (laborDetailCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            //laborDetail.Id = Guid.NewGuid().ToString();           

            LaborDetail laborDetail = new LaborDetail();
            laborDetail.afternoonSession = laborDetailCreateDto.afternoonSession;
            laborDetail.dateOfWork = laborDetailCreateDto.dateOfWork;
            laborDetail.dateType = laborDetailCreateDto.dateType;
            laborDetail.eveningSession = laborDetailCreateDto.eveningSession;
            laborDetail.morningSession = laborDetailCreateDto.morningSession;
            laborDetail.NoOfHrsPerSession = laborDetailCreateDto.NoOfHrsPerSession;
            laborDetail.PaymentDayIn = laborDetailCreateDto.PaymentDayIn;

            laborDetail.LaborerID = laborDetailCreateDto.LaborerID;



            _context.LaborDetails.Add(laborDetail);
            return laborDetail
                ;
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



        public void UpdateLaborDetail(int id, LaborDetailCreateDto laborDetailCreateDto)
        {

            if (laborDetailCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            LaborDetail laborDetail = _context.LaborDetails.FirstOrDefault(c => c.id == id);
            if (laborDetail == null)
                throw new ItemNotFoundException($"LaborDetail not found with LaborDetail Id={id}");
            laborDetail.afternoonSession = laborDetailCreateDto.afternoonSession;
            laborDetail.dateOfWork = laborDetailCreateDto.dateOfWork;
            laborDetail.dateType = laborDetailCreateDto.dateType;
            laborDetail.eveningSession = laborDetailCreateDto.eveningSession;
            laborDetail.morningSession = laborDetailCreateDto.morningSession;
            laborDetail.NoOfHrsPerSession = laborDetailCreateDto.NoOfHrsPerSession;            
            laborDetail.PaymentDayIn = laborDetailCreateDto.PaymentDayIn;

            laborDetail.LaborerID = laborDetailCreateDto.LaborerID;

            DailyLabor dailyLabor = _context.DailyLabors.FirstOrDefault(c => c.LaborerID == laborDetailCreateDto.LaborerID);
            if (dailyLabor == null)
                throw new ItemNotFoundException($"LaborDetail not found with LaborDetail Id={id}");



            laborDetail.dailyLabor = dailyLabor;            
            _context.LaborDetails.Update(laborDetail);
            _context.SaveChanges();
        }




    }
}
