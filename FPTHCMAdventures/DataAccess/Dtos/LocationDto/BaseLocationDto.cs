using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.LocationDto
{
    public abstract class BaseLocationDto
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string LocationName { get; set; }
        public string Status { get; set; }
    }
}
