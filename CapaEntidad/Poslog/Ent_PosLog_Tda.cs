using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Poslog
{
    public class Ent_PosLog_Tda
    {
        public int rtl_loc_id { get; set; }
        public int wkstn_id { get; set; }
        public decimal trans_seq { get; set; }
        public DateTime business_date { get; set; }
        public string numdoc { get; set; }
        public decimal total { get; set; }
        public string document_typcode { get; set; }
        public string pos_log_data { get; set; }
    }
}
