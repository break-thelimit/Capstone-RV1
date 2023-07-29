using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Gift
    {
        public Guid Id { get; set; }
        public Guid? EventId { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public int? Price { get; set; }
        public string Status { get; set; }

        public virtual Event Event { get; set; }
    }
}
