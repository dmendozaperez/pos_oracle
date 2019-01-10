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
            try
            {
                String pais = "PE";
                #region<INTERFACES DE PERU>
                DataTable dt_item = null;
                DataTable dt_images = null;
                DataTable dt_merch_hier = null;
                DataTable dt_price_update = null;
                get_inter = new Dat_Interfaces();
                List<Ent_InterGenera_PL> list_gen = get_inter.lista_inter_pl_genera(pais);

                Boolean _ITEM_MAINTENANCE = false;
                Boolean _ORCE_RETAIL_LOCATIONS = false;
                Boolean _MERCHANDISE_HIERARCHY_MAINTENANCE = false;

                if (list_gen!=null)
                {
                    foreach(var item in list_gen)
                    {
                        string codigo = item.int_cab_cod.ToString() + item.int_id.ToString();
                        Boolean valida_genera = false;
                        genera_automatico_inter(codigo, item.pais, item.cod_tda, item.rut_gen, item.int_nom, item.entorno,
                                                ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update,
                                                ref _ITEM_MAINTENANCE,ref _ORCE_RETAIL_LOCATIONS,ref _MERCHANDISE_HIERARCHY_MAINTENANCE,
                                                ref valida_genera);
                        if (valida_genera)
                        {
                            get_inter.Update_Interface_Genera(codigo);
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
                list_gen = get_inter.lista_inter_pl_genera(pais);

                _ITEM_MAINTENANCE = false;
                _ORCE_RETAIL_LOCATIONS = false;
                _MERCHANDISE_HIERARCHY_MAINTENANCE = false;

                if (list_gen != null)
                {
                    foreach (var item in list_gen)
                    {
                        string codigo = item.int_cab_cod.ToString() + item.int_id.ToString();
                        Boolean valida_genera = false;
                        genera_automatico_inter(codigo, item.pais, item.cod_tda, item.rut_gen, item.int_nom, item.entorno,
                                                ref dt_item, ref dt_images, ref dt_merch_hier, ref dt_price_update,
                                                ref _ITEM_MAINTENANCE, ref _ORCE_RETAIL_LOCATIONS, ref _MERCHANDISE_HIERARCHY_MAINTENANCE,
                                                ref valida_genera);
                        if (valida_genera)
                        {
                            get_inter.Update_Interface_Genera(codigo);
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
                                            ref Boolean _MERCHANDISE_HIERARCHY_MAINTENANCE,ref Boolean valida_genera)
        {
            Dat_Interfaces dat_geninter = null;
            DataTable dt = null;
            DataSet ds = null;
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
                                break;
                            #endregion
                            case "PRICE_UPDATE":
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

                                break;
                            #endregion
                            case "MERCH_HIER":
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
                                break;
                            #endregion
                            case "ITEM_IMAGES":
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
                                break;
                            #endregion
                            case "RETAIL_LOCATION":
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
                                break;
                            #endregion
                            case "ITEM_DIMENSION":
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
                                break;
                            #endregion
                            case "PARTY":
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
                                break;
                            #endregion
                            case "INV_LOCATION_PROPERTY":
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
                                break;
                            #endregion
                            case "STOCK_LEDGER":
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
                                break;
                            #endregion
                            case "COUNTRY_CITY":
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
                               

                                break;
                            #endregion
                            case "ELECTRONIC_CORRELATIVES":
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
                                break;
                            #endregion
                            case "MANUAL_CORRELATIVES":
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
                                break;
                            #endregion
                            case "TENDER_REPOSITORY":
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
                                break;
                            #endregion
                            case "INV_VALID_DESTINATIONS":
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
                                break;
                            #endregion
                            case "VENTAS HISTORICAS":
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
                                break;
                                #endregion


                        }
                        break;
                    case "ORCE":
                        switch (_gen_inter_name)
                        {
                            case "ITEM_MAINTENANCE":
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

                                break;
                            #endregion
                            case "MERCHANDISE_HIERARCHY_MAINTENANCE":
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
                                break;
                            #endregion
                            case "ORCE RETAIL_LOCATIONS":
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
                                break;
                                #endregion
                        }
                        break;
                }




            }
            catch
            {
                throw;

            }
        }
    }
}
