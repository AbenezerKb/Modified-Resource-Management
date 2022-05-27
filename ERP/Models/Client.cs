using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{


    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int clientId { get; set; }
        public string clientName { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string contractorId { get; set; } = string.Empty;
        public string estimatedDuration { get; set; } = string.Empty;
        public string estimatedCost { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime dateOfContract { get; set; }
        public string attachmentOfContract { get; set; } = string.Empty;
        public string remarks { get; set; } = string.Empty;


    }
}
