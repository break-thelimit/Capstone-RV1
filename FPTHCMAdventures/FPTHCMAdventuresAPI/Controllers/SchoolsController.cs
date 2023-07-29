using AutoMapper;
using DataAccess.Dtos.RoleDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.RoleService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.SchoolService;
using DataAccess.Dtos.SchoolDto;
using DataAccess;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;

        public SchoolsController(IMapper mapper, ISchoolService schoolService)
        {
            this._mapper = mapper;
            _schoolService = schoolService;
        }


        [HttpGet(Name = "GetSchool")]

        public async Task<ActionResult<ServiceResponse<GetSchoolDto>>> GetSchoolList()
        {
            try
            {
                var res = await _schoolService.GetSchool();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("school/pagination", Name = "GetSchoolListWithPagination")]

        public async Task<ActionResult<ServiceResponse<SchoolDto>>> GetLocationListWithPage([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var pagedsResult = await _schoolService.GetSchoolWithPage(queryParameters);
                return Ok(pagedsResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchoolById(Guid id)
        {
            var eventDetail = await _schoolService.GetSchoolById(id);
            return Ok(eventDetail);
        }

        [HttpPost("school", Name = "CreateNewSchool")]

        public async Task<ActionResult<ServiceResponse<SchoolDto>>> CreateNewSchool(CreateSchoolDto answerDto)
        {
            try
            {
                var res = await _schoolService.CreateNewSchool(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<SchoolDto>>> UpdateSchool(Guid id, [FromBody] UpdateSchoolDto eventDto)
        {
            try
            {
                var res = await _schoolService.UpdateSchool(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
