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
    public class ClientController:ControllerBase
    {


        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;

        public ClientController(IClientRepo clientRepo, IMapper mapper)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAllClients()
        {
            var _clients = _clientRepo.GetAllClient();

            return Ok(_mapper.Map<IEnumerable<ClientReadDto>>(_clients));
        }

        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpGet("{id:int}", Name = "GetClient")]
        public async Task<ActionResult<ClientReadDto>> GetClient(int id)
        {
            var _clients = _clientRepo.GetClient(id);
            if (_clients != null)
            {
                return Ok(_mapper.Map<ClientReadDto>(_clients));
            }
            else
            {
                return NotFound();
            }


        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPost]
        public async Task<ActionResult<ClientReadDto>> AddClient(ClientCreateDto client)
        {

            //var newClient = _mapper.Map<Client>(client);
            var newClient = _clientRepo.CreateClient(client);
            _clientRepo.SaveChanges();
            var clientReadDto = _mapper.Map<ClientReadDto>(newClient);

            return CreatedAtRoute(nameof(GetClient), new { id = clientReadDto }, clientReadDto);
        }



        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ClientReadDto>> DeleteClient(int id)
        {

            try
            {
                _clientRepo.DeleteClient(id);
                _clientRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "ProjectManager,Admin,OfficeEngineer")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ClientReadDto>> UpdateClient(int id, [FromBody] ClientCreateDto client)
        {
            try
            {

                //var newClient = _mapper.Map<Client>(client);
                _clientRepo.UpdateClient(id, client);
                _clientRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }



    }
}
