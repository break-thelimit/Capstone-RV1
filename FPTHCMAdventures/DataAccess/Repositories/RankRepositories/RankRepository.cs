using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.RankDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.RankRepositories
{
    public class RankRepository
    {
/*        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public RankRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

       public async Task<List<GetRankDto>> GetAllRankAsync()
        {
            var ranklist1 = await _dbContext.Ranks.Include(x => x.Player).Include(r => r.Event).Select(x => new GetRankDto
            {
                Id = x.Id,
                PlayerId=x.PlayerId,
                EventId=x.EventId,
                PlayerName=x.Player.Nickname,
                EventName=x.Event.Name,
                Place = x.Place
            }).ToListAsync();
            return ranklist1;
        }*/
    }
}
    
