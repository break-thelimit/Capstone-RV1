using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.UserDto;
using DataAccess.Repositories.SchoolRepositories;
using DataAccess.Repositories.TaskRepositories;
using DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public UserService(IUserRepository userRepository, IMapper mapper,ISchoolRepository schoolRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _schoolRepository = schoolRepository;   
        }
        public async Task<ServiceResponse<PagedResult<UserDto>>> GetUserWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _userRepository.GetAllAsync<UserDto>(queryParameters);
            return new ServiceResponse<PagedResult<UserDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }



        public async Task<ServiceResponse<IEnumerable<UserDto>>> GetUser()
        {
            var eventList = await _userRepository.GetAllAsync<UserDto>();

            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<UserDto>>
                {
                    Data = eventList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<UserDto>>
                {
                    Data = eventList,
                    Success = false,
                    Message = "Faile because List user null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<UserDto>> GetUserByEmail(string Email)
        {
            try
            {
                List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
                {
                    x => x.RefreshTokens

                };
                var taskDetail = await _userRepository.GetByWithCondition(x => x.Email == Email, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<UserDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<UserDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<UserDto>
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

        public async Task<ServiceResponse<UserDto>> GetUserById(Guid userId)
        {
            try
            {
                List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
                {
                    x => x.RefreshTokens
                  
                };
                var taskDetail = await _userRepository.GetByWithCondition(x => x.Id == userId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<UserDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<UserDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<UserDto>
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

        public async Task<ServiceResponse<string>> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
             try
             {
                updateUserDto.Id = id;  
                await _userRepository.UpdateAsync(id, updateUserDto);
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
            return await _userRepository.Exists(id);
        }

        public async Task<ServiceResponse<IEnumerable<GetUserListWithSchoolNameDto>>> GetUserWithSchoolName()
        {
            var users = await _userRepository.GetAllAsync<GetUserDto>();
            var userList = new List<GetUserListWithSchoolNameDto>();

            if (userList != null)
            {
                foreach (var user in users)
                {
                    if(user.SchoolId != null)
                    {
                        var school = await _schoolRepository.GetAsync<SchoolDto>(user.SchoolId);
                        if (school != null)
                        {
                            if (user != null)
                            {
                                var playerData = new GetUserListWithSchoolNameDto
                                {
                                    Id = user.Id,
                                    Fullname = user.Fullname,
                                    Email = user.Email,
                                    SchoolName = school.Name,
                                    Gender = user.Gender,
                                    PhoneNumber = user.PhoneNumber,
                                    Status = user.Status,
                                    Username = user.Username


                                };
                                userList.Add(playerData);

                            }
                        }
                        else
                        {
                            var playerData = new GetUserListWithSchoolNameDto
                            {
                                Id = user.Id,
                                Fullname = user.Fullname,
                                Email = user.Email,
                                Gender = user.Gender,
                                PhoneNumber = user.PhoneNumber,
                                Status = user.Status,
                                Username = user.Username

                            };
                            userList.Add(playerData);
                        }
                    }
                    else
                    {
                        var playerData = new GetUserListWithSchoolNameDto
                        {
                            Id = user.Id,
                            Fullname = user.Fullname,
                            Email = user.Email,
                            Gender = user.Gender,
                            PhoneNumber = user.PhoneNumber,
                            Status = user.Status,
                            Username = user.Username

                        };
                        userList.Add(playerData);
                    }
                }
                return new ServiceResponse<IEnumerable<GetUserListWithSchoolNameDto>>
                {
                    Data = userList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetUserListWithSchoolNameDto>>
                {
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }
        private string PasswordHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Chuyển đổi mật khẩu sang mảng byte
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Tính toán mã hash của mật khẩu
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Chuyển đổi mã hash thành dạng chuỗi hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public async Task<ServiceResponse<UserDto>> CheckUserByUserNameAndPassword(string userName, string passWord)
        {
            try
            {
                if(userName == null || passWord == null)
                {
                    return new ServiceResponse<UserDto>
                    {
                        Message = "False username or password null",
                        StatusCode = 500,
                        Success = false
                    };
                }
                else
                {
                    string hashedPassword = PasswordHash(passWord);
                    List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
                    {
                    x => x.RefreshTokens,
                    };
                    var taskDetail = await _userRepository.GetByWithCondition(x => x.Username == userName && x.Password == hashedPassword, includes, true);
                    var _mapper = config.CreateMapper();
                    var taskDetailDto = _mapper.Map<UserDto>(taskDetail);
                    if (taskDetail == null)
                    {

                        return new ServiceResponse<UserDto>
                        {
                            Message = "No rows",
                            StatusCode = 200,
                            Success = true
                        };
                    }
                    return new ServiceResponse<UserDto>
                    {
                        Data = taskDetailDto,
                        Message = "Successfully",
                        StatusCode = 200,
                        Success = true
                    };
                }
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<UserDto>> GetUserByUserName(string userName)
        {
            try
            {
                List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
                {
                    x => x.RefreshTokens
                    
                };
                var taskDetail = await _userRepository.GetByWithCondition(x => x.Username == userName, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<UserDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<UserDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<UserDto>
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

