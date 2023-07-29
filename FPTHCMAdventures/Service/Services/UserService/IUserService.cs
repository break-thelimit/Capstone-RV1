using DataAccess;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<UserDto>>> GetUser();
        Task<ServiceResponse<IEnumerable<GetUserListWithSchoolNameDto>>> GetUserWithSchoolName();
        Task<ServiceResponse<UserDto>> GetUserById(Guid userId);
        Task<ServiceResponse<UserDto>> GetUserByEmail(string Email);
        Task<ServiceResponse<UserDto>> GetUserByUserName(string userName);
        Task<ServiceResponse<UserDto>> CheckUserByUserNameAndPassword(string userName,string passWord);
        Task<ServiceResponse<PagedResult<UserDto>>> GetUserWithPage(QueryParameters queryParameters);

        Task<ServiceResponse<string>> UpdateUser(Guid id, UpdateUserDto updateUserDto);
    }
}
