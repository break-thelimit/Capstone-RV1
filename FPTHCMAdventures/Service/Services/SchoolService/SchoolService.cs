using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.SchoolEventDto;
using DataAccess.Repositories.SchoolEventRepositories;
using DataAccess.Repositories.SchoolRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.SchoolService
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public SchoolService(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewSchool(CreateSchoolDto createSchoolDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<School>(createSchoolDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _schoolRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetSchoolDto>>> GetSchool()
        {
            var majorList = await _schoolRepository.GetAllAsync<GetSchoolDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetSchoolDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetSchoolDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<SchoolDto>> GetSchoolById(Guid eventId)
        {
            try
            {

                var eventDetail = await _schoolRepository.GetAsync<SchoolDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<SchoolDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<SchoolDto>
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
        public async Task<ServiceResponse<PagedResult<SchoolDto>>> GetSchoolWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _schoolRepository.GetAllAsync<SchoolDto>(queryParameters);
            return new ServiceResponse<PagedResult<SchoolDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<string>> UpdateSchool(Guid id, UpdateSchoolDto schoolDto)
        {
            try
            {   
                
                schoolDto.Id = id;
                await _schoolRepository.UpdateAsync(id, schoolDto);
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
            return await _schoolRepository.Exists(id);
        }

       

    }
}
