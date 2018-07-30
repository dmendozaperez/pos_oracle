using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapaEntidad.Util;
using System.Web.Services;
using CapaEntidad.SCDREMB;
using CapaBasico.Util;
using CapaDato.SCDREMB;

namespace WS_PosPeru
{

    [WebService(Namespace = "http://bataperu.com.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PosPeru : System.Web.Services.WebService
    {
        Ba_WsConexion autentication_ws;
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public Ent_MsgTransac Insertar_scdremb(List_Scdrem list_scdrem)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Scdremb insert_scdremb = null;

            try
            {
                msg_transac = new Ent_MsgTransac();
                
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                   
                    insert_scdremb = new Dat_Scdremb();
                    msg_transac = insert_scdremb.insertar_Scdrem(list_scdrem);
                    
                }
                else
                {
                    msg_transac.codigo = "1";
                    msg_transac.descripcion = "Conexión sin exito";
                }

            }
            catch (Exception exc)
            {

                msg_transac.codigo = "1";
                msg_transac.descripcion = exc.Message;
            }
            return msg_transac;
        }

        


    }
}
