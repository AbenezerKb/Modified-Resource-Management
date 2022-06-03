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
    public class LaborDetailController: ControllerBase
    {




        private readonly ILaborDetailRepo _laborDetailRepo;
        private readonly IMapper _mapper;

        public LaborDetailController(ILaborDetailRepo laborDetailRepo, IMapper mapper)
        {
            _laborDetailRepo = laborDetailRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,StoreKeeper")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaborDetailReadDto>>> GetAllLaborDetails()
        {
            var _laborDetail = _laborDetailRepo.GetAllLaborDetails();

            return Ok(_mapper.Map<IEnumerable<LaborDetailReadDto>>(_laborDetail));
        }


        [Authorize(Roles = "ProjectManager,Admin,StoreKeeper")]
        [HttpGet("{id:int}", Name = "GetLaborDetail")]
        public async Task<ActionResult<LaborDetailReadDto>> GetLaborDetail(int id)
        {

            var _laborDetail = _laborDetailRepo.GetLaborDetail(id);
            if (_laborDetail != null)
            {
                return Ok(_mapper.Map<LaborDetailReadDto>(_laborDetail));
            }
            else
            {
                return NotFound();
            }


        }




        [Authorize(Roles = "ProjectManager,Admin,StoreKeeper")]
        [HttpPost]
        public async Task<ActionResult<LaborDetailReadDto>> AddLaborDetail(LaborDetailCreateDto laborDetail)
        {

            //var newLaborDetail = _mapper.Map<LaborDetail>(laborDetail);
            var newLaborDetail =  _laborDetailRepo.CreateLaborDetail(laborDetail);
            _laborDetailRepo.SaveChanges();
            var laborDetailReadDto = _mapper.Map<LaborDetailReadDto>(newLaborDetail);

            return CreatedAtRoute(nameof(GetLaborDetail), new { id = laborDetailReadDto.LaborerID }, laborDetailReadDto);
        }

        [Authorize(Roles = "ProjectManager,Admin,StoreKeeper")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LaborDetailReadDto>> DeleteLaborDetail(int id)
        {

            try
            {
                _laborDetailRepo.DeleteLaborDetails(id);
                _laborDetailRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }



        [Authorize(Roles = "ProjectManager,Admin,StoreKeeper")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<LaborDetailReadDto>> UpdateLaborDetail(int id, [FromBody] LaborDetailCreateDto laborDetail)
        {
            try
            {

                //var newLaborDetail = _mapper.Map<LaborDetail>(laborDetail);
                _laborDetailRepo.UpdateLaborDetail(id, laborDetail);
                _laborDetailRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }




    }
}
