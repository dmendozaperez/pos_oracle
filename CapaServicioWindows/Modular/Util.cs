using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Modular
{
    public class Util
    {
        public  List<BataTransac.Ent_PathDBF> get_location_dbf(ref string _error_ws)
        {
            List<BataTransac.Ent_PathDBF> list = null;
            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                var list_ws =bata_trans.ws_get_location_dbf(header_user);

                if (list_ws!=null)
                {
                    list = new List<BataTransac.Ent_PathDBF>();
                    foreach(var listar in list_ws)
                    {
                        BataTransac.Ent_PathDBF valor = new BataTransac.Ent_PathDBF();
                        valor.rutloc_namedbf = listar.rutloc_namedbf;
                        valor.rutloc_location = listar.rutloc_location;
                        list.Add(valor);
                    }
                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
                list = null;                
            }
            return list;
        }
        public void control_errores_transac(string cod_tipo, string error_des,ref string _error_ws)
        {
            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;
                /****************************************************************/

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                bata_trans.ws_errores_transaction(header_user, cod_tipo, error_des);
            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;

            }
        }

    }
}
