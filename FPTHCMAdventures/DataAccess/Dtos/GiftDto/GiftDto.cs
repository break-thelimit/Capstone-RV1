using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.GiftDto
{
    public class GiftDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string? RankName { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; }
    }
}
