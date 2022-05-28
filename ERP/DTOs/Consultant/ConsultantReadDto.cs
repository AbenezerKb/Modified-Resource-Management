using ERP.Models;

namespace ERP.DTOs
{
    public class ConsultantReadDto
    {
        public int consultantId { get; set; }
        public string consultantName { get; set; }
        public int projectId { get; set; }
        public int contractorId { get; set; } 
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








    public class ApprovedWorkListReadDto
    {

        public int ApprovedWorkId { get; set; }
        public int ConsultantId { get; set; }
        public string ApprovedWorks { get; set; }
    }

    public class DeclinedWorkListReadDto
    {
        public int DeclinedWorkId { get; set; }
        public int ConsultantId { get; set; }
        public string DeclinedWorks { get; set; }

    }

    public class DefectsCorrectionlistReadDto
    {
        public int DefectsCorrectionId { get; set; }
        public int DefectsCorrectionlistId { get; set; }
        public string DefectsCorrections { get; set; }

    }





}
