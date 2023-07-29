using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.GiftDto
{
    public class UpdateGiftDto : BaseGiftDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
