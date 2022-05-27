using ERP.Models;
namespace ERP.DTOs   
{
    public class ContractReadDto
    {
        public int ConId { get; set; }
        
        public string ConType { get; set; }
        
        public DateTime Date { get; set; }
        
        public string ConGiver { get; set; }
        
        public string ConReciever { get; set; }
        
        public ICollection<SubContractingWorkReadDto> SubConstructWorkDetail { get; set; }
        
        public string Unit { get; set; }
        
        public double Cost { get; set; }

        public string Attachement { get; set; }

    }

    public class SubContractingWorkReadDto
    {
        public int SubcontractingWorkID { get; set; }
        public string unit { get; set; }
        public double unitPrice { get; set; }
        public double priceWithVat { get; set; }
        public int ContractID { get; set; }
    }
}
