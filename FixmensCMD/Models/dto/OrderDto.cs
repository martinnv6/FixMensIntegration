using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixmensCMD.Models.dto
{
    public class OrderDto
    {
        public class OrdenModel
        {
            public string NombreCliente { get; set; }
            public string InfoTecnico { get; set; }
            public string InfoCliente { get; set; }
            public string Presupuesto { get; set; }
            public string Tecnico { get; set; }
            public string Precio { get; set; }
            public string ErrorMessage { get; set; }
            public DateTime? FechaIngreso { get; set; }
            public DateTime? FechaPrometido { get; set; }
            public DateTime? FechaEntregado { get; set; }
            public string Estado { get; set; }

        }
    }
}
