using AutoMapper;
using DataAccess.Dtos.AnswerDto;
using DataAccess.Dtos.ExchangeHistoryDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.ExchangeHistoryService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.GiftService;
using DataAccess.Dtos.GiftDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly IGiftService _giftService;
        private readonly IMapper _mapper;

        public GiftsController(IMapper mapper, IGiftService giftService)
        {
            this._mapper = mapper;
            _giftService = giftService;
        }


        [HttpGet(Name = "GetGift")]

        public async Task<ActionResult<ServiceResponse<GetGiftDto>>> GetGiftList()
        {
            try
            {
                var res = await _giftService.GetGift();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("GetTotalGift")]

        public async Task<ActionResult<ServiceResponse<string>>> GetTotalGift()
        {
            try
            {
                var res = await _giftService.GetTotalGift();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDto>> GetGiftById(Guid id)
        {
            var eventDetail = await _giftService.GetGiftById(id);
            return Ok(eventDetail);
        }

        [HttpPost("Gift", Name = "CreateNewGift")]

        public async Task<ActionResult<ServiceResponse<GiftDto>>> CreateNewGift(CreateGiftDto answerDto)
        {
            try
            {
                var res = await _giftService.CreateNewGift(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<GiftDto>>> UpdateGift(Guid id, [FromBody] UpdateGiftDto eventDto)
        {
            try
            {
                var res = await _giftService.UpdateGift(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
