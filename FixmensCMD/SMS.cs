//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FixmensCMD
{
    using System;
    using System.Collections.Generic;
    
    public partial class SMS
    {
        public long ID { get; set; }
        public long DEVICEID { get; set; }
        public string MESSAGE { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> SEND { get; set; }
        public Nullable<System.DateTime> EXPIRES { get; set; }
        public string ERROR { get; set; }
        public Nullable<System.DateTime> CREATED { get; set; }
        public Nullable<long> CONTACTID { get; set; }
        public string CONTACTNAME { get; set; }
        public string CONTACTNUMBER { get; set; }
        public Nullable<long> REPARCIONID { get; set; }
    
        public virtual REPARACIONESVIEW REPARACIONESVIEW { get; set; }
    }
}
