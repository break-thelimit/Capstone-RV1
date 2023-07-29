using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.GiftDto
{
    public abstract class BaseGiftDto
    {
        public Guid? RankId { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; }

    }
}
