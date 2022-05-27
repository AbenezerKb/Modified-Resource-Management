using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Grander
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GranderId { get; set; }
            public string ProjectName { get; set; } = string.Empty;
        public string ProjectManager { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string RequestNo { get; set; } = string.Empty;
        public DateTime Date { get; set; }
            public IList<WorkForcePlan> WorkForcePlans { get; set; }
            public IList<ResourcePlan> ResourcePlans { get; set; }
            public IList<SubcontractingPlan> SubcontractingPlans { get; set;}
            public string ApprovedBy { get; set; } = string.Empty;
        public double TotalEstiamtedReqtBudget { get; set; }
    }


    /*
    public class Grander
    {
        [Key]
        [Required]
        public string GranderId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public string Duration { get; set; }
        public string RequestNo { get; set; }
        public string Date { get; set; }       
        public string ApprovedBy { get; set; }
        public int TotalEstiamtedReqtBudget { get; set; }
    }
    */
}
