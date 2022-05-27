using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs
{
    public class LaborDetailCreateDto
    {
        [Required]
        public DateTime dateOfWork { get; set; }
        [Required]
        public int weekNo { get; set; }
        [Required]
        public string dateType { get; set; }
        [Required]
        public bool morningSession { get; set; }
        [Required]
        public bool afternoonSession { get; set; }
        [Required]
        public bool eveningSession { get; set; }
        [Required]
        public int NoOfHrsPerSession { get; set; }
        public int PaymentDayIn { get; set; } = 14;

        //FK
        //     [Required]
        //   public string LaborerID { get; set; }
    }
}
