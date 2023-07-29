using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.QuestionDto;
using DataAccess.Repositories.MajorRepositories;
using DataAccess.Repositories.QuestionRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMajorRepository _majorRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IMajorRepository majorRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _majorRepository = majorRepository;
        }
        public async Task<ServiceResponse<Guid>> CreateNewQuestion(CreateQuestionDto createQuestionDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Question>(createQuestionDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _questionRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

       
        public async Task<ServiceResponse<IEnumerable<QuestionDto>>> GetQuestion()
        {
            var majorList = await _questionRepository.GetAllAsync<QuestionDto>();


            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<QuestionDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<QuestionDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List question null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<QuestionDto>> GetQuestionById(Guid eventId)
        {
            try
            {

                var eventDetail = await _questionRepository.GetAsync<QuestionDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<QuestionDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<QuestionDto>
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
       

        public async Task<ServiceResponse<string>> UpdateQuestion(Guid id, UpdateQuestionDto questionDto)
        {
            try
            {
                questionDto.Id = id;
                await _questionRepository.UpdateAsync(id, questionDto);
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
            return await _questionRepository.Exists(id);
        }

        public async Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file)
        {
            Dictionary<string, Guid?> majorDictionary = new Dictionary<string, Guid?>();

            var majors = await _majorRepository.GetAllAsync<GetMajorDto>();

            foreach (var major in majors)
            {
                majorDictionary.Add(major.Name, major.Id);
            }

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
                            var dataList = new List<QuestionDto>();

                            for (int row = 2; row <= rowCount; row++)
                            {
                                var data = new QuestionDto
                                {
                                    Id = Guid.NewGuid(),
                                    Name = worksheet.Cells[row, 1].Value.ToString(),
                                    MajorName = worksheet.Cells[row, 2].Value.ToString(),

                                };

                                dataList.Add(data);
                            }

                            // Start from row 2 to skip the header row

                            var mapper = config.CreateMapper();
                            var locations = _mapper.Map<List<Question>>(dataList);
                            await _questionRepository.AddRangeAsync(locations);
                            await _questionRepository.SaveChangesAsync();

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
        public byte[] GenerateExcelTemplate(List<GetMajorDto> majors)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SampleDataQuestion");

                // Thiết lập header cho các cột
                worksheet.Cells[1, 1].Value = "Question Name";
                worksheet.Cells[1, 2].Value = "Major Name";

                // Điền dữ liệu Major vào tệp Excel
               
                
                var majorNameColumn = worksheet.Cells[2, 2, majors.Count + 1, 2];
                var validation = majorNameColumn.DataValidation.AddListDataValidation();
                validation.Formula.ExcelFormula = $"=MajorList";

                // Tạo Name Range cho danh sách Major Name
                var majorListRange = worksheet.Names.Add("MajorList", worksheet.Cells[2, 2, majors.Count + 1, 2]);

                // Thiết lập công thức để trích xuất danh sách Major Name
                majorListRange.Formula = $"='{worksheet.Name}'!$B$2:$B${majors.Count + 1}";

                // Đặt các tên tương ứng với danh sách Major Name
                for (int i = 0; i < majors.Count; i++)
                {
                    var majorCell = worksheet.Cells[i + 2, 2];
                    majorCell.Value = majors[i].Name;
                }

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
                var majors = await _majorRepository.GetAllAsync<GetMajorDto>();
                fileContents = GenerateExcelTemplate(majors);
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

    }
}
