﻿using CapaServicioWindows_x64.CapaDato;
using CapaServicioWindows_x64.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace CapaServicioWindows_x64.Envio_Ftp_Xstore
{
    public class Ftp_Xstore_Service_Send
    {
        public void ejecutar_genera_file_xstore_auto(string _pais, ref string error, ref Boolean gen_per_item, ref Boolean gen_ecu_item)
        {
            StreamWriter tw1 = null;
            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;
                /****************************************************************/

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO DE CONSUMIR LA WEBSERVICE (ws_get_xstore_carpeta_upload)");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                var lista = bata_trans.ws_get_xstore_carpeta_upload(header_user);

                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE CONSUMIR LA WEBSERVICE (ws_get_xstore_carpeta_upload)");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();

                /*solo filtramos los automaticos VARIABLE OPCION A*/
                #region<AUTOMATICO DE PERU>
                var proc_peru = lista.Where(f => f.opcion == "A" && f.pais == _pais).FirstOrDefault();

                string _hora_ejecucion = proc_peru.ftp_send;
                DateTime myDt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

                string _hora_actual = myDt.ToString("HH:mm");

                Boolean valida_dia = valida_lun_ejec();

                if (_hora_actual != _hora_ejecucion) return;

                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (get_tienda_xstore)");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();

                #region<SI LA HORA DE EJECUCION ES LA CORRECTA EJECUTA LAS INTERFACES XOFICCE U ORCE>

                //Dat_Tienda get_tda = new Dat_Tienda(); Lo comente por que no se usa

                DataTable dt_tienda = new DataTable(); //get_tda.get_tienda_xstore(_pais);se cambio para enviar solo COUNTRY EN AUTOMATICO
                dt_tienda.Columns.Add("cod_entid", typeof(string));
                dt_tienda.Columns.Add("des_entid", typeof(string));
                dt_tienda.Columns.Add("cod_distri", typeof(string));
                dt_tienda.Columns.Add("xstore", typeof(Boolean));
                dt_tienda.Columns.Add("outlet", typeof(Boolean));
                dt_tienda.Rows.Add(_pais, "COUNTRY", "", 0, 1);

                /*solo country PE O EC*/

                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (get_tienda_xstore)");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();

                Dat_Interfaces dat_inter = null;
                //string pais = "PE";
                DataTable dt_item = null;
                DataTable dt_images = null;
                DataTable dt_merch_hier = null;
                DataTable dt_price_update = null;
                DataTable dt_item_xref = null;
                #region<ENTORNO XOFICCE>
                foreach (DataRow fila_tda in dt_tienda.Rows)
                {
                    string cod_tda = fila_tda["cod_entid"].ToString();
                    Boolean out_let = Convert.ToBoolean(fila_tda["outlet"]);
                    var env_peru_XO = lista.Where(f => f.opcion == "A" && f.pais == _pais);
                    dat_inter = new Dat_Interfaces();

                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (lista_inter_pl) XOFICCE");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();

                    var inter_det = dat_inter.lista_inter_pl(_pais);
                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (lista_inter_pl) XOFICCE");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();
                    foreach (var env_peru_det in env_peru_XO.Where(ent => ent.entorno == "XOFICCE"))
                    {
                        var inter_det_entorno = inter_det.Where(m => m.entorno == env_peru_det.entorno);

                        foreach (var det_inter in inter_det_entorno)
                        {
                            /*solo esta interface price_update se ejecuta los lunes*/


                            if (det_inter.inter_nom == "PRICE_UPDATE" && valida_dia)
                            {
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (genera_automatico_inter) PRICE_UPDATE XOFICCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                genera_automatico_inter(_pais, cod_tda, env_peru_det.rut_upload, det_inter.inter_nom, det_inter.entorno,
                                                   ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update, ref dt_item_xref, ref gen_per_item, ref gen_ecu_item, out_let);
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (genera_automatico_inter) PRICE_UPDATE XOFICCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                            }

                            /*en este caso se actualiza el PRECIO SOLO DE ECUADOR*/

                            if (det_inter.inter_nom == "PRICE_UPDATE" && _pais=="EC")
                            {
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (genera_automatico_inter) PRICE_UPDATE XOFICCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                genera_automatico_inter(_pais, cod_tda, env_peru_det.rut_upload, det_inter.inter_nom, det_inter.entorno,
                                                   ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update, ref dt_item_xref, ref gen_per_item, ref gen_ecu_item, out_let);
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (genera_automatico_inter) PRICE_UPDATE XOFICCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                            }
                            /******************************************/

                            if (det_inter.inter_nom != "PRICE_UPDATE")
                            {
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (genera_automatico_inter) !=PRICE_UPDATE XOFICCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                genera_automatico_inter(_pais, cod_tda, env_peru_det.rut_upload, det_inter.inter_nom, det_inter.entorno,
                                                   ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update, ref dt_item_xref, ref gen_per_item, ref gen_ecu_item, out_let);
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (genera_automatico_inter) !=PRICE_UPDATE XOFICCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                            }


                        }
                    }

                }
                #endregion
                #region<ENTORNO ORCE>
                var env_peru_CE = lista.Where(f => f.opcion == "A" && f.pais == _pais);
                dat_inter = new Dat_Interfaces();
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (lista_inter_pl)  ORCE");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                var inter_det_CE = dat_inter.lista_inter_pl(_pais);
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (lista_inter_pl)  ORCE");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                foreach (var env_peru_det in env_peru_CE.Where(ent => ent.entorno == "ORCE"))
                {
                    var inter_det_entorno = inter_det_CE.Where(m => m.entorno == env_peru_det.entorno);

                    foreach (var det_inter in inter_det_entorno)
                    {
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (genera_automatico_inter)  ORCE");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                        genera_automatico_inter(_pais, "", env_peru_det.rut_upload, det_inter.inter_nom, det_inter.entorno,
                                            ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update, ref dt_item_xref, ref gen_per_item, ref gen_ecu_item);
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (genera_automatico_inter)  ORCE");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                    }
                }
                #endregion

                #region<ENTORNO OROB>
                var env_peru_ORB = lista.Where(f => f.opcion == "A" && f.pais == _pais);
                dat_inter = new Dat_Interfaces();
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (lista_inter_pl)  OROB");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                var inter_det_OROB = dat_inter.lista_inter_pl(_pais);
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (lista_inter_pl)  OROB");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                foreach (var env_peru_det in env_peru_ORB.Where(ent => ent.entorno == "OROB"))
                {
                    var inter_det_entorno = inter_det_OROB.Where(m => m.entorno == env_peru_det.entorno);

                    foreach (var det_inter in inter_det_entorno)
                    {
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " EMPEZANDO DE EJECUTAR EL METODO (genera_automatico_inter)  OROB");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                        genera_automatico_inter(_pais, "", env_peru_det.rut_upload, det_inter.inter_nom, det_inter.entorno,
                                            ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update, ref dt_item_xref, ref gen_per_item, ref gen_ecu_item);
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO DE EJECUTAR EL METODO (genera_automatico_inter)  OROB");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
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
        private Boolean valida_lun_ejec()
        {
            Boolean valida = false;
            try
            {
                DateTime fechadesdesemana = DateTime.Now;

                string nombredia = fechadesdesemana.ToString("dddd").ToUpper();

                var list_str = new List<string>();
                list_str.Add("MONDAY");
                list_str.Add("LUNES");
                //list_str.Add("VIERNES");
                //list_str.Add("FRIDAY");

                if (list_str.Contains(nombredia))
                    valida = true;

            }
            catch
            {
                valida = false;
            }
            return valida;
        }       
        private DataTable dt_replace_tda(DataTable dt, string cod_tda)
        {
            DataTable dt_replace = null;
            try
            {

                if (dt != null)
                {
                    dt_replace = dt;
                    string file_cab = dt.Rows[0][0].ToString();

                    string str_tda_ant = file_cab.Substring(file_cab.IndexOf(':') - 6, 13);

                    string str_tda_new = "\"" + str_tda_ant.Replace(str_tda_ant, "STORE:" + cod_tda) + "\"";

                    file_cab = file_cab.Replace(str_tda_ant, str_tda_new);
                    dt_replace.Rows[0][0] = file_cab.ToString();

                }
            }
            catch
            {
                dt_replace = null;
            }
            return dt_replace;
        }
        private void genera_automatico_inter(string _pais, string _codtda, string _gen_ruta, string _gen_inter_name, string _entorno,
                                             ref DataTable dt_item, ref DataTable dt_images, ref DataTable dt_merch_hier,
                                             ref DataTable dt_price_update, ref DataTable dt_item_xref , ref Boolean gen_per_item, ref Boolean gen_ecu_item, Boolean out_let = false)
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

                switch (_entorno)
                {
                    case "XOFICCE":
                        switch (_gen_inter_name)
                        {
                            case "ITEM":
                                #region<ITEM>
                                if (dt_item == null)
                                {
                                    dt = (_pais == "PE") ? dat_geninter.get_item_PE_AUTO(_pais, _codtda) : dat_geninter.get_item_EC_AUTO(_pais, _codtda);
                                    dt_item = dt;
                                }
                                else
                                {
                                    dt = dt_replace_tda(dt_item, _codtda);
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
                                        if (_pais == "PE") gen_per_item = true;
                                        if (_pais == "EC") gen_ecu_item = true;

                                    }
                                }
                                break;
                            #endregion

                            case "ITEM_XREF":
                                #region <ITEM_XREF>
                                // DataTable dt = await Task.Run(() => dat_interface.get_item_xref(pais, codtda));
                                if (dt_item_xref == null)
                                {
                                    dt = dat_geninter.get_item_xref_AUTO(_pais,_codtda); //(_pais == "PE") ? dat_geninter.get_item_PE_AUTO(_pais, _codtda) : dat_geninter.get_item_EC_AUTO(_pais, _codtda);
                                    dt_item_xref = dt;
                                }
                                else
                                {
                                    dt = dt_replace_tda(dt_item_xref, _codtda);
                                }
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["ITEM_XREF"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "ITEM_XREF_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                }
                                break;
                                #endregion
                            case "PRICE_UPDATE":
                                #region<PRICE_UPDATE> 

                                if (dt_price_update == null)
                                {
                                    dt = (_pais == "PE") ? dat_geninter.get_price_update_2_PE(_pais, _codtda) : dat_geninter.get_price_update_2_EC(_pais, _codtda);
                                    dt_price_update = dt;
                                }
                                else
                                {
                                    dt = dt_replace_tda(dt_price_update, _codtda);
                                }

                                //dt = dat_geninter.get_price_update_2_PE(_pais, _codtda);
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
                            case "PRICE_UPDATE_OUTLET":
                                #region<PRICE_UPDATE_OUTLET>
                                if (out_let)
                                {
                                    dt = (_pais == "PE") ? dat_geninter.get_price_update_2_OUTLET_PE(_pais, _codtda) : null;
                                }
                                else
                                {
                                    dt = null;
                                }

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

                                        name_file = "PRICE_UPDATE_2_OUTLET_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                }

                                break;
                            #endregion
                            case "MERCH_HIER":
                                #region<MERCH_HIER>     

                                if (dt_merch_hier == null)
                                {
                                    dt = (_pais == "PE") ? dat_geninter.get_merch_hier_PE(_pais, _codtda) : dat_geninter.get_merch_hier_EC(_pais, _codtda);
                                    dt_merch_hier = dt;
                                }
                                else
                                {
                                    dt = dt_replace_tda(dt_merch_hier, _codtda);
                                }

                                //dt = dat_geninter.get_merch_hier_PE(_pais, _codtda);
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

                                if (dt_images == null)
                                {
                                    dt = (_pais == "PE") ? dat_geninter.get_item_images_PE(_pais, _codtda) : dat_geninter.get_item_images_EC(_pais, _codtda);
                                    dt_images = dt;
                                }
                                else
                                {
                                    dt = dt_replace_tda(dt_images, _codtda);
                                }

                                //dt = dat_geninter.get_item_images_PE(_pais, _codtda);
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
                                dt = (_pais == "PE") ? dat_geninter.ItemMaintenance_PE() : dat_geninter.ItemMaintenance_EC();
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
                                dt = (_pais == "PE") ? dat_geninter.MerchandiseHierarch_PE() : dat_geninter.MerchandiseHierarch_EC();
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
                                dt = (_pais == "PE") ? dat_geninter.OrcRetailLocations_PE() : dat_geninter.OrcRetailLocations_EC();
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
                    case "OROB":
                        switch (_gen_inter_name)
                        {
                            case "PRODUCT_LOCATION":
                                DataSet ds_orb = null;
                                #region<PRODUCT_LOCATION>
                                ds_orb = dat_geninter.ds_orob(_pais);

                                if (ds_orb != null)
                                {

                                    if (ds_orb.Tables.Count > 0)
                                    {
                                        DataTable dt_production_location = ds_orb.Tables[0];
                                        DataTable dt_product = ds_orb.Tables[1];

                                        if (dt_production_location.Rows.Count > 1)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt_production_location.Rows.Count; ++i)
                                            {
                                                str.Append(dt_production_location.Rows[i]["PRODUCT_LOCATION"].ToString());

                                                if (i < dt_production_location.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();


                                            //name_file = "PRODUCT_LOCATION_2000.TXT";// + DateTime.Today.ToString("yyyyMMdd") + ".TXT";
                                            name_file = "PRODUCT_LOCATION_LOGFIRE2K.TXT";

                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            //string ruta_bata_orob = _gen_ruta + "\\BATA";

                                            //if (!Directory.Exists(@ruta_bata_orob)) Directory.CreateDirectory(@ruta_bata_orob);

                                            //String file_bata_orob= ruta_bata_orob + "\\" + name_file;

                                            //if (File.Exists(@file_bata_orob)) File.Delete(@file_bata_orob);
                                            //File.WriteAllText(@file_bata_orob, str_cadena);

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        }

                                        if (dt_product.Rows.Count >1)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt_product.Rows.Count; ++i)
                                            {
                                                str.Append(dt_product.Rows[i]["PRODUCT"].ToString());

                                                if (i < dt_product.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();



                                            //name_file = "PRODUCT_2000.TXT";// + DateTime.Today.ToString("yyyyMMdd") + ".TXT";

                                            if (_pais=="PE")
                                            { 
                                                name_file = "PRODUCT_LOGFIRE2K.TXT";
                                                in_maestros = _gen_ruta + "\\" + name_file;

                                                if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                                File.WriteAllText(@in_maestros, str_cadena);

                                                name_file = "PRODUCT_2000.TXT";// + DateTime.Today.ToString("yyyyMMdd") + ".TXT";
                                                //name_file = "PRODUCT_LOGFIRE2K.TXT";
                                                in_maestros = _gen_ruta + "\\" + name_file;

                                                string str_cadena_5007 = str_cadena;

                                                str_cadena = str_cadena.Replace("LOGFIRE2K", "2000");

                                                if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                                File.WriteAllText(@in_maestros, str_cadena);


                                                name_file = "PRODUCT_5007.TXT";
                                                in_maestros = _gen_ruta + "\\" + name_file;
                                                str_cadena = str_cadena_5007.Replace("LOGFIRE2K", "5007");
                                                if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                                File.WriteAllText(@in_maestros, str_cadena);

                                            }
                                            else
                                            {
                                                name_file = "PRODUCT_5000.TXT";// + DateTime.Today.ToString("yyyyMMdd") + ".TXT";
                                                                               //name_file = "PRODUCT_LOGFIRE2K.TXT";
                                                in_maestros = _gen_ruta + "\\" + name_file;
                                                if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                                File.WriteAllText(@in_maestros, str_cadena);
                                            }

                                        }

                                    }
                                }

                                //dt = (_pais == "PE") ? dat_geninter.OrcRetailLocations_PE() : dat_geninter.OrcRetailLocations_EC();
                                //if (dt != null)
                                //{
                                //    if (dt.Rows.Count > 0)
                                //    {
                                //        str = new StringBuilder();
                                //        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                //        {
                                //            str.Append(dt.Rows[i]["strXml"].ToString());

                                //            if (i < dt.Rows.Count - 1)
                                //            {
                                //                str.Append("\r\n");

                                //            }

                                //        }
                                //        str_cadena = str.ToString();



                                //        name_file = "RetailLocations_" + DateTime.Today.ToString("yyyyMMdd") + ".XML";
                                //        in_maestros = _gen_ruta + "\\" + name_file;

                                //        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                //        File.WriteAllText(@in_maestros, str_cadena);
                                //    }
                                //}
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
        public void generar_orce_exclud(ref string _error)
        {
            Dat_Interfaces datInt = new Dat_Interfaces();
            DataSet ds = null;
            DataTable dt_ORCE_EXCLUD_RUTA = new DataTable();
            DataTable dt_ORCE_EXCLUD_ART = new DataTable();
            DataTable dt_ORCE_EXCLUD_TDA = new DataTable();
            try
            {
                ds = datInt.XSTORE_GET_ORCE_EXCLUD();
                if (ds != null && (ds != null && ds.Tables.Count == 3))
                {
                    dt_ORCE_EXCLUD_RUTA = ds.Tables[0];
                    dt_ORCE_EXCLUD_ART = ds.Tables[1];
                    dt_ORCE_EXCLUD_TDA = ds.Tables[2];
                    if (dt_ORCE_EXCLUD_TDA.Rows.Count > 0)
                    {
                        foreach (DataRow fila in dt_ORCE_EXCLUD_TDA.Rows)
                        {
                            _error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ORCE EXCLUD - TDA: " + fila["ORC_DET_TDA"].ToString() + " (generar_orce_exclud)";
                            #region<GET_ITEM_DEAL_PROPERTY>
                            StringBuilder str = null;
                            string str_cadena = "";
                            string str_cab = "";
                            string name_maestros = ""; string in_maestros = "";

                            if (dt_ORCE_EXCLUD_ART != null)
                            {
                                //str_cab = dt_ORCE_EXCLUD_ART.Rows[0][0].ToString();
                                //str_cab = str_cab.Replace("XXXXX", fila["ORC_DET_TDA"].ToString());



                                //dt_ORCE_EXCLUD_ART.Rows[0][0] = str_cab;
                                string ruta_interface = dt_ORCE_EXCLUD_RUTA.Rows[0]["X_RUTA"].ToString();
                                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);
                                if (dt_ORCE_EXCLUD_ART.Rows.Count > 0)
                                {
                                    str = new StringBuilder();
                                    string atributo = dt_ORCE_EXCLUD_ART.Rows[0]["atributo"].ToString();
                                    for (Int32 i = 0; i < dt_ORCE_EXCLUD_ART.Rows.Count; ++i)
                                    {
                                        str_cab = (i == 0) ? dt_ORCE_EXCLUD_ART.Rows[i]["ITEM_DEAL_PROPERTY"].ToString().Replace("XXXXX", fila["ORC_DET_TDA"].ToString()) : dt_ORCE_EXCLUD_ART.Rows[i]["ITEM_DEAL_PROPERTY"].ToString();

                                        str.Append(str_cab/*dt_ORCE_EXCLUD_ART.Rows[i]["ITEM_DEAL_PROPERTY"].ToString()*/);

                                        if (i < dt_ORCE_EXCLUD_ART.Rows.Count - 1)
                                        {
                                            str.Append("\r\n");
                                        }
                                    }
                                    str_cadena = str.ToString();
                                    name_maestros += "ITEM_DEAL_PROPERTY_" + atributo + "_" + fila["ORC_DET_TDA"].ToString() + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                    in_maestros = ruta_interface + "\\" + name_maestros;

                                    if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                    File.WriteAllText(@in_maestros, str_cadena);
                                }
                            }
                            #endregion
                            _error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ORCE EXCLUD - TDA: " + fila["ORC_DET_TDA"].ToString() + " (generar_orce_exclud)";
                        }
                        string mensaje = "";
                        int f = 0;
                        f = datInt.ORCE_INTERFACE_EXCLUD_ACT(Convert.ToInt32(dt_ORCE_EXCLUD_RUTA.Rows[0]["COD_ORCE"]), "N", 296, 4, ref mensaje);
                        if (f > 0)
                        {
                            _error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ORCE COD: " + dt_ORCE_EXCLUD_RUTA.Rows[0]["COD_ORCE"].ToString() + " ACTUALIZADO A ESTADO ENVIADO (generar_orce_exclud)";
                        }
                        if (mensaje != "")
                        {
                            _error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " (generar_orce_exclud) ERROR AL ACTUALIZAR ESTADO " + mensaje;
                        }
                    }
                    else
                    {
                        //_error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " NO HAY INTERFACES PENDIENTES (generar_orce_exclud) ";
                    }
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
        }
        public void update_articulo_end_xstore(string pais)
        {
            string sqlquery = "USP_UPD_ARTICULO_ENV_XSTORE";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PAIS", pais);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch
                    {


                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch (Exception exc)
            {


            }
        }


        public void proc_envio_ftp()
        {
            try
            {
                /*acceso header user y pass clave de acceso a ws*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = ConexionWS.user;
                header_user.Password = ConexionWS.password;
                /****************************************************************/

                BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
                var lista = bata_trans.ws_get_xstore_carpeta_upload(header_user);

                string tipo_file = "";

                foreach (var item in lista)
                {
                    //tipo_file = (item.entorno == "ORCE") ? "*.xml*" : "*.mnt*";
                    switch (item.entorno)
                    {
                        case "ORCE":
                            tipo_file = "*.xml*";
                            break;
                        case "XOFICCE":
                            tipo_file = "*.mnt*";
                            break;
                        case "OROB":
                            tipo_file = "*.*";
                            break;
                    }



                    if (!Directory.Exists(@item.rut_upload)) Directory.CreateDirectory(@item.rut_upload);

                    string[] files = Directory.GetFiles(@item.rut_upload, tipo_file);

                    foreach (string str_item in files)
                    {
                        string _path_archivo = str_item;
                        string _nombrearchivo = Path.GetFileNameWithoutExtension(@str_item);
                        string _extension_archivo = Path.GetExtension(@_path_archivo);
                        string _file_path_destino = item.ftp_folder + "/" + _nombrearchivo + _extension_archivo;
                        Boolean envio_ftp = subida_server_ftp(item.ftp_server, item.ftp_user, item.ftp_pass, item.ftp_port, str_item, _file_path_destino);

                        if (envio_ftp) File.Delete(@_path_archivo);

                    }
                }

            }
            catch (Exception)
            {

            }
        }
        private Boolean subida_server_ftp(string gHostName, string gUserName, string gPassword,
                                          Int32 gPortNumber, string file_origen, string file_destino)
        {
            Boolean valida_envio = false;
            try
            {
                SessionOptions sessionOptions = new SessionOptions
                {

                    Protocol = 0,
                    HostName = gHostName,
                    UserName = gUserName,
                    Password = gPassword,
                    PortNumber = gPortNumber,
                    GiveUpSecurityAndAcceptAnySshHostKey = true,
                };

                using (Session session = new Session())
                {
                    session.Open(sessionOptions);

                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.FilePermissions = null; // This is default
                    transferOptions.PreserveTimestamp = false;
                    transferOptions.TransferMode = TransferMode.Binary;
                    TransferOperationResult transferResult;

                    transferResult =
                        session.PutFiles(file_origen, file_destino, false, transferOptions);

                    transferResult.Check();

                    valida_envio = true;
                }
            }
            catch (Exception exc)
            {
                valida_envio = false;
            }
            return valida_envio;
        }
    }
}
