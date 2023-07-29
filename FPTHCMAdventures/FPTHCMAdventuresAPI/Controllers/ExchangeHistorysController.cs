using AutoMapper;
using DataAccess.Dtos.AnswerDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.AnswerService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.ExchangeHistoryService;
using DataAccess.Dtos.ExchangeHistoryDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeHistorysController : ControllerBase
    {
        private readonly IExchangeHistoryService _exchangeHistoryService;
        private readonly IMapper _mapper;

        public ExchangeHistorysController(IMapper mapper, IExchangeHistoryService exchangeHistoryService)
        {
            this._mapper = mapper;
            _exchangeHistoryService = exchangeHistoryService;
        }


        [HttpGet(Name = "GetExchangHistory")]

        public async Task<ActionResult<ServiceResponse<ExchangeHistoryDto>>> GetExchangeHistoryList()
        {
            try
            {
                var res = await _exchangeHistoryService.GetAllExchangeHistory();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetExchangeHistoryDto>> GetExchangeHistoryById(Guid id)
        {
            var eventDetail = await _exchangeHistoryService.GetExchangeHistoryById(id);
            return Ok(eventDetail);
        }

        [HttpPost("exchangeHistory", Name = "CreateNewExchangeHistory")]

        public async Task<ActionResult<ServiceResponse<GetExchangeHistoryDto>>> CreateNewExchangeHistory(CreateExchangeHistoryDto answerDto)
        {
            try
            {
                var res = await _exchangeHistoryService.CreateNewExchangeHistory(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<GetExchangeHistoryDto>>> UpdateExchangeHistory(Guid id, [FromBody] UpdateExchangeHistoryDto eventDto)
        {
            try
            {
                var res = await _exchangeHistoryService.UpdateExchangeHistory(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
