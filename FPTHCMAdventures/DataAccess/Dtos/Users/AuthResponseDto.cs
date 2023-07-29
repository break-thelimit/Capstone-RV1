using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.Users
{
    public class AuthResponseDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
