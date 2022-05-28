using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs
{
    public class IncidentCreateDto
    {
       [Required]
        public string incidentName { get; set; } = string.Empty;
        [Required]
        public int proID { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
