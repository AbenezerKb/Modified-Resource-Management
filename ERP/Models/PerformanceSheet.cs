using ERP.Models.Others;
namespace ERP.Models
{
    public class PerformanceSheet : IAuditableEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public float PerformancePoint { get; set; }
        public string Remark { get; set; } = string.Empty;
        public int WeeklyResultValueId { get; set; }
        public WeeklyResultValue WeeklyResultValue { get; set; }
        public int? ProjectTaskId { get; set; }
        public ProjectTask? ProjectTask { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}