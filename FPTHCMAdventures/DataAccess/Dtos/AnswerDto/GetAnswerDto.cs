﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.AnswerDto
{
    public class GetAnswerDto : BaseAnswerDto, IBaseDto
    {
        public Guid Id { get; set; }
        public string QuestionName { get; set; }
    }
}
