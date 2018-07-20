using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Beam.Models
{
    [Route("api/[Model]")]
    public class FormModel
    {
        public string FirstName { get; set; }
    }
}
