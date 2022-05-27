using AutoMapper;
using ERP.DTOs;
using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;
namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedWorkForceController : ControllerBase
    {
        private readonly IAssignedWorkForceRepo _assignedWorkForceRepo;
        private readonly IMapper _mapper;

        public AssignedWorkForceController(IAssignedWorkForceRepo assignedWorkForceRepo, IMapper mapper)
        {
            _assignedWorkForceRepo = assignedWorkForceRepo;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignedWorkForceReadDto>>> GetAllAssignedWorkForces()
        {
            IEnumerable <AssignedWorkForce>_assignedWorkForces = _assignedWorkForceRepo.GetAllAssignedWorkForces();

            return Ok(_mapper.Map<IEnumerable<AssignedWorkForceReadDto>>(_assignedWorkForces));
        }



        [HttpGet("{id:int}", Name = "GetAssignedWorkForce")]
        public async Task<ActionResult<AssignedWorkForceReadDto>> GetAssignedWorkForce(int id)
        {

            var _assignedWorkForces = _assignedWorkForceRepo.GetAssignedWorkForce(id);
            if (_assignedWorkForces != null)
            {
                return Ok(_mapper.Map<AssignedWorkForceReadDto>(_assignedWorkForces));
            }
            else
            {
                return NotFound();
            }


        }



        [HttpPost]
        public async Task<ActionResult<AssignedWorkForceReadDto>> AddAssignedWorkForce(AssignedWorkForceCreateDto assignedWorkForce)
        {
            
            var newAssignedWorkForce = _mapper.Map<AssignedWorkForce>(assignedWorkForce);
            _assignedWorkForceRepo.CreateAssignedWorkForce(newAssignedWorkForce);
            _assignedWorkForceRepo.SaveChanges();
            var assignedWorkForceReadDto = _mapper.Map<AssignedWorkForceReadDto>(newAssignedWorkForce);

            return CreatedAtRoute(nameof(GetAssignedWorkForce), new { id = assignedWorkForceReadDto.assigneWorkForceNo }, assignedWorkForceReadDto);
        }



        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AssignedWorkForceReadDto>> DeleteBID(int id)
        {

            try
            {
                _assignedWorkForceRepo.DeleteAssignedWorkForce(id);
                _assignedWorkForceRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAssignedWorkForce(int id, [FromBody] AssignedWorkForceCreateDto assignedWorkForce)
        {
            try
            {
                var newAssignedWorkForce = _mapper.Map<AssignedWorkForce>(assignedWorkForce);
            _assignedWorkForceRepo.UpdateAssignedWorkForce(id,newAssignedWorkForce);
            _assignedWorkForceRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


    }
}
