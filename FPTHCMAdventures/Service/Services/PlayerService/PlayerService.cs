using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.UserDto;
using DataAccess.Repositories.InventoryRepositories;
using DataAccess.Repositories.PlayerHistoryRepositories;
using DataAccess.Repositories.PlayerRepositories;
using DataAccess.Repositories.SchoolRepositories;
using DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace Service.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public PlayerService(IPlayerRepository playerRepository, IMapper mapper,IUserRepository userRepository, ISchoolRepository schoolRepository,IInventoryRepository inventoryRepository)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _schoolRepository = schoolRepository;   
            _inventoryRepository = inventoryRepository;
        }

        

        public async Task<ServiceResponse<PlayerDto>> CheckPlayerByUserName(string username)
        {
            var userId = await _userRepository.GetUserIdByUserName(username);
            if(userId != null)
            {
                try
                {
                    List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                    {
                        x => x.Inventories,
                        x => x.ExchangeHistories,
                        x => x.PlayHistories
                    };
                    var taskDetail = await _playerRepository.GetByWithCondition(x => x.UserId == userId, includes, true);
                    var _mapper = config.CreateMapper();
                    var taskDetailDto = _mapper.Map<PlayerDto>(taskDetail);
                    if (taskDetail == null)
                    {

                        return new ServiceResponse<PlayerDto>
                        {
                            Message = "No rows",
                            StatusCode = 200,
                            Success = true
                        };
                    }
                    return new ServiceResponse<PlayerDto>
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
            else
            {
                return new ServiceResponse<PlayerDto>
                {
                    Message = "No User",
                    StatusCode = 404,
                    Success = false
                };
            }
          
        }

        public async Task<ServiceResponse<Guid?>> CreateNewPlayer(CreatePlayerDto createPlayerDto)
        {
            var playerNickName = await CheckPlayerByNickName(createPlayerDto.Nickname);
            if(playerNickName.Data == null)
            {
                var mapper = config.CreateMapper();
                var eventTaskcreate = mapper.Map<Player>(createPlayerDto);
                eventTaskcreate.Id = Guid.NewGuid();
                eventTaskcreate.CreatedAt = DateTime.Now;
                await _playerRepository.AddAsync(eventTaskcreate);

                return new ServiceResponse<Guid?>
                {
                    Data = eventTaskcreate.Id,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 201
                };
            }
            else
            {
                return new ServiceResponse<Guid?>
                {
                    Data = null,
                    Message = "Failed because nick name is exists",
                    Success = false,
                    StatusCode = 404
                };
            }
          
        }
        public async Task<ServiceResponse<GetPlayerDto>> CheckPlayerByNickName(string nickName)
        {
            try
            {
                List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                {
                    x => x.PlayHistories,
                    x => x.Inventories,
                    x => x.ExchangeHistories,
                    
                };
                var taskDetail = await _playerRepository.GetByWithCondition(x => x.Nickname.Equals(nickName), null, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<GetPlayerDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<GetPlayerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetPlayerDto>
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
        public async Task<ServiceResponse<IEnumerable<GetPlayerDto>>> GetPlayer()
        {
            var majorList = await _playerRepository.GetAllAsync<GetPlayerDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetPlayerDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetPlayerDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<PlayerDto>> GetPlayerById(Guid eventId)
        {
            try
            {

                var eventDetail = await _playerRepository.GetAsync<PlayerDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<PlayerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<PlayerDto>
                {
                    Data = eventDetail,
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

        public async Task<ServiceResponse<PlayerDto>> GetPlayerByUserId(Guid userId)
        {
            try
            {
                List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                {
                    x => x.Inventories,
                    x => x.ExchangeHistories,
                    x => x.PlayHistories
                };
                var taskDetail = await _playerRepository.GetByWithCondition(x => x.UserId == userId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<PlayerDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<PlayerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<PlayerDto>
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

        public async Task<ServiceResponse<IEnumerable<GetPlayerWithUserNameDto>>> GetPlayerWithUserName()
        {
            var players= await _playerRepository.GetAllAsync<GetPlayerDto>();
            var playerList = new List<GetPlayerWithUserNameDto>();

            if (playerList != null)
            {
                foreach (var player in players)
                {
                    var user = await _userRepository.GetAsync<UserDto>(player.UserId);
                    if(user != null)
                    {
                        var playerData = new GetPlayerWithUserNameDto
                        {
                            Id = player.Id,
                            Fullname = user.Fullname,
                            TotalPoint = player.TotalPoint,
                            TotalTime = player.TotalTime,
                            Nickname = player.Nickname,
                            
                        };
                        playerList.Add(playerData);

                    }
                }
                return new ServiceResponse<IEnumerable<GetPlayerWithUserNameDto>>
                {
                    Data = playerList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetPlayerWithUserNameDto>>
                {
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<string>> UpdatePlayer(Guid id, UpdatePlayerDto PlayerDto)
        {
            try
            {
                PlayerDto.Id = id;
                await _playerRepository.UpdateAsync(id, PlayerDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventTaskExists(id))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Invalid Record Id",
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
        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _playerRepository.Exists(id);
        }
        public async Task<ServiceResponse<IEnumerable<Player>>> GetTop5PlayerInRank()
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                List<Player> top5playerlist = context.Players.OrderByDescending(x => x.TotalPoint).ThenBy(x => x.TotalTime).Take(5).ToList();
                return new ServiceResponse<IEnumerable<Player>>
                {
                    Data = top5playerlist,
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ServiceResponse<IEnumerable<Player>>
                {
                    Message = "Invalid Record Id",
                    Success = false,
                    StatusCode = 500
                };
            }
        }

        public async Task<ServiceResponse<string>> GetTotalPlayerToday()
        {
           var total= await _playerRepository.GetTotalPlayerToday();
           return new ServiceResponse<string>
            {
                Data = total,
                Message = "Success",
                Success = true,
                StatusCode = 202
            };
        }
    }
}
