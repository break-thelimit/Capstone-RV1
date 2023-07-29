using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerDto
{
    public class UpdatePlayerDto  : IBaseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Nickname { get; set; }
        public double TotalPoint { get; set; }
        public double TotalTime { get; set; }
    }
}
