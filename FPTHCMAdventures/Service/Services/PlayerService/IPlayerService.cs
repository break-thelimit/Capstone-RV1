using BusinessObjects.Model;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.PlayerHistoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServiceResponse<IEnumerable<GetPlayerDto>>> GetPlayer();
        Task<ServiceResponse<IEnumerable<GetPlayerWithUserNameDto>>> GetPlayerWithUserName();
        Task<ServiceResponse<PlayerDto>> GetPlayerById(Guid eventId);
        Task<ServiceResponse<PlayerDto>> GetPlayerByUserId(Guid userId);
        Task<ServiceResponse<PlayerDto>> CheckPlayerByUserName(string username);
        Task<ServiceResponse<GetPlayerDto>> CheckPlayerByNickName(string nickName);
        Task<ServiceResponse<Guid?>> CreateNewPlayer(CreatePlayerDto createPlayerDto);
        Task<ServiceResponse<string>> UpdatePlayer(Guid id, UpdatePlayerDto PlayerDto);
        Task<ServiceResponse<IEnumerable<Player>>> GetTop5PlayerInRank();
        Task<ServiceResponse<string>> GetTotalPlayerToday();

    }
}
