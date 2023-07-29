using AutoMapper;
using DataAccess.Dtos.PlayerDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.PlayerService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.RankService;
using DataAccess.Dtos.RankDto;
using Microsoft.AspNetCore.Authorization;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RanksController : ControllerBase
    {
        /*private readonly IRankService _rankService;
        private readonly IMapper _mapper;

        public RanksController(IMapper mapper, IRankService rankService)
        {
            this._mapper = mapper;
            _rankService = rankService;
        }


        [HttpGet(Name = "GetRank")]

        public async Task<ActionResult<ServiceResponse<GetRankDto>>> GetRankList()
        {
            try
            {
                var res = await _rankService.GetRank();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RankDto>> GetPlayerById(Guid id)
        {
            var eventDetail = await _rankService.GetRankById(id);
            return Ok(eventDetail);
        }

        [HttpPost("rank", Name = "CreateNewRank")]

        public async Task<ActionResult<ServiceResponse<RankDto>>> CreateNewRank(CreateRankDto answerDto)
        {
            try
            {
                var res = await _rankService.CreateNewRank(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<RankDto>>> UpdateRank(Guid id, [FromBody] UpdateRankDto eventDto)
        {
            try
            {
                var res = await _rankService.UpdateRank(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }*/
    }
}
