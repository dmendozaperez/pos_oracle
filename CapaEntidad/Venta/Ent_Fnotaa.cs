using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Venta
{
    public class Ent_Fnotaa
    {
        #region<FORMA DE PAGO DE VENTA>
        public string na_nota { get; set; }
        public string na_item { get; set; }
        public string na_mone { get; set; }
        public string na_tpag { get; set; }
        public Decimal na_tasa { get; set; }
        public string na_cref { get; set; }
        public string na_sref { get; set; }
        public string na_nref { get; set; }
        public Decimal na_vref { get; set; }
        public Decimal na_vpag { get; set; }
        public string na_esta { get; set; }
        public string na_cier { get; set; }
        public string na_fcre { get; set; }
        public string na_fmod { get; set; }
        #endregion
    }
    public class Ent_List_Fnotaa
    {
        public Ent_Fnotaa[] lista_fnotaa { get; set; }
    }
}
