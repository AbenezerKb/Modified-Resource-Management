﻿using ERP.Models;
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


        public void CreateTimeCard(TimeCard timeCard)
        {
            if (timeCard == null)
            {
                throw new ArgumentNullException();
            }
         

            _context.TimeCards.Add(timeCard);
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



        public void UpdateTimeCard(int id,TimeCard updatedTimeCard)
        {

            if (updatedTimeCard  == null)
            {
                throw new ArgumentNullException();
            }

            TimeCard timeCard = _context.TimeCards.FirstOrDefault(c => c.Id == id);
            if (timeCard == null)
                throw new ItemNotFoundException($"TimeCard not found with TimeCard Id={id}");

            timeCard.approvedBy = updatedTimeCard.approvedBy;
            timeCard.dateOfWork = updatedTimeCard.dateOfWork;
            timeCard.employeeName = updatedTimeCard.employeeName;
            timeCard.jobType = updatedTimeCard.jobType;
            timeCard.LaborerID = updatedTimeCard.LaborerID;
            timeCard.NoOfAbscents = updatedTimeCard.NoOfAbscents;
            timeCard.NoOfHrsPerSession = updatedTimeCard.NoOfHrsPerSession;
            timeCard.NoOfPresents = updatedTimeCard.NoOfPresents;
            timeCard.preparedByFK = updatedTimeCard.preparedByFK;
            timeCard.remark = updatedTimeCard.remark;
            timeCard.totalPayment = updatedTimeCard.totalPayment;
            timeCard.totalWorkedHrs = updatedTimeCard.totalWorkedHrs;
            timeCard.wages = updatedTimeCard.wages;
            timeCard.weekNo = updatedTimeCard.weekNo;

            _context.TimeCards.Update(timeCard);
            _context.SaveChanges();
        }




    }
}
