using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Ecommerce
{
    /// <summary>
    /// variable para ecommerce stock de tienda
    /// </summary>
    public class Ent_Stock_Tienda
    {        
        public string cod_tda { get; set; }
        public string des_tda { get; set; }
        public string ubigeo_tda { get; set; }
        public string direccion_tda { get; set; }
        public string cod_art { get; set; }
        public string talla { get; set; }
        public Int32 cantidad { get; set; }
    }
    /// <summary>
    /// lista de resultado de stock
    /// </summary>
    public class Ent_Stock_Lista
    {
        public Ent_Stock_Tienda_Acceso valida { get; set; }
        public Ent_Stock_Tienda[] lista_stk_tda { get; set; }
    }
    /// <summary>
    /// estado de consulta web
    /// </summary>
    public class Ent_Stock_Tienda_Acceso
    {
        public string estado { get; set; }
        public string descripcion { get; set; }
    }
}
