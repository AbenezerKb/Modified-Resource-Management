namespace ERP.DTOs
{
    public class LaborDetailReadDto
    {
        public int id { get; set; }
        public DateTime dateOfWork { get; set; }
        public int weekNo { get; set; }
        public string dateType { get; set; }
        public string morningSession { get; set; }
        public string afternoonSession { get; set; }
        public string eveningSession { get; set; }
        public int NoOfHrsPerSession { get; set; }

        //FK
        public int LaborerID { get; set; }
    }
}
