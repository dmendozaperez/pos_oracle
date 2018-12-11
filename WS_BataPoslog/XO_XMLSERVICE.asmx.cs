using CapaDato.Control;
using CapaDato.Transac;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WS_BataPoslog
{
    /// <summary>
    /// Descripción breve de XO_XMLSERVICE
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class XO_XMLSERVICE : IPoslogStrReceiverApiPortBinding
    {

        [WebMethod]
        public void postTransaction(string rawPoslogString)
        {
            Dat_PosLog insert_bd = null;
            try
            {

                //Ent_Conexion.conexion = ConfigurationManager.ConnectionStrings["SQL_PROD_PE"].ConnectionString;
                //Ent_Conexion.conexion_posperu = ConfigurationManager.ConnectionStrings["SQL_PROD_PE"].ConnectionString;
                //Ent_Conexion.conexion_posperu_DES = ConfigurationManager.ConnectionStrings["SQL_DES_PE"].ConnectionString;
                //Ent_Conexion.conexion_posperu_QA = ConfigurationManager.ConnectionStrings["SQL_QA_PE"].ConnectionString;
                //Ent_Conexion.conexion_posecuador = ConfigurationManager.ConnectionStrings["SQL_PROD_EC"].ConnectionString;
                //Ent_Conexion.conexion_posecuador_QA = ConfigurationManager.ConnectionStrings["SQL_QA_EC"].ConnectionString;



                /*ambiente_bd*/
                /*PROD=PRODUCCION*/
                /*DES=DESARROLLO*/
                /*QA=QA*/

                //string ambiente_bd = "PROD";
                //string pais = "EC";

                string ambiente_bd = ConfigurationManager.AppSettings["AMBIENTE"].ToString();
                string pais = ConfigurationManager.AppSettings["PAIS"].ToString();// "EC";


                insert_bd = new Dat_PosLog();
                string _valida_error=insert_bd.InsertarTransac_Poslog(rawPoslogString, ambiente_bd, pais);
                /*en este proceso quiere decir que paso un error en la transacion del pos log
                 CODIGO DE ERROR 01*/
                if (_valida_error.Length>0)
                {
                    Dat_Error_Transac error_transac = new Dat_Error_Transac();
                    string tipo_error = "01";
                    error_transac.insertar_errores_transac(tipo_error, _valida_error, ambiente_bd);
                }

                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
    }
}
