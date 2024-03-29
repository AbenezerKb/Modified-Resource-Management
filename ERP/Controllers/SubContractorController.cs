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
    public class SubContractorController: ControllerBase
    {



        private readonly ISubContractorRepo _subcontractRepo;
        private readonly IMapper _mapper;

        public SubContractorController(ISubContractorRepo subcontractRepo, IMapper mapper)
        {
            _subcontractRepo = subcontractRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubContractorReadDto>>> GetAllSubContractors()
        {
            
            var _subcontracts = _subcontractRepo.GetAllSubContractors();            

            return Ok(_mapper.Map<IEnumerable<SubContractorReadDto>>(_subcontracts));
        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet("{id:int}", Name = "GetSubContractor")]
        public async Task<ActionResult<SubContractorReadDto>> GetSubContractor(int id)
        {
           
            var _subcontracts = _subcontractRepo.GetSubContractor(id);
            if (_subcontracts != null)
            {
                return Ok(_mapper.Map<SubContractorReadDto>(_subcontracts));
            }
            else
            {
                return NotFound();
            }


        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPost]
        public async Task<ActionResult<SubContractorReadDto>> AddSubContractor(SubContractorCreateDto contract)
        {

            //var newContract = _mapper.Map<SubContractor>(contract);
            var newContract = _subcontractRepo.CreateSubContractor(contract);
            _subcontractRepo.SaveChanges();
         
            var subcontractReadDto = _mapper.Map<SubContractorReadDto>(newContract);

            return CreatedAtRoute(nameof(GetSubContractor), new { id = subcontractReadDto.SubId }, subcontractReadDto);
        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SubContractorReadDto>> DeleteSubContractor(int id)
        {

            try
            {
                _subcontractRepo.DeleteSubContractor(id);
                _subcontractRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }







        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SubContractorReadDto>> UpdateSubContractor(int id, [FromBody] SubContractorCreateDto subContractor)
        {
            try
            {

                //var newSubContractor = _mapper.Map<SubContractor>(subContractor);
                 _subcontractRepo.UpdateSubContractor(id, subContractor);
                _subcontractRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


    }
}
