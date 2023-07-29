using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Question
    {
        public Guid Id { get; set; }
        public Guid AnswerId { get; set; }
        public Guid MajorId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Major Major { get; set; }
    }
}
