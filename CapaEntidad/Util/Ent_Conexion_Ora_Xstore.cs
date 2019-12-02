using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Util
{
    public class Ent_Conexion_Ora_Xstore
    {
        public string server { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public Int32 port { get; set; }
        public string sid { get; set; }
    }
}
