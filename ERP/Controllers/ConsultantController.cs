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
    public class ConsultantController:ControllerBase
    {

        private readonly IConsultantRepo _consultantRepo;
        private readonly IMapper _mapper;

        public ConsultantController(IConsultantRepo consultantRepo, IMapper mapper)
        {
            _consultantRepo = consultantRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultantReadDto>>> GetAllConsultants()
        {            
            var _consultants = _consultantRepo.GetAllConsultant();            

            return Ok(_mapper.Map<IEnumerable<ConsultantReadDto>>(_consultants));
        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet("{id:int}", Name = "GetConsultant")]
        public async Task<ActionResult<ConsultantReadDto>> GetConsultant(int id)
        {            
            var _consultants = _consultantRepo.GetConsultant(id);
            if (_consultants != null)
            {
                return Ok(_mapper.Map<ConsultantReadDto>(_consultants));
            }
            else
            {
                return NotFound();
            }


        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPost]
        public async Task<ActionResult<ConsultantReadDto>> AddConsultant(ConsultantCreateDto consultant)
        {

            var newConsultant = _mapper.Map<Consultant>(consultant);
            _consultantRepo.CreateConsultant(newConsultant);
            _consultantRepo.SaveChanges();            
            var consultantReadDto = _mapper.Map<ConsultantReadDto>(newConsultant);

            return CreatedAtRoute(nameof(GetConsultant), new { id = consultantReadDto }, consultantReadDto);
        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ConsultantReadDto>> DeleteConsultant(int id)
        {
            try
            {
                _consultantRepo.DeleteConsultant(id);
                _consultantRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }




        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ContractReadDto>> UpdateContract(int id, [FromBody] ConsultantCreateDto consultant)
        {
            try
            {

                var newConsultant = _mapper.Map<Consultant>(consultant);
                _consultantRepo.UpdateConsultant(id,newConsultant);
                _consultantRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
















    }
}
