namespace ERP.DTOs
{
    public class AllocatedBudgetReadDto
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int projectId { get; set; }
        public string activity { get; set; }
        public double amount { get; set; }
        public string contingency { get; set; }
        public int preparedBy { get; set; }
        public int ApprovedBy { get; set; }

    }
}
