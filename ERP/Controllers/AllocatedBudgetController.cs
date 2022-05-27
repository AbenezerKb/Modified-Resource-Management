using AutoMapper;
using ERP.DTOs;
using ERP.Models;
using ERP.Services;
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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllocatedBudgetReadDto>>> GetAllAllocatedBudgets()
        {
            var _allocatedBudget = _allocatedBudgetRepo.GetAllAllocatedBudgets();

            return Ok(_mapper.Map<IEnumerable<AllocatedBudgetReadDto>>(_allocatedBudget));
        }



        [HttpGet("{id:int}", Name = "GetAllocatedBudget")]
        public async Task<ActionResult<AllocatedBudgetReadDto>> GetAllocatedBudget(int id)
        {

            var _allocatedBudget = _allocatedBudgetRepo.GetAllocatedBudget(id);
            if (_allocatedBudget != null)
            {
                return Ok(_mapper.Map<AllocatedBudgetReadDto>(_allocatedBudget));
            }
            else
            {
                return NotFound();
            }


        }



        [HttpPost]
        public async Task<ActionResult<AllocatedBudgetReadDto>> AddAllocatedBudget(AllocatedBudgetCreateDto allocatedBudget)
        {

            var newAllocatedBudget = _mapper.Map<AllocatedBudget>(allocatedBudget);
            _allocatedBudgetRepo.CreateAllocatedBudget(newAllocatedBudget);
            _allocatedBudgetRepo.SaveChanges();
            var allocatedBudgetReadDto = _mapper.Map<AllocatedBudgetReadDto>(newAllocatedBudget);

            return CreatedAtRoute(nameof(GetAllocatedBudget), new { id = allocatedBudgetReadDto.id }, allocatedBudgetReadDto);
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AllocatedResourcesReadDto>> DeleteAllocatedResource(int id)
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


        [HttpPut("{id:int}")]       
        public async Task<ActionResult> UpdateAllocatedBudget(int id, [FromBody] AllocatedBudgetCreateDto allocatedBudget)
        {
            try
            {
                var newAllocatedBudget = _mapper.Map<AllocatedBudget>(allocatedBudget);
                _allocatedBudgetRepo.UpdateAllocatedBudget(id, newAllocatedBudget);
                _allocatedBudgetRepo.SaveChanges();
                return Ok("Success");
            }

            catch (Exception)
            {
                Console.WriteLine("hgvjhvjhghg");
                return NotFound();
            }
        }

    }
}
