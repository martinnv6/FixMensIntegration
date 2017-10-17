using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixmensIntegrationApi.Models
{
    public class ReparacionesSummaryDTO
    {
        public DateTime FECHATERMINADO { get; set; }
        public int CANTIDAD { get; set; }
        public string TECNICO { get; set; }

    }
       public class ReparacionesViewDTO
    {
        public long CODIGO { get; set; }
        public string EQUIPO { get; set; }
        public string NOMBRES { get; set; }
        public string FALLA { get; set; }
        public string INFORMETALLER { get; set; }
        public string DETALLE { get; set; }
        public string PRESPUPUESTO { get; set; }
        public string TECNICO { get; set; }
        public DateTime? FECHAINGRESO { get; set; }
        public DateTime? PROMETIDO { get; set; }
        public DateTime? FECHATERMINADO { get; set; }
        public string ESTADO { get; set; }
        public DateTime? FECHAUPDATE { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public bool? ENTREGADO { get; set; }
        public string NS { get; set; }
        public DateTime? FECHAENTREGADO { get; set; }
        public bool? AVISADO { get; set; }
        public string COLOR { get; set; }
        public string ESTADOEQUIPO { get; set; }
    }
}