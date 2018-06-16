using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Web.Http;

namespace Beam.Models
{
    [Route("api/[Model]")]
    public class City
    {
        //to get data
        public string CountryCode { get; set; }

        //data retrieved
        public int PK { get; set; }
        public string NameKey { get; set; }
    }
}
