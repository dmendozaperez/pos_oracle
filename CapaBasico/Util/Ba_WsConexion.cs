using CapaDato.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBasico.Util
{
    public class Ba_WsConexion
    {
        #region Authentication de web service
        public  Boolean ckeckAuthentication_ws(string acceso_cod, string user, string password)
        {
            Dat_Acceso valida_ws = null;
            Boolean acceso_ws = false;
            try
            {
                valida_ws = new Dat_Acceso();
                acceso_ws = valida_ws._acceso_ws(acceso_cod, user, password);     
            }
            catch (Exception)
            {

                acceso_ws=false;
            }
            return acceso_ws;
          
        }
        #endregion
    }
}
