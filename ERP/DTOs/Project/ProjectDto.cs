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
        public int SiteId { get; set; }


    }
}
