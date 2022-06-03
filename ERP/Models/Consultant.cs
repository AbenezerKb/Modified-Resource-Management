using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Consultant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int consultantId { get; set; }
        public string consultantName { get; set; }        
        public int projectId { get; set; }
        public Project project { get; set; }
        public int subContractorId { get; set; }
        public Contract contractor { get; set; }
        public DateTime reviewDate { get; set; }
        public IList<ApprovedWorkList> approvedWorkList { get; set; }        
        public string changesTaken { get; set; } = string.Empty;
        public string reasonForChange { get; set; } = string.Empty;

        public IList<DeclinedWorkList> declinedWorkList { get; set; }
       public string defectsSeen { get; set; } = string.Empty;
        public string nextWork { get; set; } = string.Empty;
        public IList<DefectsCorrectionlist> defectsCorrectionlist { get; set; }
        public string remarks { get; set; } = string.Empty;
        public string attachemnt { get; set; } = string.Empty;
    }

}
