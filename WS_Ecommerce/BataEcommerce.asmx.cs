using BarcodeLib;
using BataClub_Correo;
using BataClub_DNI;
using CapaBasico.Util;
using CapaDato.Control;
using CapaDato.Ecommerce;
using CapaEntidad.Ecommerce;
using CapaEntidad.Util;
using DnsClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WS_Ecommerce
{
    /// <summary>
    /// Descripción breve de BataEcommerce
    /// </summary>
    [WebService(Namespace = "http://bata.ecommerce.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class BataEcommerce : System.Web.Services.WebService
    {

        public ValidateAcceso Authentication;
        Ba_WsConexion autentication_ws;
        
        /// <summary>
        /// GET STOCK DE TIENDA
        /// </summary>
        /// <param name="codigo de articulo"></param>
        /// <param name="talla del articulo"></param>
        /// <param name="codigo de ubigeo"></param>
        /// <returns></returns>
        //[SoapHeader("Authentication", Required = true)]
        //[WebMethod(Description = "Enviar Stock de tienda servicio transmision net")]
        //public Ent_Stock_Lista ws_get_stk_tda(string cod_art,string talla,string cod_ubigeo)
        //{
        //    Ent_Stock_Lista result = null;
        //    autentication_ws = new Ba_WsConexion();
        //    Dat_Stock_Tienda get_stock = null;
        //    Ent_Stock_Tienda_Acceso valida_msg = null;
        //    try
        //    {
        //        valida_msg = new Ent_Stock_Tienda_Acceso();
        //        result = new Ent_Stock_Lista();
        //        /*valida acceso a web service*/
        //        Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("03", Authentication.Username, Authentication.Password);
        //        if (valida_ws)
        //        {
        //            get_stock = new Dat_Stock_Tienda();
        //            result = get_stock.get_stock_tienda(cod_art, talla, cod_ubigeo);
        //        }
        //        else
        //        {
        //            valida_msg.estado = "-1";
        //            valida_msg.descripcion = "Conexión sin exito";
        //            result.valida = valida_msg;
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        valida_msg.estado = "-1";
        //        valida_msg.descripcion = exc.Message;
        //        result.valida = valida_msg;

        //    }
        //    return result;
        //}

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Enviar los datos del cliente")]
        public Ent_MsgTransac ws_registrar_Cliente(Ent_Cliente_BataClub Cliente)
        {
            Ent_MsgTransac result = new Ent_MsgTransac();
            autentication_ws = new Ba_WsConexion();
            Dat_Cliente_Bata DaCliente = null; ;
            Ent_Conexion.conexion_posperu = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString; //ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
            Ent_Conexion.conexion = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString; //ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
            MailAddress mailAddress = null;
            LookupClient dnsClient = new LookupClient() { UseTcpOnly = true };
            try
            {

                if (Cliente.dni.Trim().Length == 0) { result.codigo = "-1"; result.descripcion = result.descripcion + "DNI es obligatorio."; };

                #region<VALIDAR NUMERO DE DNI>
                ValidaDNI obj_valida = new ValidaDNI();
                Boolean valida = obj_valida.ValidateDocument(Cliente.dni.Trim());

                if (valida) { result.codigo = "-1"; result.descripcion = result.descripcion + "El Numero de DNI no es Correcto."; };

                #endregion

                if (Cliente.canal.Trim().Length == 0) { result.codigo = "-1"; result.descripcion = result.descripcion + "Canal es obligatorio."; };
                if (Cliente.correo.Trim().Length == 0) { result.codigo = "-1"; result.descripcion = result.descripcion + "Correo es obligatorio."; };
                if (Cliente.primerNombre.Trim().Length == 0) { result.codigo = "-1"; result.descripcion = result.descripcion + "Nombre es obligatorio."; };
                if (Cliente.apellidoPater.Trim().Length == 0) { result.codigo = "-1"; result.descripcion = result.descripcion + "Apellido paterno es obligatorio."; };

                DaCliente = new Dat_Cliente_Bata();
                #region<EXISTE EMAIL>
                if (result.codigo!="-1")
                { 
                    result = DaCliente.Existe_Email_BataClub(Cliente);
                    Cliente.ubigeo_distrito = (Cliente.ubigeo_distrito == null) ? "" : Cliente.ubigeo_distrito;
                    Cliente.ubigeo = (Cliente.ubigeo == null) ? "" : Cliente.ubigeo;
                }
                #region<VALIDACION DE CORREO SI ES EXISTE>
                Dat_Acceso dat_valida_correo = new Dat_Acceso();
                Boolean valida_correo_bd = dat_valida_correo.valida_correo(Cliente.correo);
                if (!valida_correo_bd)
                {
                    if (result.codigo != "-1")
                    {
                        ValidacionEmail verifica = new ValidacionEmail();
                        Boolean valida_correo = verifica.sendEmail_verificar(Cliente.correo);

                        //mailAddress = new MailAddress(Cliente.correo);
                        //var mxRecords = dnsClient.Query(mailAddress.Host, QueryType.MX).AllRecords.MxRecords().ToList();
                        if (!valida_correo)
                        {
                            result.codigo = "-1";
                            result.descripcion = "El Correo " + Cliente.correo + " no existe..";
                        }

                    }
                }
                
                #endregion

                //if (result.codigo=="-1")
                //{

                //}

                #endregion

                if (result.codigo != "-1") { 
                    Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("03", Authentication.Username, Authentication.Password);
                    if (valida_ws)
                    {
                      
                        result = DaCliente.Resgistrar_ClienteBtaclub(Cliente, Authentication.Username);

                    } else {
                        result.codigo = "-1";
                        result.descripcion = "Error de autentificacion";
                  
                    }
                }
            }
            catch (Exception exc)
            {
                result.codigo = "-1";
                result.descripcion = exc.Message;

            }
            return result;
        }

        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Consultar los datos del cliente")]
        public Ent_Cliente_BataClub ws_consultar_Cliente(Cliente_Parameter_Bataclub dni)
        {
            Ent_Cliente_BataClub result = new Ent_Cliente_BataClub();
            autentication_ws = new Ba_WsConexion();
            Dat_Cliente_Bata DaCliente = null; ;
            Ent_Conexion.conexion_posperu = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString; //ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
            Ent_Conexion.conexion = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString; //ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;

            try
            {
                string correo_update = dni.correo_update;

                result.descripcion_error = "";
                result.existe_cliente = false;
                if (dni==null) { result.descripcion_error = "Ingrese el numero de DNI"; }
                string str_dni = "";

                Boolean envia_email = false;


                if (dni.envia_correo==null)
                {
                    /*enviar correo*/                    
                    dni.envia_correo = "1";
                    envia_email = true;
                }
                else
                {
                    if (dni.envia_correo.Length==0)
                    {
                        envia_email = true;
                        /*enviar correo*/
                        dni.envia_correo = "1";
                    }
                }


                //string str_barra_dni = "";
                    Boolean valida_dni = false;
                if (dni.dni!=null)
                {
                    if (dni.dni.Length>0)
                    {
                        str_dni = dni.dni;
                        valida_dni = true;
                    }
                }
                if (dni.dni_barra != null)
                {
                    if (dni.dni_barra.Length > 0)
                    {
                        envia_email = false;
                        valida_dni = true;
                        str_dni = dni.dni_barra;
                    }
                }

                if (!valida_dni) { result.descripcion_error = "Ingrese el numero de DNI"; }

                if (result.descripcion_error.Length == 0)
                {
                    Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("03", Authentication.Username, Authentication.Password);
                    if (valida_ws)
                    {
                        DaCliente = new Dat_Cliente_Bata();
                        result = DaCliente.Consultar_ClienteBataclub(str_dni);



                        /*si el cliente existe */
                        if (result.existe_cliente)
                        {
                            if (correo_update!=null)
                            {
                                if (correo_update.Length>0)
                                {
                                    /*si se quiere enviar email al correo*/
                                    if (envia_email)
                                    {
                                        /*03	ENVIO DE CORREO ACTUALIZACION DE DATOS*/
                                        DaCliente.Insert_Envio_Correo(correo_update/*result.correo*/, result.dni, "03");
                                    }
                                }
                            }
                            else
                            {

                                if (result.correo.Length > 0)
                                {
                                    /*si se quiere enviar email al correo*/
                                    if (envia_email)
                                    {
                                        /*03	ENVIO DE CORREO ACTUALIZACION DE DATOS*/
                                        DaCliente.Insert_Envio_Correo(result.correo, result.dni, "03");
                                    }
                                }
                            }

                            //if (result.correo.Length>0)
                            //{ 
                            //    /*si se quiere enviar email al correo*/
                            //    if (envia_email)
                            //    {
                            //        /*03	ENVIO DE CORREO ACTUALIZACION DE DATOS*/
                            //        DaCliente.Insert_Envio_Correo(result.correo, result.dni, "03");
                            //    }
                            //}
                        }

                    }
                    else
                    {                        
                        result.descripcion_error = "Error de autentificacion";

                    }
                }
            }
            catch (Exception exc)
            {
                result.descripcion_error = exc.Message;
            }
            return result;
        }

        //[SoapHeader("Authentication", Required = true)]
        //[WebMethod(Description = "Generar Codigo de barra")]
        [WebMethod]
        public Ent_MsgTransacBarra ws_genera_barra(string user,string password,string barra)
        {
            //Autenticacion
            Ent_MsgTransacBarra res = new Ent_MsgTransacBarra();
            autentication_ws = new Ba_WsConexion();
            Authentication = new ValidateAcceso();
            if (user.Length>0 && password.Length>0)
            {
                Authentication.Username = user;
                Authentication.Password = password;
            }
            Ent_Conexion.conexion_posperu = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString; //ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
            Ent_Conexion.conexion = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString; //ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
            Ba_DownloadFile DaBarra = null;
            string result = "";
            string ubi_Archivo = "";                     
            try
            {
                if (barra.Trim().Length == 0) { res.estado = "-1"; res.descripcion = res.descripcion + "Código de barra es obligatorio."; };

                if (res.estado != "-1")
                {
                    Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("03", Authentication.Username, Authentication.Password);
                    if (valida_ws)
                    {
                        Barcode bc_code = new Barcode();
                        bc_code.IncludeLabel = true;
                        Image img_barra = bc_code.Encode(TYPE.CODE128, barra, Color.Black, Color.White, 400, 100);



                        //dynamicPanel.BackgroundImage = Codigo.Encode(TYPE.CODE128, barra, Color.Black, Color.White, 400, 100);
                        //System.Drawing.Image imgFinal = (System.Drawing.Image)dynamicPanel.BackgroundImage.Clone();
                        DaBarra = new Ba_DownloadFile();

                        byte[] img_bytes= DaBarra.imageToByteArray(img_barra);

                        result = DaBarra.genera_img_barra(img_bytes, barra, ref ubi_Archivo);

                        if (result == "Ok")
                        {
                            res.estado = "0";
                            res.descripcion =  "Se generó correctamente el código de barras y se guardó en " + ubi_Archivo;
                            res.uri = ubi_Archivo;
                        }
                        else
                        {
                            res.estado = "-1";
                            res.descripcion = "Hubo un problema al generar el código de barras";
                            res.uri = ubi_Archivo;
                        }

                    }
                    else
                    {
                        res.estado = "-1";
                        res.descripcion = "Error de autentificacion";
                    }
                }
               
            }

            catch (Exception ex)
            {
                res.estado = "-1";
                res.descripcion = ex.Message;
            }
            return res;
        }

        public class ValidateAcceso : SoapHeader
        {
            public string Username;
            public string Password;
        }
    }
}
