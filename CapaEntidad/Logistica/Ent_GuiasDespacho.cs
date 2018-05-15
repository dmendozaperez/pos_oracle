using CapaEntidad.Logistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Logistica
{
    public class Ent_GuiasDespacho_Cab
    {
        public string cod_alm { get; set; }
        public string nro_guia { get; set; }
        public string nro_despacho { get; set; }
        public string cod_tda { get; set; }
        public string des_tda { get; set; }
        public string con_des { get; set; }
        public DateTime fec_des { get; set; }

    }
    public class Ent_GuiasDespacho_Det
    {
        public string art_cod { get; set; }
        public string art_tall { get; set; }
        public Int32 art_par { get; set; }
    }
    public class Ent_Guias_Articulo
    {
        public string articulo { get; set; }
        public List<Ent_Guias_Tallas> list_tallas { get; set; }

        public Int32 total_pares { get; set; }
    }
    public class Ent_Guias_Tallas
    {
        public string talla { get; set; }
        public Int32 pares { get; set; }
    }
}
