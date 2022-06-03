using ERP.Context;
using ERP.Exceptions;
using ERP.Models;
using ERP.DTOs;

namespace ERP.Services
{
    public class SubContractWorkRepo : ISubContractWorkRepo
    {
        private readonly DataContext _context;

        public SubContractWorkRepo(DataContext context)
        {
            _context = context;

        }

        public SubContractWork CreateSubContractWork( SubContractWorkCreateDto subContractWorkCreateDto)
        {
            if (subContractWorkCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            SubContractWork subContractWork = new SubContractWork();
            subContractWork.remarks = subContractWorkCreateDto.remarks;
            subContractWork.workName = subContractWorkCreateDto.workName;            

            _context.SubContractWorks.Add(subContractWork);
            _context.SaveChanges();
            return subContractWork;
        }


        public IEnumerable<SubContractWork> GetAllSubContractWorks()
        {
            return _context.SubContractWorks.ToList();
        }


        public SubContractWork GetSubContractWork(int id)
        {
            return _context.SubContractWorks.FirstOrDefault(c => c.subconractingid == id);
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }




        public void UpdateSubContractWork(int id,SubContractWorkCreateDto subContractWorkCreateDto)
        {

            if (subContractWorkCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            SubContractWork subContractWork = _context.SubContractWorks.FirstOrDefault(c => c.subconractingid == id);
            if (subContractWork == null)
                throw new ItemNotFoundException($"SubContractWork not found with Id={id}");


            subContractWork.remarks = subContractWorkCreateDto.remarks;
            subContractWork.workName = subContractWorkCreateDto.workName;            

            _context.SubContractWorks.Update(subContractWork);
            _context.SaveChanges();
        }





        public void DeleteSubContractWorks(int id)
        {
            var subContractWork = _context.SubContractWorks.FirstOrDefault(c => c.subconractingid == id);
            if (subContractWork == null)
                throw new ItemNotFoundException($"SubContractWork not found with SubContractWork Id={id}");
            _context.SubContractWorks.Remove(subContractWork);
            _context.SaveChanges();
        }


    }
}
