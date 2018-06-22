using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Ws_ConsultReniecSunat.Bll;

namespace Ws_ConsultReniecSunat
{
    /// <summary>
    /// Descripción breve de Sunat_Reniec_PE
    /// </summary>
    [WebService(Namespace = "http://bataperu.com.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Sunat_Reniec_PE : System.Web.Services.WebService
    {       
        public validateLogin Authentication;
        #region<METODO PARA CONSULTAR RENIEC>
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Consulta Data Reniec")]
        public DataReniec ws_persona_reniec(string nro_dni)
        {
            PersonaReniec myInfo = null;
            string _codigo_captcha = "";
            DataReniec data = null;
            ReniecValida valida = null;
            //string _error = "";
            try
            {

                if (ckeckAuthentication(Authentication.Username, Authentication.Password, "demo") > 0)
                { 
                    if (nro_dni.Trim().Length==0)
                    {
                        data = new DataReniec();
                        valida = new ReniecValida();
                        valida.Estado = "1";
                        valida.Descripcion = "El numero de D.N.I no puede estar vacio";
                        data.Valida_Reniec = valida;
                        return data;
                    }

                    if (nro_dni.Trim().Length != 8)
                    {
                        data = new DataReniec();
                        valida = new ReniecValida();
                        valida.Estado = "2";
                        valida.Descripcion = "Error de formato del numero D.N.I";
                        data.Valida_Reniec = valida;
                        return data;
                    }
         
                    string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
                    string _tessdata = Path.GetDirectoryName(filePath) + "\\tessdata";

                    myInfo = new PersonaReniec(true);

                    //_codigo_captcha = myInfo.UseTesseract(_tessdata);
                    myInfo.GetInfo(nro_dni, _codigo_captcha);

                    //if (myInfo.Nombres == null)
                    //{
                    //    _codigo_captcha = myInfo.UseTesseract( _tessdata);
                    //    myInfo.GetInfo(nro_dni, _codigo_captcha);
                    //}

                    //if (myInfo.Nombres == null)
                    //{
                    //    _codigo_captcha = myInfo.UseTesseract( _tessdata);
                    //    myInfo.GetInfo(nro_dni, _codigo_captcha);
                    //}

                    if (myInfo.estado=="error")
                    {
                        data = new DataReniec();
                        valida = new ReniecValida();
                        valida.Estado = "3";
                        valida.Descripcion = "El Numero de D.N.I no existe ó vuelve a intentarlo " + _tessdata;
                        data.Valida_Reniec = valida;
                    }
                    else
                    {  
                        data = new DataReniec();
                        data.Dni = nro_dni;
                        data.Nombres = myInfo.Nombres;
                        data.ApePat = myInfo.ApePaterno;
                        data.ApeMat = myInfo.ApeMaterno;
                        valida = new ReniecValida();
                        valida.Estado = "0";
                        valida.Descripcion = "Correcto";
                        data.Valida_Reniec = valida;
                    }
                }
                else
                {
                    data = new DataReniec();
                    valida = new ReniecValida();
                    valida.Estado = "4";
                    valida.Descripcion = "Error de Conexion";
                    data.Valida_Reniec = valida;
                }
            }
            catch(Exception exc)
            {
                data = new DataReniec();
                valida = new ReniecValida();
                valida.Estado = "4";
                valida.Descripcion = "Error de Conexion ";
                data.Valida_Reniec = valida;
            }
            return data;            
        }
        #endregion
        #region<METODO PARA CONSULTAR SUNAT>
        [SoapHeader("Authentication",Required =true)]
        [WebMethod(Description ="Consulta Data Sunat")]
        public DataSunat ws_persona_sunat(string nro_ruc)
        {          
            PersonaSunat myInfo=null;
            string _codigo_captcha = "";
            DataSunat data = null;
            SunatValida valida = null;
            try
            {

                if (ckeckAuthentication(Authentication.Username, Authentication.Password, "demo") > 0)
                { 
                    if (nro_ruc.Trim().Length == 0)
                        {
                            data = new DataSunat();
                            valida = new SunatValida();
                            valida.Estado = "1";
                            valida.Descripcion = "El numero de R.U.C no puede estar vacio";
                            data.Valida_Sunat = valida;
                            return data;
                        }


                    if ((nro_ruc.Trim().Substring(0, 1) != "1" && nro_ruc.Trim().Substring(0, 1) != "2") || nro_ruc.Trim().Length != 11)
                    {
                        data = new DataSunat();
                        valida = new SunatValida();
                        valida.Estado = "2";
                        valida.Descripcion = "Error de formato del numero R.U.C";
                        data.Valida_Sunat = valida;
                        return data;
                    }

                    string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
                    string _tessdata = Path.GetDirectoryName(filePath) + "\\tessdata";
                    myInfo = new PersonaSunat(true);
                    //_codigo_captcha = myInfo.UseTesseract(_tessdata);
                    myInfo.GetInfo(nro_ruc, _codigo_captcha);

                    //if (myInfo.Nombres == "Error!")
                    //{
                    //    _codigo_captcha = myInfo.UseTesseract(_tessdata);
                    //    myInfo.GetInfo(nro_ruc, _codigo_captcha);
                    //}
                    //if (myInfo.Nombres == "Error!")
                    //{
                    //    _codigo_captcha = myInfo.UseTesseract(_tessdata);
                    //    myInfo.GetInfo(nro_ruc, _codigo_captcha);
                    //}
                    //if (myInfo.Nombres == "Error!")
                    //{
                    //    _codigo_captcha = myInfo.UseTesseract(_tessdata);
                    //    myInfo.GetInfo(nro_ruc, _codigo_captcha);
                    //}


                    if (myInfo.Nombres == "Error!")
                    {
                        data = new DataSunat();
                        valida = new SunatValida();
                        valida.Estado = "3";
                        valida.Descripcion = "El Numero de R.U.C no existe ó vuelve a intentarlo";
                        data.Valida_Sunat = valida;
                    }
                    else
                    {
                        data = new DataSunat();
                        data.Ruc = nro_ruc;
                        data.Razon_Social = myInfo.Nombres;
                        data.Telefono = myInfo.telefono;
                        data.Direccion = myInfo.direccion;
                        data.Telefono = myInfo.telefono;
                        data.Estado = myInfo.estado;

                        valida = new SunatValida();
                        valida.Estado = "0";
                        valida.Descripcion = "Correcto";
                        data.Valida_Sunat = valida;
                    }
                }   
                else
                {
                    data = new DataSunat();
                    valida = new SunatValida();
                    valida.Estado = "4";
                    valida.Descripcion = "Error de Conexion";
                    data.Valida_Sunat = valida;
                }             
            }
            catch
            {
                data = new DataSunat();
                valida = new SunatValida();
                valida.Estado = "4";
                valida.Descripcion = "Error de Conexion";
                data.Valida_Sunat = valida;
                
            }
            return data;
        }
        #endregion
        #region Authentication
        public int ckeckAuthentication(string username, string password, string clientType)
        {
            int ret = 0;
            string _user = "BataPeru";
            string _pass = "Bata2018**.";
            if (clientType == "demo")
            {

            }

            if (username == _user && password == _pass)
            {
                ret = 1;
            }
            return ret;
        }
        #endregion
    }
    public class validateLogin : SoapHeader
    {
        public string Username;
        public string Password;
    }
}
