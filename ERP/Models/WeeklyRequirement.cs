using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{       

    public class WeeklyRequirement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string projId { get; set; } = string.Empty;
        public string projManager { get; set; } = string.Empty;
        public string projCoordinator { get; set; } = string.Empty;

        public DateTime date { get; set; }
        
        public IList<WeeklyMaterial> material { get; set; }
        
        public IList<WeeklyEquipment> equipment { get; set; }
        
        public IList<WeeklyLabor> labor { get; set; }

        public string weekNo  { get; set; } = string.Empty;
        public string specialRequest { get; set; } = string.Empty;

        public string remark { get; set; } = string.Empty;
    }
}
