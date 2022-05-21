using System.ComponentModel.DataAnnotations;
namespace ERP.DTOs.Project
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string ManagerId { get; set; } = string.Empty;
        [Required]
        public string CoordinatorId { get; set; } = string.Empty;
        [Required]
        public string SiteId { get; set; } = string.Empty;


    }
}
