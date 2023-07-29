using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.UserDto
{
    public class GetUserDto : BaseUserDto , IBaseDto
    {
        public Guid Id { get; set; }
    }
}
