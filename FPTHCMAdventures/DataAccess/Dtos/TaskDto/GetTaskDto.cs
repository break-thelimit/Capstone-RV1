using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.TaskDto
{
    public class GetTaskDto : BaseTaskDto, IBaseDto
    {
        public Guid Id { get; set; }
        public string LocationName { get; set; }
        public string MajorName { get; set; }
        public string NpcName { get; set; }
    }
}
