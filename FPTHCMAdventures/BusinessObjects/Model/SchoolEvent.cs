﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class SchoolEvent
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid SchoolId { get; set; }

        public virtual Event Event { get; set; }
        public virtual School School { get; set; }
    }
}
