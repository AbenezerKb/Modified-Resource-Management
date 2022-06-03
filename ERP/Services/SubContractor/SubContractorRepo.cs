using ERP.Models;
using ERP.DTOs;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class SubContractorRepo: ISubContractorRepo
    {
        private readonly DataContext _context;

        public SubContractorRepo(DataContext context)
        {
            _context = context;

        }

        public SubContractor CreateSubContractor(SubContractorCreateDto subContractorCreateDto)
        {
            if (subContractorCreateDto == null){
                throw new ArgumentNullException();
            }

            SubContractor subContractor = new SubContractor();
            subContractor.Status = subContractorCreateDto.Status;
            subContractor.subContractorAddress = subContractorCreateDto.subContractorAddress;
            subContractor.subContractorName = subContractorCreateDto.subContractorName;
            subContractor.subContractingWorkId = subContractorCreateDto.subContractingWorkId;


            SubContractingWork subcontractingWork = _context.SubcontractingWorks.FirstOrDefault(c => c.SubcontractingWorkID == subContractorCreateDto.subContractingWorkId);
            if (subcontractingWork == null)
                throw new ItemNotFoundException($"SubcontractingWork not found with Id={subContractorCreateDto.subContractingWorkId}");
            subContractor.subContractingWork = subcontractingWork;            

            _context.SubContractors.Add(subContractor);

            return subContractor;
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


        public void UpdateSubContractor(int id,SubContractorCreateDto subContractorCreateDto)
        {

            if (subContractorCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            SubContractor subContractor = _context.SubContractors.FirstOrDefault(c => c.SubId == id);
            if (subContractor == null)
                throw new ItemNotFoundException($"subContractor not found with Id={subContractorCreateDto.subContractingWorkId}");

            subContractor.Status = subContractorCreateDto.Status;
            subContractor.subContractorAddress = subContractorCreateDto.subContractorAddress;
            subContractor.subContractorName = subContractorCreateDto.subContractorName;
            subContractor.subContractingWorkId = subContractorCreateDto.subContractingWorkId;


            SubContractingWork subcontractingWork = _context.SubcontractingWorks.FirstOrDefault(c => c.SubcontractingWorkID == subContractorCreateDto.subContractingWorkId);
            if (subcontractingWork == null)
                throw new ItemNotFoundException($"SubcontractingWork not found with Id={subContractorCreateDto.subContractingWorkId}");
            subContractor.subContractingWork = subcontractingWork;

            _context.SubContractors.Update(subContractor);
            _context.SaveChanges();
        }





        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        
    }
}
