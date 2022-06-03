
using ERP.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class AssignedWorkForce
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int assigneWorkForceNo  { get; set; }
        public DateTime date { get; set; }
        public int projectId { get; set; }
        public Project project { get; set; }
        // public string professionalName { get; set; }
        public IList<WorkForce> ProfessionWithWork { get; set; }
        public string remark { get; set; } = string.Empty;

    }

    public class WorkForce
    {      
        public int WokrkForceID { get; set; }
        [Key]
        public int assigneWorkForceNo { get; set; }
        public int EmployeeId { get; set; }
        public string position { get; set; } = string.Empty;
    }
}
