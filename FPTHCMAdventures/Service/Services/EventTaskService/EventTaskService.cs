using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Repositories.EventRepositories;
using DataAccess.Repositories.EventTaskRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.EventTaskService
{
    public class EventTaskService : IEventTaskService
    {
        private readonly IEventTaskRepository _eventTaskRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public EventTaskService(IEventTaskRepository eventTaskRepository, IMapper mapper)
        {
            _eventTaskRepository = eventTaskRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewEventTask(CreateEventTaskDto createEventTaskDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<EventTask>(createEventTaskDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _eventTaskRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetEventTaskDto>>> GetEventTask()
        {
            var eventList = await _eventTaskRepository.GetAllAsync<GetEventTaskDto>();
           
            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<GetEventTaskDto>>
                {
                    Data = eventList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetEventTaskDto>>
                {
                    Data = eventList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<EventTaskDto>> GetEventById(Guid eventId)
        {
            try
            {
                var eventDetail = await _eventTaskRepository.GetAsync<EventTaskDto>(eventId);
                if (eventDetail == null)
                {

                    return new ServiceResponse<EventTaskDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EventTaskDto>
                {
                    Data = eventDetail,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> UpdateTaskEvent(Guid id, UpdateEventTaskDto eventTaskDto)
        {
            try
            {
                eventTaskDto.Id = id;   
                await _eventTaskRepository.UpdateAsync(id, eventTaskDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventTaskExists(id))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Invalid Record Id",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _eventTaskRepository.Exists(id);
        }

        public async Task<ServiceResponse<EventTaskDto>> GetEventTaskByTaskId(Guid taskId)
        {
            try
            {
                List<Expression<Func<EventTask, object>>> includes = new List<Expression<Func<EventTask, object>>>
                {
                   

                };
                var taskDetail = await _eventTaskRepository.GetByWithCondition(x => x.TaskId == taskId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<EventTaskDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<EventTaskDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EventTaskDto>
                {
                    Data = taskDetailDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
