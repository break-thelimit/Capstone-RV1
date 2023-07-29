using AutoMapper;
using DataAccess.Dtos.RoleDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.RoleService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.SchoolEventService;
using DataAccess.Dtos.SchoolEventDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolEventsController : ControllerBase
    {
        private readonly ISchoolEventService _schoolEventService;
        private readonly IMapper _mapper;

        public SchoolEventsController(IMapper mapper, ISchoolEventService schoolEventService)
        {
            this._mapper = mapper;
            _schoolEventService = schoolEventService;
        }


        [HttpGet(Name = "GetSchoolEvent")]

        public async Task<ActionResult<ServiceResponse<GetSchoolEventDto>>> GetSchoolEventList()
        {
            try
            {
                var res = await _schoolEventService.GetSchoolEvent();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolEventDto>> GetSchoolEventById(Guid id)
        {
            var eventDetail = await _schoolEventService.GetSchoolEventById(id);
            return Ok(eventDetail);
        }

        [HttpPost("schoolevent", Name = "CreateNewSchoolEvent")]

        public async Task<ActionResult<ServiceResponse<SchoolEventDto>>> CreateNewSchoolEvent(CreateSchoolEventDto answerDto)
        {
            try
            {
                var res = await _schoolEventService.CreateNewSchoolEvent(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<SchoolEventDto>>> UpdateSchoolEvent(Guid id, [FromBody] UpdateSchoolEventDto eventDto)
        {
            try
            {
                var res = await _schoolEventService.UpdateSchoolEvent(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
