namespace FixmensCMD.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REPARACIONESVIEW")]
    public partial class REPARACIONESVIEW
    {
        public long CODIGO { get; set; }

        [StringLength(50)]
        public string EQUIPO { get; set; }

        [StringLength(100)]
        public string NOMBRES { get; set; }

        public string FALLA { get; set; }

        public string INFORMETALLER { get; set; }

        public string DETALLE { get; set; }

        public string PRESPUPUESTO { get; set; }

        [StringLength(100)]
        public string TECNICO { get; set; }

        public DateTime? FECHAINGRESO { get; set; }

        public DateTime? PROMETIDO { get; set; }

        public DateTime? FECHATERMINADO { get; set; }

        [StringLength(50)]
        public string ESTADO { get; set; }

        public int ID { get; set; }

        public DateTime? FECHAUPDATE { get; set; }
    }
}
