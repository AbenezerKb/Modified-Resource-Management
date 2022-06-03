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
        public string jobType { get; set; } = string.Empty;        
        public int NoOfPresents { get; set; }
        public int NoOfAbscents { get; set; }
        public int NoOfHrsPerSession { get; set; }
        public int totalWorkedHrs { get; set; }
        public double wages { get; set; }
        public double totalPayment { get; set; }

        //FK


        public int preparedById { get; set; } 
        public Employee preparedBy { get; set; }
        //FK
        public int approvedById { get; set; }
        public Employee approvedBy { get; set; }
        //FK

        public int LaborerID { get; set; } 
        public DailyLabor dailyLabor { get; set; }

        public string remark { get; set; } = string.Empty;


    }
}
