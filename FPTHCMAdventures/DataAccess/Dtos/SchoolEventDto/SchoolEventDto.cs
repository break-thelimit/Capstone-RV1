using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.SchoolEventDto
{
    public class SchoolEventDto : IBaseDto
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }
        public string SchoolName { get; set; }
    }
}
