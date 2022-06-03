using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs
{
    public class SubContractorCreateDto
    {       
        [Required]
        public string subContractorName { get; set; }
        [Required]
        public string subContractorAddress { get; set; }        
        [Required]
        public int subContractingWorkId { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
