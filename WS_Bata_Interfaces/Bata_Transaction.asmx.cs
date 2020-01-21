using CapaBasico.Util;
using CapaDato.Basico;
using CapaDato.Control;
using CapaDato.Interfaces;
using CapaDato.Logistica;
using CapaDato.Poslog;
using CapaDato.Tienda;
using CapaDato.Util;
using CapaDato.Venta;
using CapaEntidad.FE;
using CapaEntidad.Interfaces;
using CapaEntidad.Logistica;
using CapaEntidad.Poslog;
using CapaEntidad.Util;
using CapaEntidad.Venta;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using WS_Bata_Interfaces.Paperless;

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
        //CapaEntidad.Util.Ent_Conexion.conexion_posperu = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
        
        public Bata_Transaction()
        {
            CapaEntidad.Util.Ent_Conexion.conexion_posperu = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
            CapaEntidad.Util.Ent_Conexion.conexion = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
        }


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
            //CapaEntidad.Util.Ent_Conexion.conexion= ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
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
        [WebMethod(Description = "Lista de almacenes de Ecuador")]
        public List<Ent_Alma_Ecu> ws_lista_alma_Ecu()
        {
            autentication_ws = new Ba_WsConexion();
            List<Ent_Alma_Ecu> listar = null;
            Dat_Alma_Ecu dat_alma = null;
            try
            {
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    dat_alma = new Dat_Alma_Ecu();
                    listar = dat_alma.get_lista_alma_ecu();
                    /*********************************************************/
                }

            }
            catch (Exception)
            {
                listar = null;
            }
            return listar;
        }

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Lista de carpeta Xstore Upload")]
        public List<Ent_CarpetaUpload_Xstore> ws_get_xstore_carpeta_upload()
        {
            autentication_ws = new Ba_WsConexion();
            List<Ent_CarpetaUpload_Xstore> listar = null;
            Dat_ProcXstore dat_proc = null;
            try
            {
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    dat_proc = new Dat_ProcXstore();
                    listar =dat_proc.get_xs_carpeta_upload();
                }
            }
            catch (Exception)
            {
                listar = null;
            }
            return listar;
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
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Subir archivos al server")]
        public string ws_download_file_comunicado(Byte[] file, string file_name, string ruta_server_comunicado,Ent_Comunicado obj_com)
        {
            string error = "";
            autentication_ws = new Ba_WsConexion();
            Ba_DownloadFile dow_file = null;
            try
            {
                dow_file = new Ba_DownloadFile();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    error = dow_file.download_files_comunicado(file, ruta_server_comunicado,file_name, obj_com);
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }
        /// <summary>
        /// subir archivo al server
        /// </summary>
        /// <param name="file en bytes[]"></param>
        /// <param name="nombre del arvhico"></param>
        /// <param name="tipo de archivo"></param>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Subir archivos al server")]
        public string ws_download_file(Byte[] file,string file_name,string file_tipo,string file_creacion,string file_update)
        {
            string error = "";
            autentication_ws = new Ba_WsConexion();
            Ba_DownloadFile dow_file = null;
            try
            {
                dow_file = new Ba_DownloadFile();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    error = dow_file.download_files(file, file_name, file_tipo, file_creacion,file_update);
                }
            }
            catch( Exception exc)
            {
                error = exc.Message;                            
            }
            return error;
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

        #region<ENVIO DE STOCK DE ALMACEN>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Enviar Stock de almacen servicio transaction")]
        public Ent_MsgTransac ws_envia_stock_almacen(Ent_Lista_Stock_Almacen lista_stk)
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
                    msg_transac = update_stock.insertar_stock_tda_almacen(lista_stk);
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
        #endregion

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

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "envio de traspasos de tienda")]
        public Ent_MsgTransac ws_envio_traspaso_tda(string cod_tda, List<Ent_Fvdespc> despacho)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_GuiasDespacho update_guias_traspaso = null;
            try
            {

                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    update_guias_traspaso = new Dat_GuiasDespacho();
                    msg_transac = update_guias_traspaso.insertar_guias_traspaso_tda(cod_tda, despacho);
                    /*********************************************************/
                }
                else
                {
                    msg_transac.codigo = "1";
                    msg_transac.descripcion = "Conexión sin exito";
                }

            }
            catch(Exception exc)
            {
                msg_transac.codigo = "1";
                msg_transac.descripcion = exc.Message;                
            }
            return msg_transac;
        }

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Generacion de ticket de retorno")]
        public Ent_Tk_Return ws_genera_cupon_return(Ent_Tk_Set_Parametro param)
        {
            Ent_Tk_Return msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            //Dat_GuiasDespacho update_guias_traspaso = null;
            Dat_Tk_Return tk_return = null;
            try
            {
                msg_transac = new Ent_Tk_Return();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    //update_guias_traspaso = new Dat_GuiasDespacho();
                    tk_return = new Dat_Tk_Return();
                    msg_transac = tk_return.bata_genera_tk_return(param);
                    /*********************************************************/
                }
                else
                {
                    msg_transac.estado_error = "1";
                    //msg_transac.descripcion = "Conexión sin exito";
                }

            }
            catch (Exception exc)
            {
                msg_transac.estado_error = exc.Message;
               
            }
            return msg_transac;
        }

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Valida cupon")]
        public Ent_Tk_Valores ws_valida_cupon_return(Ent_Tk_Get_Parametro param)
        {
            Ent_Tk_Valores msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Tk_Return tk_return = null;
            try
            {
                msg_transac = new Ent_Tk_Valores();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    tk_return = new Dat_Tk_Return();
                    msg_transac = tk_return.bata_valida_tk_return(param);
                }
                else
                {
                    msg_transac.estado_error = "Conexión sin exito";
                }
            }
            catch (Exception exc)
            {
                msg_transac.valida_cupon = "0";
                msg_transac.estado_error = exc.Message;

            }
            return msg_transac;
        }
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Marca como consumido un cupon")]
        public Ent_Tk_Valores ws_consumo_cupon_return (Ent_Tk_Get_Parametro param)
        {
            Ent_Tk_Valores msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            //Dat_GuiasDespacho update_guias_traspaso = null;
            Dat_Tk_Return tk_return = null;
            try
            {
                //msg_transac = new Ent_Tk_Valores();
                //tk_return = new Dat_Tk_Return();
                //msg_transac = tk_return.bata_consumo_tk_return(new Ent_Tk_Get_Parametro() { COD_CUP = cupon , COD_TDA = tienda , FC_SUNA = suna , SERIE = serie , NUMERO = numero , MONTO = monto , FECHA = Convert.ToDateTime( fecha) });

                msg_transac = new Ent_Tk_Valores();
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    //update_guias_traspaso = new Dat_GuiasDespacho();
                    tk_return = new Dat_Tk_Return();
                    msg_transac = tk_return.bata_consumo_tk_return(param);
                    /*********************************************************/
                }
                else
                {
                    msg_transac.estado_error = "Conexión sin exito";
                    //msg_transac.descripcion = "Conexión sin exito";
                }

            }
            catch (Exception exc)
            {
                msg_transac.valida_cupon = "0";
                msg_transac.estado_error = exc.Message;

            }
            return msg_transac;
        }
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "update reimpresion de tickets retorno")]
        public void ws_update_tk_return_reimprimir(string cod_tda, string barra)
        {
            Dat_Tk_Return tk_return = null;
            autentication_ws = new Ba_WsConexion();
            try
            {
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    //update_guias_traspaso = new Dat_GuiasDespacho();
                    tk_return = new Dat_Tk_Return();
                    tk_return.bata_tk_return_imp_update(cod_tda, barra);
                    /*********************************************************/
                }
            }
            catch
            {


            }
        }

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Get reimpresion de tickets retorno")]
        public List<Ent_Tk_Return> ws_get_tk_return_reimprimir(string cod_tda)
        {
            List<Ent_Tk_Return> listar = null;
            Dat_Tk_Return tk_return = null;
            autentication_ws = new Ba_WsConexion();
            try
            {
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    //update_guias_traspaso = new Dat_GuiasDespacho();
                    tk_return = new Dat_Tk_Return();
                    listar = tk_return.bata_tk_return_reimpimir(cod_tda);
                    /*********************************************************/
                }
            }
            catch
            {

            }
            return listar;
        }


        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Get conexion del xstore")]
        public Ent_Conexion_Ora_Xstore ws_get_conexion_xstore()
        {
            Ent_Conexion_Ora_Xstore con = null;
            Dat_Conexion_Ora_Xstore dat_con = null;
            autentication_ws = new Ba_WsConexion();
            try
            {
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    /*en esta validaion entonce sya verifico y va aconsumir la base de datos 
                     para la inyeccion de la guias cerreadas*/
                    //update_guias_traspaso = new Dat_GuiasDespacho();
                    dat_con = new Dat_Conexion_Ora_Xstore();
                    con = dat_con.get_conexion_ora();
                    /*********************************************************/
                }
            }
            catch
            {

            }
            return con;
        }

        #region<envio del poslog desde tienda>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "envio del poslog desde tienda")]
        public string ws_envio_poslog_xstore_tda(Ent_PosLog_Tda param)
        {
            autentication_ws = new Ba_WsConexion();
            Dat_PosLogTda dat_pos = null;
            string error = "";
            try
            {
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    dat_pos = new Dat_PosLogTda();
                    error = dat_pos.inserta_poslog_tda(param);
                    /*envio de poslog*/
                    //update_guias_traspaso = new Dat_GuiasDespacho();

                    /*********************************************************/
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }

        #endregion

        #region<SOSTIC>
        /*sostic 05/2019*/
        [WebMethod(Description = "Consultar disponibilidad de stock en otra tienda")]
        public string[] ws_consulta_stock_otra_tda(string cod_tda, string cod_art, string calidad, string talla, double cant, string cod_tda_b)
        {
            Ent_MsgTransac msg_transac = null;
            string[] _result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta consulta_stock = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                    consulta_stock = new Dat_Venta();
                    //Ent_Conexion.conexion_posperu = "Server=201.240.53.68;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    msg_transac = consulta_stock.consultar_stock_otra_tienda(cod_art, calidad, talla, cant, cod_tda_b);


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
            catch (Exception ex)
            {
                msg_transac.codigo = "1";
                msg_transac.descripcion = ex.ToString() + " ____error";
            }
            _result = new string[] { msg_transac.codigo, msg_transac.descripcion };
            return _result;
        }
        /*sostic 05/2019*/
        [WebMethod(Description = "Enviar guia")]
        public string[] ws_insertar_guia_cvt(string cod_tda, DataSet dsGuia)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta update_venta = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                /**/
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;

                if (valida_ws)
                {
                    update_venta = new Dat_Venta();
                    //Ent_Conexion.conexion_posperu = "Server=201.240.53.68;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    msg_transac = update_venta.insertar_guia(cod_tda, dsGuia.Tables[0], dsGuia.Tables[1]);


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
            return new string[] { msg_transac.codigo, msg_transac.descripcion };
        }

        /*sostic 05/2019*/
        [WebMethod(Description = "Actualizar guia con la serie y numero")]
        public string[] ws_actualizar_guia(string cod_tda, string serie, string numero, int id)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta update_venta = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                /**/
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;

                if (valida_ws)
                {
                    update_venta = new Dat_Venta();
                    //Ent_Conexion.conexion_posperu = "Server=201.240.53.68;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    msg_transac = update_venta.actualizar_guia(cod_tda, serie, numero, id);


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
            return new string[] { msg_transac.codigo, msg_transac.descripcion };
        }
        /*sostic 05/2019*/
        [WebMethod(Description = "Insertar registro en historial_estados_cv")]
        public string[] ws_insertar_historial_estado_cv(string cod_tda, string cod_entid, string fc_nint, string id_estado, string cod_usuario, string descripcion, string cod_vendedor, string serie_numero)
        {

            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta update_venta = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                /**/
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;

                if (valida_ws)
                {
                    update_venta = new Dat_Venta();
                    //Ent_Conexion.conexion_posperu = "Server=201.240.53.68;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    msg_transac = update_venta.insertar_historial_estado_cv(cod_tda, cod_entid, fc_nint, id_estado, cod_usuario, descripcion, cod_vendedor, serie_numero);
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
            return new string[] { msg_transac.codigo, msg_transac.descripcion };
        }

        /*sostic 05/2019*/
        [WebMethod(Description = "Consultar guias")]
        public DataSet ws_consultar_guias(string cod_tda)
        {
            DataSet result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta consulta_stock = null;
            try
            {
                result = new DataSet();
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                    consulta_stock = new Dat_Venta();
                    //Ent_Conexion.conexion_posperu = "Server=201.240.53.68;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    result = consulta_stock.consultar_guias(cod_tda);


                    //if (msg_transac.codigo != "0")
                    //{
                    //    /*transaccione de tiendas*/
                    //    String tip_error = "04";
                    //    Dat_Error_Transac error_transac = new Dat_Error_Transac();
                    //    error_transac.insertar_errores_transac(tip_error, msg_transac.descripcion, cod_tda);
                    //}
                }
                else
                {
                    result = new DataSet();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("codigo");
                    dt.Columns.Add("descripcion");
                    dt.Rows.Add("1", "Conexión sin exito");
                    result.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                result = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo");
                dt.Columns.Add("descripcion");
                dt.Rows.Add("1", ex.ToString() + " ==> Error");
                result.Tables.Add(dt);
            }
            //_result = new string[] { msg_transac.codigo, msg_transac.descripcion };
            return result;
        }
        /*sostic 05/2019*/
        [WebMethod(Description = "Consultar guias actualizadas con el correlativo")]
        public DataSet ws_consultar_guias_actualizadas(string cod_tda, int id)
        {
            DataTable result = null;
            string[] _result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta consulta_stock = null;
            try
            {
                result = new DataTable();
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                    consulta_stock = new Dat_Venta();
                    //Ent_Conexion.conexion_posperu = "Server=201.240.53.68;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    result = consulta_stock.consultar_guias_actualizadas(id);


                    if (result.Columns[0].ColumnName == "codigo")
                    {
                        /*transaccione de tiendas*/
                        String tip_error = "04";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, result.Rows[0][1].ToString(), cod_tda);
                    }
                }
                else
                {
                    result = new DataTable();
                    result.Columns.Add("codigo");
                    result.Columns.Add("descripcion");
                    result.Rows.Add("1", "Conexión sin exito");
                }
            }
            catch (Exception ex)
            {
                result = new DataTable();
                result.Columns.Add("codigo");
                result.Columns.Add("descripcion");
                result.Rows.Add("1", ex.ToString() + " ==> Error");
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(result);
            //_result = new string[] { msg_transac.codigo, msg_transac.descripcion };
            return ds;
        }
        /*sostic 05/2019*/
        [WebMethod(Description = "consultar tiendas disponibles para el canal de ventas.")]
        public DataSet ws_consultar_tiendas_disponibles_cv(string cod_tda)
        {
            DataTable result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta consulta_stock = null;
            DataSet ds = null;
            try
            {
                result = new DataTable();
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                    consulta_stock = new Dat_Venta();
                    result = new DataTable();
                    //Ent_Conexion.conexion_posperu = "Server=201.240.53.68;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    result = consulta_stock.consultar_tiendas_disponibles_cv();


                    if (result.Columns[0].ColumnName == "codigo")
                    {
                        /*transaccione de tiendas*/
                        String tip_error = "04";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, result.Rows[0][1].ToString(), cod_tda);
                    }
                }
                else
                {

                    result = new DataTable();
                    result.Columns.Add("codigo");
                    result.Columns.Add("descripcion");
                    result.Rows.Add("1", "Conexión sin exito");
                }
            }
            catch (Exception ex)
            {

                result = new DataTable();
                result.Columns.Add("codigo");
                result.Columns.Add("descripcion");
                result.Rows.Add("1", ex.ToString() + " ==> Error");
            }
            //_result = new string[] { msg_transac.codigo, msg_transac.descripcion };
            ds = new DataSet();
            ds.Tables.Add(result.Copy());
            return ds;
        }
        /*sostic 06.2018*/
        [WebMethod(Description = "consultar comprobantes Nota de credito multitiendas")]
        public DataSet ws_consultar_comprobantes(string cod_tda, string tipo, string serie, string numero, string cod_entid)
        {
            DataSet result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta consulta_stock = null;
            try
            {
                result = new DataSet();
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                    consulta_stock = new Dat_Venta();
                    //Ent_Conexion.conexion_posperu = "Server=sostic.dyndns.org,10015;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    result = consulta_stock.consultar_comprobantes_nc(tipo, serie, numero, cod_entid);
                    //if (result.Tables[0].Rows[0][0].ToString() != "0")
                    //{
                    //    /*transaccione de tiendas*/
                    //    String tip_error = "04";
                    //    Dat_Error_Transac error_transac = new Dat_Error_Transac();
                    //    error_transac.insertar_errores_transac(tip_error, result.Tables[0].Rows[0][1].ToString(), cod_tda);
                    //}
                }
                else
                {
                    result = new DataSet();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("codigo");
                    dt.Columns.Add("descripcion");
                    dt.Rows.Add("1", "Conexión sin exito");
                    result.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                result = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo");
                dt.Columns.Add("descripcion");
                dt.Rows.Add("1", ex.ToString() + " ==> Error");
                result.Tables.Add(dt);
            }
            //_result = new string[] { msg_transac.codigo, msg_transac.descripcion };
            return result;
        }
        /*sostic 06.2018*/
        #endregion

        #region<consulta facturacion electronica PAPERLESS>
        [WebMethod(Description = "consultar facturacion electronica PAPERLESS")]
        public Ent_Paperless_Return ws_get_FE(string ruc,string login,string password,string tipodoc,string folio,string tipoRetorno)
        {
            Ent_Paperless_Envio env = null;
            Ent_Paperless_Return rt = null;
            FE fe_return = null;
            try
            {
                env = new Ent_Paperless_Envio();
                env.ruc = ruc;
                env.login = login;
                env.password = password;
                env.tipodoc = tipodoc;
                env.folio = folio;
                env.tipoRetorno = tipoRetorno;
                fe_return = new FE();
                rt = fe_return.get_docuento(env);

            }
            catch (Exception exc)
            {
                rt = new Ent_Paperless_Return();
                rt.codigo = "-x";
                rt.respuesta = exc.Message;
            }
            return rt;
        }
        /*sostic 07-2019*/
        [WebMethod(Description = "Consultar si hay algun ganador de la ruleta en la tienda")]
        public DataSet ws_consultar_ganador_ruleta_bata(string cod_tda)
        {
            DataTable result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta datos = null;
            DataSet ds = null;
            try
            {
                result = new DataTable();
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                    datos = new Dat_Venta();
                    result = new DataTable();
                    //Ent_Conexion.conexion_posperu = "Server=sostic.dyndns.org,10015;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    result = datos.consultar_ganador_ruleta_bata(cod_tda);


                    if (result.Columns[0].ColumnName == "codigo")
                    {
                        /*transaccione de tiendas*/
                        String tip_error = "04";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, result.Rows[0][1].ToString(), cod_tda);
                    }
                }
                else
                {

                    result = new DataTable();
                    result.Columns.Add("codigo");
                    result.Columns.Add("descripcion");
                    result.Rows.Add("1", "Conexión sin exito");
                }
            }
            catch (Exception ex)
            {

                result = new DataTable();
                result.Columns.Add("codigo");
                result.Columns.Add("descripcion");
                result.Rows.Add("1", ex.ToString() + " ==> Error");
            }
            //_result = new string[] { msg_transac.codigo, msg_transac.descripcion };
            ds = new DataSet();
            ds.Tables.Add(result.Copy());
            return ds;
        }
        /*sostic 07-2019*/
        [WebMethod(Description = "Actualizar el estado del cupon de ruleta bata")]
        public string[] ws_actualizar_cupon_ruleta(string cod_tda, string codigo, string estado, string doc_vta)
        {
            Ent_MsgTransac msg_transac = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta update_venta = null;
            try
            {
                msg_transac = new Ent_MsgTransac();
                /**/
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;

                if (valida_ws)
                {
                    update_venta = new Dat_Venta();
                   // Ent_Conexion.conexion_posperu = "Server=sostic.dyndns.org,10015;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    msg_transac = update_venta.actualizar_cupon_ruleta(cod_tda, codigo, estado, doc_vta);

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
            return new string[] { msg_transac.codigo, msg_transac.descripcion };
        }
        /*sostic 07-2019*/
        [WebMethod(Description = "Validar cupon ruleta bata")]
        public DataSet ws_validar_cupon_ruleta_bata(string cod_tda, string codigo)
        {
            DataTable result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Venta datos = null;
            DataSet ds = null;
            try
            {
                result = new DataTable();
                //Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("01", Authentication.Username, Authentication.Password);
                Boolean valida_ws = true;
                if (valida_ws)
                {
                    datos = new Dat_Venta();
                    result = new DataTable();
                    //Ent_Conexion.conexion_posperu = "Server=sostic.dyndns.org,10015;Database=BDPOS;User ID=sa;Password=S0stic04052011;Trusted_Connection=False;";
                    result = datos.validar_cupon_ruleta_bata(cod_tda, codigo);


                    if (result.Columns[0].ColumnName == "codigo")
                    {
                        /*transaccione de tiendas*/
                        String tip_error = "04";
                        Dat_Error_Transac error_transac = new Dat_Error_Transac();
                        error_transac.insertar_errores_transac(tip_error, result.Rows[0][1].ToString(), cod_tda);
                    }
                }
                else
                {

                    result = new DataTable();
                    result.Columns.Add("codigo");
                    result.Columns.Add("descripcion");
                    result.Rows.Add("1", "Conexión sin exito");
                }
            }
            catch (Exception ex)
            {

                result = new DataTable();
                result.Columns.Add("codigo");
                result.Columns.Add("descripcion");
                result.Rows.Add("1", ex.ToString() + " ==> Error");
            }
            //_result = new string[] { msg_transac.codigo, msg_transac.descripcion };
            ds = new DataSet();
            ds.Tables.Add(result.Copy());
            return ds;
        }

        #endregion
    }

    public class ValidateAcceso:SoapHeader
    {
        public string Username;
        public string Password;
    }
}
