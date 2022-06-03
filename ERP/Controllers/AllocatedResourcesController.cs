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
    public class AllocatedResourcesController : ControllerBase
    {
        private readonly IAllocatedResourcesRepo _allocatedResourcesRepo;
        private readonly IMapper _mapper;

        public AllocatedResourcesController(IAllocatedResourcesRepo allocatedResourcesRepo, IMapper mapper)
        {
            _allocatedResourcesRepo = allocatedResourcesRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllocatedResourcesReadDto>>> GetAllAssignedWorkForces()
        {
            var _allocatedResources = _allocatedResourcesRepo.GetAllAllocatedResources();

            return Ok(_mapper.Map<IEnumerable<AllocatedResourcesReadDto>>(_allocatedResources));
        }


        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpGet("{id:int}", Name = "GetAllocatedResources")]
        public async Task<ActionResult<AllocatedResourcesReadDto>> GetAllocatedResources(int id)
        {

            var _allocatedResources = _allocatedResourcesRepo.GetAllocatedResources(id);
            if (_allocatedResources != null)
            {
                return Ok(_mapper.Map<AllocatedResourcesReadDto>(_allocatedResources));
            }
            else
            {
                return NotFound();
            }


        }


        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpPost]
        public async Task<ActionResult<AllocatedResourcesReadDto>> AddAllocatedResources(AllocatedResourcesCreateDto allocatedResources)
        {

            //var newAllocatedResources = _mapper.Map<AllocatedResources>(allocatedResources);
            var newAllocatedResources = _allocatedResourcesRepo.CreateAllocatedResources(allocatedResources);
            _allocatedResourcesRepo.SaveChanges();
            //var allocatedResourcesReadDto = _mapper.Map<AllocatedResourcesReadDto>(newAllocatedResources);

            return CreatedAtRoute(nameof(GetAllocatedResources), new { id = newAllocatedResources.allocatedResourcesNo  }, newAllocatedResources);
        }

        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AllocatedResourcesReadDto>> DeleteBID(int id)
        {

            try
            {
                _allocatedResourcesRepo.DeleteAllocatedResource(id);
                _allocatedResourcesRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "ProjectManager,Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AllocatedResourcesReadDto>> UpdateAllocatedResource(int id, [FromBody] AllocatedResourcesCreateDto allocatedResource)
        {
            try
            {
                //var newAllocatedResources = _mapper.Map<AllocatedResources>(allocatedResource);
            _allocatedResourcesRepo.UpdateAllocatedResource(id, allocatedResource);
            _allocatedResourcesRepo.SaveChanges();
            return Ok("Success");
        }
            catch (Exception)
            {
                return NotFound();
    }
}


    }
}
