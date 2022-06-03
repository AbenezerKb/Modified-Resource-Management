
namespace ERP.Controllers
{
   
using AutoMapper;
using ERP.DTOs;
using ERP.Models;
using ERP.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController:ControllerBase
    {
        private readonly IIncidentRepo _incidentRepo;
        private readonly IMapper _mapper;

        public IncidentController(IIncidentRepo incidentRepo, IMapper mapper)
        {
            _incidentRepo = incidentRepo;
            _mapper = mapper;

        }

        [Authorize(Roles = "ProjectManager,Admin,SiteEngineer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentReadDto>>> GetAllIncidents()
        {
            Console.WriteLine("....started");
            var _incidents = _incidentRepo.GetAllIncidents();
            Console.WriteLine("....started", _incidents);

            return Ok(_mapper.Map<IEnumerable<IncidentReadDto>>(_incidents));
        }

        [Authorize(Roles = "ProjectManager,Admin,SiteEngineer")]
        [HttpGet("{id:int}", Name = "GetIncident")]
        public async Task<ActionResult<IncidentReadDto>> GetIncident(int id)
        {            
            var _incidents = _incidentRepo.GetIncident(id);
            
            if (_incidents != null)
            {
                return Ok(_mapper.Map<IncidentReadDto>(_incidents));
            }
            else
            {
                return NotFound();
            }


        }

        [Authorize(Roles = "ProjectManager,Admin,SiteEngineer")]
        [HttpPost]
        public async Task<ActionResult<IncidentReadDto>> AddIncident(IncidentCreateDto incident)
        {

            //var newIncident = _mapper.Map<Incident>(incident);
            var newIncident = _incidentRepo.CreateIncident(incident);
            _incidentRepo.SaveChanges();
            Console.WriteLine("the incident id: " + newIncident.incidentNo);
            var incidentReadDto = _mapper.Map<IncidentReadDto>(newIncident);

            return CreatedAtRoute(nameof(GetIncident), new { id = incidentReadDto.incidentNo }, incidentReadDto);
        }
        
        [Authorize(Roles = "ProjectManager,Admin,SiteEngineer")]        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<IncidentReadDto>> DeleteIncident(int id)
        {

            try
            {
                _incidentRepo.DeleteIncident(id);
                _incidentRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "ProjectManager,Admin,SiteEngineer")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<IncidentReadDto>> UpdateIncident(int id, [FromBody] IncidentCreateDto incident)
        {
            try
            {

                //var newIncident = _mapper.Map<Incident>(incident);
                _incidentRepo.UpdateIncident(id, incident);
                _incidentRepo.SaveChanges();
                return Ok("Success");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


    }
}
