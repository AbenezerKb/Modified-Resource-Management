using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class AllocatedResources
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int allocatedResourcesNo { get; set; }
        public DateTime date { get; set; }
        public int projId { get; set; }
        P
        public int itemId { get; set; }
        public string unit { get; set; }
        public string remark { get; set; }

    }
}
