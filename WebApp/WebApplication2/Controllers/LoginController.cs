using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Beam.Models;
using Beam.DAT;
using Beam.Models;

namespace Beam.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Login")]
    public class LoginController : ApiController
    {
        // POST: api/Login
        [HttpPost]
        public string Post([FromBody]Login model)
        {
            string returnVal = "failed";
            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {
                if (new UserDataAccessLayer().Login(model.Email, model.Password))
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
