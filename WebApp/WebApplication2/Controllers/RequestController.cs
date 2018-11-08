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
        //[HttpGet("{pk}")]
        [HttpGet]
        public IEnumerable<Request> Get(int id)
        {
            return new RequestDataAccessLayer().GetAllRequestControllers(id);
        }

        [HttpGet]
        public IEnumerable<Request> Get(int id, int resUserFK,string reqType)
        {
            return new RequestDataAccessLayer().GetAllRequestReceivedControllers(id, resUserFK, reqType);
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
        [HttpPost]
        public string Post([FromBody] Beam.Models.Request RequestModel,int AcceptDecline)
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
                //if (new RequestDataAccessLayer().RequestUpdate(RequestModel))
                //{
                //    returnVal = "success";

                //}
                if (RequestModel.IsStatusChange)
                {
                    //
                    if (new RequestDataAccessLayer().RequestStatusUpdate(RequestModel))
                    {
                        returnVal = "success";
                    }
                }
                else if (RequestModel.IsStatusChange == false)
                {
                    if (new RequestDataAccessLayer().RequestUpdate(RequestModel))
                    {
                        returnVal = "success";

                    }
                }


            }
            catch (Exception ex)
            {

                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
                returnVal = ex.Message;
            }
            return returnVal;

        }

        // POST api/request
        [HttpDelete]
        public string Delete(int id)
        {
            returnVal = "failed";
            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {

                if (new RequestDataAccessLayer().RequestDelete(id))
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
