using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.GiftDto;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.ExchangeHistoryRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GiftRepositories
{
    public class GiftRepository : GenericRepository<Gift>, IGiftRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public GiftRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
      

    }
}