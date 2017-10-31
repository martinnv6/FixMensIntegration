using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FixmensIntegrationApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GenericCallBackController : ApiController
    {
        [HttpPost]
        public async Task<string> Post()
        {
            string result = await Request.Content.ReadAsStringAsync();
           
            return result;
            serviceEntities entiti = new serviceEntities();
            SMS model = entiti.SMS.Last();
            model.STATUS = result.Substring(0,49);
            entiti.SaveChanges();
        }
    }
}
