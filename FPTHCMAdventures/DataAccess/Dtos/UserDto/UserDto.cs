using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.UserDto
{
    public class UserDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Schoolname { get; set; }
        public string RoleName { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

    }
}
