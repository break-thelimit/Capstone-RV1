using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.RoleDto;
using DataAccess.Repositories.RankRepositories;
using DataAccess.Repositories.RoleRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewRole(CreateRoleDto createRoleDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Role>(createRoleDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _roleRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetRoleDto>>> GetRole()
        {
            var majorList = await _roleRepository.GetAllAsync<GetRoleDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetRoleDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetRoleDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<RoleDto>> GetRoleById(Guid eventId)
        {
            try
            {

                var eventDetail = await _roleRepository.GetAsync<RoleDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<RoleDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<RoleDto>
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

        public async Task<ServiceResponse<string>> UpdateRole(Guid id, UpdateRoleDto roleDto)
        {
            try
            {   
                roleDto.Id = id;
                await _roleRepository.UpdateAsync(id, roleDto);
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
            return await _roleRepository.Exists(id);
        }
      
    }
}
