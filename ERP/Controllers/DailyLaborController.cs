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
    public class DailyLaborController:ControllerBase
    {


        private readonly IDailyLaborRepo _dailyLaborRepo;
        private readonly IMapper _mapper;

        public DailyLaborController(IDailyLaborRepo dailyLaborRepo, IMapper mapper)
        {
            _dailyLaborRepo = dailyLaborRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,StoreKeeper")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyLaborReadDto>>> GetAllDailyLabors()
        {
            var _dailyLabors = _dailyLaborRepo.GetAllDailyLabors();

            return Ok(_mapper.Map<IEnumerable<DailyLaborReadDto>>(_dailyLabors));
        }


        [Authorize(Roles = "ProjectManager,StoreKeeper")]
        [HttpGet("{id:int}", Name = "GetDailyLabor")]
        public async Task<ActionResult<DailyLaborReadDto>> GetDailyLabor(int id)
        {

            var _dailyLabors = _dailyLaborRepo.GetDailyLabor(id);
            if (_dailyLabors != null)
            {
                return Ok(_mapper.Map<DailyLaborReadDto>(_dailyLabors));
            }
            else
            {
                return NotFound();
            }


        }




        [Authorize(Roles = "ProjectManager,StoreKeeper")]
        [HttpPost]
        public async Task<ActionResult<DailyLaborReadDto>> AddDailyLabor(DailyLaborCreateDto dailyLabor)
        {

            //var newDailyLabor = _mapper.Map<DailyLabor>(dailyLabor);
            var newDailyLabor = _dailyLaborRepo.CreateDailyLabor(dailyLabor);
            _dailyLaborRepo.SaveChanges();
            var dailyLaborReadDto = _mapper.Map<DailyLaborReadDto>(newDailyLabor);

            return CreatedAtRoute(nameof(GetDailyLabor), new { id = dailyLaborReadDto.LaborerID }, dailyLaborReadDto);
        }

        [Authorize(Roles = "ProjectManager,StoreKeeper")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DailyLaborReadDto>> DeleteDailyLabor(int id)
        {

            try
            {
                _dailyLaborRepo.DeleteDailyLabor(id);
                _dailyLaborRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }






        [Authorize(Roles = "ProjectManager,StoreKeeper")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DailyLaborReadDto>> UpdateDailyLabor(int id, [FromBody] DailyLaborCreateDto dailyLabor)
        {
            try
            {

                //var newDailyLabor = _mapper.Map<DailyLabor>(dailyLabor);
                _dailyLaborRepo.UpdateDailyLabor(id, dailyLabor);
                _dailyLaborRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "ProjectManager,StoreKeeper")]
        [HttpPut("{id:int}/status")]
        public async Task<ActionResult<DailyLaborReadDto>> UpdateDailyLaborStatus(int id, [FromBody] DailyLaborApproveCreateDto dailyLabor)
        {
            try
            {

                //var newDailyLabor = _mapper.Map<DailyLabor>(dailyLabor);
                _dailyLaborRepo.UpdateDailyLabor(id, dailyLabor);
                _dailyLaborRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }







    }


}

