using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FixmensIntegrationApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TecnicosController : ApiController
    {

        public HttpResponseMessage GetAll(DateTime? fechaInicio,DateTime? fechaFin)
        {

            try
            {

                //UserToken token = new UserToken(User);
                serviceEntities model = new serviceEntities();
                model.Configuration.LazyLoadingEnabled = false;
                

                List<string> result = model.REPARACIONESVIEW.Where(y=>(y.FECHATERMINADO >= fechaInicio && y.FECHATERMINADO<= fechaFin) || fechaInicio == null || fechaFin == null).Select(x => x.TECNICO).Distinct().ToList();

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
