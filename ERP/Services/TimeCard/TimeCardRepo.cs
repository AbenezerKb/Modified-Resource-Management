using ERP.Models;
using ERP.DTOs;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class TimeCardRepo: ITimeCardRepo
    {
        private readonly DataContext _context;

        public TimeCardRepo(DataContext context)
        {
            _context = context;
        }


        public TimeCard CreateTimeCard(TimeCardCreateDto timeCardCreateDto)
        {
            if (timeCardCreateDto == null)
            {
                throw new ArgumentNullException();
            }
            TimeCard timeCard = new TimeCard();

            var approvedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == timeCardCreateDto.approvedById);
            if (approvedBy == null)
                throw new ItemNotFoundException($"Grander with Id {timeCardCreateDto.approvedById} not found");


            timeCard.approvedBy = approvedBy;
            timeCard.approvedById = timeCardCreateDto.approvedById;


            DailyLabor dailyLabor = _context.DailyLabors.FirstOrDefault(c => c.LaborerID == timeCardCreateDto.LaborerID);
            if (dailyLabor == null)
                throw new ItemNotFoundException($"LaborDetail not found with LaborDetail Id={timeCardCreateDto.LaborerID}");



            timeCard.dailyLabor = dailyLabor;
            timeCard.dateOfWork = timeCardCreateDto.dateOfWork;
            timeCard.jobType = timeCardCreateDto.jobType;
            timeCard.LaborerID = timeCardCreateDto.LaborerID;
            timeCard.NoOfAbscents = timeCardCreateDto.NoOfAbscents;
            timeCard.NoOfHrsPerSession = timeCardCreateDto.NoOfHrsPerSession;
            timeCard.NoOfPresents = timeCardCreateDto.NoOfPresents;
            timeCard.preparedById = timeCardCreateDto.preparedById;
            timeCard.remark = timeCardCreateDto.remark;
            timeCard.totalPayment = timeCardCreateDto.totalPayment;
            timeCard.totalWorkedHrs = timeCardCreateDto.totalWorkedHrs;
            timeCard.wages = timeCardCreateDto.wages;

            var preparedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == timeCardCreateDto.preparedById);
            if (preparedBy == null)
                throw new ItemNotFoundException($"Employee with Id {timeCardCreateDto.preparedById} not found");



            timeCard.preparedBy = preparedBy;


            _context.TimeCards.Add(timeCard);

            return timeCard;
        }



        public TimeCard GetTimeCard(int id)
        {
            return _context.TimeCards.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<TimeCard> GetAllTimeCards()
        {
            return _context.TimeCards.ToList();
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


        public void DeleteTimeCard(int id)
        {
            var timeCard = _context.TimeCards.FirstOrDefault(c => c.Id == id);
            if (timeCard == null)
                throw new ItemNotFoundException($"TimeCard not found with TimeCard Id={id}");
            _context.TimeCards.Remove(timeCard);
            _context.SaveChanges();
        }



        public void UpdateTimeCard(int id, TimeCardCreateDto timeCardCreateDto)
        {

            if (timeCardCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            TimeCard timeCard = _context.TimeCards.FirstOrDefault(c => c.Id == id);
            if (timeCard == null)
                throw new ItemNotFoundException($"TimeCard not found with TimeCard Id={id}");

            var approvedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == timeCardCreateDto.approvedById);
            if (approvedBy == null)
                throw new ItemNotFoundException($"Employee with Id {timeCardCreateDto.approvedById} not found");


            timeCard.approvedBy = approvedBy;
            timeCard.approvedById = timeCardCreateDto.approvedById;


            DailyLabor dailyLabor = _context.DailyLabors.FirstOrDefault(c => c.LaborerID == timeCardCreateDto.LaborerID);
            if (dailyLabor == null)
                throw new ItemNotFoundException($"LaborDetail not found with LaborDetail Id={id}");



            timeCard.dailyLabor = dailyLabor;
            timeCard.dateOfWork = timeCardCreateDto.dateOfWork;
            timeCard.jobType = timeCardCreateDto.jobType;
            timeCard.LaborerID = timeCardCreateDto.LaborerID;
            timeCard.NoOfAbscents = timeCardCreateDto.NoOfAbscents;
            timeCard.NoOfHrsPerSession = timeCardCreateDto.NoOfHrsPerSession;
            timeCard.NoOfPresents = timeCardCreateDto.NoOfPresents;
            timeCard.preparedById = timeCardCreateDto.preparedById;
            timeCard.remark = timeCardCreateDto.remark;
            timeCard.totalPayment = timeCardCreateDto.totalPayment;
            timeCard.totalWorkedHrs = timeCardCreateDto.totalWorkedHrs;
            timeCard.wages = timeCardCreateDto.wages;

            var preparedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == timeCardCreateDto.preparedById);
            if (preparedBy == null)
                throw new ItemNotFoundException($"Grander with Id {timeCardCreateDto.preparedById} not found");



            timeCard.preparedBy = preparedBy;



            _context.TimeCards.Update(timeCard);
            _context.SaveChanges();
        }




    }
}
