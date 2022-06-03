using System.ComponentModel.DataAnnotations;


namespace ERP.DTOs
{
    public class TimeCardCreateDto
    {
       
        [Required]
        public DateTime dateOfWork { get; set; }       
        [Required]
        public string jobType { get; set; }
        
        [Required]
        public int NoOfPresents { get; set; }
        [Required]
        public int NoOfAbscents { get; set; }
        [Required]
        public int NoOfHrsPerSession { get; set; }
        [Required]
        public int totalWorkedHrs { get; set; }
        [Required]
        public double wages { get; set; }
        [Required]
        public double totalPayment { get; set; }
        
        [Required]
        public int preparedById { get; set; }        
        
        [Required]
        public int approvedById { get; set; }
        
        [Required]
        public int LaborerID { get; set; }
        [Required]
        public string remark { get; set; }
    }
}
