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
    public class AllocatedBudgetController:ControllerBase
    {
        private readonly IAllocatedBudgetRepo _allocatedBudgetRepo;
        private readonly IMapper _mapper;

        public AllocatedBudgetController(IAllocatedBudgetRepo allocatedBudgetRepo, IMapper mapper)
        {
            _allocatedBudgetRepo = allocatedBudgetRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllocatedBudgetReadDto>>> GetAllAllocatedBudgets()
        {
            var _allocatedBudget = _allocatedBudgetRepo.GetAllAllocatedBudgets();

            return Ok(_mapper.Map<IEnumerable<AllocatedBudgetReadDto>>(_allocatedBudget));
        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet("{id:int}", Name = "GetAllocatedBudget")]
        public async Task<ActionResult<AllocatedBudget>> GetAllocatedBudget(int id)
        {

            var _allocatedBudget = _allocatedBudgetRepo.GetAllocatedBudget(id);
            if (_allocatedBudget != null)
            {               
                return Ok(_allocatedBudget);
            }
            else
            {
                return NotFound();
            }


        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPost]
        public async Task<ActionResult<AllocatedBudget>> AddAllocatedBudget(AllocatedBudgetCreateDto allocatedBudget)
        {

            //var newAllocatedBudget = _mapper.Map<AllocatedBudget>(allocatedBudget);
            var newAllocatedBudget = _allocatedBudgetRepo.CreateAllocatedBudget(allocatedBudget);
            _allocatedBudgetRepo.SaveChanges();
        //    var allocatedBudgetReadDto = _mapper.Map<AllocatedBudgetReadDto>(allocatedBudget);

            return CreatedAtRoute(nameof(GetAllocatedBudget), new { id = newAllocatedBudget.Id }, newAllocatedBudget);
        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AllocatedResources>> DeleteAllocatedResource(int id)
        {

            try
            {
                _allocatedBudgetRepo.DeleteAllocatedBudget(id);
                _allocatedBudgetRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPut("{id:int}")]       
        public async Task<ActionResult> UpdateAllocatedBudget(int id, [FromBody] AllocatedBudgetCreateDto allocatedBudget)
        {
            try
            {
                //var newAllocatedBudget = _mapper.Map<AllocatedBudget>(allocatedBudget);
                _allocatedBudgetRepo.UpdateAllocatedBudget(id, allocatedBudget);
                _allocatedBudgetRepo.SaveChanges();
                return Ok("Success");
            }

            catch (Exception)
            {              
                return NotFound();
            }
        }

    }
}
