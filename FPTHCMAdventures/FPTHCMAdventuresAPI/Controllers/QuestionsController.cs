using AutoMapper;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.MajorDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.MajorService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.QuestionService;
using DataAccess.Dtos.QuestionDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionsController(IMapper mapper, IQuestionService questionService)
        {
            this._mapper = mapper;
            _questionService = questionService;
        }


        [HttpGet(Name = "GetQuestionList")]

        public async Task<ActionResult<ServiceResponse<QuestionDto>>> GetQuestionList()
        {
            try
            {
                var res = await _questionService.GetQuestion();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MajorDto>> GetEventTaskById(Guid id)
        {
            var eventDetail = await _questionService.GetQuestionById(id);
            return Ok(eventDetail);
        }

        [HttpPost("question", Name = "CreateQuestion")]

        public async Task<ActionResult<ServiceResponse<MajorDto>>> CreateNewQuestion(CreateQuestionDto eventTaskDto)
        {
            try
            {
                var res = await _questionService.CreateNewQuestion(eventTaskDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<MajorDto>>> CreateNewEvent(Guid id, [FromBody] UpdateQuestionDto eventDto)
        {
            try
            {
                var res = await _questionService.UpdateQuestion(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("upload-excel-question")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            var serviceResponse = await _questionService.ImportDataFromExcel(file);

            if (serviceResponse.Success)
            {
                // Xử lý thành công
                return Ok(serviceResponse.Message);
            }
            else
            {
                // Xử lý lỗi
                return BadRequest(serviceResponse.Message);
            }
        }

        [HttpGet("excel-template-question")]
        public async Task<IActionResult> DownloadExcelTemplate()
        {
            var serviceResponse = await _questionService.DownloadExcelTemplate();

            if (serviceResponse.Success)
            {
                // Trả về file Excel dưới dạng response
                return new FileContentResult(serviceResponse.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "SampleDataQuestion.xlsx"
                };
            }
            else
            {
                // Xử lý lỗi nếu có
                return BadRequest(serviceResponse.Message);
            }
        }
    }
}

