using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Answer
    {
        public Answer()
        {
            Questions = new HashSet<Question>();
        }

        public Guid Id { get; set; }
        public string AnswerName { get; set; }
        public bool IsRight { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
