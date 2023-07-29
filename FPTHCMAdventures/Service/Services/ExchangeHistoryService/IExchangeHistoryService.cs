using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.LocationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ExchangeHistoryService
{
    public interface IExchangeHistoryService
    {
        Task<ServiceResponse<IEnumerable<GetExchangeHistoryDto>>> GetExchangeHistory();
        Task<ServiceResponse<ExchangeHistoryDto>> GetExchangeHistoryById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewExchangeHistory(CreateExchangeHistoryDto createEventTaskDto);
        Task<ServiceResponse<string>> UpdateExchangeHistory(Guid id, UpdateExchangeHistoryDto eventTaskDto);
        Task<ServiceResponse<IEnumerable<GetExchangeHistoryDto>>> GetAllExchangeHistory();
    }
}
