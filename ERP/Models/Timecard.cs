using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class TimeCard
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public DateTime dateOfWork { get; set; }      
        public string employeeName { get; set; } = string.Empty;
        public string jobType { get; set; } = string.Empty;
        public int weekNo { get; set; }
        public int NoOfPresents { get; set; }
        public int NoOfAbscents { get; set; }
        public int NoOfHrsPerSession { get; set; }
        public int totalWorkedHrs { get; set; }
        public double wages { get; set; }
        public double totalPayment { get; set; }

        //FK
        public string preparedByFK { get; set; } = string.Empty;

        //FK
        public string approvedBy { get; set; } = string.Empty;

        //FK
        public string LaborerID { get; set; } = string.Empty;

        public string remark { get; set; } = string.Empty;


    }
}
