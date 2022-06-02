using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class BID
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  BIDID { get; set; }
                        
        public DateTime initailDate { get; set; }        
        public DateTime finalDate { get; set; }

        public string WorkDescription { get; set; } = string.Empty;

        public string ConBID { get; set; } = string.Empty;

        public double EstimatedBID { get; set; }
        public double ActualCost { get; set; }    

        public string PenalityDescription { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public int ProjectId { get; set; } 
        public Project project { get; set; }        

        public string fileName { get; set; } = string.Empty;
    }
}

