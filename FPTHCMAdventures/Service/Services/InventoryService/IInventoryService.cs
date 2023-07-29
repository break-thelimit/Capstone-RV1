using DataAccess.Dtos.GiftDto;
using DataAccess.Dtos.InventoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<ServiceResponse<IEnumerable<GetInventoryDto>>> GetInventory();
        Task<ServiceResponse<InventoryDto>> GetInventoryById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewInventory(CreateInventoryDto createGiftDto);
        Task<ServiceResponse<string>> UpdateInventory(Guid id, UpdateInventoryDto giftDto);
    }
}
