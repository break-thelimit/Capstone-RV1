using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.LocationDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Exceptions;
using DataAccess.Repositories.EventRepositories;
using DataAccess.Repositories.RankRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IEventRepositories _eventRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public EventService(IEventRepositories eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Guid>> CreateNewEvent(CreateEventDto createEventDto)
        {
            var mapper = config.CreateMapper();
            var eventcreate = mapper.Map<Event>(createEventDto);
            eventcreate.Id = Guid.NewGuid();
            await _eventRepository.AddAsync(eventcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }
       

        public async Task<ServiceResponse<IEnumerable<GetEventDto>>> GetEvent()
        {
            var eventList = await _eventRepository.GetAllAsync();
            var _mapper = config.CreateMapper();
            var lstDto = _mapper.Map<List<GetEventDto>>(eventList);
            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<GetEventDto>>
                {
                    Data = lstDto,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetEventDto>>
                {
                    Data = lstDto,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<EventDto>> GetEventById(Guid eventId)
        {
            try
            {
                List<Expression<Func<Event, object>>> includes = new List<Expression<Func<Event, object>>>
                {
                    x => x.SchoolEvents,
                    x => x.EventTasks,
                };
                var eventDetail = await _eventRepository.GetByWithCondition(x => x.Id == eventId, includes, true);
                var _mapper = config.CreateMapper();
                var eventDetailDto = _mapper.Map<EventDto>(eventDetail);
                if (eventDetail == null)
                {

                    return new ServiceResponse<EventDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EventDto>
                {
                    Data = eventDetailDto,
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

       
        public async Task<ServiceResponse<string>> UpdateEvent(Guid id, UpdateEventDto eventDto)
        {
            try
            {
                eventDto.Id = id;
                await _eventRepository.UpdateAsync(id, eventDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    throw;
                }
            }
            return new ServiceResponse<string>
            {
                Message = "Update Success",
                Success = true,
                StatusCode = 200
            };
        }

        private async Task<bool> CountryExists(Guid id)
        {
            return await _eventRepository.Exists(id);
        }


        public async Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file)
        {

            if (file == null || file.Length <= 0)
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "No file uploaded.",
                    Success = false,
                    StatusCode = 400
                };
            }

            if (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "Invalid file format. Only Excel files are allowed.",
                    Success = false,
                    StatusCode = 400
                };
            }
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet != null)
                        {
                            var rowCount = worksheet.Dimension.Rows;
                            var dataList = new List<EventDto>();
                           
                            for (int row = 2; row <= rowCount; row++)
                            {
                                var data = new EventDto
                                {
                                    Id = Guid.NewGuid(),
                                    Name = worksheet.Cells[row, 1].Value?.ToString(),
                                    StartTime = Convert.ToDateTime(worksheet.Cells[row, 2].Value),
                                    EndTime = Convert.ToDateTime(worksheet.Cells[row, 3].Value),
                                    Status = worksheet.Cells[row, 4].Value?.ToString()

                                };

                                dataList.Add(data);
                            }

                            // Start from row 2 to skip the header row

                            var mapper = config.CreateMapper();
                            var locations = _mapper.Map<List<Event>>(dataList);
                            await _eventRepository.AddRangeAsync(locations);
                            await _eventRepository.SaveChangesAsync();

                        }
                    }
                }
                return new ServiceResponse<string>
                {
                    Data = "Upload successful.",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "Failed to process uploaded file.",
                    Success = false,
                    StatusCode = 500
                };
            }
        }
        public byte[] GenerateExcelTemplate()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SampleDataEvent");

                // Thiết lập header cho các cột
                worksheet.Cells[1, 1].Value = "Event Name";
                worksheet.Cells[1, 2].Value = "Start Time";
                worksheet.Cells[1, 3].Value = "End Time";
                worksheet.Cells[1, 4].Value = "Status";

                // Thiết lập kiểm tra dữ liệu cho cột StartTime
                var startTimeColumn = worksheet.Cells["B2"]; // Dòng 2

                var startTimeValidation = startTimeColumn.DataValidation.AddCustomDataValidation();
                startTimeValidation.ShowErrorMessage = true;
                startTimeValidation.ErrorTitle = "Lỗi";
                startTimeValidation.Error = "Vui lòng nhập ngày và giờ theo định dạng chính xác.";
                startTimeValidation.Formula.ExcelFormula = "AND(ISNUMBER(B2), B2>=DATE(1900,1,1), B2<=DATE(9999,12,31))";

                // Tạo dữ liệu mẫu cho cột StartTime
                DateTime startTime = new DateTime(2023, 7, 4, 10, 30, 0); // Ví dụ: 2023-07-04 10:30:00 AM
                worksheet.Cells[2, 2].Value = startTime.ToString("yyyy-MM-dd HH:mm:ss");

                // Thiết lập kiểm tra dữ liệu cho cột EndTime
                var endTimeColumn = worksheet.Cells["C2"]; // Dòng 2

                var endTimeValidation = endTimeColumn.DataValidation.AddCustomDataValidation();
                endTimeValidation.ShowErrorMessage = true;
                endTimeValidation.ErrorTitle = "Lỗi";
                endTimeValidation.Error = "Vui lòng nhập ngày và giờ theo định dạng chính xác.";
                endTimeValidation.Formula.ExcelFormula = "AND(ISNUMBER(C2), C2>=DATE(1900,1,1), C2<=DATE(9999,12,31))";

                // Tạo dữ liệu mẫu cho cột EndTime
                DateTime endTime = startTime.AddHours(2); // Kết thúc sự kiện sau 2 giờ
                worksheet.Cells[2, 3].Value = endTime.ToString("yyyy-MM-dd HH:mm:ss");

                // Lưu file Excel vào MemoryStream
                var stream = new MemoryStream(package.GetAsByteArray());
                return stream.ToArray();
            }
        }






        public async Task<ServiceResponse<byte[]>> DownloadExcelTemplate()
        {
            byte[] fileContents;
            try
            {
                fileContents = GenerateExcelTemplate();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new ServiceResponse<byte[]>
                {
                    Data = null,
                    Message = "Failed to generate Excel template.",
                    Success = false,
                    StatusCode = 500
                };
            }

            // Trả về file Excel dưới dạng byte array
            return new ServiceResponse<byte[]>
            {
                Data = fileContents,
                Success = true,
                StatusCode = 200
            };
        }


        public async Task<ServiceResponse<IEnumerable<GetEventDto>>> GetEventByDate(DateTime dateTimeStart)
        {
            try
            {
                DateTime startDate = dateTimeStart.Date; // Lấy ngày bắt đầu của ngày đầu vào
                DateTime endDate = startDate.AddDays(1).AddTicks(-1); // Lấy ngày kết thúc của ngày đầu vào

               
                var eventList = await _eventRepository.GetAllAsync();
             
                if (eventList == null || eventList.Count == 0)
                {
                    return new ServiceResponse<IEnumerable<GetEventDto>>
                    {
                        Message = "No events found",
                        StatusCode = 200,
                        Success = true
                    };
                }
                else
                {
                    var eventListByDate = eventList.Where(x => x.StartTime.Date == startDate && x.EndTime.Date == startDate).ToList();
                    if (eventListByDate == null || eventListByDate.Count == 0)
                    {
                        return new ServiceResponse<IEnumerable<GetEventDto>>
                        {
                            Message = "No events found for the specified date",
                            StatusCode = 200,
                            Success = true
                        };
                    }
                    else
                    {
                        var filteredEvents = eventListByDate.Where(x => x.StartTime.TimeOfDay <= dateTimeStart.TimeOfDay && x.EndTime.TimeOfDay >= dateTimeStart.TimeOfDay).ToList();

                        if (filteredEvents.Count == 0 )
                        {
                            return new ServiceResponse<IEnumerable<GetEventDto>>
                            {
                                Message = "No events found within the specified time range",
                                StatusCode = 200,
                                Success = true
                            };
                        }
                        var _mapper = config.CreateMapper();
                        var lstDto = _mapper.Map<List<GetEventDto>>(filteredEvents);

                        return new ServiceResponse<IEnumerable<GetEventDto>>
                        {
                            Data = lstDto,
                            Message = "Successfully retrieved events",
                            StatusCode = 200,
                            Success = true
                        };
                    }

                }
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<PagedResult<EventDto>>> GetEventWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _eventRepository.GetAllAsync<EventDto>(queryParameters);
            return new ServiceResponse<PagedResult<EventDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }


        public async Task<ServiceResponse<IEnumerable<GetTaskAndEventDto>>> GetTaskAndEventListByTimeNow()
        {
            var events = await _eventRepository.GetTaskAndEventListByTimeNow();
            if(events == null)
            {
                return new ServiceResponse<IEnumerable<GetTaskAndEventDto>>
                {
                    Data = null,
                    Message = "Failed Data null",
                    StatusCode = 200,
                    Success = true
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetTaskAndEventDto>>
                {
                    Data = events,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }

        public async Task<ServiceResponse<string>> GetTotalEventToday()
        {
            var total = await _eventRepository.GetTotalEventsToday();
            return new ServiceResponse<string>
            {
                Data = total,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }
    }
}
