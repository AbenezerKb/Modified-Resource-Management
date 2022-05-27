using System.ComponentModel.DataAnnotations;


namespace ERP.DTOs
{
    public class AllocatedBudgetCreateDto
    {
        
        [Required]
        public DateTime date { get; set; }
        [Required]
        public int projectId { get; set; }
        [Required]
        public string activity { get; set; }
        [Required]
        public double amount { get; set; }
        [Required]
        public string contingency { get; set; }
        [Required]
        public int preparedBy { get; set; }
        [Required]
        public int ApprovedBy { get; set; }

    }
}
