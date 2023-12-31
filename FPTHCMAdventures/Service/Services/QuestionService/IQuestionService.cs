﻿using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.QuestionDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<ServiceResponse<IEnumerable<QuestionDto>>> GetQuestion();
        Task<ServiceResponse<QuestionDto>> GetQuestionById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewQuestion(CreateQuestionDto createQuestionDto);
        Task<ServiceResponse<string>> UpdateQuestion(Guid id, UpdateQuestionDto questionDto);

        Task<ServiceResponse<byte[]>> DownloadExcelTemplate();
        Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file);
    }
}
