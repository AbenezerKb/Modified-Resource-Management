using ERP.Models;

namespace ERP.DTOs
{
    public class WeeklyRequirementReadDto
    {
        public int id { get; set; }
        public int projId { get; set; }
        public int projManager { get; set; }
        public int projCoordinator { get; set; }

        public DateTime date { get; set; }
        public IList<WeeklyMaterialReadDto> material { get; set; }

        public IList<WeeklyEquipmentReadDto> equipment { get; set; }

        public IList<WeeklyLaborReadDto> labor { get; set; }
        public string specialRequest { get; set; }

        public string remark { get; set; }
        public string weekNo { get; set; }

    }

     public class WeeklyEquipmentReadDto
    {
        
        public int equipmentId { get; set; }
        public int WeeklyRequirementFK { get; set; }
        public string unit { get; set; }
        public int amount { get; set; }
        public double budget { get; set; }
        //    public WeeklyRequirement WeeklyRequirement { get; set; }
    }




    public class WeeklyLaborReadDto
    {
        
        public int laborId { get; set; }
        public int WeeklyRequirementFK { get; set; }
        public string labor { get; set; }
        public int number { get; set; }
        public double budget { get; set; }
        // public WeeklyRequirement WeeklyRequirement { get; set; }
    }



    public class WeeklyMaterialReadDto
    {
        public int materialId { get; set; }
        public int WeeklyRequirementFK { get; set; }
        public string unit { get; set; }
        public int amount { get; set; }
        public double budget { get; set; }
        //public WeeklyRequirement WeeklyRequirement { get; set; }
    }

}

