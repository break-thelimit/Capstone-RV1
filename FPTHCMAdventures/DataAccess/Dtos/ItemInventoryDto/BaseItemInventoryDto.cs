using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.ItemInventoryDto
{
    public abstract class BaseItemInventoryDto
    {
        public Guid InventoryId { get; set; }
        public Guid? ItemId { get; set; }
        public int? Quantity { get; set; }
    }
}
