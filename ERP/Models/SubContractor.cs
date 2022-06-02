using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class SubContractor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubId { get; set; }

        public string SubName { get; set; } = string.Empty;

        public string SubAddress { get; set; } = string.Empty;

        public int subContractingWorkId { get; set; }
        public SubContractingWork subContractingWork { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
