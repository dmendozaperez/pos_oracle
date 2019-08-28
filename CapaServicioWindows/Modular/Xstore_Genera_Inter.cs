using CapaServicioWindows.CapaDato.Interfaces;
using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Modular
{
    public class Xstore_Genera_Inter
    {
        public void ejecutar_genera_interface_xstore(ref string error)
        {
            Dat_Interfaces get_inter = null;
            StreamWriter tw1 = null;
            try
            {
                String pais = "PE";
                #region<INTERFACES DE PERU>
                DataTable dt_item = null;
                DataTable dt_images = null;
                DataTable dt_merch_hier = null;
                DataTable dt_price_update = null;
                get_inter = new Dat_Interfaces();
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (lista_inter_pl_genera) PERU");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                List<Ent_InterGenera_PL> list_gen = get_inter.lista_inter_pl_genera(pais);
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (lista_inter_pl_genera) PERU");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();

                Boolean _ITEM_MAINTENANCE = false;
                Boolean _ORCE_RETAIL_LOCATIONS = false;
                Boolean _MERCHANDISE_HIERARCHY_MAINTENANCE = false;

                if (list_gen!=null)
                {
                    foreach(var item in list_gen)
                    {
                        string codigo = item.int_cab_cod.ToString() + item.int_id.ToString();
                        Boolean valida_genera = false;
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (genera_automatico_inter) PERU");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                        genera_automatico_inter(codigo, item.pais, item.cod_tda, item.rut_gen, item.int_nom, item.entorno,
                                                ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update,
                                                ref _ITEM_MAINTENANCE,ref _ORCE_RETAIL_LOCATIONS,ref _MERCHANDISE_HIERARCHY_MAINTENANCE,
                                                ref valida_genera,item.outlet);
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (genera_automatico_inter) PERU");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                        if (valida_genera)
                        {
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (Update_Interface_Genera) PERU");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                            get_inter.Update_Interface_Genera(codigo);
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (Update_Interface_Genera) PERU");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                        }
                    }
                }
                #endregion
                #region<INTERFACES DE ECUADOR>
                pais = "EC";
                dt_item = null;
                dt_images = null;
                dt_merch_hier = null;
                dt_price_update = null;
                get_inter = new Dat_Interfaces();
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (lista_inter_pl_genera) ECUADOR");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                list_gen = get_inter.lista_inter_pl_genera(pais);
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (lista_inter_pl_genera) ECUADOR");
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                _ITEM_MAINTENANCE = false;
                _ORCE_RETAIL_LOCATIONS = false;
                _MERCHANDISE_HIERARCHY_MAINTENANCE = false;

                if (list_gen != null)
                {
                    foreach (var item in list_gen)
                    {
                        string codigo = item.int_cab_cod.ToString() + item.int_id.ToString();
                        Boolean valida_genera = false;
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (genera_automatico_inter) ECUADOR");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                        genera_automatico_inter(codigo, item.pais, item.cod_tda, item.rut_gen, item.int_nom, item.entorno,
                                                ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update,
                                                ref _ITEM_MAINTENANCE, ref _ORCE_RETAIL_LOCATIONS, ref _MERCHANDISE_HIERARCHY_MAINTENANCE,
                                                ref valida_genera);
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (genera_automatico_inter) ECUADOR");
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                        if (valida_genera)
                        {
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (Update_Interface_Genera) ECUADOR");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                            get_inter.Update_Interface_Genera(codigo);
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (Update_Interface_Genera) ECUADOR");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                        }

                    }
                }
                #endregion

            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
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
        private void genera_automatico_inter(string codigo,string _pais, string _codtda, string _gen_ruta, string _gen_inter_name, string _entorno,
                                            ref DataTable dt_item, ref DataTable dt_images, ref DataTable dt_merch_hier,
                                            ref DataTable dt_price_update,
                                            ref Boolean _ITEM_MAINTENANCE,ref Boolean _ORCE_RETAIL_LOCATIONS,
                                            ref Boolean _MERCHANDISE_HIERARCHY_MAINTENANCE,ref Boolean valida_genera, Boolean out_let = false)
        {
            Dat_Interfaces dat_geninter = null;
            DataTable dt = null;
            DataSet ds = null;
            TextWriter tw1 = null;
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
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ITEM");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<ITEM>
                                if (dt_item == null)
                                {

                                    dt = (_pais=="PE")? dat_geninter.get_item_PE(_pais, _codtda): dat_geninter.get_item_EC(_pais, _codtda);
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



                                        name_file = "ITEM_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                }
                              
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ITEM");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "PRICE_UPDATE":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE PRICE_UPDATE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<PRICE_UPDATE> 

                                if (dt_price_update == null)
                                {
                                    dt =(_pais=="PE")? dat_geninter.get_price_update_2_PE(_pais, _codtda): dat_geninter.get_price_update_2_EC(_pais, _codtda);
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

                                        name_file = "PRICE_UPDATE_2_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                }

                                
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE PRICE_UPDATE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "PRICE_UPDATE_OUTLET":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE PRICE_UPDATE_OUTLET");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
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

                                        name_file = "PRICE_UPDATE_2_OUTLET_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                }                              
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE PRICE_UPDATE_OUTLET");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "MERCH_HIER":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE MERCH_HIER");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<MERCH_HIER>     

                                if (dt_merch_hier == null)
                                {
                                    dt = (_pais=="PE")? dat_geninter.get_merch_hier_PE(_pais, _codtda): dat_geninter.get_merch_hier_EC(_pais, _codtda);
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



                                        name_file = "MERCH_HIER_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                }
                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE MERCH_HIER");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "ITEM_IMAGES":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ITEM_IMAGES");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<ITEM_IMAGES>    

                                if (dt_images == null)
                                {
                                    dt =(_pais=="PE")? dat_geninter.get_item_images_PE(_pais, _codtda): dat_geninter.get_item_images_EC(_pais, _codtda);
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



                                        name_file = "ITEM_IMAGES_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                }                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ITEM_IMAGES");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "RETAIL_LOCATION":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE RETAIL_LOCATION");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<RETAIL_LOCATION>
                                ds = (_pais=="PE")? dat_geninter.get_retail_location_PE(_codtda,_pais): dat_geninter.get_retail_location_EC(_codtda, _pais); 
                                str = new StringBuilder();                               
                                if (ds != null)
                                {
                                    DataTable dt_retail_location = ds.Tables[0];
                                    DataTable dt_retail_location_property = ds.Tables[1];                                 
                                    if (dt_retail_location.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt_retail_location.Rows.Count; ++i)
                                        {
                                            str.Append(dt_retail_location.Rows[i]["RETAIL_LOCATION"].ToString());

                                            if (i < dt_retail_location.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "RETAIL_LOCATION_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                    if (dt_retail_location_property.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt_retail_location_property.Rows.Count; ++i)
                                        {
                                            str.Append(dt_retail_location_property.Rows[i]["RETAIL_LOCATION_PROPERTY"].ToString());

                                            if (i < dt_retail_location_property.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();

                                        name_file = "RETAIL_LOCATION_PROPERTY_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }

                                }
                                
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE RETAIL_LOCATION");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "ITEM_DIMENSION":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ITEM_DIMENSION");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<ITEM_DIMENSION> 

                                dt = (_pais=="PE")? dat_geninter.get_item_dimension_type_PE(_pais, _codtda): dat_geninter.get_item_dimension_type_EC(_pais, _codtda); 
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["ITEM_DIMENSION_TYPE"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "ITEM_DIMENSION_TYPE_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                    }
                                }

                                dt =(_pais=="PE")? dat_geninter.get_item_dimension_value_PE(_pais,_codtda) : dat_geninter.get_item_dimension_value_EC(_pais, _codtda); ;
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["ITEM_DIMENSION_VALUE"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "ITEM_DIMENSION_VALUE_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                }                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ITEM_DIMENSION");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "PARTY":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE PARTY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<PARTY>                                                             
                                string strCodEmpl = "S";
                                    string strCodSupl = "S";                                 

                                     dt = (_pais=="PE")? dat_geninter.get_Party_PE(_pais, _codtda, strCodSupl, strCodEmpl): dat_geninter.get_Party_EC(_pais, _codtda, strCodSupl, strCodEmpl);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            Decimal i = 0;
                                            foreach (DataRow fila in dt.Rows)
                                            {

                                                str.Append(fila["PARTY"].ToString());
                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }
                                                i += 1;

                                            }
                                      
                                            str_cadena = str.ToString();



                                            name_file = "PARTY_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                    }                                                                                           
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE PARTY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "INV_LOCATION_PROPERTY":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE INV_LOCATION_PROPERTY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<INV_LOCATION_PROPERTY>                                
                                dt = (_pais=="PE")? dat_geninter.get_Location_Property_PE(_pais, _codtda) : dat_geninter.get_Location_Property_EC(_pais, _codtda);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            Decimal i = 0;
                                            foreach (DataRow fila in dt.Rows)
                                            {

                                                str.Append(fila["INV_LOCATION_PROPERTY"].ToString());
                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }
                                                i += 1;

                                            }

                                            str_cadena = str.ToString();

                                            name_file = "INV_LOCATION_PROPERTY_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                    }                                
                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE INV_LOCATION_PROPERTY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "STOCK_LEDGER":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE STOCK_LEDGER");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<STOCK_LEDGER>
                                dt = (_pais=="PE")? dat_geninter.get_stock_ledger_PE("", _codtda, _pais): dat_geninter.get_stock_ledger_EC("", _codtda, _pais);                                
                                if (dt != null)
                                {
                                    string in_stock_ledger = "";
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["STOCK_LEDGER"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");
                                            }
                                        }
                                        str_cadena = str.ToString();

                                        name_file = "STOCK_LEDGER_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_stock_ledger = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_stock_ledger)) File.Delete(@in_stock_ledger);
                                        File.WriteAllText(@in_stock_ledger, str_cadena);
                                        valida_genera = true;
                                    }
                                }
                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE STOCK_LEDGER");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "COUNTRY_CITY":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE COUNTRY_CITY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<COUNTRY_CITY>                                                             
                                dt =(_pais=="PE")? dat_geninter.get_county_city_PE(_codtda, _pais): dat_geninter.get_county_city_EC(_codtda, _pais);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["BCL_COUNTY_CITY"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();

                                            name_file = "BCL_COUNTY_CITY_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        }
                                    }
                                                                 
                                    dt =(_pais=="PE")? dat_geninter.get_state_county_PE(_codtda, _pais) : dat_geninter.get_state_county_EC(_codtda, _pais);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["BCL_STATE_COUNTY"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();

                                            name_file = "BCL_STATE_COUNTY_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                    }
                               

                                
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE COUNTRY_CITY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "ELECTRONIC_CORRELATIVES":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ELECTRONIC_CORRELATIVES");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<ELECTRONIC_CORRELATIVES>                                                      
                                dt = dat_geninter.get_electronic_correlatives_PE(_codtda, _pais);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["BCL_ELECTRONIC_CORRELATIVES"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();



                                            name_file = "BCL_ELECTRONIC_CORRELATIVES_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                    }                                                             
                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ELECTRONIC_CORRELATIVES");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "MANUAL_CORRELATIVES":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE MANUAL_CORRELATIVES");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<MANUAL_CORRELATIVES>                                                            
                                dt = dat_geninter.get_manual_correlatives_PE(_codtda, _pais);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            Decimal i = 0;
                                            foreach (DataRow fila in dt.Rows)
                                            {

                                                str.Append(fila["BCL_MANUAL_CORRELATIVES"].ToString());
                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }
                                                i += 1;

                                            }
                                       
                                            str_cadena = str.ToString();


                                            name_file = "BCL_MANUAL_CORRELATIVES_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                    }                                                           
                                
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE MANUAL_CORRELATIVES");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "TENDER_REPOSITORY":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE TENDER_REPOSITORY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<TENDER_REPOSITORY>                                                          
                                dt = (_pais=="PE")? dat_geninter.get_tender_repository_PE(_codtda, _pais): dat_geninter.get_tender_repository_EC(_codtda, _pais);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["TENDER_REPOSITORY"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();



                                            name_file = "TENDER_REPOSITORY_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        }
                                    }                               
                                    dt = (_pais=="PE")? dat_geninter.get_tender_repository_property_PE(_codtda, _pais) : dat_geninter.get_tender_repository_property_EC(_codtda, _pais);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            str = new StringBuilder();
                                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                            {
                                                str.Append(dt.Rows[i]["TENDER_REPOSITORY_PROPERTY"].ToString());

                                                if (i < dt.Rows.Count - 1)
                                                {
                                                    str.Append("\r\n");

                                                }

                                            }
                                            str_cadena = str.ToString();



                                            name_file = "TENDER_REPOSITORY_PROPERTY_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                            in_maestros = _gen_ruta + "\\" + name_file;

                                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                            File.WriteAllText(@in_maestros, str_cadena);
                                        valida_genera = true;
                                    }
                                    }                                                          
                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE TENDER_REPOSITORY");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "INV_VALID_DESTINATIONS":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE INV_VALID_DESTINATIONS");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<INV_VALID_DESTINATIONS>                               
                                dt = (_pais=="PE")? dat_geninter.get_inv_valid_destinations_PE(_codtda, _pais): dat_geninter.get_inv_valid_destinations_EC(_codtda, _pais);                                   
                                if (dt != null)
                                {
                                    string name_inv_valid = ""; string in_inv_valid = "";
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["INV_VALID_DESTINATIONS"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "INV_VALID_DESTINATIONS_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_inv_valid = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_inv_valid)) File.Delete(@in_inv_valid);
                                        File.WriteAllText(@in_inv_valid, str_cadena);
                                        valida_genera = true;
                                    }

                                }                               
                               
                                dt=(_pais=="PE")? dat_geninter.get_inv_valid_destinations_property_PE(_codtda, _pais): dat_geninter.get_inv_valid_destinations_property_EC(_codtda, _pais);                                
                                if (dt != null)
                                {
                                    string name_inv_valid = ""; string in_inv_valid = "";
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["INV_VALID_DESTINATIONS_PROPERTY"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "INV_VALID_DESTINATIONS_PROPERTY_" + _codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".MNT";
                                        in_inv_valid = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_inv_valid)) File.Delete(@in_inv_valid);
                                        File.WriteAllText(@in_inv_valid, str_cadena);
                                        valida_genera = true;
                                    }

                                }                             
                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE INV_VALID_DESTINATIONS");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "VENTAS HISTORICAS":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE VENTAS HISTORICAS");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<VENTAS HISTORICAS>
                                DateTime fechaIni = DateTime.UtcNow.AddMonths(-2);
                                DateTime fecha_Fin = DateTime.UtcNow;
                                string in_archivos = "";
                                ds = (_pais=="PE") ?dat_geninter.SET_XSTORE_VENTA_EXPORTAR_PE(_codtda, fechaIni, fecha_Fin): dat_geninter.SET_XSTORE_VENTA_EXPORTAR_EC(_codtda, fechaIni, fecha_Fin);
                                #region<TRANS_LINE_TENDER>                               
                                dt = ds.Tables[0];
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["TRANS_LINE_TENDER"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "TRANS_LINE_TENDER_" + _codtda + "_" + codigo + ".MNT";
                                        in_archivos = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                                        File.WriteAllText(@in_archivos, str_cadena);
                                    }
                                }
                                #endregion
                                #region<TRANS_TAX>                               
                                dt = ds.Tables[1];
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["TRANS_TAX"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "TRANS_TAX_" + _codtda + "_" + codigo + ".MNT";
                                        @in_archivos = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                                        File.WriteAllText(@in_archivos, str_cadena);
                                    }
                                }
                                #endregion
                                #region<TRANS_LINE_ITEM_TAX>                               
                                dt = ds.Tables[2];
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        Decimal i = 0;
                                        foreach (DataRow fila in dt.Rows)
                                        {

                                            str.Append(fila["TRANS_LINE_ITEM_TAX"].ToString());
                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }
                                            i += 1;

                                        }


                                        str_cadena = str.ToString();



                                        name_file = "TRANS_LINE_ITEM_TAX_" + _codtda + "_" + codigo + ".MNT";
                                        in_archivos = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                                        File.WriteAllText(@in_archivos, str_cadena);
                                    }
                                }
                                #endregion
                                #region<TRANS_LINE_ITEM>                               
                                dt = ds.Tables[3];
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["TRANS_LINE_ITEM"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "TRANS_LINE_ITEM_" + _codtda + "_" + codigo + ".MNT";
                                        in_archivos = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                                        File.WriteAllText(@in_archivos, str_cadena);
                                    }
                                }

                                #endregion
                                #region<TRANS_HEADER>                               
                                dt = ds.Tables[4];
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        str = new StringBuilder();
                                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            str.Append(dt.Rows[i]["TRANS_HEADER"].ToString());

                                            if (i < dt.Rows.Count - 1)
                                            {
                                                str.Append("\r\n");

                                            }

                                        }
                                        str_cadena = str.ToString();



                                        name_file = "TRANS_HEADER_" + _codtda + "_" + codigo + ".MNT";
                                        in_archivos = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                                        File.WriteAllText(@in_archivos, str_cadena);
                                        valida_genera = true;
                                    }
                                }
                                #endregion
                              
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE VENTAS HISTORICAS");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                        }
                        break;
                    case "ORCE":
                        switch (_gen_inter_name)
                        {
                            case "ITEM_MAINTENANCE":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ITEM_MAINTENANCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<ITEM_MAINTENANCE>    
                                if (_ITEM_MAINTENANCE) return;
                                dt =(_pais=="PE") ?dat_geninter.ItemMaintenance_PE(): dat_geninter.ItemMaintenance_EC();
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



                                        name_file = "ItemMaintenance_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".XML";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        _ITEM_MAINTENANCE = true;
                                        valida_genera = true;
                                    }
                                }

                                
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ITEM_MAINTENANCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "MERCHANDISE_HIERARCHY_MAINTENANCE":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE MERCHANDISE_HIERARCHY_MAINTENANCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<MERCHANDISE_HIERARCHY_MAINTENANCE>   
                                if (_MERCHANDISE_HIERARCHY_MAINTENANCE) return;                            
                                dt = (_pais=="PE")? dat_geninter.MerchandiseHierarch_PE() : dat_geninter.MerchandiseHierarch_EC();
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

                                        name_file = "MerchandiseHierarchyMaintenance_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".XML";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        _MERCHANDISE_HIERARCHY_MAINTENANCE = true;
                                        valida_genera = true;
                                    }
                                }
                               
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE MERCHANDISE_HIERARCHY_MAINTENANCE");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                            case "ORCE RETAIL_LOCATIONS":
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ORCE RETAIL_LOCATIONS");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                #region<ORCE RETAIL_LOCATIONS>      
                                if (_ORCE_RETAIL_LOCATIONS) return;                          
                                dt =(_pais=="PE")? dat_geninter.OrcRetailLocations_PE(): dat_geninter.OrcRetailLocations_EC();
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



                                        name_file = "RetailLocations_" + DateTime.Today.ToString("yyyyMMdd") + "_" + codigo + ".XML";
                                        in_maestros = _gen_ruta + "\\" + name_file;

                                        if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                        File.WriteAllText(@in_maestros, str_cadena);
                                        _ORCE_RETAIL_LOCATIONS = true;
                                        valida_genera = true;
                                    }
                                }
                              
                                #endregion
                                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                                tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ORCE RETAIL_LOCATIONS");
                                tw1.Flush();
                                tw1.Close();
                                tw1.Dispose();
                                break;
                        }
                        break;
                }




            }
            catch(Exception exc)
            {
                tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                tw1.WriteLine(exc.Message);
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
                throw;

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
                if (ds != null && (ds!= null && ds.Tables.Count == 3))
                {
                    dt_ORCE_EXCLUD_RUTA = ds.Tables[0];
                    dt_ORCE_EXCLUD_ART = ds.Tables[1];
                    dt_ORCE_EXCLUD_TDA = ds.Tables[2];
                    if (dt_ORCE_EXCLUD_TDA.Rows.Count > 0)
                    {
                        foreach (DataRow fila in dt_ORCE_EXCLUD_TDA.Rows)
                        {                            
                            //_error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INICIANDO GENERACION DE INTERFACE ORCE EXCLUD - TDA: " + fila["ORC_DET_TDA"].ToString() + " (generar_orce_exclud)";                         
                            #region<GET_ITEM_DEAL_PROPERTY>
                            StringBuilder str = null;
                            string str_cadena = "";
                            string str_cab = "";
                            string name_maestros = ""; string in_maestros = "";

                            if (dt_ORCE_EXCLUD_ART != null)
                            {
                                str_cab = dt_ORCE_EXCLUD_ART.Rows[0][0].ToString();
                                str_cab = str_cab.Replace("XXXXX", fila["ORC_DET_TDA"].ToString());
                                dt_ORCE_EXCLUD_ART.Rows[0][0] = str_cab;
                                string ruta_interface = dt_ORCE_EXCLUD_RUTA.Rows[0]["X_RUTA"].ToString();
                                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);
                                if (dt_ORCE_EXCLUD_ART.Rows.Count > 0)
                                {
                                    str = new StringBuilder();
                                    for (Int32 i = 0; i < dt_ORCE_EXCLUD_ART.Rows.Count; ++i)
                                    {
                                        str.Append(dt_ORCE_EXCLUD_ART.Rows[i]["ITEM_DEAL_PROPERTY"].ToString());

                                        if (i < dt_ORCE_EXCLUD_ART.Rows.Count - 1)
                                        {
                                            str.Append("\r\n");
                                        }
                                    }
                                    str_cadena = str.ToString();
                                    name_maestros += "ITEM_DEAL_PROPERTY_" + fila["ORC_DET_TDA"].ToString() + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                    in_maestros = ruta_interface + "\\" + name_maestros;

                                    if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                    File.WriteAllText(@in_maestros, str_cadena);
                                }
                            }
                            #endregion
                            //_error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " TERMINANDO GENERACION DE INTERFACE ORCE EXCLUD - TDA: " + fila["ORC_DET_TDA"].ToString() + " (generar_orce_exclud)";                                           
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
                        _error += DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " NO HAY INTERFACES PENDIENTES (generar_orce_exclud) ";
                    }
                }                
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }            
        }
    }
}
