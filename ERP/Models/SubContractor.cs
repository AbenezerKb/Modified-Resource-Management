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
        public string SubName { get; set; } = string.Empty;

        [Required]
        public string SubAddress { get; set; } = string.Empty;

        [Required]
        public int SubWorkId { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
