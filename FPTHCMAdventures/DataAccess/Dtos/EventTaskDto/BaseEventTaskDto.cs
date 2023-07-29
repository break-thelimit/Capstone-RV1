using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Dtos.EventTaskDto
{
    public abstract class BaseEventTaskDto
    {
        public Guid TaskId { get; set; }
        public Guid EventId { get; set; }

    }
}
