using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.UserDto
{
    public class GetUserListWithSchoolNameDto : IBaseDto
    {
        public Guid Id { get; set; }

        public Guid SchoolId { get; set; }

        public string SchoolName { get; set; }  
        public Guid RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }

    }
}
