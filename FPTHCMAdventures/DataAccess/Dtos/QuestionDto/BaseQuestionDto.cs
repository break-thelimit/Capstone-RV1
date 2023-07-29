using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.QuestionDto
{
    public abstract class BaseQuestionDto
    {
        public Guid AnswerId { get; set; }
        public Guid MajorId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
