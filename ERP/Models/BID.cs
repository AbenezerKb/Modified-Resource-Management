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

        public string WorkDescription { get; set; }

        public string ConBID { get; set; }
        
        public double EstimatedBID { get; set; }
        public double ActualCost { get; set; }    

        public string PenalityDescription { get; set; }
        public string Remark { get; set; }
        public string ProjectId { get; set; }

        public string fileName { get; set; }
    }
}

