using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs.ProjectTask
{
    public class ProjectTaskDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}