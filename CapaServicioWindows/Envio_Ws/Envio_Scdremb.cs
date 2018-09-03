using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Envio_Ws
{
    public class Envio_Scdremb
    {
        public string envio_ws_Scdremb(BataTransac.Ent_List_Scdrem listscdremb)
        {
            string valida_envio = "";

            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;
                /****************************************************************/

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                bata_trans.Endpoint.Binding.SendTimeout = TimeSpan.FromMinutes(5);
                var envio = bata_trans.ws_envia_scdremb(header_user, listscdremb);

                if (envio.codigo != "0")
                {
                    valida_envio = envio.descripcion;
                }

            }
            catch (Exception exc)
            {
                valida_envio = exc.Message;
            }
            return valida_envio;
        }
    }
}
