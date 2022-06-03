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
    public class SubContractWorkController : ControllerBase
    {


        private readonly ISubContractWorkRepo _subcontractWorkRepo;
        private readonly IMapper _mapper;

        public SubContractWorkController(ISubContractWorkRepo subcontractWorkRepo, IMapper mapper)
        {
            _subcontractWorkRepo = subcontractWorkRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubContractWorkReadDto>>> GetAllSubContractWorks()
        {

            var _subcontractWorks = _subcontractWorkRepo.GetAllSubContractWorks();

            return Ok(_mapper.Map<IEnumerable<SubContractWorkReadDto>>(_subcontractWorks));
        }

        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpGet("{id:int}", Name = "GetSubContractWork")]
        public async Task<ActionResult<SubContractWorkReadDto>> GetSubContractWork(int id)
        {

            var _subcontractWorks = _subcontractWorkRepo.GetSubContractWork(id);
            if (_subcontractWorks != null)
            {
                return Ok(_mapper.Map<SubContractWorkReadDto>(_subcontractWorks));
            }
            else
            {
                return NotFound();
            }


        }

        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpPost]
        public async Task<ActionResult<SubContractWorkReadDto>> AddSubContractWork(SubContractWorkCreateDto subcontractWork)
        {

        //    var newSubContractWork = _mapper.Map<SubContractWork>(subcontractWork);
            var newSubContractWork = _subcontractWorkRepo.CreateSubContractWork(subcontractWork);
            _subcontractWorkRepo.SaveChanges();

            var subcontractReadDto = _mapper.Map<SubContractWorkReadDto>(newSubContractWork);

            return CreatedAtRoute(nameof(GetSubContractWork), new { id = subcontractReadDto.subconractingid }, subcontractReadDto);
        }



        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SubContractWorkReadDto>> DeleteSubContractWork(int id)
        {

            try
            {
                _subcontractWorkRepo.DeleteSubContractWorks(id);
                _subcontractWorkRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }





        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SubContractWorkReadDto>> UpdateSubContractWork(int id, [FromBody] SubContractWorkCreateDto subContractWork)
        {
            try
            {

                //var newsubContractWork = _mapper.Map<SubContractWork>(subContractWork);
                _subcontractWorkRepo.UpdateSubContractWork(id, subContractWork);
                _subcontractWorkRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }




    

    }
}
