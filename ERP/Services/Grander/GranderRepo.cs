using ERP.Models;
using ERP.DTOs;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class GranderRepo: IGranderRepo
    {


        private readonly DataContext _context;
        public GranderRepo(DataContext context)
        {
            _context = context;

        }

        public Grander CreateGrander(GranderCreateDto granderCreateDto)
        {
            if (granderCreateDto == null)
            {
                throw new ArgumentNullException();
            }
            Grander grander = new Grander();
            var approvedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == granderCreateDto.ApprovedById);
            if (approvedBy == null)
                throw new ItemNotFoundException($"Grander with Id {granderCreateDto.ApprovedById} not found");

            grander.approvedBy = approvedBy;
            grander.approvedById = granderCreateDto.ApprovedById;
            grander.Date = granderCreateDto.Date;
            grander.Duration = granderCreateDto.Duration;
            grander.ProjectId = granderCreateDto.ProjectId;

            var project = _context.Projects.FirstOrDefault(c => c.Id == granderCreateDto.ProjectId);
            if (project == null)
                throw new ItemNotFoundException($"Project with Id {granderCreateDto.ProjectId} not found");

            grander.project = project;
            grander.RequestNo = granderCreateDto.RequestNo;
            grander.ResourcePlans = granderCreateDto.ResourcePlans;
            grander.SubcontractingPlans = granderCreateDto.SubcontractingPlans;
            grander.TotalEstiamtedReqtBudget = granderCreateDto.TotalEstiamtedReqtBudget;
            grander.WorkForcePlans = granderCreateDto.WorkForcePlans;
            _context.Granders.Add(grander);
            _context.SaveChanges();
            return grander;
            //grander.GranderId = Guid.NewGuid().ToString();

            /*
            foreach (SubcontractingPlan scp in grander.SubcontractingPlans)
            {
          
                scp.GranderFK = 0;
                _context.SubcontractingPlans.Add(scp);
            }


            foreach (ResourcePlan rp in grander.ResourcePlans)
            {
          
                rp.GranderFK = 0;
                _context.ResourcePlans.Add(rp);
            }


            foreach (WorkForcePlan wfp in grander.WorkForcePlans)
            {
          
                wfp.GranderFK = 0;
                _context.WorkForcePlans.Add(wfp);
            }         

            _context.Granders.Add(grander);
            _context.SaveChanges();
            //  Grander granders = _context.Granders.ToList();


            for (int i = 0; i < grander.ResourcePlans.Count; i++)
            {
                if (grander.ResourcePlans[i].GranderFK == 0)
                {
                    grander.ResourcePlans[i].GranderFK = grander.GranderId;
                    _context.ResourcePlans.First(q => q.GranderFK == 0).GranderFK = grander.ResourcePlans[i].GranderFK;
                    _context.SaveChanges();
                }

                    
            }


            for (int i = 0; i < grander.SubcontractingPlans.Count; i++)
            {
                if (grander.SubcontractingPlans[i].GranderFK == 0)
                {
                    grander.SubcontractingPlans[i].GranderFK = grander.GranderId;
                    _context.SubcontractingPlans.First(q => q.GranderFK == 0).GranderFK = grander.SubcontractingPlans[i].GranderFK;
                    _context.SaveChanges();
                }


            }

            for (int i = 0; i < grander.WorkForcePlans.Count; i++)
            {
                if (grander.WorkForcePlans[i].GranderFK == 0)
                {
                    grander.WorkForcePlans[i].GranderFK = grander.GranderId;
                    _context.WorkForcePlans.First(q => q.GranderFK==0).GranderFK=grander.WorkForcePlans[i].GranderFK;
                    _context.SaveChanges();
                }


            }
            */



        }

        public Grander GetGrander(int granderId)
        {
            var grander =_context.Granders.FirstOrDefault(c => c.GranderId == granderId);
            if (grander == null)
                throw new ItemNotFoundException($"Grander not found with Grander Id={granderId}");

            //var subcontractingPlans = _context.SubcontractingPlans.ToList();
            //var resourcePlans = _context.ResourcePlans.ToList();
            //var workForcePlan = _context.WorkForcePlans.ToList();



            /*
            foreach (SubcontractingPlan scp in subcontractingPlans)
            {
                if (scp.GranderFK == grander.GranderId)
                {
                    grander.SubcontractingPlans.Add(scp);
                }
            }


            foreach (ResourcePlan scp in resourcePlans)
            {
                if (scp.GranderFK == grander.GranderId)
                {
                    grander.ResourcePlans.Add(scp);
                }
            }


            foreach (WorkForcePlan scp in workForcePlan)
            {
                if (scp.GranderFK == grander.GranderId)
                {
                    grander.WorkForcePlans.Add(scp);
                }
            }
            */

            return grander;

        }

        public IEnumerable<Grander> GetAllGranders()
        {

            var granders = _context.Granders.ToList();
            if (granders == null)
                throw new ArgumentNullException();
            /*
            var subcontractingPlans = _context.SubcontractingPlans.ToList();
            var resourcePlans = _context.ResourcePlans.ToList();
            var workForcePlan = _context.WorkForcePlans.ToList();
            
            for (int i = 0; i < granders.Count; i++)
            {
                foreach (SubcontractingPlan scp in subcontractingPlans)
                {
                    if (scp.GranderFK == granders[i].GranderId)
                    {
                        granders[i].SubcontractingPlans.Add(scp);
                    }
                }


                foreach (ResourcePlan scp in resourcePlans)
                {
                    if (scp.GranderFK == granders[i].GranderId)
                    {
                        granders[i].ResourcePlans.Add(scp);
                    }
                }


                foreach (WorkForcePlan scp in workForcePlan)
                {
                    if (scp.GranderFK == granders[i].GranderId)
                    {
                        granders[i].WorkForcePlans.Add(scp);
                    }
                }


            }
            */

            return granders;

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteGrander(int id)
        {
            var grander = _context.Granders.FirstOrDefault(c => c.GranderId == id);
            if (grander == null)
                throw new ItemNotFoundException($"Grander not found with Grander Id={id}");
            _context.Granders.Remove(grander);
        }



        public void UpdateGrander(int id,GranderCreateDto updatedGrander)
        {

            if (updatedGrander == null)
            {
                throw new ArgumentNullException();
            }

            Grander grander = _context.Granders.FirstOrDefault(c => c.GranderId == id);
            if (grander == null)
                throw new ItemNotFoundException($"Grander not found with Grander Id={id}");

           
            /*
            for (int i = 0; i < grander.ResourcePlans.Count; i++)
            {
                for (int j = 0; j < updatedGrander.ResourcePlans.Count; j++)
                {
                    if (updatedGrander.ResourcePlans[j].equipmentId == grander.ResourcePlans[j].equipmentId)
                    {
                        grander.ResourcePlans[j].amount = updatedGrander.ResourcePlans[j].amount;
                        grander.ResourcePlans[j].budget = updatedGrander.ResourcePlans[j].budget;
                        grander.ResourcePlans[j].unit = updatedGrander.ResourcePlans[j].unit;
                    }

                }
            }


            for (int i = 0; i < grander.SubcontractingPlans.Count; i++)
            {
                for (int j = 0; j < updatedGrander.SubcontractingPlans.Count; j++)
                {
                    if (updatedGrander.SubcontractingPlans[j].subcontractingPlanId == grander.SubcontractingPlans[j].subcontractingPlanId)
                    {
                        grander.SubcontractingPlans[j].Subcontractor = updatedGrander.SubcontractingPlans[j].Subcontractor;
                       
                    }

                }
            }


            for (int i = 0; i < grander.WorkForcePlans.Count; i++)
            {
                for (int j = 0; j < updatedGrander.WorkForcePlans.Count; j++)
                {
                    if (updatedGrander.WorkForcePlans[j].laborId == grander.WorkForcePlans[j].laborId)
                    {
                        grander.WorkForcePlans[j].budget = updatedGrander.WorkForcePlans[j].budget;
                        grander.WorkForcePlans[j].labor = updatedGrander.WorkForcePlans[j].labor;
                        grander.WorkForcePlans[j].number = updatedGrander.WorkForcePlans[j].number;

                    }

                }
            }

            */

            Employee ApprovedBy = _context.Employees.FirstOrDefault(c => c.EmployeeId == updatedGrander.ApprovedById);
            if (ApprovedBy == null)
                throw new ItemNotFoundException($"Employee with {grander.approvedById} Id not found ");



            grander.approvedBy = ApprovedBy;
            grander.approvedById = updatedGrander.ApprovedById;
            grander.Date = updatedGrander.Date;
            grander.Duration = updatedGrander.Duration;            
            grander.ProjectId = updatedGrander.ProjectId;
            grander.RequestNo = updatedGrander.RequestNo;                 
            grander.TotalEstiamtedReqtBudget = updatedGrander.TotalEstiamtedReqtBudget;

            Project project = _context.Projects.FirstOrDefault(c => c.Id == updatedGrander.ProjectId);
            if (project == null)
                throw new ItemNotFoundException($"Projects with {updatedGrander.ProjectId} Id not found ");




            grander.project = project;
            grander.WorkForcePlans = updatedGrander.WorkForcePlans;
            grander.ResourcePlans = updatedGrander.ResourcePlans;
            grander.SubcontractingPlans = updatedGrander.SubcontractingPlans;
            
            _context.Granders.Update(grander);
            _context.SaveChanges();
        }




    }
}

