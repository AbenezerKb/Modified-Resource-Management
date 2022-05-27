using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class SubContractor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubId { get; set; }

        [Required]
        public string SubName { get; set; }

        [Required]
        public string SubAddress { get; set; }

        [Required]
        public string SubWorkId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
