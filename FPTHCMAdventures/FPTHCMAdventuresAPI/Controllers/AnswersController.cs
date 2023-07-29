using AutoMapper;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.EventTaskService;
using Service;
using System.Threading.Tasks;
using System;
using DataAccess.Repositories.AnswerRepositories;
using DataAccess.Dtos.AnswerDto;
using Service.Services.AnswerService;
using Microsoft.AspNetCore.Authorization;
using DataAccess;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IMapper _mapper;

        public AnswersController(IMapper mapper, IAnswerService answerService)
        {
            this._mapper = mapper;
            _answerService = answerService;
        }


        [HttpGet(Name = "GetAnswerList")]

        public async Task<ActionResult<ServiceResponse<GetAnswerDto>>> GetAnswerList()
        {
            try
            {
                var res = await _answerService.GetAnswer();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("GetAnswers/{majorId}", Name = "GetAnswersByMajor")]

        public async Task<ActionResult<ServiceResponse<GetAnswerAndQuestionNameDto>>> GetAnswerListWithQuestionId(Guid majorId)
        {
            try
            {
                var res = await _answerService.GetListQuestionByMajorIdAsync(majorId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("/pagination" ,Name = "GetAnswerListWithPagination")]

        public async Task<ActionResult<ServiceResponse<AnswerDto>>> GetAnswerListWithPage([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var pagedsResult = await _answerService.GetAnswerWithPage(queryParameters);
                return Ok(pagedsResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswerById(Guid id)
        {
            var eventDetail = await _answerService.GetAnswerById(id);
            return Ok(eventDetail);
        }

        [HttpPost("answer", Name = "CreateNewAnswer")]

        public async Task<ActionResult<ServiceResponse<AnswerDto>>> CreateNewAnswer(CreateAnswerDto answerDto)
        {
            try
            {
                var res = await _answerService.CreateNewAnswer(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<AnswerDto>>> UpdateAnswer(Guid id, [FromBody] UpdateAnswerDto eventDto)
        {
            try
            {
                var res = await _answerService.UpdateAnswer(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}