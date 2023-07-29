using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.AnswerDto
{
    public abstract class BaseAnswerDto
    {
        public string AnswerName { get; set; }
        public bool IsRight { get; set; }
    }
}
