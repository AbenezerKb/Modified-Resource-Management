using System.ComponentModel.DataAnnotations;
using ERP.Models;

namespace ERP.DTOs
{
    public class GranderCreateDto
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int ProjectManagerId { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public int RequestNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public IList<WorkForcePlan> WorkForcePlans { get; set; }
        [Required]
        public IList<ResourcePlan> ResourcePlans { get; set; }
        [Required]
        public IList<SubcontractingPlan> SubcontractingPlans { get; set; }
        [Required]
        public int ApprovedById { get; set; }
        [Required]
        public int TotalEstiamtedReqtBudget { get; set; }

    }





    
    
    





    public class WorkForcePlanCreateDto {

        //public string laborId { get; set; }
        
    //    public string GranderFK { get; set; }
        [Required]
        public string labor { get; set; }
        [Required]
        public int number { get; set; }
        [Required]
        public double budget { get; set; }
    }


    

    public class ResourcePlanCreateDto
    {
      
        [Required]
        public string equipment { get; set; }
        
      //  public string GranderFK { get; set; }
        [Required]
        public string unit { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public double budget { get; set; }
    }

    public class SubcontractingPlanCreateDto
    {
        [Required]
        public int SubcontractingWork { get; set; }
        [Required]
        public int Subcontractor { get; set; }
        
     //   public string GranderFK { get; set; }
    }
}
