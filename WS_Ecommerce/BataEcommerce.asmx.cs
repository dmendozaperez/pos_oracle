﻿using CapaBasico.Util;
using CapaDato.Ecommerce;
using CapaEntidad.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Enviar Stock de tienda servicio transmision net")]
        public Ent_Stock_Lista ws_get_stk_tda(string cod_art,string talla,string cod_ubigeo)
        {
            Ent_Stock_Lista result = null;
            autentication_ws = new Ba_WsConexion();
            Dat_Stock_Tienda get_stock = null;
            Ent_Stock_Tienda_Acceso valida_msg = null;
            try
            {
                valida_msg = new Ent_Stock_Tienda_Acceso();
                result = new Ent_Stock_Lista();
                /*valida acceso a web service*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("03", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    get_stock = new Dat_Stock_Tienda();
                    result = get_stock.get_stock_tienda(cod_art, talla, cod_ubigeo);
                }
                else
                {
                    valida_msg.estado = "-1";
                    valida_msg.descripcion = "Conexión sin exito";
                    result.valida = valida_msg;
                }
            }
            catch (Exception exc)
            {
                valida_msg.estado = "-1";
                valida_msg.descripcion = exc.Message;
                result.valida = valida_msg;

            }
            return result;
        }
        public class ValidateAcceso : SoapHeader
        {
            public string Username;
            public string Password;
        }
    }
}