using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.ItemDto
{
    public abstract class BaseItemDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool? LimitExchange { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}
