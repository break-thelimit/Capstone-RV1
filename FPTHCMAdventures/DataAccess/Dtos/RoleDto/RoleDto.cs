using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.RoleDto
{
    public class RoleDto : BaseRoleDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
