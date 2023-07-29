using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.PlayerHistoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PlayerHistoryService
{
    public interface IPlayerHistoryService
    {
        Task<ServiceResponse<IEnumerable<GetPlayerHistoryDto>>> GetPlayerHistory();
        Task<ServiceResponse<PlayerHistoryDto>> GetPlayerHistoryById(Guid eventId);
        Task<ServiceResponse<PlayerHistoryDto>> GetPlayerHistoryByTaskId(Guid taskId);
        Task<ServiceResponse<PlayerHistoryDto>> GetPlayerHistoryByTaskIdAndPlayerId(Guid taskId, Guid PlayerId);
        Task<ServiceResponse<Guid>> CreateNewPlayerHistory(CreatePlayerHistoryDto createPlayerHistoryDto);
        Task<ServiceResponse<string>> UpdatePlayerHistory(Guid id, UpdatePlayerHistoryDto PlayerHistoryDto);
    }
}
