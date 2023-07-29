using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Inventory
    {
        public Inventory()
        {
            ItemIventories = new HashSet<ItemIventory>();
        }

        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual ICollection<ItemIventory> ItemIventories { get; set; }
    }
}
