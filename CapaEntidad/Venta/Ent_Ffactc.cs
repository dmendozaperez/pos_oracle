using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Venta
{
    public class Ent_Ffactc
    {
        #region<variables de venta cabecera>
        public string fc_nint { get; set; }
        public string fc_nnot { get; set; }
        public string fc_codi { get; set; }
        public string fc_suna { get; set; }
        public string fc_sfac { get; set; }
        public string fc_nfac { get; set; }
        public DateTime fc_ffac { get; set; }
        public string fc_nord { get; set; }
        public string fc_cref { get; set; }
        public string fc_sref { get; set; }
        public string fc_nref { get; set; }
        public string fc_pvta { get; set; }
        public string fc_csuc { get; set; }
        public string fc_gvta { get; set; }
        public string fc_zona { get; set; }
        public string fc_clie { get; set; }
        public string fc_ncli { get; set; }
        public string fc_nomb { get; set; }
        public string fc_apep { get; set; }
        public string fc_apem { get; set; }
        public string fc_dcli { get; set; }
        public string fc_cubi { get; set; }
        public string fc_ruc { get; set; }
        public string fc_vuse { get; set; }
        public string fc_vend { get; set; }
        public string fc_ipre { get; set; }
        public string fc_tint { get; set; }
        public decimal fc_pint { get; set; }
        public string fc_lcsg { get; set; }
        public string fc_ncon { get; set; }
        public string fc_dcon { get; set; }
        public string fc_lcon { get; set; }
        public string fc_lruc { get; set; }
        public string fc_agen { get; set; }
        public string fc_mone { get; set; }
        public Decimal fc_tasa { get; set; }
        public string fc_fpag { get; set; }
        public decimal fc_nlet { get; set; }
        public decimal fc_qtot { get; set; }
        public decimal fc_pref { get; set; }
        public decimal fc_dref { get; set; }
        public decimal fc_brut { get; set; }
        public decimal fc_vimp1 { get; set; }
        public decimal fc_vimp2 { get; set; }
        public decimal fc_vdct1 { get; set; }
        public decimal fc_vdct4 { get; set; }
        public decimal fc_pdc2 { get; set; }
        public decimal fc_pdc3 { get; set; }
        public decimal fc_vdc23 { get; set; }
        public decimal fc_vvta { get; set; }
        public decimal fc_vimp3 { get; set; }
        public decimal fc_pimp4 { get; set; }
        public decimal fc_vimp4 { get; set; }
        public decimal fc_total { get; set; }
        public string fc_esta { get; set; }
        public string fc_tdoc { get; set; }

        public string fc_cuse { get; set; }
        public string fc_muse { get; set; }
        public DateTime fc_fcre { get; set; }
        public DateTime fc_fmod { get; set; }
        public string fc_hora { get; set; }
        public string fc_auto { get; set; }
        public string fc_ftx { get; set; }
        public string fc_estc { get; set; }
        public string fc_sexo { get; set; }
        public string fc_mpub { get; set; }
        public string fc_edad { get; set; }
        public string fc_regv { get; set; }

        /*sostic 05-2019*/
        /*Campos para el canal de venta*/
        public string fc_idtda_b { get; set; }
        public string fc_id_est { get; set; }
        public string fc_id_tcv { get; set; }
        public string fc_refere { get; set; }
        public string fc_ubi { get; set; }

        #endregion

    }
    public class Ent_List_Ffactc
    {
        public Ent_Ffactc[] lista_ffactc { get; set; }
    }
}
