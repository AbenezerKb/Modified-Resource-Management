using AutoMapper;
using ERP.DTOs;
using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




namespace ERP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyRequirementController : ControllerBase
    {



        private readonly IWeeklyRequirementRepo _weeklyRequirementRepo;
        private readonly IMapper _mapper;

        public WeeklyRequirementController(IWeeklyRequirementRepo weeklyRequirementRepo, IMapper mapper)
        {
            _weeklyRequirementRepo = weeklyRequirementRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer,ProjectCoordinator,SiteEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeeklyRequirementReadDto>>> GetAllWeeklyRequirements()
        {
            
            var _weeklyRequirements = _weeklyRequirementRepo.GetAllWeeklyRequirement();

            return Ok(_mapper.Map<IEnumerable<WeeklyRequirementReadDto>>(_weeklyRequirements));
        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer,ProjectCoordinator,SiteEngineer")]
        [HttpGet("{id:int}", Name = "GetWeeklyRequirement")]
        public async Task<ActionResult<WeeklyRequirementReadDto>> GetWeeklyRequirement(int id)
        {

            var _weeklyRequirement = _weeklyRequirementRepo.GetWeeklyRequirements(id);
            if (_weeklyRequirement != null)
            {
                return Ok(_mapper.Map<WeeklyRequirementReadDto>(_weeklyRequirement));
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPost]
        public async Task<ActionResult<WeeklyRequirementReadDto>> AddWeeklyRequirement(WeeklyRequirementCreateDto weeklyRequirement)
        {

            var newWeeklyRequirement = _mapper.Map<WeeklyRequirement>(weeklyRequirement);
            _weeklyRequirementRepo.CreateWeeklyRequirement(newWeeklyRequirement);
            _weeklyRequirementRepo.SaveChanges();
            var weeklyRequirementReadDto = _mapper.Map<WeeklyRequirementReadDto>(newWeeklyRequirement);

            return CreatedAtRoute(nameof(GetWeeklyRequirement), new { id = weeklyRequirementReadDto.id }, weeklyRequirementReadDto);
        }

        /*
          [Http("{id:int}", Name = "GetWeeklyRequirement")]
          public async Task<ActionResult<WeeklyRequirementReadDto>> GetWeeklyRequirement(int id)
          {

              var _weeklyRequirement = _weeklyRequirementRepo.GetWeeklyRequirements(id);
              if (_weeklyRequirement != null)
              {
                  return Ok(_mapper.Map<WeeklyRequirementReadDto>(_weeklyRequirement));
              }
              else
              {
                  return NotFound();
              }
          }
        */




        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<WeeklyRequirementReadDto>> DeleteWeeklyRequirement(int id)
        {

            try
            {
                
                    _weeklyRequirementRepo.DeleteWeeklyRequirements(id);
                _weeklyRequirementRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception) 
            {
                return NotFound();
            }
        }




        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateWeeklyRequirement(int id, [FromBody] WeeklyRequirementCreateDto weeklyRequirement)
        {
            try
            {

                var newWeeklyRequirement = _mapper.Map<WeeklyRequirement>(weeklyRequirement);
            _weeklyRequirementRepo.UpdateWeeklyRequirement(id,newWeeklyRequirement);
            _weeklyRequirementRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }



    }
}
