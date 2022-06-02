using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{       

    public class WeeklyRequirement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int projectId { get; set; }
        public Project project { get; set; }
        public int projectManagerId { get; set; }
        public Employee projectManager { get; set; }
        public int projectCoordinatorId{ get; set; }
        public Employee projectCoordinator { get; set; }
        public DateTime date { get; set; }
        
        public IList<WeeklyMaterial> material { get; set; }
        
        public IList<WeeklyEquipment> equipment { get; set; }
        
        public IList<WeeklyLabor> labor { get; set; }
        
        public string specialRequest { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string remark { get; set; } = string.Empty;
    }
}
