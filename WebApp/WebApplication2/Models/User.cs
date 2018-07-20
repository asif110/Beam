using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Web.Http;

namespace Beam.Models
{
    [Route("api/[Model]")]
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; } //TODO encrypt
        public int CityFK { get; set; }
        public int CityTravelTo1FK { get; set; }
        public int CityTravelTo2FK { get; set; }
    }
}
