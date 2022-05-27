﻿using ERP.Context;
using ERP.Exceptions;
using ERP.Models;

namespace ERP.Services
{
    public class SubContractWorkRepo : ISubContractWorkRepo
    {
        private readonly DataContext _context;

        public SubContractWorkRepo(DataContext context)
        {
            _context = context;

        }

        public void CreateSubContractWork(SubContractWork subContractWork)
        {
            if (subContractWork == null)
            {
                throw new ArgumentNullException();
            }

            //subContractWork.subconractingid = Guid.NewGuid().ToString();
            _context.SubContractWorks.Add(subContractWork);
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




        public void UpdateSubContractWork(int id,SubContractWork updatedSubContractWork)
        {

            if (updatedSubContractWork == null)
            {
                throw new ArgumentNullException();
            }

            SubContractWork subContractWork = _context.SubContractWorks.FirstOrDefault(c => c.subconractingid == id);
            if (subContractWork == null)
                throw new ItemNotFoundException($"SubContractWork not found with SubContractWork Id={id}");


            subContractWork.remarks = updatedSubContractWork.remarks;
            subContractWork.workName = updatedSubContractWork.workName;            

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
