using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Web.Http;

namespace Beam.Models
{
    [Route("api/[Model]")]
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; } //TODO encrypt
    }
}
