using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerHistoryDto
{
    public class PlayerHistoryDto : IBaseDto
    {
        public Guid Id { get; set; }

        public string TaskName { get; set; }
        public string PlayerName { get; set; }
        public int CompletedTime { get; set; }
        public int TaskPoint { get; set; }
        public string Status { get; set; }
    }
}
