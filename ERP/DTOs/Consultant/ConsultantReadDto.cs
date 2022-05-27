using ERP.Models;

namespace ERP.DTOs
{
    public class ConsultantReadDto
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        public int consultantId { get; set; }
        public int contractorId { get; set; }
        public DateTime reviewDate { get; set; }
        public IList<ApprovedWorkListReadDto> approvedWorkList { get; set; }
        public string changesTaken { get; set; }
        public string reasonForChange { get; set; }
        public IList<DeclinedWorkListReadDto> declinedWorkList { get; set; }
        public string defectsSeen { get; set; }
        public string nextWork { get; set; }
        public IList<DefectsCorrectionlistReadDto> defectsCorrectionlist { get; set; }
        public string remarks { get; set; }
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
