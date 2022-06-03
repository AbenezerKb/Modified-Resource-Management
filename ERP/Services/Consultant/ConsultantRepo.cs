using AutoMapper;
using ERP.Models;
using ERP.Context;
using ERP.Exceptions;
using ERP.DTOs;

namespace ERP.Services
{
    public class ConsultantRepo: IConsultantRepo
    {


        private readonly DataContext _context;

        public ConsultantRepo(DataContext context)
        {
            _context = context;

        }

        public Consultant CreateConsultant(ConsultantCreateDto consultantCreateDto)
        {
            if (consultantCreateDto == null)
            {
                throw new ArgumentNullException();
            }
            Consultant consultant = new Consultant();
            consultant.approvedWorkList = consultantCreateDto.approvedWorkList;
            consultant.changesTaken = consultantCreateDto.changesTaken;
            consultant.subContractorId = consultantCreateDto.subContractorId;

            var contractor = _context.Contracts.FirstOrDefault(c => c.ConId == consultantCreateDto.subContractorId);
            if (contractor == null)
                throw new ItemNotFoundException($"Contract not found with Id={consultantCreateDto.subContractorId}");

            consultant.contractor = contractor;
            consultant.declinedWorkList = consultantCreateDto.declinedWorkList;
            consultant.defectsCorrectionlist = consultantCreateDto.defectsCorrectionlist;
            consultant.defectsSeen = consultantCreateDto.defectsSeen;
            consultant.nextWork = consultantCreateDto.nextWork;
            consultant.projectId = consultantCreateDto.projectId;

            var project = _context.Projects.FirstOrDefault(c => c.Id == consultantCreateDto.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {consultantCreateDto.projectId} not found");
            consultant.project = project;
            consultant.consultantName = consultantCreateDto.consultantName;
            consultant.attachemnt = consultantCreateDto.attachement;
            consultant.reasonForChange = consultantCreateDto.reasonForChange;
            consultant.remarks = consultantCreateDto.remarks;
            consultant.reviewDate = consultantCreateDto.reviewDate;



            /*
            foreach (ApprovedWorkList awl in consultant.approvedWorkList)
            {                
            
                awl.ConsultantId = 0;
                _context.ApprovedWorkLists.Add(awl);
            }

            foreach (DeclinedWorkList dwl in consultant.declinedWorkList)
            {
             
                dwl.ConsultantId = 0;
                _context.DeclinedWorkLists.Add(dwl);
            }

            foreach (DefectsCorrectionlist dcl in consultant.defectsCorrectionlist)
            {
               
                dcl.ConsultantId = 0;
                _context.DefectsCorrectionlists.Add(dcl);
            }

            //consultant. = DateTime.Now.Date;//DateTime.Now.ToString("yyyy-MM-dd");


            for (int i = 0; i < consultant.approvedWorkList.Count; i++)
            {
                if (consultant.approvedWorkList[i].ConsultantId == 0)
                {                 
                    _context.ApprovedWorkLists.First(q => q.ConsultantId == 0).ConsultantId = consultant.approvedWorkList[i].ConsultantId;
                    _context.SaveChanges();
                }


            }





            for (int i = 0; i < consultant.declinedWorkList.Count; i++)
            {
                if (consultant.declinedWorkList[i].ConsultantId == 0)
                {
                    _context.DeclinedWorkLists.First(q => q.ConsultantId == 0).ConsultantId = consultant.declinedWorkList[i].ConsultantId;
                    _context.SaveChanges();
                }


            }



            for (int i = 0; i < consultant.defectsCorrectionlist.Count; i++)
            {
                if (consultant.defectsCorrectionlist[i].ConsultantId == 0)
                {
                    _context.DefectsCorrectionlists.First(q => q.ConsultantId == 0).ConsultantId = consultant.defectsCorrectionlist[i].ConsultantId;
                    _context.SaveChanges();
                }


            }*/


            _context.Consultants.Add(consultant);
            return consultant;
        }



        public IEnumerable<Consultant> GetAllConsultant()
        {

            var consultants = _context.Consultants.ToList();
           
            /*
            var approvedWorkLists = _context.ApprovedWorkLists.ToList();
            var declinedWorkLists = _context.DeclinedWorkLists.ToList();
            var defectsCorrectionlists = _context.DefectsCorrectionlists.ToList();
            
            for (int i = 0; i < consultants.Count; i++)
            {
                foreach (ApprovedWorkList awl in approvedWorkLists)
                {
                    if (consultants[i].consultantId == awl.ConsultantId)
                    {                        
                        consultants[i].approvedWorkList.Add(awl);
                    }
                }


                foreach (DeclinedWorkList dwl in declinedWorkLists)
                {
                    if (consultants[i].consultantId == dwl.ConsultantId)
                    {
                        consultants[i].declinedWorkList.Add(dwl);
                    }
                }

                foreach (DefectsCorrectionlist dcl in defectsCorrectionlists)
                {
                    if (consultants[i].consultantId == dcl.ConsultantId)
                    {
                        consultants[i].defectsCorrectionlist.Add(dcl);
                    }
                }

            }
            */
            return consultants;
        }

        private readonly IMapper _mapper;
        public Consultant GetConsultant(int id)
        {

            
            var consultants = _context.Consultants.FirstOrDefault(c => c.consultantId == id);

            /*
            var approvedWorkLists = _context.ApprovedWorkLists.ToList();
            var declinedWorkLists = _context.DeclinedWorkLists.ToList();
            var defectsCorrectionlists = _context.DefectsCorrectionlists.ToList();
            var project = _context.Projects.FirstOrDefault(c => c.Id == consultants.projectId);

            foreach (ApprovedWorkList awl in approvedWorkLists)
            {
                if (consultants.consultantId == awl.ConsultantId)
                {
                    consultants.approvedWorkList.Add(awl);
                }
            }


            foreach (DeclinedWorkList dwl in declinedWorkLists)
            {
                if (consultants.consultantId == dwl.ConsultantId)
                {
                    consultants.declinedWorkList.Add(dwl);
                }
            }

            foreach (DefectsCorrectionlist dcl in defectsCorrectionlists)
            {
                if (consultants.consultantId == dcl.ConsultantId)
                {
                    consultants.defectsCorrectionlist.Add(dcl);
                }
            }


           
            ConsultantWithProjectCreateDto consultantsWtihProject = new ConsultantWithProjectCreateDto();
            ConsultantCreateDto consultant = _mapper.Map<ConsultantCreateDto>(consultants);
            consultantsWtihProject.nextWork = consultant.nextWork;
            
            consultantsWtihProject.approvedWorkList = consultant.approvedWorkList;
            
              
            


            consultantsWtihProject.changesTaken = consultant.changesTaken;            
            consultantsWtihProject.consultantName = consultant.consultantName;
            consultantsWtihProject.contractorId = consultant.contractorId;
            consultantsWtihProject.declinedWorkList = consultant.declinedWorkList;
            consultantsWtihProject.defectsCorrectionlist = consultant.defectsCorrectionlist;
            consultantsWtihProject.defectsSeen = consultant.defectsSeen;
            consultantsWtihProject.reasonForChange = consultant.reasonForChange;
            consultantsWtihProject.remarks = consultant.remarks;
            consultantsWtihProject.reviewDate = consultant.reviewDate;
            consultantsWtihProject.project = project;
            */
            if (consultants == null)
                throw new ItemNotFoundException($"Consultant not found with Consultant Id={id}");

            return consultants;
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteConsultant(int id)
        {
            var consultant = _context.Consultants.FirstOrDefault(c => c.consultantId == id);
            if (consultant == null)
                throw new ItemNotFoundException($"Consultant not found with Consultant Id={id}");
            _context.Consultants.Remove(consultant);
            _context.SaveChanges();
        }




        public void UpdateConsultant(int id, ConsultantCreateDto updatedConsultant)
        {
            if (updatedConsultant == null)
            {
                throw new ArgumentNullException();
            }

            Consultant consultant = _context.Consultants.FirstOrDefault(c => c.consultantId == id);
            if (consultant == null)
                throw new ItemNotFoundException($"Consultant not found with Consultant Id={id}");          

            consultant.approvedWorkList = updatedConsultant.approvedWorkList;
            consultant.changesTaken = updatedConsultant.changesTaken;
            consultant.subContractorId = updatedConsultant.subContractorId;
           
            var contractor = _context.Contracts.FirstOrDefault(c => c.ConId == updatedConsultant.subContractorId);
            if (contractor == null)
                throw new ItemNotFoundException($"Contract not found with Id={updatedConsultant.subContractorId}");
            
            consultant.contractor = contractor;
            consultant.declinedWorkList = updatedConsultant.declinedWorkList;
            consultant.defectsCorrectionlist = updatedConsultant.defectsCorrectionlist;
            consultant.defectsSeen = updatedConsultant.defectsSeen;
            consultant.nextWork = updatedConsultant.nextWork;
            consultant.projectId = updatedConsultant.projectId;

            var project = _context.Projects.FirstOrDefault(c => c.Id == updatedConsultant.projectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {updatedConsultant.projectId} not found");
            consultant.project = project;
            consultant.consultantName = updatedConsultant.consultantName;
            consultant.attachemnt = updatedConsultant.attachement;
            consultant.reasonForChange = updatedConsultant.reasonForChange;
            consultant.remarks = updatedConsultant.remarks;
            consultant.reviewDate = updatedConsultant.reviewDate;

            _context.Consultants.Update(consultant);
            _context.SaveChanges();
        }






    }
}
