using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Web.Http;

namespace Beam.Models
{
    [Route("api/[Model]")]
    public class Country
    {
        public int PK { get; set; }
        public string NameKey { get; set; }
    }    
}
