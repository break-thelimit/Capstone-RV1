using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.NPCDto;
using DataAccess.Repositories.MajorRepositories;
using DataAccess.Repositories.NPCRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.NpcService
{
    public class NpcService : INpcService
    {
        private readonly INpcRepository _npcRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public NpcService(INpcRepository npcRepository, IMapper mapper)
        {
            _npcRepository = npcRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewNpc(CreateNpcDto createMajorDto)
        {
            var eventTaskcreate = _mapper.Map<Npc>(createMajorDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _npcRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<NpcDto>> GetNpcById(Guid eventId)
        {
            try
            {

                var eventDetail = await _npcRepository.GetAsync<NpcDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<NpcDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<NpcDto>
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

        public async Task<ServiceResponse<IEnumerable<NpcDto>>> GetNpc()
        {
            var majorList = await _npcRepository.GetAllAsync<NpcDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<NpcDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<NpcDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List npc null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<string>> UpdateNpc(Guid id, UpdateNpcDto majorDto)
        {
            try
            {   
                majorDto.Id = id;
                await _npcRepository.UpdateAsync(id, majorDto);
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
            return await _npcRepository.Exists(id);
        }

        public async Task<ServiceResponse<NpcDto>> GetNpcByName(string npcName)
        {
            try
            {

                var npcDetail = await _npcRepository.GetNpcByName(npcName);

                if (npcDetail == null)
                {

                    return new ServiceResponse<NpcDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<NpcDto>
                {
                    Data = npcDetail,
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
