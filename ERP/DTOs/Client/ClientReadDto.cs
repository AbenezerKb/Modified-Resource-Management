namespace ERP.DTOs
{
    public class ClientReadDto
    {
        public int clientId { get; set; }
        public string clientName { get; set; }
        public string address { get; set; }
        public int contractorId { get; set; }
        public string estimatedDuration { get; set; }
        public string estimatedCost { get; set; }
        public string description { get; set; }
        public DateTime dateOfContract { get; set; }
        public string attachmentOfContract { get; set; }
        public string remarks { get; set; }

    }
}
