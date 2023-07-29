using BusinessObjects.Model;
using DataAccess.Dtos.UserDto;
using DataAccess.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<Guid> GetUserIdByUserName(string userName);
    }
}
