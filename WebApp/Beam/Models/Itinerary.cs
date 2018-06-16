using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Web.Http;

namespace Beam.Models
{
    [Route("api/[Model]")]
    public class Itinerary
    {
        public string UserEmail { get; set; }
        public int FromCityFK { get; set; }
        public int ToCityFK { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public bool IsDocument { get; set; }
        public bool IsPackage { get; set; }
        public bool IsCarpool { get; set; }
        public int ModeOfTravel { get; set; }
        public string Details { get; set; }
    }
}
