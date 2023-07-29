using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.InventoryDto
{
    public abstract class BaseInventoryDto
    {
        public Guid PlayerId { get; set; }
    }
}
