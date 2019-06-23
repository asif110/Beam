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
        public int FromId { get; set; }
        public int ToId { get; set; }
        public int RequestId { get; set; }
       // public string ChatImage { get; set; }
        public Boolean IsRead { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime ReceivedDate { get; set; }


    }
}