using CapaServicioWindows.Conexion;
using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Envio_Ws
{
    public class Envio_Guias
    {
        /// <summary>
        /// envia las guias por web service 
        /// </summary>
        /// <returns></returns>
        public string envio_ws_guias(BataTransac.Ent_Fvdespc fvdespc, BataTransac.Ent_Scdddes scdddes)
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
                var envio=bata_trans.ws_update_transaction_guias(header_user, fvdespc, scdddes);

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
        public string envio_ws_guias_recepcion(BataTransac.Ent_Fvdespc fvdespc)
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
                var envio = bata_trans.ws_update_transaction_guias_recepcion(header_user, fvdespc);

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
