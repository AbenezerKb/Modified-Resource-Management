using ERP.Models;
using ERP.DTOs;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class BIDRepo: IBIDRepo
    {
        private readonly DataContext _context;

        public BIDRepo(DataContext context)
        {
            _context = context;

        }

        public BID CreateBID(BIDCreateDto bidCreateDto)
        {
            if (bidCreateDto == null){
                throw new ArgumentNullException();
            }
            BID bid = new BID();
            bid.ActualCost = bidCreateDto.ActualCost;
            bid.ConBID = bidCreateDto.ConBID;
            bid.EstimatedBID = bidCreateDto.EstimatedBID;
            bid.fileName = bidCreateDto.fileName;
            bid.finalDate = bidCreateDto.finalDate;
            bid.initailDate = bidCreateDto.initailDate;
            bid.PenalityDescription = bidCreateDto.PenalityDescription;
            bid.ProjectId = bidCreateDto.ProjectId;


            var project = _context.Projects.FirstOrDefault(c => c.Id == bidCreateDto.ProjectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {bidCreateDto.ProjectId} not found");



            bid.project = project;
            bid.Remark = bidCreateDto.Remark;
            bid.WorkDescription = bidCreateDto.WorkDescription;

            _context.BIDs.Add(bid);
            return bid;
        }



        public IEnumerable<BID> GetAllBIDs()
        {
            return _context.BIDs.ToList();
        }


        public BID GetBID(int id)
        {
            return _context.BIDs.FirstOrDefault(c => c.BIDID == id);
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }



        public void UpdateBID(int id, BIDCreateDto bidCreateDto)
        {
            if (bidCreateDto.Equals(null))
            {
                throw new ArgumentNullException();
            }

            BID bid = _context.BIDs.FirstOrDefault(c => c.BIDID ==id);
            if (bid == null)
                throw new ItemNotFoundException($"Bid not found with bid Id={id}");
            bid.ActualCost = bidCreateDto.ActualCost;
            bid.ConBID = bidCreateDto.ConBID;
            bid.EstimatedBID = bidCreateDto.EstimatedBID;
            bid.fileName = bidCreateDto.fileName;
            bid.finalDate = bidCreateDto.finalDate;
            bid.initailDate = bidCreateDto.initailDate;
            bid.PenalityDescription = bidCreateDto.PenalityDescription;
            bid.ProjectId = bidCreateDto.ProjectId;


            var project = _context.Projects.FirstOrDefault(c => c.Id == bidCreateDto.ProjectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {bidCreateDto.ProjectId} not found");



            bid.project = project;
            bid.Remark = bidCreateDto.Remark;
            bid.WorkDescription = bidCreateDto.WorkDescription;

            _context.BIDs.Update(bid);
            _context.SaveChanges();
        }





        public void DeleteBID(int id)
        {
            var bid = _context.BIDs.FirstOrDefault(c => c.BIDID == id);
           if (bid == null)
                throw new ItemNotFoundException($"Bid not found with bid Id={id}");
            _context.BIDs.Remove(bid);
            _context.SaveChanges();
        }


    }
}