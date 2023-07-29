using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.EventTaskDto
{
    public class EventTaskDto :  IBaseDto
    {
        public Guid Id { get; set; }

        public string TaskName { get; set; }
        public string EventName { get; set; }
    }
}
