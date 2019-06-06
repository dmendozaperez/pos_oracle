using CapaEntidad.Util;
using CapaEntidad.Venta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Venta
{
    public class Dat_Venta
    {
        /// <summary>
        /// insertar transaccion de ventas de tiendas
        /// </summary>
        /// <param name="tablas de transacciones de tda dataset"></param>
        /// <returns></returns>
        public Ent_MsgTransac inserta_venta(string cod_tda,DataSet ds_venta)
        {
            string sqlquery = "[USP_INSERTAR_VENTAS_TDA]";
            Ent_MsgTransac msg = null;
            try
            {
                msg = new Ent_MsgTransac();
                if (ds_venta.Tables.Count > 0)
                {
                    DataTable dt_ffc = new DataTable("FFACTC");
                    DataTable dt_ffd = new DataTable("FFACTD");
                    DataTable dt_not = new DataTable("FNOTAA");

                    dt_ffc = ds_venta.Tables[0];
                    dt_ffd = ds_venta.Tables[1];
                    dt_not = ds_venta.Tables[2];

                    using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery,cn))
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                                cmd.Parameters.AddWithValue("@TMP_FFC", dt_ffc);
                                cmd.Parameters.AddWithValue("@TMP_FFD", dt_ffd);
                                cmd.Parameters.AddWithValue("@TMP_NOT", dt_not);
                                cmd.ExecuteNonQuery();
                                msg.codigo = "0";
                                msg.descripcion = "Se actualizo correctamente";
                            }
                        }
                        catch (Exception exc)
                        {
                            msg.codigo = "1";
                            msg.descripcion = exc.Message;
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
                else
                {
                    msg.codigo = "0";
                    msg.descripcion = "No hay trasacciones para actualizar";
                }
            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message;                
            }
            return msg;
        }

        /// <summary>
        /// insercion de data por listas
        /// </summary>
        /// <param name="cod_tda"></param>
        /// <param name="ds_venta"></param>
        /// <returns></returns>
        public Ent_MsgTransac inserta_venta_lista(string cod_tda, 
                                                  Ent_List_Ffactc list_ffactc,
                                                  Ent_List_Ffactd list_ffactd,
                                                  Ent_List_Fnotaa list_fnotaa)
        {
            string sqlquery = "[USP_INSERTAR_VENTAS_TDA]";
            Ent_MsgTransac msg = null;
            try
            {
                msg = new Ent_MsgTransac();
                
                if (list_ffactc.lista_ffactc.Count() > 0)
                {
                    DataTable dt_ffc = ConvertListToDataTable_Ffactc(list_ffactc);
                    DataTable dt_ffd = ConvertListToDataTable_Ffactd(list_ffactd);
                    DataTable dt_not = ConvertListToDataTable_FNotaa(list_fnotaa);

                   

                    using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                            {
                                cmd.CommandTimeout = 120;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                                cmd.Parameters.AddWithValue("@TMP_FFC", dt_ffc);
                                cmd.Parameters.AddWithValue("@TMP_FFD", dt_ffd);
                                cmd.Parameters.AddWithValue("@TMP_NOT", dt_not);
                                cmd.ExecuteNonQuery();
                                msg.codigo = "0";
                                msg.descripcion = "Se actualizo correctamente";
                            }
                        }
                        catch (Exception exc)
                        {
                            msg.codigo = "1";
                            msg.descripcion = exc.Message + "==> SQL";
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
                else
                {
                    msg.codigo = "0";
                    msg.descripcion = "No hay trasacciones para actualizar";
                }
            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message + "==error";
            }
            return msg;
        }

        public Ent_MsgTransac inserta_venta_208(string cod_tda, DataSet ds_venta)
        {
            string sqlquery = "[USP_INSERTAR_VENTAS_TDA]";
            Ent_MsgTransac msg = null;
            try
            {
                msg = new Ent_MsgTransac();
                if (ds_venta.Tables.Count > 0)
                {
                    DataTable dt_ffc = new DataTable("FFACTC");
                    //DataTable dt_ffd = new DataTable("FFACTD");
                    //DataTable dt_not = new DataTable("FNOTAA");

                    dt_ffc = ds_venta.Tables[0];
                    //dt_ffd = ds_venta.Tables[1];
                    //dt_not = ds_venta.Tables[2];

                    using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_208))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                                cmd.Parameters.AddWithValue("@TMP_FFC", dt_ffc);
                                //cmd.Parameters.AddWithValue("@TMP_FFD", dt_ffd);
                                //cmd.Parameters.AddWithValue("@TMP_NOT", dt_not);
                                cmd.ExecuteNonQuery();
                                msg.codigo = "0";
                                msg.descripcion = "Se actualizo correctamente";
                            }
                        }
                        catch (Exception exc)
                        {
                            msg.codigo = "1";
                            msg.descripcion = exc.Message;
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
                else
                {
                    msg.codigo = "0";
                    msg.descripcion = "No hay trasacciones para actualizar";
                }
            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message;
            }
            return msg;
        }

        public Ent_MsgTransac inserta_venta_list(string cod_tda, Ent_Venta_List lista_venta)
        {
            string sqlquery = "[USP_INSERTAR_VENTAS_TDA_LIST]";
            Ent_MsgTransac msg = null;
            DataTable dt_venta = null;
            try
            {
                msg = new Ent_MsgTransac();
                dt_venta = ConvertListToDataTable(lista_venta);                
                    using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_208))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                                cmd.Parameters.AddWithValue("@TMP_FFC", dt_venta);                                
                                cmd.ExecuteNonQuery();
                                msg.codigo = "0";
                                msg.descripcion = "Se actualizo correctamente";
                            }
                        }
                        catch (Exception exc)
                        {
                            msg.codigo = "1";
                            msg.descripcion = exc.Message;
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }                                
            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message;
            }
            return msg;
        }

        private DataTable ConvertListToDataTable(Ent_Venta_List list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable();
                table.Columns.Add("fecha", typeof(DateTime));
                table.Columns.Add("cod_tda", typeof(string));
                table.Columns.Add("fc_suna", typeof(string));
                table.Columns.Add("fc_sfac", typeof(string));
                table.Columns.Add("fc_nfac", typeof(string));
                table.Columns.Add("fc_nint", typeof(string));
                table.Columns.Add("fc_fecha2", typeof(DateTime));

                foreach (var item in list.lista_venta)
                {                  

                    table.Rows.Add(item.fecha,item.cod_tda,item.fc_suna,item.fc_sfac,item.fc_nfac,item.fc_nint,item.fecha2);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return table;
        }

        private DataTable ConvertListToDataTable_Ffactc(Ent_List_Ffactc list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable("FFACTC");
                table.Columns.Add("fc_nint", typeof(string));
                table.Columns.Add("fc_nnot", typeof(string));
                table.Columns.Add("fc_codi", typeof(string));
                table.Columns.Add("fc_suna", typeof(string));
                table.Columns.Add("fc_sfac", typeof(string));
                table.Columns.Add("fc_nfac", typeof(string));
                table.Columns.Add("fc_ffac", typeof(DateTime));
                table.Columns.Add("fc_nord", typeof(string));
                table.Columns.Add("fc_cref", typeof(string));
                table.Columns.Add("fc_sref", typeof(string));
                table.Columns.Add("fc_nref", typeof(string));
                table.Columns.Add("fc_pvta", typeof(string));
                table.Columns.Add("fc_csuc", typeof(string));
                table.Columns.Add("fc_gvta", typeof(string));
                table.Columns.Add("fc_zona", typeof(string));
                table.Columns.Add("fc_clie", typeof(string));
                table.Columns.Add("fc_ncli", typeof(string));
                table.Columns.Add("fc_nomb", typeof(string));
                table.Columns.Add("fc_apep", typeof(string));
                table.Columns.Add("fc_apem", typeof(string));
                table.Columns.Add("fc_dcli", typeof(string));
                table.Columns.Add("fc_cubi", typeof(string));
                table.Columns.Add("fc_ruc", typeof(string));
                table.Columns.Add("fc_vuse", typeof(string));
                table.Columns.Add("fc_vend", typeof(string));
                table.Columns.Add("fc_ipre", typeof(string));
                table.Columns.Add("fc_tint", typeof(string));
                table.Columns.Add("fc_pint", typeof(decimal));
                table.Columns.Add("fc_lcsg", typeof(string));
                table.Columns.Add("fc_ncon", typeof(string));
                table.Columns.Add("fc_dcon", typeof(string));
                table.Columns.Add("fc_lcon", typeof(string));
                table.Columns.Add("fc_lruc", typeof(string));
                table.Columns.Add("fc_agen", typeof(string));
                table.Columns.Add("fc_mone", typeof(string));
                table.Columns.Add("fc_tasa", typeof(Decimal));
                table.Columns.Add("fc_fpag", typeof(string));
                table.Columns.Add("fc_nlet", typeof(decimal));
                table.Columns.Add("fc_qtot", typeof(decimal));
                table.Columns.Add("fc_pref", typeof(decimal));
                table.Columns.Add("fc_dref", typeof(decimal));
                table.Columns.Add("fc_brut", typeof(decimal));
                table.Columns.Add("fc_vimp1", typeof(decimal));
                table.Columns.Add("fc_vimp2", typeof(decimal));
                table.Columns.Add("fc_vdct1", typeof(decimal));
                table.Columns.Add("fc_vdct4", typeof(decimal));
                table.Columns.Add("fc_pdc2", typeof(decimal));
                table.Columns.Add("fc_pdc3", typeof(decimal));
                table.Columns.Add("fc_vdc23", typeof(decimal));
                table.Columns.Add("fc_vvta", typeof(decimal));
                table.Columns.Add("fc_vimp3", typeof(decimal));
                table.Columns.Add("fc_pimp4", typeof(decimal));
                table.Columns.Add("fc_vimp4", typeof(decimal));
                table.Columns.Add("fc_total", typeof(decimal));
                table.Columns.Add("fc_esta", typeof(string));
                table.Columns.Add("fc_tdoc", typeof(string));
                table.Columns.Add("fc_cuse", typeof(string));
                table.Columns.Add("fc_muse", typeof(string));
                table.Columns.Add("fc_fcre", typeof(DateTime));
                table.Columns.Add("fc_fmod", typeof(DateTime));
                table.Columns.Add("fc_hora", typeof(string));
                table.Columns.Add("fc_auto", typeof(string));
                table.Columns.Add("fc_ftx", typeof(string));
                table.Columns.Add("fc_estc", typeof(string));
                table.Columns.Add("fc_sexo", typeof(string));
                table.Columns.Add("fc_mpub", typeof(string));
                table.Columns.Add("fc_edad", typeof(string));
                table.Columns.Add("fc_regv", typeof(string));

                /*sostic 05/2019*/
                table.Columns.Add("fc_idtda", typeof(string));
                table.Columns.Add("fc_id_est", typeof(string));
                table.Columns.Add("fc_id_tcv", typeof(string));
                table.Columns.Add("fc_refere", typeof(string));
                table.Columns.Add("fc_ubi", typeof(string));
                /*sostic 05/2019*/

                foreach (var item in list.lista_ffactc)
                {
                    
                        table.Rows.Add(item.fc_nint,
                                       item.fc_nnot,                                      
                                       item.fc_codi,
                                       item.fc_suna,
                                       item.fc_sfac,
                                       item.fc_nfac,
                                       item.fc_ffac,
                                       item.fc_nord,
                                       item.fc_cref,
                                       item.fc_sref,
                                       item.fc_nref,
                                       item.fc_pvta,
                                       item.fc_csuc,
                                       item.fc_gvta,
                                       item.fc_zona,
                                       item.fc_clie,
                                       item.fc_ncli,
                                       item.fc_nomb,
                                       item.fc_apep,
                                       item.fc_apem,
                                       item.fc_dcli,
                                       item.fc_cubi,
                                       item.fc_ruc,
                                       item.fc_vuse,
                                       item.fc_vend,
                                       item.fc_ipre,
                                       item.fc_tint,
                                       item.fc_pint,
                                       item.fc_lcsg,
                                       item.fc_ncon,
                                       item.fc_dcon,
                                       item.fc_lcon,
                                       item.fc_lruc,
                                       item.fc_agen,
                                       item.fc_mone,
                                       item.fc_tasa,
                                       item.fc_fpag,
                                       item.fc_nlet,
                                       item.fc_qtot,
                                       item.fc_pref,
                                       item.fc_dref,
                                       item.fc_brut,
                                       item.fc_vimp1,
                                       item.fc_vimp2,
                                       item.fc_vdct1,
                                       item.fc_vdct4,
                                       item.fc_pdc2,
                                       item.fc_pdc3,
                                       item.fc_vdc23,
                                       item.fc_vvta,
                                       item.fc_vimp3,
                                       item.fc_pimp4,
                                       item.fc_vimp4,
                                       item.fc_total,
                                       item.fc_esta,
                                       item.fc_tdoc,
                                       item.fc_cuse,                                      
                                       item.fc_muse,
                                       item.fc_fcre,
                                       item.fc_fmod,
                                       item.fc_hora,
                                       item.fc_auto,
                                       item.fc_ftx,
                                       item.fc_estc,
                                       item.fc_sexo,
                                       item.fc_mpub,
                                       item.fc_edad,
                                       item.fc_regv,
                                       /*sostic 05-2019*/
                                       (item.fc_idtda_b==null)?"": item.fc_idtda_b,
                                       (item.fc_id_est == null) ? "" : item.fc_id_est,
                                       (item.fc_id_tcv == null)?"": item.fc_id_tcv,
                                       (item.fc_refere == null)?"": item.fc_refere,
                                       (item.fc_ubi == null)?"": item.fc_ubi
                                       /*sostic 05-2019*/
                                       );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return table;
        }
        private DataTable ConvertListToDataTable_Ffactd(Ent_List_Ffactd list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable("FFACTD");
                table.Columns.Add("fd_nint", typeof(string));
                table.Columns.Add("fd_tipo", typeof(string));
                table.Columns.Add("fd_arti", typeof(string));
                table.Columns.Add("fd_regl", typeof(string));
                table.Columns.Add("fd_colo", typeof(string));
                table.Columns.Add("fd_item", typeof(string));
                table.Columns.Add("fd_icmb", typeof(string));
                table.Columns.Add("fd_qfac", typeof(Decimal));
                table.Columns.Add("fd_lpre", typeof(string));
                table.Columns.Add("fd_calm", typeof(string));
                table.Columns.Add("fd_pref", typeof(Decimal));
                table.Columns.Add("fd_dref", typeof(Decimal));
                table.Columns.Add("fd_prec", typeof(Decimal));
                table.Columns.Add("fd_brut", typeof(Decimal));
                table.Columns.Add("fd_pimp1", typeof(Decimal));
                table.Columns.Add("fd_vimp1", typeof(Decimal));
                table.Columns.Add("fd_subt1", typeof(Decimal));
                table.Columns.Add("fd_pimp2", typeof(Decimal));
                table.Columns.Add("fd_vimp2", typeof(Decimal));
                table.Columns.Add("fd_subt2", typeof(Decimal));
                table.Columns.Add("fd_pdct1", typeof(Decimal));
                table.Columns.Add("fd_vdct1", typeof(Decimal));
                table.Columns.Add("fd_subt3", typeof(Decimal));
                table.Columns.Add("fd_vdct4", typeof(Decimal));
                table.Columns.Add("fd_vdc23", typeof(Decimal));
                table.Columns.Add("fd_vvta", typeof(Decimal));
                table.Columns.Add("fd_pimp3", typeof(Decimal));
                table.Columns.Add("fd_vimp3", typeof(Decimal));
                table.Columns.Add("fd_pimp4", typeof(decimal));
                table.Columns.Add("fd_vimp4", typeof(decimal));                
                table.Columns.Add("fd_total", typeof(decimal));
                table.Columns.Add("fd_cuse", typeof(string));
                table.Columns.Add("fd_muse", typeof(string));
                table.Columns.Add("fd_fcre", typeof(DateTime));
                table.Columns.Add("fd_fmod", typeof(DateTime));
                table.Columns.Add("fd_auto", typeof(string));
                table.Columns.Add("fd_dre2", typeof(decimal));
                table.Columns.Add("fd_asoc", typeof(string));
                

                foreach (var item in list.lista_ffactd)
                {
                    table.Rows.Add(item.fd_nint,
                                   item.fd_tipo,
                                   item.fd_arti,
                                   item.fd_regl,
                                   item.fd_colo,
                                   item.fd_item,
                                   item.fd_icmb,
                                   item.fd_qfac,
                                   item.fd_lpre,
                                   item.fd_calm,
                                   item.fd_pref,
                                   item.fd_dref,
                                   item.fd_prec,
                                   item.fd_brut,
                                   item.fd_pimp1,
                                   item.fd_vimp1,
                                   item.fd_subt1,
                                   item.fd_pimp2,
                                   item.fd_vimp2,
                                   item.fd_subt2,
                                   item.fd_pdct1,
                                   item.fd_vdct1,
                                   item.fd_subt3,
                                   item.fd_vdct4,
                                   item.fd_vdc23,
                                   item.fd_vvta,
                                   item.fd_pimp3,
                                   item.fd_vimp3,
                                   item.fd_pimp4,
                                   item.fd_vimp4,
                                   item.fd_total,
                                   item.fd_cuse,
                                   item.fd_muse,
                                   item.fd_fcre,
                                   item.fd_fmod,
                                   item.fd_auto,
                                   item.fd_dre2,
                                   item.fd_asoc
                                   );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return table;
        }
        private DataTable ConvertListToDataTable_FNotaa(Ent_List_Fnotaa list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable("FNOTAA");
                table.Columns.Add("na_nota", typeof(string));
                table.Columns.Add("na_item", typeof(string));
                table.Columns.Add("na_mone", typeof(string));
                table.Columns.Add("na_tpag", typeof(string));
                table.Columns.Add("na_tasa", typeof(Decimal));
                table.Columns.Add("na_cref", typeof(string));
                table.Columns.Add("na_sref", typeof(string));
                table.Columns.Add("na_nref", typeof(string));
                table.Columns.Add("na_vref", typeof(Decimal));
                table.Columns.Add("na_vpag", typeof(Decimal));
                table.Columns.Add("na_esta", typeof(string));
                table.Columns.Add("na_cier", typeof(string));
                table.Columns.Add("na_fcre", typeof(string));
                table.Columns.Add("na_fmod", typeof(string));
              


                foreach (var item in list.lista_fnotaa)
                {
                    table.Rows.Add(item.na_nota,
                                   item.na_item,
                                   item.na_mone,
                                   item.na_tpag,
                                   item.na_tasa,
                                   item.na_cref,
                                   item.na_sref,
                                   item.na_nref,
                                   item.na_vref,
                                   item.na_vpag,
                                   item.na_esta,
                                   item.na_cier,
                                   item.na_fcre,
                                   item.na_fmod                                   
                                   );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return table;
        }

        public DataSet GET_OBTENER_VENTA_XSTORE(string cod_tda, DateTime fecha)
        {
            string sqlquery = "USP_EXTRAER_VENTAS_TDA";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                        cmd.Parameters.AddWithValue("@FEC_INI", fecha);
                     
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            ds.Tables[0].TableName = "FFACTC";
                            ds.Tables[1].TableName = "FFACTD";
                            ds.Tables[2].TableName = "FNOTAA";
                            ds.Tables[3].TableName = "FSTKG";
                            ds.Tables[4].TableName = "FCIERR"; 
                            ds.Tables[5].TableName = "FFLASH";

                        }
                    }
                }
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }


        public DataSet SET_XSTORE_VENTA_EXPORTAR(string cod_tda, DateTime fechaIni, DateTime fechaFin)
        {
            string sqlquery = "USP_SET_XSTORE_VENTA_EXPORTAR";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                        cmd.Parameters.AddWithValue("@FEC_INI", fechaIni);
                        cmd.Parameters.AddWithValue("@FEC_FIN", fechaFin);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            ds.Tables[0].TableName = "TRANS_LINE_TENDER";
                            ds.Tables[1].TableName = "TRANS_TAX";
                            ds.Tables[2].TableName = "TRANS_LINE_ITEM_TAX";
                            ds.Tables[3].TableName = "TRANS_LINE_ITEM";
                            ds.Tables[4].TableName = "TRANS_HEADER";                           

                        }
                    }
                }
            }
            catch ( Exception exc)
            {
                ds = null;
            }
            return ds;
        }


        /*sostic 05/2019*/
        public DataSet consultar_guias(string cod_tda)
        {
            string sqlquery = "USP_CONSULTAR_GUIAS";
            Ent_MsgTransac msg = null;
            DataSet ds = null;
            try
            {
                msg = new Ent_MsgTransac();
                ds = new DataSet();
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@cod_tda", cod_tda);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                        }
                    }
                    catch (Exception ex)
                    {
                        ds = new DataSet();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("codigo");
                        dt.Columns.Add("descripcion");
                        dt.Rows.Add("1", ex.Message + " ==> SQL");
                        ds.Tables.Add(dt);
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo");
                dt.Columns.Add("descripcion");
                dt.Rows.Add("1", ex.Message + " ==> SQL");
                ds.Tables.Add(dt);
            }
            return ds;
        }
        /*sostic 05/2019*/
        public DataTable consultar_guias_actualizadas(int id)
        {
            string sqlquery = "usp_consultar_guias_actualizada";
            Ent_MsgTransac msg = null;
            DataTable dt = null;
            try
            {
                msg = new Ent_MsgTransac();
                dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", id);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        dt = new DataTable();
                        dt.Columns.Add("codigo");
                        dt.Columns.Add("descripcion");
                        dt.Rows.Add("1", ex.Message + " ==> SQL");
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("codigo");
                dt.Columns.Add("descripcion");
                dt.Rows.Add("1", ex.Message + " ==> SQL");
            }
            return dt;
        }

        /*sostic 05/2019*/
        public Ent_MsgTransac consultar_stock_otra_tienda(string cod_art, string calidad, string talla, double cant, string cod_tda_b)
        {
            string sqlquery = "USP_CONSULTAR_STOCK_OTRA_TDA";
            Ent_MsgTransac msg = null;
            try
            {
                msg = new Ent_MsgTransac();
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {

                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@COD_ART", cod_art);
                            cmd.Parameters.AddWithValue("@CALIDAD", calidad);
                            cmd.Parameters.AddWithValue("@TALLA", talla);
                            cmd.Parameters.AddWithValue("@CANT", cant);
                            cmd.Parameters.AddWithValue("@COD_TDA", cod_tda_b);
                            int filas = Convert.ToInt32(cmd.ExecuteScalar());
                            msg.codigo = "0";
                            msg.descripcion = filas.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.codigo = "1";
                        msg.descripcion = ex.Message + "==> SQL";
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                msg.codigo = "1";
                msg.descripcion = ex.Message + "==error";
            }
            return msg;
        }

        /*sostic 05/2019*/
        public Ent_MsgTransac insertar_guia(string cod_tda,
                                                   DataTable fmc,
                                                   DataTable fmd)
        {
            string sqlquery = "[USP_INSERTAR_GUIAT_TEMP]";
            Ent_MsgTransac msg = null;
            string id = "";
            try
            {
                msg = new Ent_MsgTransac();

                if (fmc.Rows.Count > 0)
                {
                    using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                            {
                                cmd.CommandTimeout = 120;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@TMPFMC", fmc);
                                cmd.Parameters.AddWithValue("@TMPFMD", fmd);
                                id = cmd.ExecuteScalar().ToString();
                                msg.codigo = "0";
                                msg.descripcion = id;
                            }
                        }
                        catch (Exception exc)
                        {
                            msg.codigo = "1";
                            msg.descripcion = exc.Message + "==> SQL";
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
                else
                {
                    msg.codigo = "0";
                    msg.descripcion = "No hay trasacciones para actualizar";
                }
            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message + "==error";
            }
            return msg;
        }
        /*sostic 05/2019*/
        public Ent_MsgTransac insertar_historial_estado_cv(string cod_tda, string cod_entid, string fc_nint, string id_estado, string cod_usuario, string descripcion, string cod_vendedor, string serie_numero)
        {
            string sqlquery = "[usp_insertar_historial_estados_cv]";
            Ent_MsgTransac msg = null;
            int _filas = 0;
            try
            {
                msg = new Ent_MsgTransac();

                //if ( != null)
                // {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@cod_entid", cod_entid);
                            cmd.Parameters.AddWithValue("@fc_nint", fc_nint);
                            cmd.Parameters.AddWithValue("@id_estado", id_estado);
                            cmd.Parameters.AddWithValue("@cod_usuario", cod_usuario);
                            cmd.Parameters.AddWithValue("@descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@cod_vendedor", cod_vendedor);
                            cmd.Parameters.AddWithValue("@serie_numero", serie_numero);
                            _filas = cmd.ExecuteNonQuery();
                            msg.codigo = "0";
                            msg.descripcion = (_filas > 0 ? "Se actualizó el estado de la venta." : "No se actualizó el estado");
                        }
                    }
                    catch (Exception exc)
                    {
                        msg.codigo = "1";
                        msg.descripcion = exc.Message + "==> SQL";
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
                //}
                //else
                //{
                //    msg.codigo = "0";
                //    msg.descripcion = "No hay trasacciones para actualizar";
                //}
            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message + "==error";
            }
            return msg;
        }
        /*sostic 05/2019*/
        public Ent_MsgTransac actualizar_guia(string cod_tda, string serie, string numero, int id)
        {
            string sqlquery = "usp_actualizar_guia";
            Ent_MsgTransac msg = null;
            try
            {
                msg = new Ent_MsgTransac();


                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@serie", serie);
                            cmd.Parameters.AddWithValue("@numero", numero);
                            cmd.Parameters.AddWithValue("@id", id);

                            cmd.ExecuteNonQuery();
                            msg.codigo = "0";
                            msg.descripcion = "Se actualizo correctamente";
                        }
                    }
                    catch (Exception exc)
                    {
                        msg.codigo = "1";
                        msg.descripcion = exc.Message + "==> SQL";
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message + "==error";
            }
            return msg;
        }
        /*sostic 05/2019*/
        public DataTable consultar_tiendas_disponibles_cv()
        {
            string sqlquery = "usp_get_lsita_tiendas_cv";
            Ent_MsgTransac msg = null;
            DataTable dt = null;
            try
            {
                msg = new Ent_MsgTransac();
                dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                    catch (Exception ex)
                    {

                        dt = new DataTable();
                        dt.Columns.Add("codigo");
                        dt.Columns.Add("descripcion");
                        dt.Rows.Add("1", ex.Message + " ==> SQL");

                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {

                dt = new DataTable();
                dt.Columns.Add("codigo");
                dt.Columns.Add("descripcion");
                dt.Rows.Add("1", ex.Message + " ==> SQL");

            }
            return dt;
        }
    }
}
