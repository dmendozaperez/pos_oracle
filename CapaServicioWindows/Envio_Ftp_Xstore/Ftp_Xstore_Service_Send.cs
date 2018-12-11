using CapaServicioWindows.CapaDato.Interfaces;
using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Envio_Ftp_Xstore
{
    public class Ftp_Xstore_Service_Send
    {
        public void ejecutar_upload_xstore_auto(ref string error)
        {            
            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;
                /****************************************************************/

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                var lista= bata_trans.ws_get_xstore_carpeta_upload(header_user);

                /*solo filtramos los automaticos VARIABLE OPCION A*/
                #region<AUTOMATICO DE PERU>
                var proc_peru = lista.Where(f => f.opcion == "A" && f.pais == "PE" ).FirstOrDefault();

                string _hora_ejecucion=proc_peru.ftp_send;
                DateTime myDt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

                string _hora_actual = myDt.ToString("HH:mm");

                //if (_hora_actual != _hora_ejecucion) return;

                #region<SI LA HORA DE EJECUCION ES LA CORRECTA EJECUTA LAS INTERFACES XOFICCE U ORCE>

                Dat_Tienda get_tda = new Dat_Tienda();

                DataTable dt_tienda = get_tda.get_tienda_xstore("PE");

                Dat_Interfaces dat_inter = null;
                string pais = "PE";
                DataTable dt_item = null;
                DataTable dt_images = null;
                DataTable dt_merch_hier = null;
                DataTable dt_price_update = null;
                #region<ENTORNO XOFICCE>
                foreach (DataRow fila_tda in dt_tienda.Rows)
                {                   
                    string cod_tda = fila_tda["cod_entid"].ToString();
                    var env_peru_XO = lista.Where(f => f.opcion == "A" && f.pais == pais);
                    dat_inter = new Dat_Interfaces();
                    var inter_det = dat_inter.lista_inter_pl(pais);                  
                    foreach (var env_peru_det in env_peru_XO.Where(ent=>ent.entorno=="XOFICCE") )
                    {
                        var inter_det_entorno = inter_det.Where(m => m.entorno == env_peru_det.entorno);

                        foreach(var det_inter in inter_det_entorno)
                        {
                          
                            genera_automatico_inter(pais, cod_tda, env_peru_det.rut_upload, det_inter.inter_nom,det_inter.entorno,
                                                   ref dt_item,ref dt_images,ref dt_merch_hier,ref  dt_price_update);
                        }
                    }
                                        
                }
                #endregion
                #region<ENTORNO ORCE>
                    var env_peru_CE = lista.Where(f => f.opcion == "A" && f.pais == pais);                 
                    dat_inter = new Dat_Interfaces();
                    var inter_det_CE = dat_inter.lista_inter_pl(pais);
                    foreach (var env_peru_det in env_peru_CE.Where(ent => ent.entorno == "ORCE"))
                    {
                        var inter_det_entorno = inter_det_CE.Where(m => m.entorno == env_peru_det.entorno);

                        foreach (var det_inter in inter_det_entorno)
                        {                          
                            genera_automatico_inter(pais, "", env_peru_det.rut_upload, det_inter.inter_nom, det_inter.entorno,
                                                    ref dt_item,ref dt_images,ref dt_merch_hier,ref dt_price_update);
                        }
                    }
                #endregion

                #endregion

                #endregion
            }
            catch (Exception exc) 
            {
                error = exc.Message;
            }         
        }
        private DataTable dt_replace_tda(DataTable dt)
        {
            DataTable dt_replace = null;
            try
            {

                if (dt != null)
                {
                    dt_replace = dt;
                    string file_cab = dt.Rows[0][0].ToString();

                    string cad = file_cab.Substring(file_cab.IndexOf(':') - 6, file_cab.IndexOf(':') + 7);
                }
            }
            catch 
            {
                dt_replace = null;                
            }
            return dt_replace;
        }
        private void genera_automatico_inter(string _pais,string _codtda, string _gen_ruta,string _gen_inter_name,string _entorno,
                                             ref DataTable dt_item,ref DataTable dt_images, ref DataTable dt_merch_hier,
                                             ref DataTable dt_price_update)
        {
            Dat_Interfaces dat_geninter = null;
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                if (!Directory.Exists(@_gen_ruta)) Directory.CreateDirectory(@_gen_ruta);

                StringBuilder str = null;
                string str_cadena = ""; string name_file = ""; string sufijoNombre = _codtda + "_"; string in_maestros = "";
                dat_geninter = new Dat_Interfaces();

                switch(_entorno)
                {
                    case "XOFICCE":
                        switch (_gen_inter_name)
                        {
                            case "ITEM":
                                #region<ITEM>
                                if (dt_item==null)
                                {
                                    dt = dat_geninter.get_item_PE(_pais, _codtda);
                                    dt_item = dt;
                                }
                                else
                                {
                                    dt_replace_tda(dt_item);
                                }                                                              

                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        Decimal i = 0;
                                        foreach (DataRow fila in dt.Rows)
                                        {

                                            str.Append(fila["ITEM"].ToString());
                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }
                                            i += 1;

                                        }

                                        str_cadena = str.ToString();



                                        name_file = "ITEM_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                }
                                break;
                            #endregion
                            case "PRICE_UPDATE":
                                #region<PRICE_UPDATE>                        
                                dt = dat_geninter.get_price_update_2_PE(_pais, _codtda);
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["PRICE_UPDATE_2"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();

                                        name_file = "PRICE_UPDATE_2_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                }

                                break;
                            #endregion
                            case "MERCH_HIER":
                                #region<MERCH_HIER>                       
                                dt = dat_geninter.get_merch_hier_PE(_pais, _codtda);
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        Decimal i = 0;
                                        foreach (DataRow fila in dt.Rows)
                                        {

                                            str.Append(fila["MERCH_HIER"].ToString());
                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }
                                            i += 1;

                                        }


                                        str_cadena = str.ToString();



                                        name_file = "MERCH_HIER_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                }
                                break;
                            #endregion
                            case "ITEM_IMAGES":
                                #region<ITEM_IMAGES>                   
                                dt = dat_geninter.get_item_images_PE(_pais, _codtda);
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["ITEM_IMAGES"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "ITEM_IMAGES_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                }
                                break;
                                #endregion
                        }
                        break;
                    case "ORCE":
                        switch (_gen_inter_name)
                        {
                            case "ITEM_MAINTENANCE":
                                #region<ITEM_MAINTENANCE>                                                                
                                    dt = dat_geninter.ItemMaintenance_PE();
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["ItemMaintenance"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();



                                            name_file = "ItemMaintenance_" + DateTime.Today.ToString("yyyyMMdd") + ".XML";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        }
                                    }
                                
                                break;
                            #endregion
                            case "MERCHANDISE_HIERARCHY_MAINTENANCE":
                                #region<MERCHANDISE_HIERARCHY_MAINTENANCE>                               
                                    dt = dat_geninter.MerchandiseHierarch_PE();
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["MerchandiseHierarchyMaintenance"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();

                                            name_file = "MerchandiseHierarchyMaintenance_" + DateTime.Today.ToString("yyyyMMdd") + ".XML";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        }
                                    }                              
                                break;
                            #endregion
                            case "ORCE RETAIL_LOCATIONS":
                                #region<ORCE RETAIL_LOCATIONS>                                
                                    dt = dat_geninter.OrcRetailLocations_PE();
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["strXml"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();



                                            name_file = "RetailLocations_" + DateTime.Today.ToString("yyyyMMdd") + ".XML";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        }
                                    }                                
                                break;
                                #endregion
                        }
                        break;
                }

               


            }
            catch
            {


            }
        }
    }
}
