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
    public class TimeCardController: ControllerBase
    {
        private readonly ITimeCardRepo _timeCardRepo;
        private readonly IMapper _mapper;

        public TimeCardController(ITimeCardRepo timeCardRepo, IMapper mapper)
        {
            _timeCardRepo = timeCardRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,StoreManager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeCardReadDto>>> GetAllTimeCards()
        {
            Console.WriteLine("....started");
            var _timeCards = _timeCardRepo.GetAllTimeCards();
            Console.WriteLine("....started", _timeCards);

            return Ok(_mapper.Map<IEnumerable<TimeCardReadDto>>(_timeCards));
        }

        [Authorize(Roles = "ProjectManager,Admin,StoreManager")]
        [HttpGet("{id:int}", Name = "GetTimeCard")]
        public async Task<ActionResult<TimeCardReadDto>> GetTimeCard(int id)
        {
            Console.WriteLine("....started");
            var _timeCard = _timeCardRepo.GetTimeCard(id);
            if (_timeCard != null)
            {
                return Ok(_mapper.Map<TimeCardReadDto>(_timeCard));
            }
            else
            {
                return NotFound();
            }
        }
            [Authorize(Roles = "ProjectManager,Admin,StoreManager")]
            [HttpPost]
            public async Task<ActionResult<TimeCardReadDto>> AddTimeCard(TimeCardCreateDto timeCard)
            {

             //   var newTimeCard = _mapper.Map<TimeCard>(timeCard);
            var newTimeCard = _timeCardRepo.CreateTimeCard(timeCard);
                _timeCardRepo.SaveChanges();                
                var timeCardReadDto = _mapper.Map<TimeCardReadDto>(newTimeCard);

                return CreatedAtRoute(nameof(GetTimeCard), new { id = timeCardReadDto.id }, timeCardReadDto);
            }

        [Authorize(Roles = "ProjectManager,Admin,StoreManager")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TimeCardReadDto>> DeleteTimeCard(int id)
        {

            try
            {
                _timeCardRepo.DeleteTimeCard(id);
                _timeCardRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }




        [Authorize(Roles = "ProjectManager,Admin,StoreManager")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TimeCardReadDto>> UpdateTimeCard(int id, [FromBody] TimeCardCreateDto timeCard)
        {
            try
            {

                //var newTimeCard = _mapper.Map<TimeCard>(timeCard);
                _timeCardRepo.UpdateTimeCard(id, timeCard);
                _timeCardRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }






    }
}

