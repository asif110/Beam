using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Beam.Models;
using Beam.DAT;

namespace Beam.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Login")]
    public class LoginController : ApiController
    {
        // POST: api/Login
        [HttpPost]
        public int Post([FromBody]Login model)
        {
            int returnVal = 0;
            ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

            try
            {
                returnVal = new UserDataAccessLayer().Login(model.Email, model.Password);      
            }
            catch (Exception ex)
            {

                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
            }
            
            return returnVal;
        }
    }
}
