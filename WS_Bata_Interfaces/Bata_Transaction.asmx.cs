using CapaBasico.Util;
using CapaDato.Basico;
using CapaDato.Control;
using CapaDato.Interfaces;
using CapaEntidad.Interfaces;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WS_Bata_Interfaces
{
    /// <summary>
    /// Descripción breve de Bata_Transaction
    /// </summary>
    [WebService(Namespace = "http://bataperu.com.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Bata_Transaction : System.Web.Services.WebService
    {
        public ValidateAcceso Authentication;

       

        Ba_WsConexion autentication_ws;
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Consula de bienvenida")]
        public Ent_MsgTransac HelloWorld()
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            try
            {
                msg_transac = new Ent_MsgTransac();
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    msg_transac.codigo = "0";
                    msg_transac.descripcion = "Conexión exitosa";
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

        [WebMethod]
        public Ent_Fvdespc fvdespc()
        {
            return new Ent_Fvdespc(); 
        }
        [WebMethod]
        public Ent_Scdddes scdddes()
        {
            return new Ent_Scdddes();
        }
        [WebMethod]
        public Ent_PathDBF pathdbf()
        {
            return new Ent_PathDBF();
        }

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Update de transaccion de guias")]
        public Ent_MsgTransac ws_update_transaction_guias(Ent_Fvdespc fvdespc,Ent_Scdddes scdddes)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_RecepcionGuias update_guias = null;
            try
            {
                msg_transac =new Ent_MsgTransac();
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    update_guias = new Dat_RecepcionGuias();
                    msg_transac=update_guias.update_transaction_guias(fvdespc,scdddes);
                    /*********************************************************/   
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

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Control de Errores de Transacciones")]
        public Ent_MsgTransac ws_errores_transaction(string tip_error,string msg)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Error_Transac error_transac = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    error_transac = new Dat_Error_Transac();
                    error_transac.insertar_errores_transac(tip_error,msg);
                    msg_transac.codigo = "0";
                    msg_transac.descripcion = "ok";
                    /*********************************************************/
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

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Location DBF")]
        public List<Ent_PathDBF> ws_get_location_dbf()
        {
            List<Ent_PathDBF> list = null;
            autentication_ws = new Ba_WsConexion();
            try
            {
                list = new List<Ent_PathDBF>();
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    Dat_Util locationDBF = new Dat_Util();
                    list = locationDBF.get_location_dbf();
                }
            }
            catch (Exception)
            {

                list=null;
            }
            return list;
        } 

    }

    public class ValidateAcceso:SoapHeader
    {
        public string Username;
        public string Password;
    }
}
