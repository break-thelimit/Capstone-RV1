using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.ExchangeHistoryDto
{
    public abstract class BaseExchangeHistoryDto
    {
        public Guid PlayerId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime ExchangeDate { get; set; }
        public int Quantity { get; set; }

    }
}
