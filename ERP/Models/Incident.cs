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
        public int projectID { get; set; }
        public Project project { get; set; }
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
