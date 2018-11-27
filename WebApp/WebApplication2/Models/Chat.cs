using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using System.Web.Http;


namespace Beam.Models
{
    [Route("api/[Model]")]
    public class Chat
    {
        ////to get data
        public int UserFK { get; set; }

        //data retrieved
        public int PK { get; set; }
        public string ChatMessage { get; set; }
       

    }
}