using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Incident
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int incidentNo { get; set; }
        public string incidentName { get; set; } = string.Empty;
        public string proID { get; set; } = string.Empty;
        public string empName { get; set; } = string.Empty;
        public DateTime date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
