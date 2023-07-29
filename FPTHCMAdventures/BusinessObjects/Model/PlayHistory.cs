using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class PlayHistory
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid PlayerId { get; set; }
        public int CompletedTime { get; set; }
        public int TaskPoint { get; set; }
        public string Status { get; set; }

        public virtual Player Player { get; set; }
        public virtual Task Task { get; set; }
    }
}
