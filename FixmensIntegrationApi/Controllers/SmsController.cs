using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FixmensIntegrationApi.BLL;

namespace FixmensIntegrationApi.Controllers
{
    public class SmsController : ApiController
    {
        // GET: api/Sms
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sms/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sms
        public HttpResponseMessage Post(string id,string status)//[FromBody]MyModel value)
        {
            
            try
            {
                SmsBLL smsbll = new SmsBLL();

                smsbll.UpdateSMSStatus(long.Parse(id), status);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.InnerException + e.Message);
            }

        }

        // PUT: api/Sms/5
        public void Put(int id,[FromBody]string value)
        {
            
            
        }

        // DELETE: api/Sms/5
        public void Delete(int id)
        {
        }

        public class MyModel
        {
            public long id { get; set; }
        }
    }
}
