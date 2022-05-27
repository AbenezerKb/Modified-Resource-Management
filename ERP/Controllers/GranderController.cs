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
    public class GranderController: ControllerBase
    {

        private readonly IGranderRepo _granderRepo;
        private readonly IMapper _mapper;

        public GranderController(IGranderRepo granderRepo, IMapper mapper)
        {
            _granderRepo = granderRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,OfficeEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GranderReadDto>>> GetAllGranders()
        {
            
            var _granders = _granderRepo.GetAllGranders();
          

            return Ok(_mapper.Map<IEnumerable<GranderReadDto>>(_granders));
        }

        [Authorize(Roles = "ProjectManager,OfficeEngineer")]
        [HttpGet("{id:int}", Name = "GetGrander")]
        public async Task<ActionResult<GranderReadDto>> GetGrander(int id)
        {            
            var _granders = _granderRepo.GetGrander(id);
            if (_granders != null)
            {
                return Ok(_mapper.Map<GranderReadDto>(_granders));
            }
            else
            {
                return NotFound();
            }


        }

        [Authorize(Roles = "ProjectManager,OfficeEngineer")]
        [HttpPost]
        public async Task<ActionResult<GranderReadDto>> AddGrander(GranderCreateDto grander)
        {

            var newGrander = _mapper.Map<Grander>(grander);
            _granderRepo.CreateGrander(newGrander);
            _granderRepo.SaveChanges();
            
            var granderReadDto = _mapper.Map<GranderReadDto>(newGrander);

            return CreatedAtRoute(nameof(GetGrander), new { id = granderReadDto.GranderId }, granderReadDto);
        }


        [Authorize(Roles = "ProjectManager,OfficeEngineer")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GranderReadDto>> DeleteGrander(int id)
        {

            try
            {
                _granderRepo.DeleteGrander(id);
                _granderRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }




        [Authorize(Roles = "ProjectManager,OfficeEngineer")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GranderReadDto>> UpdateGrander(int id, [FromBody] GranderCreateDto laborDetail)
        {
            try
            {

                var newLaborDetail = _mapper.Map<Grander>(laborDetail);
                _granderRepo.UpdateGrander(id, newLaborDetail);
                _granderRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }




    }
}
