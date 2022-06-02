using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class AllocatedBudget
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int  Id { get; set; }
        public DateTime date { get; set; }
        public int projectId { get; set; }
        public Project project { get; set; }
        public string activity { get; set; }
        public double amount { get; set; }
        public string contingency { get; set; }
        public int preparedById { get; set; }
        public Employee preparedBy { get; set; }
        public int ApprovedById { get; set; }
        public Employee ApprovedBy { get; set; }

    }
}
