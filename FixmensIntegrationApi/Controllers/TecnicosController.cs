﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FixmensIntegrationApi.Controllers
{
    public class TecnicosController : ApiController
    {

        public HttpResponseMessage GetAll()
        {

            try
            {

                //UserToken token = new UserToken(User);
                serviceEntities model = new serviceEntities();
                model.Configuration.LazyLoadingEnabled = false;
                

                List<string> result = model.REPARACIONESVIEW.Select(x => x.TECNICO).Distinct().ToList();

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