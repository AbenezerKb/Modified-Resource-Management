namespace ERP.DTOs
{
    public class BIDReadDto
    {
        
        public int BIDID { get; set; }
        
        public DateTime initailDate { get; set; }
        public DateTime finalDate { get; set; }

        public string WorkDescription { get; set; }

        public int ConBID { get; set; }

        public double EstimatedBID { get; set; }
        public double ActualCost { get; set; }      

        public string PenalityDescription { get; set; }
        public string Remark { get; set; }

        public int ProjectId { get; set; }

        public string fileName { get; set; }
    }
}
