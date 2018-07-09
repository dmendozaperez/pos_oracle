using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Logistica
{
    /// <summary>
    /// objeto stock
    /// </summary>
    public class Ent_Stock
    {
        public string cod_tda { get; set; }
        public string art_cod { get; set; }
        public string art_cal { get; set; }
        public string art_talla { get; set; }
        public Int32 art_pares { get; set; }
        //public string _0 { get; set; }
        //public string _1 {get;set;}
        //public string _2 { get; set; }
        //public string _3 { get; set; }
        //public string _4 { get; set; }
        //public string _5 { get; set; }
        //public string _6 { get; set; }
        //public string _7 { get; set; }
        //public string _8 { get; set; }
        //public string _9 { get; set; }
        //public string _10 { get; set; }
        //public string _11 { get; set; }        
    }
    /// <summary>
    /// object lista de stock
    /// </summary>
    public class Ent_Lista_Stock
    {
        public Ent_Stock[] lista_stock { get; set; } 
    }
}
