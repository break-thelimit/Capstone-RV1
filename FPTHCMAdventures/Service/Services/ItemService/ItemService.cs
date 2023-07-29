using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.ItemInventoryDto;
using DataAccess.Repositories.ItemInventoryRepositories;
using DataAccess.Repositories.ItemRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewItem(CreateItemDto createItemDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Item>(createItemDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _itemRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetItemDto>>> GetItem()
        {
            var majorList = await _itemRepository.GetAllAsync<GetItemDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetItemDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetItemDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<ItemDto>> GetItemById(Guid eventId)
        {
            try
            {

                var eventDetail = await _itemRepository.GetAsync<ItemDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<ItemDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<ItemDto>
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

        public async Task<ServiceResponse<string>> UpdateItem(Guid id, UpdateItemDto ItemDto)
        {
            try
            {   
                ItemDto.Id = id;
                await _itemRepository.UpdateAsync(id, ItemDto);
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
            return await _itemRepository.Exists(id);
        }
       
    }
}
