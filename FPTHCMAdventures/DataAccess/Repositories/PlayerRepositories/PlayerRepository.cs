using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.PlayerDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PlayerRepositories
{
    public class PlayerRepository : GenericRepository<Player> , IPlayerRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public PlayerRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> GetTotalPlayerToday()
        {
            var total = _dbContext.Players.Where(p => p.CreatedAt.Equals(DateTime.Today)).Count().ToString();
            return total;
        }
    }
}