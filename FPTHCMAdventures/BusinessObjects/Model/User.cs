using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class User
    {
        public User()
        {
            Players = new HashSet<Player>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid RoleId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
