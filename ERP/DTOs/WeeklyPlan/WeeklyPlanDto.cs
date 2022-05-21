
using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs.WeeklyPlan
{
    public class WeeklyPlanDto
    {
        [Required]
        public int WeekNo { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public List<WeeklyPlanValueDto> PlanValues { get; set; } = new();
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Remark { get; set; } = string.Empty;



    }
}