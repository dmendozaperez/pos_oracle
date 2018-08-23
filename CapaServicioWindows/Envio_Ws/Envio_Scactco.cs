using CapaServicioWindows.Conexion;
using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Envio_Ws
{
    public class Envio_Scactco
    {
        /// <summary>
        /// envia las guias por web service  BataTransac
        /// </summary>
        /// <returns></returns>
        public string envio_ws_Scactco(BataTransac.Ent_List_Scactco listCactco)
        {
            string valida_envio = "";

            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username =ConexionWS.user;
                header_user.Password =ConexionWS.password;
                /****************************************************************/

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                bata_trans.Endpoint.Binding.SendTimeout = TimeSpan.FromMinutes(5);
                 var envio=bata_trans.ws_envia_Scactco_list(header_user, listCactco);

                if (envio.codigo!="0")
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
