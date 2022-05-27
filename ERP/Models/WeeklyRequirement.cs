using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{       

    public class WeeklyRequirement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string projId { get; set; }
        public string projManager { get; set; }
        public string projCoordinator { get; set; }

        public DateTime date { get; set; }
        
        public IList<WeeklyMaterial> material { get; set; }
        
        public IList<WeeklyEquipment> equipment { get; set; }
        
        public IList<WeeklyLabor> labor { get; set; }

        public string weekNo  { get; set; }
        public string specialRequest { get; set; }

        public string remark { get; set; }
    }
}
