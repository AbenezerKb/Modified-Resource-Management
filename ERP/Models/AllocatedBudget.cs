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
        public string projectId { get; set; }
        public string activity { get; set; }
        public double amount { get; set; }
        public string contingency { get; set; }
        public int preparedBy { get; set; }
        public int ApprovedBy { get; set; }

    }
}
