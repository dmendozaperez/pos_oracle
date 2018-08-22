using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Interfaces
{
    public class Ent_Scactco
    {
        public string ctco_talpr { get; set; }
        public string ctco_plaoc { get; set; }
        public string ctco_artic { get; set; }
        public string ctco_calid { get; set; }
        public string ctco_plpr { get; set; }
        public string ctco_impr { get; set; }
        public string ctco_med00 { get; set; }
        public string ctco_med01 { get; set; }
        public string ctco_med02 { get; set; }
        public string ctco_med03 { get; set; }
        public string ctco_med04 { get; set; }
        public string ctco_med05 { get; set; }
        public string ctco_med06 { get; set; }
        public string ctco_med07 { get; set; }
        public string ctco_med08 { get; set; }
        public string ctco_med09 { get; set; }
        public string ctco_med10 { get; set; }
        public string ctco_med11 { get; set; }
        public string ctco_orige { get; set; }
        public DateTime ctco_fecha { get; set; }
         public string ctco_usern { get; set; }
        public string ctco_empre { get; set; }
        public string ctco_ftx { get; set; }
        public string ctco_txpos { get; set; }

    }

    public class Ent_List_Scactco
    {
        public Ent_Scactco[] lista_scactco { get; set; }
    }
    

}
