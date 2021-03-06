﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Helpers;
using System.Web.Http;
using FixmensIntegrationApi.Models;

namespace FixmensIntegrationApi.Controllers
{
    public class ReparacionesController : ApiController
    {
        MediaTypeFormatter formatter = null;
        public ReparacionesController()
        {
            //Get Conection

            this.formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        }

        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public HttpResponseMessage Get(long id)
        {
            //Esta es una prueba de comentario para probar bitbucket
            try
            {
                //UserToken token = new UserToken(User);
                FixMensAzure model = new FixMensAzure();
                REPARACIONESVIEW result = model.REPARACIONESVIEW.FirstOrDefault(x => x.CODIGO ==id);

                return Request.CreateResponse(HttpStatusCode.OK,
                    result, formatter);
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.InnerException + e.Message);
            }
            
        }

        public HttpResponseMessage Get(string nombre)
        {

            try
            {
                //UserToken token = new UserToken(User);
                FixMensAzure model = new FixMensAzure();
                List<REPARACIONESVIEW> result = model.REPARACIONESVIEW.Where(x => x.NOMBRES.Contains(nombre)).ToList();

                return Request.CreateResponse(HttpStatusCode.OK,
                    result, formatter);
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.InnerException + e.Message);
            }

        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}