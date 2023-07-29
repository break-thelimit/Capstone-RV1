using AutoMapper;
using DataAccess.Dtos.LocationDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.LocationServoce;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.NpcService;
using DataAccess.Dtos.NPCDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NpcsController : ControllerBase
    {
        private readonly INpcService _npcService;
        private readonly IMapper _mapper;

        public NpcsController(IMapper mapper, INpcService npcService)
        {
            this._mapper = mapper;
            _npcService = npcService;
        }


        [HttpGet(Name = "GetNpcList")]

        public async Task<ActionResult<ServiceResponse<GetNpcDto>>> GetNpcList()
        {
            try
            {
                var res = await _npcService.GetNpc();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NpcDto>> GetNpcById(Guid id)
        {
            var eventDetail = await _npcService.GetNpcById(id);
            return Ok(eventDetail);
        }

        [HttpPost("npc", Name = "CreateNpc")]

        public async Task<ActionResult<ServiceResponse<NpcDto>>> CreateNpc(CreateNpcDto eventTaskDto)
        {
            try
            {
                var res = await _npcService.CreateNewNpc(eventTaskDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<NpcDto>>> UpdateNpc(Guid id, [FromBody] UpdateNpcDto eventDto)
        {
            try
            {
                var res = await _npcService.UpdateNpc(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}


