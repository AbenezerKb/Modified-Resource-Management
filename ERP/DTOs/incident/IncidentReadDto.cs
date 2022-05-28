namespace ERP.DTOs
{
    public class IncidentReadDto
    {
        public int incidentNo { get; set; }
        public string incidentName { get; set; } = string.Empty;
        public int proID { get; set; }
        public int EmployeeId { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
