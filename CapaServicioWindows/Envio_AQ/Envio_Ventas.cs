using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Envio_AQ
{
    public class Envio_Ventas
    {
        private void insertar_error_aq(string error)
        {
            string sqlquery = "USP_Insertar_Errores_Service";           
            try
            {

                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion_aq))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@error", error);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
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
        private DataSet ds_ventas()
        {
            string sqlquery = "USP_EXPORTAR_AQ_NOVELL";
            DataSet ds = null;
            try
            {

                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion_aq))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);
                        }
                    }
                }

            }
            catch(Exception exc) 
            {
                throw;
                
            }
            return ds;
        }

        private Boolean update_dbf(string fc_tdoc,string fc_ndoc,DataTable dt_VMAFC,DataTable dt_VMAFD,
                                   /*OleDbConnection cn_dbf,*/string ruta_VMAFC,string ruta_VMAFD,
                                   ref string error,Boolean solo_detalle=false)
        {
            Boolean valida = false;
            string _ruta_erro_file = @"D:\BataTransaction\LOG_AQ.txt";
            string str = "";
            TextWriter tw = null;
            try
            {
                DataRow[] fila_cab = dt_VMAFC.Select("FC_TDOC='" + fc_tdoc + "' AND FC_NDOC='" + fc_ndoc + "'");

                 if (fila_cab.Length>0)
                {

                    //string vfpScript = @"
                    //SET DELETED ON
                    //SET TALK OFF
                    //SET ECHO OFF
                    //USE D:\AQ_EXPORTA_SIS\vmafc.DBF
                    //IF SEEK('BOB03100002871', 'VMAFC', 'LLAVE1') 
                    //    DELETE IN VMAFC 
                    //ENDIF";

                    //string sqlquery_borrar_cab = "DELETE FROM VMAFC WHERE FC_TDOC='" + fc_tdoc + "' AND FC_NDOC='" + fc_ndoc +"'";
                    //string sqlquery_borrar_det = "DELETE FROM VMAFD WHERE FD_TDOC='" + fc_tdoc + "' AND FD_NDOC='" + fc_ndoc + "'";
                    //NetworkShare.ConnectToShare(ruta_VMAFC, ConexionDBF.user_novell, ConexionDBF.password_novell);
                    //string sqlquery_fox1 = "SET DELETED ON ";
                    //string sqlquery_fox2 = "SET TALK OFF ";
                    //string sqlquery_fox3 = "SET ECHO OFF ";
                    //string sqlquery_fox4 = "USE  " + @ruta_VMAFC + "\vmafc.DBF ";
                    //string sqlquery_fox5 = "IF SEEK(" + fc_tdoc + fc_ndoc + ", 'VMAFC', 'LLAVE1') " + 
                    //                       "    DELETE IN VMAFC " + 
                    //                       " ENDIF";

                    //using (OleDbConnection cn_dbf = new OleDbConnection(ConexionDBF._conexion_vfpoledb_1(ruta_VMAFC)))
                    //{
                    //    if (cn_dbf.State == 0) cn_dbf.Open();
                    //    using (OleDbCommand cmd_VMAFC = new OleDbCommand(/*sqlquery_borrar_cab*/vfpScript, cn_dbf))
                    //    {
                    //        tw = new StreamWriter(_ruta_erro_file, true);
                    //        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  entrando delete VMAFC";
                    //        tw.WriteLine(str);
                    //        tw.Flush();
                    //        tw.Close();
                    //        tw.Dispose();

                    //        cmd_VMAFC.CommandTimeout = 0;
                    //        cmd_VMAFC.CommandType = CommandType.Text;
                    //        cmd_VMAFC.ExecuteNonQuery();

                    //        tw = new StreamWriter(_ruta_erro_file, true);
                    //        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  saiendo delete VMAFC";
                    //        tw.WriteLine(str);
                    //        tw.Flush();
                    //        tw.Close();
                    //        tw.Dispose();
                    //    }
                    //    using (OleDbCommand cmd_VMAFD = new OleDbCommand(sqlquery_borrar_det, cn_dbf))
                    //    {
                    //        tw = new StreamWriter(_ruta_erro_file, true);
                    //        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  entrando delete VMAFD";
                    //        tw.WriteLine(str);
                    //        tw.Flush();
                    //        tw.Close();
                    //        tw.Dispose();

                    //        cmd_VMAFD.CommandTimeout = 0;
                    //        cmd_VMAFD.CommandType = CommandType.Text;
                    //        cmd_VMAFD.ExecuteNonQuery();

                    //        tw = new StreamWriter(_ruta_erro_file, true);
                    //        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  saiendo delete VMAFD";
                    //        tw.WriteLine(str);
                    //        tw.Flush();
                    //        tw.Close();
                    //        tw.Dispose();
                    //    }
                    //}
                        /*fin*/
                        /*inicio de conexion insert*/
                        using (OleDbConnection cn_dbf_insert = new OleDbConnection(ConexionDBF._conexion_vfpoledb_1(ruta_VMAFC)))
                        {
                            if (cn_dbf_insert.State == 0) cn_dbf_insert.Open();
                            string sqlquery_insert_cab = "INSERT INTO VMAFC(fc_tdoc,fc_ndoc,fc_nint,fc_norden,fc_nped,fc_nguia,fc_tipref,fc_docref,fc_tipref2," +
                                                                  "fc_docref2,fc_ndes,fc_ndesp,fc_ffact,fc_estado,fc_festad,fc_exte,fc_fltf,fc_empre," +
                                                                  "fc_canal,fc_caden,fc_almac,fc_ccond,fc_flcsg,fc_plataf,fc_client,fc_clisuc,fc_ccli," +
                                                                  "fc_ruc,fc_ccli2,fc_tiden,fc_diden,fc_qftot,fc_vbruta,fc_vimport,fc_pdcto1,fc_pdcto2," +
                                                                  "fc_vdcto,fc_pigv,fc_vigv,fc_vtotal,fc_valcob,fc_moneda,fc_valdol,fc_dcli,fc_ddir," +
                                                                  "fc_dloc,fc_cubi,fc_almcli,fc_flagc,fc_dcsg,fc_ddcsg,fc_dlocon,fc_cven,fc_tiketer," +
                                                                  "fc_flemb,fc_zona,fc_grupo,fc_compr,fc_grupov,fc_comprv,fc_cta,fc_pertc,fc_tipo,fc_pperc," +
                                                                  "fc_vperc,fc_dua,fc_cuu,fc_trx,fc_glosa,fc_hash,fc_motivo,log_ultmod) " +
                                                                  "VALUES(?,?,?,?,?,?,?,?,?," +
                                                                  "?,?,?,?,?,?,?,?,?," +
                                                                  "?,?,?,?,?,?,?,?,?," +
                                                                  "?,?,?,?,?,?,?,?,?," +
                                                                  "?,?,?,?,?,?,?,?,?," +
                                                                  "?,?,?,?,?,?,?,?,?," +
                                                                  "?,?,?,?,?,?,?,?,?,?," +
                                                                  "?,?,?,?,?,?,?,?)";


                        string sqlquery_insert_det = "INSERT INTO VMAFD(fd_tdoc,fd_ndoc,fd_nint,fd_empre,fd_secci,fd_canal,fd_almac,fd_cart,fd_cali,fd_cliart," +
                                                                        "fd_qftot,fd_vcosto,fd_vuni2,fd_vuni,fd_vtot,fd_prom,fd_pdcto,fd_vdcto1,fd_vdcto2," +
                                                                        "fd_vdcto,fd_vstot,fd_qp00,fd_qp01,fd_qp02,fd_qp03,fd_qp04,fd_qp05,fd_qp06,fd_qp07," +
                                                                        "fd_qp08,fd_qp09,fd_qp10,fd_qp11,fd_merc,fd_categ,fd_csubcat,fd_marca,fd_merc3," +
                                                                        "fd_cate3,fd_subc3,fd_marc3,fd_clase,fd_flagc,fd_orige,fd_prov,fd_cunid,fd_regla," +
                                                                        "fd_regini,fd_regfin,fd_cdesc,fd_docref,fd_fecref,fd_tikref,fd_cuu,fd_dua," +
                                                                        "fd_fnoperc,fd_pcomis,fd_ftx)" +
                                                                        "VALUES(?,?,?,?,?,?,?,?,?,?," +
                                                                        "?,?,?,?,?,?,?,?,?," +
                                                                        "?,?,?,?,?,?,?,?,?,?," +
                                                                        "?,?,?,?,?,?,?,?,?," +
                                                                        "?,?,?,?,?,?,?,?,?," +
                                                                        "?,?,?,?,?,?,?,?," +
                                                                        "?,?,?)";


                        string _fc_tdoc = fila_cab[0]["fc_tdoc"].ToString();
                        string _fc_ndoc = fila_cab[0]["fc_ndoc"].ToString();
                        string fc_tipref= fila_cab[0]["fc_tipref"].ToString();
                        string fc_docref = fila_cab[0]["fc_docref"].ToString();
                        string fc_docref2 = fila_cab[0]["fc_docref2"].ToString();
                        DateTime fc_ffact =Convert.ToDateTime(fila_cab[0]["fc_ffact"]);
                        string fc_estado = fila_cab[0]["fc_estado"].ToString();
                        string fc_fltf = fila_cab[0]["fc_fltf"].ToString();
                        string fc_empre = fila_cab[0]["fc_empre"].ToString();
                        string fc_canal = fila_cab[0]["fc_canal"].ToString();
                        string fc_cadena = fila_cab[0]["fc_cadena"].ToString();
                        string fc_almac = fila_cab[0]["fc_almac"].ToString();
                        string fc_ccond = fila_cab[0]["fc_ccond"].ToString();
                        string fc_client = fila_cab[0]["fc_client"].ToString();
                        string fc_ccli2 = fila_cab[0]["fc_ccli2"].ToString();
                        string fc_tiden = fila_cab[0]["fc_tiden"].ToString();
                        string fc_diden = fila_cab[0]["fc_diden"].ToString();
                        Int32 fc_qftot =Convert.ToInt32(fila_cab[0]["fc_qftot"]);
                        Decimal fc_vbruta =Convert.ToDecimal(fila_cab[0]["fc_vbruta"]);
                        Decimal fc_vimport =Convert.ToDecimal(fila_cab[0]["fc_vimport"]);
                        Decimal fc_pigv =Convert.ToDecimal(fila_cab[0]["fc_pigv"]);
                        Decimal fc_vigv =Convert.ToDecimal(fila_cab[0]["fc_vigv"]);
                        Decimal fc_vtotal =Convert.ToDecimal(fila_cab[0]["fc_vtotal"]);
                        string fc_moneda = fila_cab[0]["fc_moneda"].ToString();
                        string fc_dcli = fila_cab[0]["fc_dcli"].ToString();
                        string fc_ddir = fila_cab[0]["fc_ddir"].ToString();
                        string fc_dloc = fila_cab[0]["fc_dloc"].ToString();
                        string fc_flagc = fila_cab[0]["fc_flagc"].ToString();
                        string fc_cven = fila_cab[0]["fc_cven"].ToString();
                        string fc_ticketer = fila_cab[0]["fc_ticketer"].ToString();
                        Decimal fc_pperc =Convert.ToDecimal(fila_cab[0]["fc_pperc"]);
                        Decimal fc_vperc =Convert.ToDecimal(fila_cab[0]["fc_vperc"]);
                        string fc_hash = fila_cab[0]["fc_hash"].ToString();
                        string fc_motivo = fila_cab[0]["fc_motivo"].ToString();

                        /*insertando cabecera d elas ventas*/
                        //string hab = "SET NULL OFF";
                        using (OleDbCommand cmd_VMAFC = new OleDbCommand(sqlquery_insert_cab, cn_dbf_insert))
                        {
                            //cmd_VMAFC.ExecuteNonQuery();
                            //cmd_VMAFC.CommandText = sqlquery_insert_cab;

                            cmd_VMAFC.CommandTimeout = 0;
                            cmd_VMAFC.CommandType = CommandType.Text;
                            cmd_VMAFC.Parameters.Add("@fc_tdoc", OleDbType.Char).Value = _fc_tdoc;
                            cmd_VMAFC.Parameters.Add("@fc_ndoc", OleDbType.Char).Value = _fc_ndoc;
                            cmd_VMAFC.Parameters.Add("@fc_nint", OleDbType.Char).Value ="" ;
                            cmd_VMAFC.Parameters.Add("@fc_norden", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_nped", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_nguia", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_tipref", OleDbType.Char).Value = fc_tipref;
                            cmd_VMAFC.Parameters.Add("@fc_docref", OleDbType.Char).Value = fc_docref;
                            cmd_VMAFC.Parameters.Add("@fc_tipref2", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_docref2", OleDbType.Char).Value = fc_docref2;
                            cmd_VMAFC.Parameters.Add("@fc_ndes", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_ndesp", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_ffact", OleDbType.Date).Value = fc_ffact;
                            cmd_VMAFC.Parameters.Add("@fc_estado", OleDbType.Char).Value = fc_estado;
                            cmd_VMAFC.Parameters.Add("@fc_festad", OleDbType.Date).Value = Convert.ToDateTime("01/01/1900");// fc_ffact;//"{}";// DBNull.Value;
                            cmd_VMAFC.Parameters.Add("@fc_exte", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_fltf", OleDbType.Char).Value = fc_fltf;
                            cmd_VMAFC.Parameters.Add("@fc_empre", OleDbType.Char).Value = fc_empre;
                            cmd_VMAFC.Parameters.Add("@fc_canal", OleDbType.Char).Value = fc_canal;
                            cmd_VMAFC.Parameters.Add("@fc_caden", OleDbType.Char).Value = fc_cadena;
                            cmd_VMAFC.Parameters.Add("@fc_almac", OleDbType.Char).Value = fc_almac;
                            cmd_VMAFC.Parameters.Add("@fc_ccond", OleDbType.Char).Value = fc_ccond;
                            cmd_VMAFC.Parameters.Add("@fc_flcsg", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_plataf", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_client", OleDbType.Char).Value = fc_client;
                            cmd_VMAFC.Parameters.Add("@fc_clisuc", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_ccli", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_ruc", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_ccli2", OleDbType.Char).Value = fc_ccli2;
                            cmd_VMAFC.Parameters.Add("@fc_tiden", OleDbType.Char).Value = fc_tiden;
                            cmd_VMAFC.Parameters.Add("@fc_diden", OleDbType.Char).Value = fc_diden;
                            cmd_VMAFC.Parameters.Add("@fc_qftot", OleDbType.Decimal).Value = fc_qftot;
                            cmd_VMAFC.Parameters.Add("@fc_vbruta", OleDbType.Decimal).Value = fc_vbruta;
                            cmd_VMAFC.Parameters.Add("@fc_vimport", OleDbType.Decimal).Value = fc_vimport;
                            cmd_VMAFC.Parameters.Add("@fc_pdcto1", OleDbType.Decimal).Value = 0;
                            cmd_VMAFC.Parameters.Add("@fc_pdcto2", OleDbType.Decimal).Value = 0;
                            cmd_VMAFC.Parameters.Add("@fc_vdcto", OleDbType.Decimal).Value = 0;
                            cmd_VMAFC.Parameters.Add("@fc_pigv", OleDbType.Decimal).Value = fc_pigv;
                            cmd_VMAFC.Parameters.Add("@fc_vigv", OleDbType.Decimal).Value = fc_vigv;
                            cmd_VMAFC.Parameters.Add("@fc_vtotal", OleDbType.Decimal).Value = fc_vtotal;
                            cmd_VMAFC.Parameters.Add("@fc_valcob", OleDbType.Decimal).Value = 0;
                            cmd_VMAFC.Parameters.Add("@fc_moneda", OleDbType.Char).Value = fc_moneda;
                            cmd_VMAFC.Parameters.Add("@fc_valdol", OleDbType.Decimal).Value = 0;
                            cmd_VMAFC.Parameters.Add("@fc_dcli", OleDbType.Char).Value = fc_dcli;
                            cmd_VMAFC.Parameters.Add("@fc_ddir", OleDbType.Char).Value = fc_ddir;
                            cmd_VMAFC.Parameters.Add("@fc_dloc", OleDbType.Char).Value = fc_dloc;
                            cmd_VMAFC.Parameters.Add("@fc_cubi", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_almcli", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_flagc", OleDbType.Char).Value = fc_flagc;
                            cmd_VMAFC.Parameters.Add("@fc_dcsg", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_ddcsg", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_dlocon", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_cven", OleDbType.Char).Value = fc_cven;
                            cmd_VMAFC.Parameters.Add("@fc_tiketer", OleDbType.Char).Value = fc_ticketer;
                            cmd_VMAFC.Parameters.Add("@fc_flemb", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_zona", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_grupo", OleDbType.Char).Value = "";

                            cmd_VMAFC.Parameters.Add("@fc_compr", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_grupov", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_comprv", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_cta", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_pertc", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_tipo", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_pperc", OleDbType.Decimal).Value = fc_pperc;
                            cmd_VMAFC.Parameters.Add("@fc_vperc", OleDbType.Decimal).Value = fc_vperc;
                            cmd_VMAFC.Parameters.Add("@fc_dua", OleDbType.Date).Value =Convert.ToDateTime("01/01/1900");//;DBNull.Value;
                            cmd_VMAFC.Parameters.Add("@fc_cuu", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_trx", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_glosa", OleDbType.Char).Value = "";
                            cmd_VMAFC.Parameters.Add("@fc_hash", OleDbType.Char).Value = fc_hash;
                            cmd_VMAFC.Parameters.Add("@fc_motivo", OleDbType.Char).Value = fc_motivo;
                            cmd_VMAFC.Parameters.Add("@log_ultmod", OleDbType.Char).Value = "";


                            tw = new StreamWriter(_ruta_erro_file, true);
                            str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  entrando a insert VMAFC";
                            tw.WriteLine(str);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();

                            if (!solo_detalle)
                            { 
                                cmd_VMAFC.ExecuteNonQuery();
                            }

                            tw = new StreamWriter(_ruta_erro_file, true);
                            str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  saliendo a insert VMAFC";
                            tw.WriteLine(str);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();

                        }
                        
                        /*INSERTANDO DETALLE*/
                        foreach (DataRow fila in dt_VMAFD.Select("FD_TDOC='" + _fc_tdoc + "' AND FD_NDOC='" + _fc_ndoc +"'"))
                        {
                            string fd_empre = fila["fd_empre"].ToString();
                            string fd_canal = fila["fd_canal"].ToString();
                            string fd_almac = fila["fd_almac"].ToString();
                            string fd_cart = fila["fd_cart"].ToString();
                            string fd_cali = fila["fd_cali"].ToString();
                            Decimal fd_qftot =Convert.ToDecimal(fila["fd_qftot"]);
                            Decimal fd_vcosto =Convert.ToDecimal(fila["fd_vcosto"]);
                            Decimal fd_vuni2 =Convert.ToDecimal(fila["fd_vuni2"]);
                            Decimal fd_vuni =Convert.ToDecimal(fila["fd_vuni"]);
                            Decimal fd_vtot =Convert.ToDecimal(fila["fd_vtot"]);
                            Decimal fd_pdcto =Convert.ToDecimal(fila["fd_pdcto"]);
                            Decimal fd_vdcto1 =Convert.ToDecimal(fila["fd_vdcto1"]);
                            Decimal fd_vdcto2 =Convert.ToDecimal(fila["fd_vdcto2"]);
                            Decimal fd_vdcto =Convert.ToDecimal(fila["fd_vdcto"]);
                            Decimal fd_vstot =Convert.ToDecimal(fila["fd_vstot"]);
                            Decimal _00 =Convert.ToDecimal(fila["00"]);
                            Decimal _01 =Convert.ToDecimal(fila["01"]);
                            Decimal _02 =Convert.ToDecimal(fila["02"]);
                            Decimal _03 =Convert.ToDecimal(fila["03"]);
                            Decimal _04 =Convert.ToDecimal(fila["04"]);
                            Decimal _05 =Convert.ToDecimal(fila["05"]);
                            Decimal _06 =Convert.ToDecimal(fila["06"]);
                            Decimal _07 =Convert.ToDecimal(fila["07"]);
                            Decimal _08 =Convert.ToDecimal(fila["08"]);
                            Decimal _09 =Convert.ToDecimal(fila["09"]);
                            Decimal _10 =Convert.ToDecimal(fila["10"]);
                            Decimal _11 =Convert.ToDecimal(fila["11"]);
                            string fd_categ = fila["fd_categ"].ToString();
                            string fd_csubcat = fila["fd_csubcat"].ToString();
                            string fd_clase = fila["fd_clase"].ToString();
                            string fd_flagc = fila["fd_flagc"].ToString();
                            string fd_orige = fila["fd_orige"].ToString();
                            string fd_prov = fila["fd_prov"].ToString();
                            string fd_cunid = fila["fd_cunid"].ToString();
                            string fd_regla = fila["fd_regla"].ToString();
                            string fd_regini = fila["fd_regini"].ToString();
                            string fd_regfin = fila["fd_regfin"].ToString();
                            string fd_cdesc = fila["fd_cdesc"].ToString();
                            string fd_docref = fila["fd_docref"].ToString();
                            DateTime fd_fecref = DateTime.Today; 
                            if (fila["fd_fecref"]!=DBNull.Value)
                            {
                                fd_fecref= Convert.ToDateTime(fila["fd_fecref"]);
                            }
                                
                            string fd_tikref = fila["fd_tikref"].ToString();


                            using (OleDbCommand cmd_VMAFD = new OleDbCommand(sqlquery_insert_det, cn_dbf_insert))
                            {
                                cmd_VMAFD.CommandTimeout = 0;
                                cmd_VMAFD.CommandType = CommandType.Text;
                                cmd_VMAFD.Parameters.Add("@fd_tdoc", OleDbType.Char).Value = _fc_tdoc;
                                cmd_VMAFD.Parameters.Add("@fd_ndoc", OleDbType.Char).Value = _fc_ndoc;
                                cmd_VMAFD.Parameters.Add("@fd_nint", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_empre", OleDbType.Char).Value = fd_empre;
                                cmd_VMAFD.Parameters.Add("@fd_secci", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_canal", OleDbType.Char).Value = fd_canal;
                                cmd_VMAFD.Parameters.Add("@fd_almac", OleDbType.Char).Value = fd_almac;
                                cmd_VMAFD.Parameters.Add("@fd_cart", OleDbType.Char).Value = fd_cart;
                                cmd_VMAFD.Parameters.Add("@fd_cali", OleDbType.Char).Value = fd_cali;
                                cmd_VMAFD.Parameters.Add("@fd_cliart", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_qftot", OleDbType.Decimal).Value = fd_qftot;
                                cmd_VMAFD.Parameters.Add("@fd_vcosto", OleDbType.Decimal).Value = fd_vcosto;
                                cmd_VMAFD.Parameters.Add("@fd_vuni2", OleDbType.Decimal).Value = fd_vuni2;
                                cmd_VMAFD.Parameters.Add("@fd_vuni", OleDbType.Decimal).Value = fd_vuni;
                                cmd_VMAFD.Parameters.Add("@fd_vtot", OleDbType.Decimal).Value = fd_vtot;
                                cmd_VMAFD.Parameters.Add("@fd_prom", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_pdcto", OleDbType.Decimal).Value = fd_pdcto;
                                cmd_VMAFD.Parameters.Add("@fd_vdcto1", OleDbType.Decimal).Value = fd_vdcto1;
                                cmd_VMAFD.Parameters.Add("@fd_vdcto2", OleDbType.Decimal).Value = fd_vdcto2;
                                cmd_VMAFD.Parameters.Add("@fd_vdcto", OleDbType.Decimal).Value = fd_vdcto;
                                cmd_VMAFD.Parameters.Add("@fd_vstot", OleDbType.Decimal).Value = fd_vstot;
                                cmd_VMAFD.Parameters.Add("@fd_qp00", OleDbType.Decimal).Value = _00;
                                cmd_VMAFD.Parameters.Add("@fd_qp01", OleDbType.Decimal).Value = _01;
                                cmd_VMAFD.Parameters.Add("@fd_qp02", OleDbType.Decimal).Value = _02;
                                cmd_VMAFD.Parameters.Add("@fd_qp03", OleDbType.Decimal).Value = _03;
                                cmd_VMAFD.Parameters.Add("@fd_qp04", OleDbType.Decimal).Value = _04;
                                cmd_VMAFD.Parameters.Add("@fd_qp05", OleDbType.Decimal).Value = _05;
                                cmd_VMAFD.Parameters.Add("@fd_qp06", OleDbType.Decimal).Value = _06;
                                cmd_VMAFD.Parameters.Add("@fd_qp07", OleDbType.Decimal).Value = _07;
                                cmd_VMAFD.Parameters.Add("@fd_qp08", OleDbType.Decimal).Value = _08;
                                cmd_VMAFD.Parameters.Add("@fd_qp09", OleDbType.Decimal).Value = _09;
                                cmd_VMAFD.Parameters.Add("@fd_qp10", OleDbType.Decimal).Value = _10;
                                cmd_VMAFD.Parameters.Add("@fd_qp11", OleDbType.Decimal).Value = _11;
                                cmd_VMAFD.Parameters.Add("@fd_merc", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_categ", OleDbType.Char).Value = fd_categ;
                                cmd_VMAFD.Parameters.Add("@fd_csubcat", OleDbType.Char).Value = fd_csubcat;
                                cmd_VMAFD.Parameters.Add("@fd_marca", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_merc3", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_cate3", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_subc3", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_marc3", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_clase", OleDbType.Char).Value = fd_clase;
                                cmd_VMAFD.Parameters.Add("@fd_flagc", OleDbType.Char).Value = fd_flagc;
                                cmd_VMAFD.Parameters.Add("@fd_orige", OleDbType.Char).Value = fd_orige;
                                cmd_VMAFD.Parameters.Add("@fd_prov", OleDbType.Char).Value = fd_prov;
                                cmd_VMAFD.Parameters.Add("@fd_cunid", OleDbType.Char).Value = fd_cunid;
                                cmd_VMAFD.Parameters.Add("@fd_regla", OleDbType.Char).Value = fd_regla;
                                cmd_VMAFD.Parameters.Add("@fd_regini", OleDbType.Char).Value = fd_regini;
                                cmd_VMAFD.Parameters.Add("@fd_regfin", OleDbType.Char).Value = fd_regfin;
                                cmd_VMAFD.Parameters.Add("@fd_cdesc", OleDbType.Char).Value = fd_cdesc;
                                cmd_VMAFD.Parameters.Add("@fd_docref", OleDbType.Char).Value = fd_docref;
                                if (fila["fd_fecref"] != DBNull.Value)
                                {
                                    cmd_VMAFD.Parameters.Add("@fd_fecref", OleDbType.Date).Value = fd_fecref;
                                }
                                else
                                {
                                    cmd_VMAFD.Parameters.Add("@fd_fecref", OleDbType.Date).Value = Convert.ToDateTime("01/01/1900"); //DBNull.Value;
                                }
                                
                                cmd_VMAFD.Parameters.Add("@fd_tikref", OleDbType.Char).Value = fd_tikref;
                                cmd_VMAFD.Parameters.Add("@fd_cuu", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_dua", OleDbType.Date).Value = Convert.ToDateTime("01/01/1900");//DBNull.Value;
                                cmd_VMAFD.Parameters.Add("@fd_fnoperc", OleDbType.Char).Value = "";
                                cmd_VMAFD.Parameters.Add("@fd_pcomis", OleDbType.Decimal).Value = 0;
                                cmd_VMAFD.Parameters.Add("@fd_ftx", OleDbType.Char).Value = "";

                                tw = new StreamWriter(_ruta_erro_file, true);
                                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  entrando a insert VMAFD";
                                tw.WriteLine(str);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                cmd_VMAFD.ExecuteNonQuery();

                                tw = new StreamWriter(_ruta_erro_file, true);
                                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  saliendo a insert VMAFD";
                                tw.WriteLine(str);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                            }
                        }
                        valida = true;

                    }                                     
                }

            }
            catch (Exception exc)
            {
                error = exc.Message;
                valida = false;
            }
            return valida;
        }
        private Boolean valida_file_ecu()
        {
            Boolean valida = false;
            try
            {
                string file = @"D:\BataTransaction\ECU.txt";
                if (File.Exists(file))
                {
                    valida = true;
                }
            }
            catch
            {
                valida = false;
            }
            return valida;
        }
        public string envio_ventas_aq()
        {
            string error = "";
            string _ruta_erro_file = @"D:\BataTransaction\LOG_AQ.txt";
            string str = "";
            TextWriter tw = null;
            try
            {
               
                //tw = new StreamWriter(_ruta_erro_file, true);
                //str = "entrando al metodo envio_ventas_aq";
                //tw.WriteLine(str);
                //tw.Flush();
                //tw.Close();
                //tw.Dispose();

                List<Ruta_DBF> ruta_dbf = get_ruta();

                var loc_vmafc = ruta_dbf.Where(p => p.dbf_name == "VMAFC").ToList();
                var loc_vmafd = ruta_dbf.Where(p => p.dbf_name == "VMAFD").ToList();

                

                string ruta_VMAFC= loc_vmafc[0].dbf_ruta.ToString();
                string ruta_VMAFD= loc_vmafd[0].dbf_ruta.ToString();


              

                DataSet ds = ds_ventas();

                DataTable dt_VMAFC = ds.Tables[0];
                DataTable dt_VMAFD = ds.Tables[1];

                if (dt_VMAFC!=null && dt_VMAFD!=null)
                {
                    if (dt_VMAFC.Rows.Count == 0) return error;
                    /*AUTOLOGEO NOVELL*/
                    if (valida_file_ecu())
                    {
                        string cone= NetworkShare.ConnectToShare(ruta_VMAFC, ConexionDBF.user_novell, ConexionDBF.password_novell);
                    }
                    /**********************/
                    DataTable dt_update = new DataTable();
                    dt_update.Columns.Add("tipo", typeof(string));
                    dt_update.Columns.Add("numero", typeof(string));

                    //ruta_VMAFC = @"D:\COMPARTIR\ventaaq";
                    //ruta_VMAFD = @"D:\ventaaq";

                    //ruta_VMAFC = @"D:\AQ_EXPORTA_SIS";
                    //ruta_VMAFD = @"D:\AQ_EXPORTA_SIS";

                    DataTable dt_existe_cab = null;
                    DataTable dt_existe_det = null;
                    /*VALIDAR SI EXISTE EN DETALLE SI NO EXISTE POR AL ERROR ENTONCES */

                    using (OleDbConnection cn_dbf = new OleDbConnection(ConexionDBF._conexion_fmc_fmd_vfpoledb(ruta_VMAFC)))
                    {
                        DateTime _fecha_ffac = DateTime.Today.AddDays(-7);
                        string sqlquery_consulta = "SELECT FC_TDOC,FC_NDOC,FC_FFACT FROM VMAFC WHERE FC_CANAL='6' AND FC_CADEN='CA'  AND FC_FFACT>=?";
                        using (OleDbCommand cmd_query = new OleDbCommand(sqlquery_consulta, cn_dbf))
                        {
                            cmd_query.CommandTimeout = 0;
                            cmd_query.Parameters.Add("DATE", OleDbType.Date).Value = _fecha_ffac;
                            using (OleDbDataAdapter da_query = new OleDbDataAdapter(cmd_query))
                            {
                                dt_existe_cab = new DataTable();

                                tw = new StreamWriter(_ruta_erro_file, true);
                                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  entrando al query select VMAFC";
                                tw.WriteLine(str);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                da_query.Fill(dt_existe_cab);

                                tw = new StreamWriter(_ruta_erro_file, true);
                                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  saliendo al query select VMAFC";
                                tw.WriteLine(str);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();
                            }
                        }
                    }

                    /*select al detalle*/
                    using (OleDbConnection cn_dbf = new OleDbConnection(ConexionDBF._conexion_fmc_fmd_vfpoledb(ruta_VMAFC)))
                    {
                        DateTime _fecha_ffac = DateTime.Today.AddDays(-7);
                        string sqlquery_consulta = "SELECT FC_TDOC,FC_NDOC FROM VMAFC WHERE FC_CANAL='6' AND FC_CADEN='CA'  AND FC_FFACT>=? AND " +                            
                                                   "FC_TDOC + FC_NDOC NOT IN (SELECT FD_TDOC + FD_NDOC FROM VMAFD)";
                        using (OleDbCommand cmd_query = new OleDbCommand(sqlquery_consulta, cn_dbf))
                        {
                            cmd_query.CommandTimeout = 0;
                            cmd_query.Parameters.Add("DATE", OleDbType.Date).Value = _fecha_ffac;
                            using (OleDbDataAdapter da_query = new OleDbDataAdapter(cmd_query))
                            {
                                dt_existe_det = new DataTable();

                                tw = new StreamWriter(_ruta_erro_file, true);
                                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  entrando al query select VMAFD";
                                tw.WriteLine(str);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                da_query.Fill(dt_existe_det);

                                tw = new StreamWriter(_ruta_erro_file, true);
                                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "  saliendo al query select VMAFD";
                                tw.WriteLine(str);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();
                            }
                        }
                    }



                    var result = dt_VMAFC.AsEnumerable().Select(row => (string)row["FC_TDOC"] + row["FC_NDOC"]).Except(dt_existe_cab.AsEnumerable().Select(row => (string)row["FC_TDOC"] + row["FC_NDOC"]));
                    //var result = dt_VMAFC.AsEnumerable().Select(row => (string)row["FC_TDOC"] + row["FC_NDOC"]);
                    /*EN ESTE CASO VAMOS A INSERTAR LOS DATOS QUE YA EXISTE PARA NO REALIZAR CONSULTAS SOBRE DATOS QUE YA EXISTE */
                    //var result = dt_VMAFC.AsEnumerable().Select(row => (string)row["FC_TDOC"] + row["FC_NDOC"]).Except(dt_existe.AsEnumerable().Select(row => (string)row["FC_TDOC"] + row["FC_NDOC"]));


                    //.Except(da_query.AsEnumerable().Select(r => r.Field<string>("crs_name"), r.Field<string>("name")));
                    #region<METODO DE VENTAS PARA LA INSERCION DE CABECERA Y DETALLE>

                    //if (cn_dbf.State == 0) cn_dbf.Open();
                    foreach (var fila in result)
                        {
                            string error_proc = "";
                            string _FC_TDOC = fila.Substring(0,2);
                            String _FC_NDOC = fila.Substring(2,12);
                            tw = new StreamWriter(_ruta_erro_file, true);
                            str = DateTime.Today.ToString() + "==>" + "entrando al metodo update_dbf";
                            tw.WriteLine(str);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();

                            

                            Boolean upd = update_dbf(_FC_TDOC, _FC_NDOC, dt_VMAFC, dt_VMAFD, /*cn_dbf,*/ ruta_VMAFC, ruta_VMAFD, ref error_proc);

                            tw = new StreamWriter(_ruta_erro_file, true);
                            str = DateTime.Today.ToString() + "==> terminando update dbf";
                            tw.WriteLine(str);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();


                            if (upd)
                            {
                                dt_update.Rows.Add(_FC_TDOC, _FC_NDOC);
                                //string _ruta_erro_file = @"D:\BataTransaction\LOG_AQ.txt";
                                tw = new StreamWriter(_ruta_erro_file, true);
                                str = DateTime.Today.ToString() + "==>" + "insertando datos tipo:" + _FC_TDOC + " numero:" + _FC_NDOC;
                                tw.WriteLine(str);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();
                            }
                            else
                            {
                                if (error_proc.Length > 0)
                                    insertar_error_aq(error_proc + " TIPO:" + _FC_TDOC + " NUMERO:" + _FC_NDOC);
                            }

                        }
                    #endregion
                    //    if (cn_dbf.State == ConnectionState.Open) cn_dbf.Close();
                    //}
                    #region<INSERCION DEL DETALLE SI ES QUE NO SE GRABO>
                    var result_det = dt_existe_det.AsEnumerable().Select(row => (string)row["FC_TDOC"] + row["FC_NDOC"]);
                    foreach (var fila in result_det)
                    {
                        string error_proc = "";
                        string _FC_TDOC = fila.Substring(0, 2);
                        String _FC_NDOC = fila.Substring(2, 12);
                        tw = new StreamWriter(_ruta_erro_file, true);
                        str = DateTime.Today.ToString() + "==>" + "entrando al metodo update_dbf detalle";
                        tw.WriteLine(str);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();


                        /*solo detalle*/
                        Boolean upd = update_dbf(_FC_TDOC, _FC_NDOC, dt_VMAFC, dt_VMAFD, /*cn_dbf,*/ ruta_VMAFC, ruta_VMAFD, ref error_proc,true);

                        tw = new StreamWriter(_ruta_erro_file, true);
                        str = DateTime.Today.ToString() + "==> terminando update detalle dbf";
                        tw.WriteLine(str);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();


                        if (upd)
                        {
                            dt_update.Rows.Add(_FC_TDOC, _FC_NDOC);
                            //string _ruta_erro_file = @"D:\BataTransaction\LOG_AQ.txt";
                            tw = new StreamWriter(_ruta_erro_file, true);
                            str = DateTime.Today.ToString() + "==>" + "insertando datos detalle tipo:" + _FC_TDOC + " numero:" + _FC_NDOC;
                            tw.WriteLine(str);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();
                        }
                        else
                        {
                            if (error_proc.Length > 0)
                                insertar_error_aq(error_proc + " TIPO:" + _FC_TDOC + " NUMERO:" + _FC_NDOC);
                        }

                    }


                    #endregion
                    if (dt_update.Rows.Count>0)
                    {
                        update_envio_sis_sql(dt_update);
                    }
                }

            }
            catch (Exception exc)
            {
                error = exc.Message;                
            }
            return error;
        }

        private void update_envio_sis_sql(DataTable dt)
        {
            string sqlquery = "USP_UPDATE_ENVIO_SIS";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion_aq))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TMP", dt);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch 
            {

                
            }
        }
        private List<Ruta_DBF> get_ruta()
        {
            List<Ruta_DBF> list;
            string sqlquery = "USP_RUTAS_DBF_NOVELL";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion_aq))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataReader dr = cmd.ExecuteReader();
                            list = new List<Ruta_DBF>();
                            if (dr.HasRows)
                            {                              
                                while (dr.Read())
                                {
                                    Ruta_DBF it = new Ruta_DBF();
                                    it.dbf_name = dr["dbf_name"].ToString();
                                    it.dbf_ruta = dr["dbf_ruta"].ToString();
                                    list.Add(it);
                                }
                            }

                        }
                    }
                    catch 
                    {
                        if (cn!=null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                        throw;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch 
            {
                throw;
                
            }
            return list;
        }
    }
    public class Ruta_DBF
    {
        public string dbf_name { get; set; }
        public string dbf_ruta { get; set; }
    }
}
