using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using FixmensIntegrationApi.BLL;
using FixMens.Models;

namespace FixmensIntegrationApi.Controllers
{
    [EnableCors(origins: "https://www.fixmens.com.mx", headers: "*", methods: "*")]
    public class MailInvoicesController : ApiController
    {

        MediaTypeFormatter formatter = null;
        public MailInvoicesController()
        {
            //Get Conection

            this.formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post(FacturaModel model)
        {
            try
            {
                InvoicesBLL.sendEmailToCreateInvoice(model);
                return Request.CreateResponse(HttpStatusCode.OK,
                    "La factura llegara a su correo en maximo 24hrs", formatter);
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.InnerException + e.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}