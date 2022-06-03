using ERP.Models;
using System.ComponentModel.DataAnnotations;
namespace ERP.DTOs
{
    public class ConsultantCreateDto
    {
        [Required]
        public int projectId { get; set; }             

        [Required]
        public string consultantName { get; set; }

        [Required]
        public int subContractorId { get; set; }
        [Required]
        public DateTime reviewDate { get; set; }
        [Required]
        public IList<ApprovedWorkList> approvedWorkList { get; set; }
        [Required]
        public string changesTaken { get; set; }
        [Required]
        public string reasonForChange { get; set; }
        [Required]
        public IList<DeclinedWorkList> declinedWorkList { get; set; }
        [Required]
        public string defectsSeen { get; set; }
        [Required]
        public string nextWork { get; set; }
        [Required]
        public IList<DefectsCorrectionlist> defectsCorrectionlist { get; set; }
        [Required]
        public string remarks { get; set; }

        [Required]
        public string attachement { get; set; }
    }


    public class ApprovedWorkListCreateDto
    {
        [Required]
        public string ApprovedWorks { get; set; }
    }

    public class DeclinedWorkListCreateDto
    {
        [Required]
        public string DeclinedWorks { get; set; }

    }

    public class DefectsCorrectionlistCreateDto
    {
        [Required]
        public string DefectsCorrections { get; set; } 

    }


    public class ConsultantWithProjectCreateDto: ConsultantCreateDto
    {
        public ERP.Models.Project project { get; set; }
    }



}
