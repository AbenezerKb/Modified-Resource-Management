using System.Text.Json.Serialization;
using ERP.Models.Others;

namespace ERP.Models
{
    public class Project : IAuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int SiteId { get; set; }
        public Site Site { get; set; }
        public ProjectStatus Status { get; set; }
        [JsonIgnore]
        public List<ProjectTask> Tasks { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public float GetProgress()
        {
            return (float)Tasks.Select(t =>
                  {
                      return t.Progress;

                  }).DefaultIfEmpty().Average();
        }
        public double GetTotalBudget()
        {
            return Tasks.Sum(t => t.GetTotalBudget());

        }
    }
}
