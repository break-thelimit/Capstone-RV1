using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.AnswerDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.AnswerRepositories
{
    public class AnswerRepository : GenericRepository<Answer> , IAnswerRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public AnswerRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAnswerAndQuestionNameDto>> GetListQuestionByMajorIdAsync(Guid majorId)
        {
            var major = await _dbContext.Set<Major>().FindAsync(majorId);

            if (major == null)
            {
                return null;
            }
            else
            {
                var questions = await _dbContext.Set<Question>()
                    .Where(q => q.MajorId == majorId)
                    .ToListAsync();

                var questionDtos = questions.Select(question =>
                {
                    var correctAnswer = _dbContext.Set<Answer>()
                        .FirstOrDefault(a => a.Id == question.AnswerId);

                    var incorrectAnswers = _dbContext.Set<Answer>()
                        .Where(a => a.Id != question.AnswerId)
                        .ToList();

                    // Randomize the incorrect answers
                    var random = new Random();
                    var randomIncorrectAnswers = incorrectAnswers
                        .OrderBy(a => random.Next())
                        .Take(3)
                        .ToList();

                    var answerDtos = new List<AnswerDto>();
                    answerDtos.Add(new AnswerDto
                    {
                        Id = correctAnswer.Id,
                        AnswerName = correctAnswer.AnswerName,
                        IsRight = true
                    });
                    answerDtos.AddRange(randomIncorrectAnswers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        AnswerName = a.AnswerName,
                    }));

                    return new GetAnswerAndQuestionNameDto
                    {
                        QuestionName = question.Name,
                        answerDtos = answerDtos
                    };
                }).ToList();

                return questionDtos;
            }
        }

       

       

    }
}
