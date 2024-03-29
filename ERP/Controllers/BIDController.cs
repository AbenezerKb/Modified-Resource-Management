﻿using AutoMapper;
using ERP.DTOs;
using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace ERP.Controllers

{
    [Route("api/[controller]")]
    [ApiController]    
    public class BIDController : ControllerBase
    {

        private readonly IBIDRepo _bidRepo;
        private readonly IMapper _mapper;

        public BIDController(IBIDRepo bidRepo, IMapper mapper)
        {
            _bidRepo = bidRepo;
            _mapper = mapper;

        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BIDReadDto>>> GetAllBIDs()
        {            
            var _bids = _bidRepo.GetAllBIDs();

            return Ok(_mapper.Map<IEnumerable<BIDReadDto>>(_bids));
        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet("{id:int}", Name = "GetBID")]
        public async Task<ActionResult<BIDReadDto>> GetBID(int id)
        {

            var _bids = _bidRepo.GetBID(id);
            if (_bids != null)
            {
                return Ok(_mapper.Map<BIDReadDto>(_bids));
            }
            else
            {
                return NotFound();
            }


        }




        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPost]
        public async Task<ActionResult<BIDReadDto>> AddBID(BIDCreateDto bid)
        {

            //var newBid = _mapper.Map<BID>(bid);
            var newBid =  _bidRepo.CreateBID(bid);
            _bidRepo.SaveChanges();            
            var bidReadDto = _mapper.Map<BIDReadDto>(newBid);

            return CreatedAtRoute(nameof(GetBID), new { id = bidReadDto.BIDID }, bidReadDto);
        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BIDReadDto>> DeleteBID(int id)
        {

            try
            {
                _bidRepo.DeleteBID(id);
                _bidRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }





        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BIDReadDto>> UpdateBID(int id, [FromBody] BIDCreateDto bid)
        {
            try
            {

                //var newBid = _mapper.Map<BID>(bid);
                _bidRepo.UpdateBID(id, bid);
                _bidRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }








    }
}



