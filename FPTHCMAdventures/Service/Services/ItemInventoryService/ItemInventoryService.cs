using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.InventoryDto;
using DataAccess.Dtos.ItemInventoryDto;
using DataAccess.Repositories.InventoryRepositories;
using DataAccess.Repositories.ItemInventoryRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ItemInventoryService
{
    public class ItemInventoryService : IItemInventoryService
    {
        private readonly IItemInventoryRepositories _itemInventoryRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public ItemInventoryService(IItemInventoryRepositories itemInventoryRepository, IMapper mapper)
        {
            _itemInventoryRepository = itemInventoryRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewItemInventory(CreateItemInventoryDto createItemInventoryDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<ItemIventory>(createItemInventoryDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _itemInventoryRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<ItemInventoryDto>> GetItemByItemName(string itemName)
        {
            var item = await _itemInventoryRepository.getItemByItemName(itemName);
            if(item == null)
            {
                return new ServiceResponse<ItemInventoryDto>
                {
                    Message = "No rows",
                    StatusCode = 200,
                    Success = true
                };
            }
            else
            {
                return new ServiceResponse<ItemInventoryDto>
                {
                    Data = item,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetItemInventoryDto>>> GetItemInventory()
        {
            var majorList = await _itemInventoryRepository.GetAllAsync<GetItemInventoryDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetItemInventoryDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetItemInventoryDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<ItemInventoryDto>> GetItemInventoryById(Guid eventId)
        {
            try
            {

                var eventDetail = await _itemInventoryRepository.GetAsync<ItemInventoryDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<ItemInventoryDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<ItemInventoryDto>
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

        public async Task<ServiceResponse<GetListItemInventoryByPlayer>> getListItemInventoryByPlayer(string PlayerNickName)
        {
            try
            {

                var eventDetail = await _itemInventoryRepository.GetListItemInventoryByPlayer(PlayerNickName);

                if (eventDetail == null)
                {

                    return new ServiceResponse<GetListItemInventoryByPlayer>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetListItemInventoryByPlayer>
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

        public async Task<ServiceResponse<string>> UpdateItemInventory(Guid id, UpdateItemInventoryDto ItemInventoryDto)
        {
            try
            {   
                ItemInventoryDto.Id = id;
                await _itemInventoryRepository.UpdateAsync(id, ItemInventoryDto);
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
            return await _itemInventoryRepository.Exists(id);
        }
       

       
    }
}
