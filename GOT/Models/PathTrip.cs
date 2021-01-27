using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Models
{
    public class PathTrip
    {
        public int PathTripID { get; set; }

        public int Order { get; set; }
        public int PathId { get; set; }
        public Path Path { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public bool IsFromAToB { get; set; }
    }
}
