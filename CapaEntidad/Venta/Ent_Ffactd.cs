using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Venta
{
    public class Ent_Ffactd
    {
        #region<DETALLE DE VENTA>
        public string fd_nint { get; set; }
        public string fd_tipo { get; set; }
        public string fd_arti { get; set; }
        public string fd_regl { get; set; }
        public string fd_colo { get; set; }
        public string fd_item { get; set; }
        public string fd_icmb { get; set; }
        public Decimal fd_qfac { get; set; }
        public string fd_lpre { get; set; }
        public string fd_calm { get; set; }
        public Decimal fd_pref { get; set; }
        public Decimal fd_dref { get; set; }
        public Decimal fd_prec { get; set; }
        public Decimal fd_brut { get; set; }
        public Decimal fd_pimp1 { get; set; }
        public Decimal fd_vimp1 { get; set; }
        public Decimal fd_subt1 { get; set; }
        public Decimal fd_pimp2 { get; set; }
        public Decimal fd_vimp2 { get; set; }
        public Decimal fd_subt2 { get; set; }
        public Decimal fd_pdct1 { get; set; }
        public Decimal fd_vdct1 { get; set; }
        public Decimal fd_subt3 { get; set; }
        public Decimal fd_vdct4 { get; set; }
        public Decimal fd_vdc23 { get; set; }
        public Decimal fd_vvta { get; set; }
        public Decimal fd_pimp3 { get; set; }
        public Decimal fd_vimp3 { get; set; }
        public Decimal fd_pimp4 { get; set; }
        public Decimal fd_vimp4 { get; set; }
        public Decimal fd_total { get; set; }
        public string fd_cuse { get; set; }
        public string fd_muse { get; set; }
        public DateTime fd_fcre { get; set; }
        public DateTime fd_fmod { get; set; }
        public string fd_auto { get; set; }

        public Decimal fd_dre2 { get; set; }
        public string fd_asoc { get; set; }
        #endregion
    }
    public class Ent_List_Ffactd
    {
        public Ent_Ffactd[] lista_ffactd { get; set; }
    }
}
