using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.EventTaskRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ExchangeHistoryRepositories
{
    public class ExchangeHistoryRepository : GenericRepository<ExchangeHistory>, IExchangeHistoryRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public ExchangeHistoryRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetExchangeHistoryDto>> GetAllExchangeHistoryRepository()
        {
            var userlist1 = await _dbContext.ExchangeHistories.Include(x => x.Player).Include(r => r.Item).Select(x => new GetExchangeHistoryDto
            {
                Id = x.Id,
                PlayerId=x.PlayerId,
                PlayerNickname=x.Player.Nickname,
                ItemId=x.ItemId,
                ItemName=x.Item.Name,
                ExchangeDate=x.ExchangeDate
            }).ToListAsync();
            return userlist1;
        }

    }
}