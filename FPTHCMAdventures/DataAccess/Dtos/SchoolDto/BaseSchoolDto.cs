using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.SchoolDto
{
    public abstract class BaseSchoolDto
    {
        public string Name { get; set; }
        public long? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }


    }
}
