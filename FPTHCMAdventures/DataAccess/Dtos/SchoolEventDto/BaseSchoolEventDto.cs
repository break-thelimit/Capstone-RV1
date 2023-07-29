using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.SchoolEventDto
{
    public abstract class BaseSchoolEventDto
    {
        public Guid EventId { get; set; }
        public Guid SchoolId { get; set; }

    }
}
