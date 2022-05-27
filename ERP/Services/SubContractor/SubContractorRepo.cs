using ERP.Models;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class SubContractorRepo: ISubContractorRepo
    {
        private readonly AppDbContext _context;

        public SubContractorRepo(AppDbContext context)
        {
            _context = context;

        }

        public void CreateSubContractor(SubContractor subcontract)
        {
            if (subcontract == null){
                throw new ArgumentNullException();
            }

          
            _context.SubContractors.Add(subcontract);
        }



        public IEnumerable<SubContractor> GetAllSubContractors()
        {
            return _context.SubContractors.ToList();
        }


        public SubContractor GetSubContractor(int id)
        {
            return _context.SubContractors.FirstOrDefault(c => c.SubId == id);
        }
        
        public void DeleteSubContractor(int id)
        {
            var subContractor = _context.SubContractors.FirstOrDefault(c => c.SubId == id);
            if (subContractor == null)
                throw new ItemNotFoundException($"SubContractor not found with SubContractor Id={id}");
            _context.SubContractors.Remove(subContractor);
            _context.SaveChanges();
        }


        public void UpdateSubContractor(int id,SubContractor updatedSubContractor)
        {

            if (updatedSubContractor == null)
            {
                throw new ArgumentNullException();
            }

            SubContractor subContractor = _context.SubContractors.FirstOrDefault(c => c.SubId == id);
            if (subContractor == null)
                throw new ItemNotFoundException($"SubContractor not found with SubContractor Id={id}");

            subContractor.Status = updatedSubContractor.Status;
            subContractor.SubAddress = updatedSubContractor.SubAddress;
            subContractor.SubName = updatedSubContractor.SubName;
            subContractor.SubWorkId = updatedSubContractor.SubWorkId;            


            _context.SubContractors.Update(subContractor);
            _context.SaveChanges();
        }





        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        
    }
}