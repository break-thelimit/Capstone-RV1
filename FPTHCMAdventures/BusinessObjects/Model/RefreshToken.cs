using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class RefreshToken
    {
        public int TokenId { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expirydate { get; set; }

        public virtual User User { get; set; }
    }
}
