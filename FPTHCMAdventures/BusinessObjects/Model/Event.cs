using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Event
    {
        public Event()
        {
            EventTasks = new HashSet<EventTask>();
            Gifts = new HashSet<Gift>();
            Players = new HashSet<Player>();
            SchoolEvents = new HashSet<SchoolEvent>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        public virtual ICollection<EventTask> EventTasks { get; set; }
        public virtual ICollection<Gift> Gifts { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<SchoolEvent> SchoolEvents { get; set; }
    }
}
