namespace FixmensIntegrationApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REPARACIONES")]
    public partial class REPARACIONE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CLIENTE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PROMETIDO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHAINGRESO { get; set; }

        [StringLength(13)]
        public string HORAINGRESO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RECEPCIONADO { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TECNICO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHATERMINADO { get; set; }

        [StringLength(13)]
        public string HORATERMINADO { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ESTADO { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool ENTREGADO { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool AVISADO { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool BATERIA { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool BATERIA_ORIG { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool CARGADOR { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool CARGADOR_ORIG { get; set; }

        [StringLength(13)]
        public string PROMETIDO_HORA { get; set; }

        [Key]
        [Column(Order = 11)]
        public bool CHIP { get; set; }

        [StringLength(40)]
        public string CHIP_NS { get; set; }

        [StringLength(20)]
        public string ORDEN_CLI { get; set; }

        [StringLength(60)]
        public string CLI_NOMBRE_EVENTUAL { get; set; }

        [StringLength(30)]
        public string TEL_EVENTUAL { get; set; }

        [Key]
        [Column(Order = 12)]
        public bool CLI_EVENTUAL { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_ENTREGADO { get; set; }

        [StringLength(80)]
        public string CLIENTE_GARANTIA { get; set; }

        public int? SUCURSALCLI { get; set; }

        [StringLength(10)]
        public string PIN { get; set; }

        [StringLength(40)]
        public string CAMPOLIBRE1 { get; set; }

        [StringLength(40)]
        public string CAMPOLIBRE2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GARANTIA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_AUD { get; set; }

        public TimeSpan? HORA_AUD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GARANTIA_FECHA { get; set; }

        [StringLength(20)]
        public string GARANTIA_FACTURA { get; set; }

        public short? SITUACION { get; set; }

        public short? TAREA { get; set; }

        public short? UBICACION_ENTRADA { get; set; }

        public short? UBICACION_SALIDA { get; set; }

        [StringLength(20)]
        public string REMITO_ENTREGADO { get; set; }

        [StringLength(20)]
        public string REMITO_INGRESO { get; set; }

        [StringLength(13)]
        public string BARCODE { get; set; }

        [StringLength(13)]
        public string ARTICULO { get; set; }

        public short? AREAVENTA { get; set; }
    }
}
