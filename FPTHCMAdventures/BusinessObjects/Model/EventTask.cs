using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class EventTask
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Task Task { get; set; }
    }
}
