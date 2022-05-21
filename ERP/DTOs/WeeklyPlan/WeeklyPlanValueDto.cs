using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs.WeeklyPlan
{
    public class WeeklyPlanValueDto
    {
        [Required]
        public int SubTaskId { get; set; }
        [Required]
        public int PerformedBy { get; set; }
    }

}