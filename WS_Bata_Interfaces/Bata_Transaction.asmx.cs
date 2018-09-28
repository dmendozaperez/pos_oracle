using CapaBasico.Util;
using CapaDato.Basico;
using CapaDato.Control;
using CapaDato.Interfaces;
using CapaDato.Logistica;
using CapaDato.Tienda;
using CapaDato.Venta;
using CapaEntidad.Interfaces;
using CapaEntidad.Logistica;
using CapaEntidad.Util;
using CapaEntidad.Venta;
using System;
using System.Collections.Generic;
using System.Data;
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
        public Ent_MsgTransac HelloWorld(string cod_tda)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Acceso conexion_tda = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    msg_transac.codigo = "0";
                    msg_transac.descripcion = "Conexión exitosa";

                    conexion_tda = new Dat_Acceso();
                    conexion_tda._conexion_tda(cod_tda, msg_transac.descripcion);
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
        [WebMethod]
        public Ent_File list_file()
        {
            return new Ent_File();
        }
        public Ent_Lista_File list_file_items()
        {
            return new Ent_Lista_File();
        }
        public Ent_List_Scdrem scdremb()
        {
            return new Ent_List_Scdrem();
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

        /// <summary>
        /// errores de transaccionnes a nivel dbf o sql
        /// </summary>
        /// <param name="tipo de error"></param>
        /// <param name="mensaje del error"></param>
        /// <returns></returns>
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

        /// <summary>
        /// localizacion de los dbf de traspasos y otros
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// metodo para verificar los archivos no subido
        /// </summary>
        /// <param name="tipo de archivo"></param>
        /// <param name="lista de achivo del cliente compara con server"></param>
        /// <returns></returns>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Get File Upload")]
        public List<Ent_File> ws_get_file_upload(string tipofile_cod,Ent_Lista_File lista_in)
        {
            List<Ent_File> lista_out = null;
            Dat_File file_upload = null;
            autentication_ws = new Ba_WsConexion();
            try
            {
                file_upload = new Dat_File();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    lista_out = file_upload.get_filtrar_file(tipofile_cod, lista_in);
                }
                   
            }
            catch (Exception)
            {
                lista_out = null;                
            }
            return lista_out;
        }
        /// <summary>
        /// metodo para verificar la ruta de origen de los archivos
        /// </summary>
        /// <param name="tipo de archivo"></param>
        /// <returns></returns>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Get File Path")]
        public Ent_File_Ruta ws_get_file_path(string tipo_file_cod)
        {
            Ent_File_Ruta file_ruta = null;
            autentication_ws = new Ba_WsConexion();
            Dat_File file_path = null;
            try
            {
                file_path = new Dat_File();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    file_ruta = file_path.get_ruta_file(tipo_file_cod);
                }
            }
            catch (Exception)
            {
                file_ruta = null;
            }
            return file_ruta;
        }
        /// <summary>
        /// subir archivo al server
        /// </summary>
        /// <param name="file en bytes[]"></param>
        /// <param name="nombre del arvhico"></param>
        /// <param name="tipo de archivo"></param>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Subir archivos al server")]
        public void ws_download_file(Byte[] file,string file_name,string file_tipo)
        {
            autentication_ws = new Ba_WsConexion();
            Ba_DownloadFile dow_file = null;
            try
            {
                dow_file = new Ba_DownloadFile();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    dow_file.download_files(file, file_name, file_tipo);
                }
            }
            catch
            {    
                            
            }
        }

        /// <summary>
        /// Get de tiempo de ejecucion (Intervalo) del servicio transmision net stock de tiendas
        /// </summary>
        /// <param name="cser_cod"></param>
        /// <returns></returns>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Get de tiempo de ejecucion del servicio transmision net stock de tiendas")]
        public Ent_Config_Service ws_get_time_servicetrans(string cser_cod)
        {
            Ent_Config_Service config = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Util dat_service = null;
            try
            {
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    dat_service = new Dat_Util();
                    config = new Ent_Config_Service();
                    config = dat_service.get_config_service(cser_cod);
                }
            }
            catch 
            {
                config = null;
            }
            return config;
        }
        /// <summary>
        /// enviar stock de tiendas
        /// </summary>
        /// <param name="lista_stk"></param>
        /// <returns></returns>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Enviar Stock de tienda servicio transmision net")]
        public Ent_MsgTransac ws_envia_stock_tda(Ent_Lista_Stock lista_stk)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Stock update_stock = null;            
            try
            {
                msg_transac = new Ent_MsgTransac();
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion el stock*/
                    update_stock = new Dat_Stock();
                    msg_transac = update_stock.insertar_stock_tda(lista_stk);
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
        [WebMethod(Description = "Enviar Ventas de tienda")]
        public Ent_MsgTransac ws_envia_venta_tda(string cod_tda, DataSet ds_transac_tda)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta  update_venta = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    update_venta = new Dat_Venta();
                    msg_transac = update_venta.inserta_venta(cod_tda, ds_transac_tda);

                    //update_venta.inserta_venta_208(cod_tda, ds_transac_tda);

                    if (msg_transac.codigo!="0")
                    { 
                        /*transaccione de tiendas*/
                        String tip_error = "04";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, msg_transac.descripcion);
                    }
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
        [WebMethod(Description = "Enviar Ventas de tienda")]
        public Ent_MsgTransac ws_envia_venta_tda_list(string cod_tda, Ent_Venta_List listaventa)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta update_venta = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    update_venta = new Dat_Venta();
                    //msg_transac = update_venta.inserta_venta(cod_tda, ds_transac_tda);

                    update_venta.inserta_venta_list(cod_tda, listaventa);

                    if (msg_transac.codigo != "0")
                    {
                        /*transaccione de tiendas*/
                        String tip_error = "04";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, msg_transac.descripcion);
                    }
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
        [WebMethod(Description = "Enviar Ventas de tienda Lista")]
        public Ent_MsgTransac ws_envia_venta_tda_lista(string cod_tda,Ent_List_Ffactc ffactc,Ent_List_Ffactd ffactd,Ent_List_Fnotaa fnotaa)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta update_venta = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    update_venta = new Dat_Venta();
                    msg_transac = update_venta.inserta_venta_lista(cod_tda, ffactc, ffactd, fnotaa);
                  

                    if (msg_transac.codigo != "0")
                    {
                        /*transaccione de tiendas*/
                        String tip_error = "04";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, msg_transac.descripcion, cod_tda);
                    }
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
        [WebMethod(Description = "Enviar Ventas Paquetes")]
        public string[] ws_transmision_ingreso_nube(Byte[] _archivo_zip, string _name)
        {
            string valida = "";
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Ba_Modular ba_modular = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("04", Authentication.Username, Authentication.Password);

                if (valida_ws)
                {
                    ba_modular = new Ba_Modular();
                    valida = ba_modular.copiar_archivo_tienda_server(_archivo_zip, _name);

                }
                else
                {
                    valida = "usuario y/o contraseña no valida";
                }
            }
            catch (Exception exc)
            {
                valida = exc.Message;
            }

            String[] _respuesta = new String[] { "codigo", "descripcion" };
            string _error_codigo = "";
            string _mensaje = "";            
            if (valida.Length == 0)
            {
                _error_codigo = "1";
                _mensaje = "transmision exitosa";              
                _respuesta[0] = _error_codigo.ToString();
                _respuesta[1] = _mensaje.ToString();
            }
            else
            {                
                _error_codigo = "0";
                _mensaje = valida;
                _respuesta[0] = _error_codigo.ToString();
                _respuesta[1] = _mensaje.ToString();
            }         
            return _respuesta;
        }


        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Enviar Scactco")]
        public Ent_MsgTransac ws_envia_Scactco_list(Ent_List_Scactco listscactco)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Scactco datScactco = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    datScactco = new Dat_Scactco();
                    msg_transac = datScactco.insertar_Scactco(listscactco);

                    if (msg_transac.codigo != "0")
                    {
                        String tip_error = "07";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, msg_transac.descripcion);
                    }
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
        [WebMethod(Description = "Enviar scdremb")]
        public Ent_MsgTransac ws_envia_scdremb(Ent_List_Scdrem list_scdrem)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Scdremb insert_scdremb = null;

            try
            {
                msg_transac = new Ent_MsgTransac();

                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                //Boolean valida_ws = true;
                if (valida_ws)
                {

                    insert_scdremb = new Dat_Scdremb();
                    msg_transac = insert_scdremb.insertar_Scdrem(list_scdrem);

                    if (msg_transac.codigo != "0")
                    {
                        String tip_error = "07";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, msg_transac.descripcion);
                    }

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
        [WebMethod(Description = "Valida Tiendas traspasos")]
        public Boolean ws_valida_traspaso_tda(string  cod_tda)
        {
            Boolean acceso = false;
            autentication_ws = new Ba_WsConexion();
            Dat_Tienda valida_tda = null; 
            try
            {            

                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                //Boolean valida_ws = true;
                if (valida_ws)
                {
                    valida_tda = new Dat_Tienda();

                    acceso = valida_tda.tienda_traspaso(cod_tda);                   
                }
                else
                {
                    acceso = false;
                }

            }
            catch 
            {

                acceso = false;
            }
            return acceso;
        }

    }

    public class ValidateAcceso:SoapHeader
    {
        public string Username;
        public string Password;
    }
}
