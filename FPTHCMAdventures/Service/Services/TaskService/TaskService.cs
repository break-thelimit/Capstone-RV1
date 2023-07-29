using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Repositories.EventRepositories;
using DataAccess.Repositories.EventTaskRepositories;
using DataAccess.Repositories.TaskRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task = BusinessObjects.Model.Task;

namespace Service.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepositories _taskRepository;
        private readonly IEventRepositories _eventRepository;
        private readonly IEventTaskRepository _eventTaskRepository;

        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public TaskService(ITaskRepositories taskRepository, IMapper mapper,IEventTaskRepository eventTaskRepository , IEventRepositories eventRepositories)
        {
            _taskRepository = taskRepository;
            _eventRepository = eventRepositories;
            _eventTaskRepository = eventTaskRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewTask(CreateTaskDto createEventDto)
        {
            var mapper = config.CreateMapper();
            var taskcreate = mapper.Map<Task>(createEventDto);
            taskcreate.Id = Guid.NewGuid();
            await _taskRepository.AddAsync(taskcreate);

            return new ServiceResponse<Guid>
            {
                Data = taskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }


        public async Task<ServiceResponse<IEnumerable<Task>>> GetTaskDoneByMajor(Guid majorId)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                List<Task> taskList = context.Tasks.Include(t => t.PlayHistories).Where(t => (t.PlayHistories.Count > 0) && (t.MajorId == majorId)).ToList();

                if (taskList != null)
                {
                    return new ServiceResponse<IEnumerable<Task>>
                    {
                        Data = taskList,
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ServiceResponse<IEnumerable<Task>>
                    {
                        Data = taskList,
                        Success = false,
                        Message = "Failed because List task null",
                        StatusCode = 200
                    };
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResponse<IEnumerable<TaskDto>>> GetTask()
        {
            List<Expression<Func<Task, object>>> includes = new List<Expression<Func<Task, object>>>
                {
                    x => x.Location,
                    x => x.Major,
                    x => x.Npc
                };
            var eventList = await _taskRepository.GetAllAsync<TaskDto>();

            
            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<TaskDto>>
                {
                    Data = eventList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<TaskDto>>
                {
                    Data = eventList,
                    Success = false,
                    Message = "Failed because List task null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<TaskDto>> GetTaskById(Guid eventId)
        {
            try
            {
                List<Expression<Func<Task, object>>> includes = new List<Expression<Func<Task, object>>>
                {
                    x => x.PlayHistories,
                    x => x.EventTasks,
                };
                var taskDetail = await _taskRepository.GetByWithCondition(x => x.Id == eventId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<TaskDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<TaskDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<TaskDto>
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

        public async Task<ServiceResponse<string>> UpdateTask(Guid id, UpdateTaskDto updateTaskDto)
        {
            try
            {
                updateTaskDto.Id = id;
                await _taskRepository.UpdateAsync(id, updateTaskDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
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
        private async Task<bool> CountryExists(Guid id)
        {
            return await _taskRepository.Exists(id);
        }

      
    }
}
