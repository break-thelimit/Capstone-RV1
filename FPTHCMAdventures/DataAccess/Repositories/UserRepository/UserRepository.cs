using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.UserDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> GetUserIdByUserName(string userName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if(user == null)
            {
                throw new Exception("Loi user null");
            }
            return user.Id;
        }

      
    }
}

