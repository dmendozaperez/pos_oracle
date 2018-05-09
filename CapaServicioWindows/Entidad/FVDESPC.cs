using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Entidad
{
    public class FVDESPC
    {
        #region<VARIABLES DE GUIAS FVDESPC CERRADAS , TRANSACCIONES>
        public string DESC_ALMAC { get; set; }
        public string DESC_GUDIS { get; set; }
        public string DESC_NDESP { get; set; }
        public string DESC_TDES { get; set; }
        public string DESC_FECHA { get; set; }
        public string DESC_FDESP { get; set; }
        public string DESC_ESTAD { get; set; }
        public string DESC_TIPO { get; set; }
        public string DESC_TORI { get; set; }
        public string DESC_FEMI { get; set; }
        public string DESC_SEMI { get; set; }
        public string DESC_FTRA { get; set; }
        public string DESC_NUME { get; set; }
        public string DESC_CONCE { get; set; }
        public string DESC_NMOVC { get; set; }
        public string DESC_EMPRE { get; set; }
        public string DESC_SECCI { get; set; }
        public string DESC_CANAL { get; set; }
        public string DESC_CADEN { get; set; }
        public string DESC_FTX { get; set; }
        public string DESC_TXPOS { get; set; }
        public DataTable DT_FVDESPD { get; set; }
        /// <summary>
        /// con todas las reglas de medida horizontal
        /// </summary>
        public DataTable DT_FVDESPD_TREGMEDIDA { get; set; }

        #endregion
    }
}
