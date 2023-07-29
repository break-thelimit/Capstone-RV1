using AutoMapper;
using DataAccess.Dtos.EventDto;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using Service.Services.EventService;
using Service.Services.EventTaskService;
using System.Threading.Tasks;
using System;
using DataAccess.Dtos.EventTaskDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTasksController : ControllerBase
    {
        private readonly IEventTaskService _eventTaskService;
        private readonly IMapper _mapper;

        public EventTasksController(IMapper mapper,IEventTaskService eventTaskService)
        {
            this._mapper = mapper;
            _eventTaskService = eventTaskService;
        }


        [HttpGet(Name = "GetEventTaskList")]

        public async Task<ActionResult<ServiceResponse<GetEventTaskDto>>> GetEventTaskList()
        {
            try
            {
                var res = await _eventTaskService.GetEventTask();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EventTaskDto>> GetEventTaskById(Guid id)
        {
            var eventDetail = await _eventTaskService.GetEventById(id);
            return Ok(eventDetail);
        }

        [HttpGet("eventtask/{taskId}")]
        public async Task<ActionResult<EventTaskDto>> GetEventTaskByTaskId(Guid taskId)
        {
            var eventDetail = await _eventTaskService.GetEventTaskByTaskId(taskId);
            return Ok(eventDetail);
        }
        [HttpPost("eventtask", Name = "CreateNewEventTask")]

        public async Task<ActionResult<ServiceResponse<EventDto>>> CreateNewEvent(CreateEventTaskDto eventTaskDto)
        {
            try
            {
                var res = await _eventTaskService.CreateNewEventTask(eventTaskDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<EventDto>>> UpdateEvent(Guid id, [FromBody] UpdateEventTaskDto eventDto)
        {
            try
            {
                var res = await _eventTaskService.UpdateTaskEvent(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
