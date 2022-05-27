using ERP.Models;
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

        public void CreateGrander(Grander grander)
        {
            if (grander == null)
            {
                throw new ArgumentNullException();
            }
          
            //grander.GranderId = Guid.NewGuid().ToString();


            foreach (SubcontractingPlan scp in grander.SubcontractingPlans)
            {
          
                scp.GranderFK = grander.GranderId;
                _context.SubcontractingPlans.Add(scp);
            }


            foreach (ResourcePlan rp in grander.ResourcePlans)
            {
          
                rp.GranderFK = grander.GranderId;
                _context.ResourcePlans.Add(rp);
            }


            foreach (WorkForcePlan wfp in grander.WorkForcePlans)
            {
          
                wfp.GranderFK = grander.GranderId;
                _context.WorkForcePlans.Add(wfp);
            }         

            _context.Granders.Add(grander);

        }

        public Grander GetGrander(int granderId)
        {
            var grander =_context.Granders.FirstOrDefault(c => c.GranderId == granderId);
            var subcontractingPlans = _context.SubcontractingPlans.ToList();
            var resourcePlans = _context.ResourcePlans.ToList();
            var workForcePlan = _context.WorkForcePlans.ToList();




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

            return grander;

        }

        public IEnumerable<Grander> GetAllGranders()
        {





            var granders = _context.Granders.ToList();
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

            return granders;

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteGrander(int id)
        {
            var grander = _context.Granders.FirstOrDefault(c => c.GranderId == id);
            _context.Granders.Remove(grander);
        }



        public void UpdateGrander(int id,Grander updatedGrander)
        {

            if (updatedGrander == null)
            {
                throw new ArgumentNullException();
            }

            Grander grander = _context.Granders.FirstOrDefault(c => c.GranderId == id);
            if (grander == null)
                throw new ItemNotFoundException($"Grander not found with Grander Id={id}");



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



            grander.ApprovedBy = updatedGrander.ApprovedBy;
            grander.Date = updatedGrander.Date;
            grander.Duration = updatedGrander.Duration;
            grander.ProjectManager = updatedGrander.ProjectManager;
            grander.ProjectName = updatedGrander.ProjectName;
            grander.RequestNo = updatedGrander.RequestNo;                 
            grander.TotalEstiamtedReqtBudget = updatedGrander.TotalEstiamtedReqtBudget;
           
            grander.WorkForcePlans = updatedGrander.WorkForcePlans;            
         
            _context.Granders.Update(grander);
            _context.SaveChanges();
        }




    }
}
