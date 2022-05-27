using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class WeeklyEquipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int equipmentId { get; set; }        
        public int WeeklyRequirementFK { get; set; }
        public string unit { get; set; } = string.Empty;
        public int amount { get; set; }
        public double budget { get; set; }
        //    public WeeklyRequirement WeeklyRequirement { get; set; }
    }

    public class WeeklyLabor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int laborId { get; set; }
        public int WeeklyRequirementFK { get; set; }        
        public int number { get; set; }
        public double  budget { get; set; }
        // public WeeklyRequirement WeeklyRequirement { get; set; }
    }

    public class WorkForcePlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int laborId { get; set; }
        public int GranderFK { get; set; }
        public string labor { get; set; } = string.Empty;
        public int number { get; set; }
        public double budget { get; set; }       

    }


    public class WeeklyMaterial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int materialId { get; set; }        
        public int WeeklyRequirementFK { get; set; }
        public string unit { get; set; } = string.Empty;
        public int amount { get; set; }
        public double budget { get; set; }
        //public WeeklyRequirement WeeklyRequirement { get; set; }
    }

    public class SubcontractingPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subcontractingPlanId { get; set; }
        public string Subcontractor { get; set; } = string.Empty;
        public double estimatedAmount { get; set; }
        public int GranderFK { get; set; }        
    }

    public class ResourcePlan 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int equipmentId { get; set; }        
        public int GranderFK { get; set; }
        public string unit { get; set; } = string.Empty;
        public int amount { get; set; }
        public double budget { get; set; }        
    }

    public class ApprovedWorkList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovedWorkId { get; set; }
        public int ConsultantId { get; set; }
        public string ApprovedWorks { get; set; } = string.Empty;

    }
    public class DeclinedWorkList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeclinedWorkId { get; set; }
        public int ConsultantId { get; set; }
        public string DeclinedWorks { get; set; } = string.Empty;

    }
    
public class DefectsCorrectionlist
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DefectsCorrectionId { get; set; }
        public int ConsultantId { get; set; }
        public string DefectsCorrections { get; set; } = string.Empty;
    }

}
