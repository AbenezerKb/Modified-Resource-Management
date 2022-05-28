namespace ERP.DTOs
{
    public class DailyLaborReadDto
    {
        public int LaborerID { get; set; }
        public string fullName { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string jobTitle { get; set; } = string.Empty;
        public double wagePerhour { get; set; }
        public DateTime date { get; set; }
        public string remarks { get; set; } = string.Empty;        
        public int projectId { get; set; }

        public int apprvedBy { set; get; }
        public string status { get; set; } = string.Empty;
    }
}
