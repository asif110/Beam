using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Beam.Models
{
    public class RequestController : ApiController
    {
        string returnVal = "failed";

        // POST api/request
        [HttpGet]
        public IEnumerable<Request> Get([FromBody] Beam.Models.Request RequestModel)
        {

            return new RequestDataAccessLayer().GetAllRequestControllers(RequestModel.PK);
        }

        // POST api/request
        [HttpPost]
        public string Post([FromBody] Beam.Models.Request RequestModel)
        {
            returnVal = "failed";

            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {

                if (new RequestDataAccessLayer().RequestInsert(RequestModel))
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
        public string Put([FromBody] Beam.Models.Request RequestModel)
        {
            returnVal = "failed";

            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {

                if (new RequestDataAccessLayer().RequestUpdate(RequestModel))
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
        [HttpDelete]
        public string Delete([FromBody] Beam.Models.Request RequestModel)
        {
            returnVal = "failed";
            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {

                if (new RequestDataAccessLayer().RequestDelete(RequestModel.PK))
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
    }
}
