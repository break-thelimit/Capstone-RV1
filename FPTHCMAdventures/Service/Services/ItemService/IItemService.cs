using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.ItemInventoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponse<IEnumerable<GetItemDto>>> GetItem();
        Task<ServiceResponse<ItemDto>> GetItemById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewItem(CreateItemDto createItemDto);
        Task<ServiceResponse<string>> UpdateItem(Guid id, UpdateItemDto ItemDto);
    }
}
