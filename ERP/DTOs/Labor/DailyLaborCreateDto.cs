using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs
{
    public class DailyLaborCreateDto
    {
        [Required]
        public string fullName { get; set; } = string.Empty;
        [Required]
        public string name { get; set; } = string.Empty;
        [Required]
        public string jobTitle { get; set; } = string.Empty;
        [Required]
        public double wagePerhour { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public int projectId { get; set; }
        [Required]
        public int approvedById { set; get; }

        [Required]
        public string remarks { get; set; } = string.Empty;
       
    }

    public class DailyLaborApproveCreateDto:DailyLaborCreateDto
    {

        public string status { get; set; } = string.Empty;
    }

}
