using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObjects.Model;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Exceptions;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EventRepositories
{
    public class EventRepositories : GenericRepository<Event>, IEventRepositories
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public EventRepositories(FPTHCMAdventuresDBContext dbContext,IMapper mapper) : base(dbContext,mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTaskAndEventDto>> GetTaskAndEventListByTimeNow()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime vietnamTime = ConvertToVietnamTime(utcNow);
            var status = "active";

            var getTaskAndEventDtos = await _dbContext.Events
                .Include(e => e.EventTasks).ThenInclude(et => et.Task)
                .Include(e => e.EventTasks).ThenInclude(et => et.Task.Location)
                .Include(e => e.EventTasks).ThenInclude(et => et.Task.Major)
                .Include(e => e.EventTasks).ThenInclude(et => et.Task.Npc)
                .Where(e => e.StartTime <= vietnamTime && e.EndTime > vietnamTime && e.Status == "active")
                .Select(e => new GetTaskAndEventDto
                {
                    EventName = e.Name,
                    TaskDtos = e.EventTasks
                        .Where(et => et.Task.Status == "active") // Lọc theo trạng thái "active"
                        .Select(et => _mapper.Map<TaskDto>(et.Task))
                        .ToList(),
                    StartTime = e.StartTime,
                    EndTime = e.EndTime
                })
                .ToListAsync();

             return getTaskAndEventDtos;
        }

        public DateTime ConvertToVietnamTime(DateTime dateTimeUtc)
        {
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, vietnamTimeZone);
            return vietnamTime;
        }

        public async Task<string> GetTotalEventsToday()
        {
            string total = _dbContext.Events.Where(p => p.StartTime.Equals(DateTime.Today)).Count().ToString();
            return total;
        }
    }
} 


