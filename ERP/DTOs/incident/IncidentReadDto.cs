namespace ERP.DTOs
{
    public class IncidentReadDto
    {
        public int incidentNo { get; set; }
        public string incidentName { get; set; }
        public int proID { get; set; }
        public string empName { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; }
    }
}
