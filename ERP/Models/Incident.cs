using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Incident
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int incidentNo { get; set; }        
        public string incidentName { get; set; }
        public string proID { get; set; }
        public string empName { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; }
    }
}
