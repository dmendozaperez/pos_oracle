using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Venta
{
    public class Ent_Venta
    {
        public DateTime fecha { get; set; }
        public string cod_tda { get; set; }
        public string fc_suna { get; set; }
        public string fc_sfac { get; set; }
        public string fc_nfac { get; set; }
        public string fc_nint { get; set; }
        public DateTime fecha2 { get; set; }
    }
    public class Ent_Venta_List
    {
        public Ent_Venta[] lista_venta { get; set; }
    }
}
