using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Venta
{
    public class Ent_Tk_Return
    {
        public string estado_error { get; set; }
        public decimal genera_cupon { get; set; }
        public string cupon_imprimir { get; set; }
    }
    public class Ent_Tk_Set_Parametro
    {
        public string COD_TDA { get; set; }
        public string FC_SUNA { get; set; }
        public string SERIE { get; set; }
        public string NUMERO { get; set; }
        public decimal MONTO { get; set; }
        public DateTime FECHA { get; set; }

        //public Ent_Tk_Return tk_return { get; set; }
    }
}
