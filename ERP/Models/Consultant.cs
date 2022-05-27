using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Consultant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int consultantId { get; set; }
        public string projectId { get; set; } = string.Empty;
        public string contractorId { get; set; } = string.Empty;
        public DateTime reviewDate { get; set; }
        public ICollection<ApprovedWorkList> approvedWorkList { get; set; }        
        public string changesTaken { get; set; } = string.Empty;
        public string reasonForChange { get; set; } = string.Empty;

        public ICollection<DeclinedWorkList> declinedWorkList { get; set; }
       public string defectsSeen { get; set; } = string.Empty;
        public string nextWork { get; set; } = string.Empty;
        public ICollection<DefectsCorrectionlist> defectsCorrectionlist { get; set; }
        public string remarks { get; set; } = string.Empty;
        public string attachemnt { get; set; } = string.Empty;
    }

}
