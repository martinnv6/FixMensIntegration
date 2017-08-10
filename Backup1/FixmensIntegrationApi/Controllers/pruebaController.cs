using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using FixmensIntegrationApi.Models;


namespace FixmensIntegrationApi.Controllers
{
    public class pruebaController : ApiController
    {
        MediaTypeFormatter formatter = null;

        public pruebaController()
        {
            //Define Formatter
            
            this.formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            
        }

        [HttpGet]
        public string  Index()
        {
            FixmensModel model = new FixmensModel();
            var a = model.PRUEBAs.Select(x => x).FirstOrDefault();
            
            return "Codigo:aslkdasjd " + a.CODIGO + "Descripcion  " + a.DESCRIPCION;
        }

        //[HttpPost]
        //public HttpResponseMessage Prueba2(PRUEBA prueba)
        //{
        //     FixmensModel model = new FixmensModel();

            
           
        //    model.PRUEBAs.AddOrUpdate(prueba);
        //    model.SaveChanges();

        //    var result = model.PRUEBAs.Select(x => x);



        //    return Request.CreateResponse(HttpStatusCode.OK,
        //        result, formatter);
        //}

        [HttpPost]
        public HttpResponseMessage AddReparaciones(REPARACIONE reparaciones)
        {
            FixmensModel model = new FixmensModel();



            model.REPARACIONES.AddOrUpdate(reparaciones);
            model.SaveChanges();

            var result = model.REPARACIONES.Select(x => x);



            return Request.CreateResponse(HttpStatusCode.OK,
                result, formatter);
        }
    }
}
