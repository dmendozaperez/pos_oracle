using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Entidad
{
    public class FPTRANC
    {
        public string trac_tipo { get; set; }
	    public string trac_ndesp { get; set; }
        public string trac_nume { get; set; }
        public string trac_srem { get; set; }
        public string trac_nrem { get; set; }
        public string trac_tori { get; set; }
        public string trac_gudis { get; set; }
        public string trac_estad { get; set; }
        public string trac_motiv { get; set; }
        public string trac_tdes { get; set; }
        public string trac_semi { get; set; }
        public string trac_srec { get; set; }
        public DateTime trac_fdoc { get; set; }
        public DateTime trac_ftra { get; set; }
        public DateTime trac_fori { get; set; }
        public DateTime trac_fdes { get; set; }
        public DateTime trac_festa { get; set; }
        public string trac_user { get; set; }
        public string trac_indi { get; set; }
        public string trac_obs { get; set; }
        public string trac_log { get; set; }
        public string trac_tralm { get; set; }
        public DateTime trac_freco { get; set; }
        public string trac_creco { get; set; }
        public string trac_rucdv { get; set; }
        public string trac_tramo { get; set; }
        public string trac_trx { get; set; }
        public string trac_empre { get; set; }
        public string trac_canal { get; set; }
        public string trac_caden { get; set; }
        public string trac_docrf { get; set; }
        public string trac_auto { get; set; }
        public DataTable det_FPTRAND { get; set; }
    }
}