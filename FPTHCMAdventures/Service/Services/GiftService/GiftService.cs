using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.GiftDto;
using DataAccess.Repositories.ExchangeHistoryRepositories;
using DataAccess.Repositories.GiftRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.GiftService
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _giftRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public GiftService(IGiftRepository giftRepository, IMapper mapper)
        {
            _giftRepository = giftRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewGift(CreateGiftDto createGiftDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Gift>(createGiftDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _giftRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GiftDto>>> GetGift()
        {
            var giftList = await _giftRepository.GetAllAsync<GiftDto>();

            if (giftList != null)
            {
                return new ServiceResponse<IEnumerable<GiftDto>>
                {
                    Data = giftList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GiftDto>>
                {
                    Data = giftList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<string>> GetTotalGift()
        {
            var context = new FPTHCMAdventuresDBContext();
            try
            {
                long total = context.Gifts.Count();
                return new ServiceResponse<string>
                {
                    Data = total.ToString(),
                    Message = "Success!",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new ServiceResponse<string>
                {
                    Message = ex.ToString(),
                    Success = false,
                    StatusCode = 500
                };
            }

        }

        public async Task<ServiceResponse<GiftDto>> GetGiftById(Guid eventId)
        {
            try
            {

                var eventDetail = await _giftRepository.GetAsync<GiftDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<GiftDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GiftDto>
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

        public async Task<ServiceResponse<string>> UpdateGift(Guid id, UpdateGiftDto giftDto)
        {
            try
            {
                giftDto.Id = id;
                await _giftRepository.UpdateAsync(id, giftDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GiftExists(id))
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

       

        private async Task<bool> GiftExists(Guid id)
        {
            return await _giftRepository.Exists(id);
        }
    }
}