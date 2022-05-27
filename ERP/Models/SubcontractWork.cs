using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class SubContractWork
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subconractingid { get; set; }
        public string workName { get; set; }
        public string remarks { get; set; }
    }
}
