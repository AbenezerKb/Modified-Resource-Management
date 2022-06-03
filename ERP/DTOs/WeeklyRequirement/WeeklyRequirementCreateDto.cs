﻿using ERP.Models;
using System.ComponentModel.DataAnnotations;

namespace ERP.DTOs
{
    public class WeeklyRequirementCreateDto
    {
        [Required]
        public int projectId { get; set; }
        [Required]
        public int projectManager { get; set; }

        public int projectCoordinator { get; set; }

        public DateTime date { get; set; }
        [Required]
        public IList<WeeklyMaterial> material { get; set; }

        // 
        //public string[] materialNo { get; set; }

        [Required]
        public IList<WeeklyEquipment> equipment { get; set; }

        //public string[] equipmentNo { get; set; }
        [Required]
        public IList<WeeklyLabor> labor { get; set; }

        //public string[] laborNo { get; set; }
        [Required]
        public string specialRequest { get; set; }

        [Required]
        public string remark { get; set; }
        

    }





    public class WeeklyEquipmentCreateDto
    {
        public int WeeklyRequirementFK { get; set; }
        public string unit { get; set; }
        public int amount { get; set; }
        public double budget { get; set; }
        //    public WeeklyRequirement WeeklyRequirement { get; set; }
    }




    public class WeeklyLaborCreateDto
    {

        public int WeeklyRequirementFK { get; set; }
        public string labor { get; set; }
        public int number { get; set; }
        public double budget { get; set; }
        // public WeeklyRequirement WeeklyRequirement { get; set; }
    }



    public class WeeklyMaterialCreateDto
    {
        public int WeeklyRequirementFK { get; set; }
        public string unit { get; set; }
        public int amount { get; set; }
        public double budget { get; set; }
        //public WeeklyRequirement WeeklyRequirement { get; set; }
    }





    public class WeeklyRequirementApproveCreateDto:WeeklyEquipmentCreateDto{
        public string status { get; set; } = string.Empty;
    }



}
