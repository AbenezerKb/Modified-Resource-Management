using ERP.Models;
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

        public void CreateBID(BID bid)
        {
            if (bid == null){
                throw new ArgumentNullException();
            }
          

            _context.BIDs.Add(bid);
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



        public void UpdateBID(int id,BID updateBID)
        {
            if (updateBID.Equals(null))
            {
                throw new ArgumentNullException();
            }

            BID bid = _context.BIDs.FirstOrDefault(c => c.BIDID ==id);
            if (bid == null)
                throw new ItemNotFoundException($"Bid not found with bid Id={id}");
            bid.ActualCost = updateBID.ActualCost;
            bid.ConBID = updateBID.ConBID;
            bid.EstimatedBID = updateBID.EstimatedBID;
            bid.fileName = updateBID.fileName;
            bid.finalDate = updateBID.finalDate;
            bid.initailDate = updateBID.initailDate;
            bid.ProjectId = updateBID.ProjectId;
            bid.Remark = updateBID.Remark;
            bid.WorkDescription = updateBID.WorkDescription;

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