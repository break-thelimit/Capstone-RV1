using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Rank
    {
        public Rank()
        {
            Gifts = new HashSet<Gift>();
        }

        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }


        public virtual Event Event { get; set; }
        public virtual Player Player { get; set; }
        public virtual ICollection<Gift> Gifts { get; set; }
    }
}
