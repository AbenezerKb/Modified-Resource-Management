using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class DailyLabor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LaborerID { get; set; }
        public string fullName { get; set; }
        public string name { get; set; }
        public string jobTitle { get; set; }
        public int projectId { get; set; }
        public double wagePerhour { get; set; }
        public DateTime date { get; set; }
        public int apprvedBy { set; get; }
        public string remarks { get; set; }
        public string status { get; set; }

    }


    
    public class LaborDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime dateOfWork { get; set; }
        public int weekNo { get; set; }
        public string dateType { get; set; } = string.Empty;
        public bool morningSession { get; set; } 
        public bool afternoonSession { get; set; } 
        public bool eveningSession { get; set; }               
        public int NoOfHrsPerSession { get; set; } 
        public int PaymentDayIn { get; set; } = 14;       

    }
}
