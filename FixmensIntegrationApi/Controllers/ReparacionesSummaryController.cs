using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FixmensIntegrationApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReparacionesSummaryController : ApiController
    {
        MediaTypeFormatter formatter = null;
        public ReparacionesSummaryController()
        {
            //Get Conection

            this.formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        }

        
        
        public HttpResponseMessage Get(DateTime fechaInicio, DateTime fechaFin,bool soloEntregado, string userName = "")
        {

            try
            {

                //UserToken token = new UserToken(User);
                serviceEntities model = new serviceEntities();
                model.Configuration.LazyLoadingEnabled = false;
                List<REPARACIONESVIEW> result = model.REPARACIONESVIEW.Where(x => x.TECNICO.Contains(userName) && x.FECHATERMINADO >= fechaInicio && x.FECHATERMINADO <= fechaFin && (!soloEntregado || x.ENTREGADO.Value)).ToList();

                return Request.CreateResponse(HttpStatusCode.OK,
                    result);
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.InnerException + e.Message);
            }

        }

       
       
    }
}
