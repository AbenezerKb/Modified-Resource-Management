namespace ERP.DTOs
{
    public class LaborDetailReadDto
    {
        public int id { get; set; }
        public DateTime dateOfWork { get; set; }
        public int weekNo { get; set; }
        public string dateType { get; set; }
        public bool morningSession { get; set; }
        public bool afternoonSession { get; set; }
        public bool eveningSession { get; set; }
        public int NoOfHrsPerSession { get; set; }
        public int PaymentDayIn { get; set; } = 14;
        //FK
        public int LaborerID { get; set; }
        
    }
}
