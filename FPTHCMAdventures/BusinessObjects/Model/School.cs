﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class School
    {
        public School()
        {
            SchoolEvents = new HashSet<SchoolEvent>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public long? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public virtual ICollection<SchoolEvent> SchoolEvents { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
