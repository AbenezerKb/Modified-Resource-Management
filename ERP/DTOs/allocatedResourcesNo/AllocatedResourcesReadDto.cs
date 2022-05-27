namespace ERP.DTOs
{
    public class AllocatedResourcesReadDto
    {
        public int allocatedResourcesNo { get; set; }
        public DateTime date { get; set; }
        public int projId { get; set; }
        public int itemId { get; set; }
        public string unit { get; set; }
        public string remark { get; set; }
    }
}
