using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Beam.Models
{
    public class ChatController : ApiController
    {
        string returnVal = "failed";

        [HttpGet]
        public IEnumerable<Chat> Get(int id, int FromId, int ToId)
        {
            return new ChatDataAccessLayer().GetAllChat(id, FromId,ToId);
        }

      

        // POST api/request
        [HttpPost]
        public string Post([FromBody] Beam.Models.Chat ChatModel)
        {
            returnVal = "failed";

            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {

                if (new ChatDataAccessLayer().ChatInsert(ChatModel))
                {
                    returnVal = "success";

                }
            }
            catch (Exception ex)
            {

                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
            }
            return returnVal;

        }
        // POST api/request
        [HttpPut]
        public string Put([FromBody] Beam.Models.Chat ChatModel)
        {
            returnVal = "failed";

            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {

                    if (new ChatDataAccessLayer().ChatUpdate(ChatModel))
                    {
                        returnVal = "success";
                    }
               
            }
            catch (Exception ex)
            {

                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
                returnVal = ex.Message;
            }
            return returnVal;

        }

    }
}