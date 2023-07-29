using AutoMapper;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.EventTaskService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.MajorService;
using DataAccess.Dtos.MajorDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IMajorService _majorService;
        private readonly IMapper _mapper;

        public MajorController(IMapper mapper, IMajorService majorService)
        {
            this._mapper = mapper;
            _majorService = majorService;
        }


        [HttpGet(Name = "GetMajorList")]

        public async Task<ActionResult<ServiceResponse<GetMajorDto>>> GetEventTaskList()
        {
            try
            {
                var res = await _majorService.GetMajor();
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
            var eventDetail = await _majorService.GetMajorById(id);
            return Ok(eventDetail);
        }

        [HttpPost("major", Name = "CreateMajor")]

        public async Task<ActionResult<ServiceResponse<MajorDto>>> CreateNewEvent(CreateMajorDto eventTaskDto)
        {
            try
            {
                var res = await _majorService.CreateNewMajor(eventTaskDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<MajorDto>>> UpdateNewEvent(Guid id, [FromBody] UpdateMajorDto eventDto)
        {
            try
            {
                var res = await _majorService.UpdateMajor(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("upload-excel-major")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            var serviceResponse = await _majorService.ImportDataFromExcel(file);

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

        [HttpGet("excel-template-major")]
        public async Task<IActionResult> DownloadExcelTemplate()
        {
            var serviceResponse = await _majorService.DownloadExcelTemplate();

            if (serviceResponse.Success)
            {
                // Trả về file Excel dưới dạng response
                return new FileContentResult(serviceResponse.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "SampleDataMajor.xlsx"
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
