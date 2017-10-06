using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixmensCMD.Models
{
    public class PhoneModel
    {
        public string PhoneNumber { get; set; }
        
        public long ReparacionId { get; set; }
        public string Status { get; set; }
    }
}
