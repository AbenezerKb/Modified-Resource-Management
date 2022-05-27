using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConId { get; set; }
        
        [Required]
        public string ConType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ConGiver { get; set; }

        [Required]
        public string ConReciever { get; set; }

        [Required]
        public ICollection<SubContractingWork> SubConstructWorkDetail { get; set; }

        [Required]
        public String Unit { get; set; }

        [Required]
        public double Cost { get; set; }
        public string Attachement { get; set; }
    }

    public class SubContractingWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubcontractingWorkID { get; set; }
        public string unit { get; set; }
        public double unitPrice { get; set; }
        public double priceWithVat { get; set; }
        public int ContractID { get; set; }
    }
}
