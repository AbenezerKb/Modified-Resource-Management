using AutoMapper;
using ERP.DTOs;
using ERP.Models;



namespace ERP.Profiles
{
    public class DailyLaborProfile: Profile
    {
        public DailyLaborProfile()
        {
            CreateMap<DailyLabor, DailyLaborReadDto>();
            CreateMap<DailyLaborCreateDto, DailyLabor>();
        }

    }


    public class DailyLaborApproveProfile : Profile
    {
        public DailyLaborApproveProfile()
        {
            CreateMap<DailyLabor, DailyLaborReadDto>();
            CreateMap<DailyLaborApproveCreateDto, DailyLabor>();
        }

    }
}
