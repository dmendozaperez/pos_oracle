using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Interfaces
{
    public class Ent_SCDREMB
    {
        public string remb_guiac { get; set; }
        public string remb_artic { get; set; }
        public string remb_calid { get; set; }
        public string remb_medid { get; set; }
        public string remb_corra { get; set; }
        public Decimal remb_canti { get; set; }
        public string remb_almac { get; set; }
        public string remb_cpack { get; set; }
        public string remb_condm { get; set; }
        public string remb_rmed { get; set; }
        public string remb_u_med { get; set; }
        public string remb_categ { get; set; }
        public string remb_subca { get; set; }
        public string remb_clase { get; set; }
        public Decimal remb_prvta { get; set; }
        public Decimal remb_costo { get; set; }
        public string remb_talpr { get; set; }
        public string remb_plaoc { get; set; }
        public string remb_femba { get; set; }
        public string remb_hemba { get; set; }
        public string remb_empre { get; set; }
        public string remb_secci { get; set; }
        public string remb_user { get; set; }
        public string remb_aassd { get; set; }
        public string remb_flag { get; set; }
        public Decimal remb_secue { get; set; }
        public string remb_estad { get; set; }
        public string remb_log { get; set; }
        public string remb_ftx { get; set; }
    }

    public class Ent_List_Scdrem
    {
        public Ent_SCDREMB[] lista_scdremb { get; set; }
    }

}
