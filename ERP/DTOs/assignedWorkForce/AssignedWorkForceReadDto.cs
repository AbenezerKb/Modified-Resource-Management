using ERP.Models;
namespace ERP.DTOs
{
    public class AssignedWorkForceReadDto
    {
        public int assigneWorkForceNo { get; set; }
        public DateTime date { get; set; }
        public int projId { get; set; }
        // public string professionalName { get; set; }
        public IList<WorkForceReadDto> ProfessionWithWork { get; set; }
        //   public string profession { get; set; }
        public string remark { get; set; }
    }


    public class WorkForceReadDto
    {        
        public int WokrkForceID { get; set; }
        public int assigneWorkForceNo { get; set; }
        public int EmployeeId { get; set; }
        public string position { get; set; }
    }




}
