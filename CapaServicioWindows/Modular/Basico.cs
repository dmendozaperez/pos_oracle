using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
//using CapaDato.Basico;
using System.Text;
using System.Data.OleDb;
using System.Data;
using CapaServicioWindows.Conexion;
using CapaServicioWindows.Envio_Ws;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using CapaServicioWindows.CapaDato.Venta;
using WinSCP;
using CapaServicioWindows.CapaDato.Novell;

namespace CapaServicioWindows.Modular
{
    public class Basico
    {
        private DateTime fecha_despacho = DateTime.Today.AddDays(-25);
        private string gHostName = "172.16.24.216";
        private string gUserName = "webposbpe";
        private string gPassword = "JU737CbDmJvu";
        private int  gPortNumber = 22;
        private string gftp_ruta_destino = "";

        /// <summary>
        /// tiempo de espera a ejecutar
        /// </summary>
        /// <param name="_segundos"></param>
        private  void _espera_ejecuta(Int32 _segundos)
        {
            try
            {
                _segundos = _segundos * 1000;
                System.Threading.Thread.Sleep(_segundos);
            }
            catch
            {

            }
        }       
        /// <summary>
        /// verificar las guias cerradas
        /// </summary>
        /// <returns></returns>
        private List<BataTransac.Ent_Scdddes> get_scdddes(string _path,ref string error)
        {
            /*fecha para traer los pedido cerrados desde una fecha*/
            List<BataTransac.Ent_Scdddes> _lista_scdddes = null;
            String sqlquery_scdddes = "SELECT  DDES_TIPO,DDES_ALMAC,DDES_GUIRE,DDES_NDESP,DDES_MFDES,DDES_DESTI," +
                                      "DDES_N_INI,DDES_N_FIN,DDES_CPAGO,DDES_FEMBA,DDES_FECHA,DDES_FDESP,DDES_ESTAD," +
                                      "DDES_GGUIA,DDES_CCOND,DDES_CALZ,DDES_NCALZ,DDES_TOCAJ,DDES_IMPRE,DDES_GVALO," +
                                      "DDES_SUBGR,DDES_RUCTC,DDES_TRANS,DDES_TRAN2,DDES_OBSER,DDES_NOMTC,DDES_NGUIA," +
                                      "DDES_NRLIQ,DDES_LIMPR,DDES_EMPRE,DDES_CANAL,DDES_CADEN,DDES_SECCI,DDES_FTX,DDES_FTXTD " +
                                      "FROM SCDDDES WHERE DDES_FDESP>=CTOD('" + fecha_despacho.ToString("MM/dd/yy") + "') and DDES_TIPO='DES' and DDES_ESTAD<>'A' AND EMPTY(DDES_FTXTD) AND (NOT EMPTY(DDES_CADEN)) " + get_query_alm_ecu() ;            
            try
            {
                //Util dd = new Util();
                //dd.get_location_dbf();

                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_scdddes, cn))
                    {
                        cmd.CommandTimeout = 0;
                        //cmd.Parameters.Add("DATE", OleDbType.Date).Value = fecha_despacho;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            _lista_scdddes = new List<BataTransac.Ent_Scdddes>();
                            _lista_scdddes = (from DataRow dr in dt.Rows
                                              select new BataTransac.Ent_Scdddes()
                                              {
                                                  DDES_TIPO= dr["DDES_TIPO"].ToString(),
                                                  DDES_ALMAC = dr["DDES_ALMAC"].ToString(),
                                                  DDES_GUIRE = dr["DDES_GUIRE"].ToString(),
                                                  DDES_NDESP = dr["DDES_NDESP"].ToString(),
                                                  DDES_MFDES = dr["DDES_MFDES"].ToString(),
                                                  DDES_DESTI = dr["DDES_DESTI"].ToString(),
                                                  DDES_N_INI = dr["DDES_N_INI"].ToString(),
                                                  DDES_N_FIN = dr["DDES_N_FIN"].ToString(),
                                                  DDES_CPAGO = dr["DDES_CPAGO"].ToString(),
                                                  DDES_FEMBA =Convert.ToDateTime(dr["DDES_FEMBA"]),
                                                  DDES_FECHA =Convert.ToDateTime(dr["DDES_FECHA"]),
                                                  DDES_FDESP =Convert.ToDateTime(dr["DDES_FDESP"]),
                                                  DDES_ESTAD = dr["DDES_ESTAD"].ToString(),
                                                  DDES_GGUIA = dr["DDES_GGUIA"].ToString(),
                                                  DDES_CCOND = dr["DDES_CCOND"].ToString(),
                                                  DDES_CALZ =Convert.ToDecimal(dr["DDES_CALZ"]),
                                                  DDES_NCALZ =Convert.ToDecimal(dr["DDES_NCALZ"]),
                                                  DDES_TOCAJ =Convert.ToDecimal(dr["DDES_TOCAJ"]),
                                                  DDES_IMPRE = dr["DDES_IMPRE"].ToString(),
                                                  DDES_GVALO = dr["DDES_GVALO"].ToString(),
                                                  DDES_SUBGR = dr["DDES_SUBGR"].ToString(),
                                                  DDES_RUCTC = dr["DDES_RUCTC"].ToString(),
                                                  DDES_TRANS = dr["DDES_TRANS"].ToString(),
                                                  DDES_TRAN2 = dr["DDES_TRAN2"].ToString(),
                                                  DDES_OBSER = dr["DDES_OBSER"].ToString(),
                                                  DDES_NOMTC = dr["DDES_NOMTC"].ToString(),
                                                  DDES_NGUIA = dr["DDES_NGUIA"].ToString(),
                                                  DDES_NRLIQ = dr["DDES_NRLIQ"].ToString(),
                                                  DDES_LIMPR = dr["DDES_LIMPR"].ToString(),
                                                  DDES_EMPRE = dr["DDES_EMPRE"].ToString(),
                                                  DDES_CANAL = dr["DDES_CANAL"].ToString(),
                                                  DDES_CADEN = dr["DDES_CADEN"].ToString(),
                                                  DDES_SECCI = dr["DDES_SECCI"].ToString(),
                                                  DDES_FTX = dr["DDES_FTX"].ToString(),
                                                  DDES_FTXTD= dr["DDES_FTXTD"].ToString(),
                                              }).ToList(); 
                        }
                    }

                }

            }
            catch (Exception exc)
            {
                throw;
                //error = exc.Message;
                _lista_scdddes = null;
            }
            return _lista_scdddes;
        }
        #region<METODOS PARA LAS PRESCRIPCIONES>
        private DataTable get_scddgud(string codalm, string nroguia, string _path, ref string error, string ruta_scccgud)
        {
            DataTable SCDDGUD = null;
            //string sqlquery_fvdespd = "SELECT DESD_TIPO,DESD_GUDIS,DESD_NDESP,DESD_ALMAC,DESD_ARTIC,DESD_CALID," +
            //                           "DESD_ME00,DESD_ME01,DESD_ME02,DESD_ME03,DESD_ME04,DESD_ME05,DESD_ME06,DESD_ME07,DESD_ME08," + 
            //                           "DESD_ME09,DESD_ME10,DESD_ME11,DESD_CLASE,DESD_MERC,DESD_CATEG,DESD_SUBCA," + 
            //                           "DESD_MARCA,DESD_MERC3,DESD_CATE3,DESD_SUBC3,DESD_MARC3,DESD_CNDME," + 
            //                           "DESD_EMPRE,DESD_SECCI,DESD_CANAL," +
            //                           "DESD_CADEN,DESD_GGUIA,DESD_ESTAD,DESD_PRVTA,DESD_COSTO FROM FVDESPD WHERE DESD_GUDIS='" + nroguia + "'" +
            //                           " AND DESD_ALMAC='" + codalm + "'";
            string sqlquery_fvdespd = "SELECT dgud_gudis,dgud_artic,dgud_calid,dgud_prvta,dgud_costo,dgud_codpp,dgud_cpack," + 
                                      "dgud_ppack,dgud_touni,dgud_med00,dgud_med01,dgud_med02,dgud_med03,dgud_med04," + 
	                                  "dgud_med05,dgud_med06,dgud_med07,dgud_med08,dgud_med09,dgud_med10,dgud_med11," +
	                                  "dgud_merc,dgud_clase,dgud_categ,dgud_subca,dgud_marca,dgud_merc3,dgud_cate3," + 
	                                  "dgud_subc3,dgud_marc3,dgud_rmed,dgud_condm,dgud_orige,dgud_u_med," +
                                      "dgud_log FROM SCDDGUD WHERE  dgud_gudis IN (SELECT cgud_gudis FROM " + ruta_scccgud + "/SCCCGUD WHERE cgud_femis>=CTOD('" + fecha_despacho.ToString("MM/dd/yy") + "')  AND EMPTY(FLAG_XSTOR))";
            try
            {
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_fvdespd, cn))
                    {
                        cmd.CommandTimeout = 0;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            SCDDGUD = new DataTable();
                            da.Fill(SCDDGUD);
                            SCDDGUD.TableName = "SCDDGUD";
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
                SCDDGUD = null;
            }
            return SCDDGUD;
        }
        private void edit_list_scccgud(string cod_alm, List<string> listGuia, string _path, ref string _error_ws)
        {

            string strListGuia = "";
            int limite = 20;
            int contador = 0;
            try
            {
                foreach (string strguia in listGuia)
                {
                    contador++;

                    strListGuia += "'" + strguia + "',";

                    if (contador == limite)
                    {

                        strListGuia = strListGuia.TrimEnd(',');

                        string sqlquery = "UPDATE scccgud SET flag_xstor='X' WHERE CGUD_GUDIS in (" + strListGuia + ")";

                        strListGuia = "";
                        contador = 0;

                        String error_cursor = "";

                        update_list_scccgud(sqlquery, _path, ref error_cursor);



                        if (error_cursor.Length > 0)
                        {

                            string _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " Error de update registros==>" + error_cursor;
                            TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                            tw.WriteLine(_hora);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();


                        }
                        else
                        {
                            string _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " Update registros correctamente..";
                            TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                            tw.WriteLine(_hora);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();
                        }

                    }


                }

                if (contador > 0)
                {
                    strListGuia = strListGuia.TrimEnd(',');

                    //string sqlquery = "UPDATE SCDDDES SET DDES_FTXTD='X' WHERE DDES_ALMAC='" + cod_alm + "' AND DDES_GUIRE in (" + strListGuia + ")";
                    //string sqlquery = "UPDATE SCDDDES SET DDES_FTXTD='X' WHERE DDES_ALMAC + DDES_GUIRE in (" + strListGuia + ")";
                    string sqlquery = "UPDATE scccgud SET flag_xstor='X' WHERE CGUD_GUDIS in (" + strListGuia + ")";

                    strListGuia = "";
                    contador = 0;

                    String error_cursor = "";

                    update_list_scccgud(sqlquery, _path, ref error_cursor);
                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
            }
        }
        private List<SCCCGUD> get_scccgud(string _path, ref string error,ref Boolean fila_existe)
        {
            /*fecha para traer los pedido cerrados desde una fecha*/
            List<SCCCGUD> _lista_scccgud = null;
            String sqlquery_scdddes = "SELECT cgud_gudis,cgud_tndcl,cgud_calid,cgud_empre,cgud_canal,cgud_caden " +
                                      ",cgud_almac" + 
                                      ",cgud_secci" + 
                                      ",cgud_estad" + 
                                      ",cgud_ftnda" + 
                                      ",cgud_nomtc" + 
                                      ",cgud_ructc" +

                                      ",STR(cgud_vorca) AS cgud_vorca" +
                                      ",STR(cgud_vornc) AS cgud_vornc" + 
                                      ",cgud_unoca" + 
                                      ",cgud_unonc" + 
                                      ",cgud_uneca" + 
                                      ",cgud_unenc" +

                                      ", cgud_ftx, cgud_dspch, cgud_ssd, cgud_semre, cgud_anore, cgud_frect " +
                                      ",cgud_fecre" + 
                                      ",cgud_scal" +
                                      ",STR(cgud_scalm) AS cgud_scalm" +
                                      ",STR(cgud_sacc) AS cgud_sacc " +
                                      ",STR(cgud_saccm) AS cgud_saccm" +
                                      ",STR(cgud_ccal) AS cgud_ccal" +
                                      ", cgud_ccalm, cgud_cacc, cgud_caccm, cgud_caj, cgud_cajm, cgud_ftda, cgud_subgr " +
                                      ", cgud_ftxtd, cgud_ftxan, cgud_ano, cgud_seman, cgud_user, cgud_femis, cgud_hemis " +
                                      ", cgud_conce, cgud_flsf, cgud_aorig, cgud_pedid, cgud_deliv, log_ultmod " +
                                      "FROM SCCCGUD WHERE cgud_femis>=CTOD('" + fecha_despacho.ToString("MM/dd/yy") + "')  AND EMPTY(FLAG_XSTOR)  " ;
            try
            {
                //Util dd = new Util();
                //dd.get_location_dbf();

                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_vfpoledb_1(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_scdddes, cn))
                    {
                        cmd.CommandTimeout = 0;
                        //cmd.Parameters.Add("DATE", OleDbType.Date).Value = fecha_despacho;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            _lista_scccgud = new List<SCCCGUD>();
                            _lista_scccgud = (from DataRow dr in dt.Rows
                                              select new SCCCGUD()
                                              {
                                                  cgud_gudis = dr["cgud_gudis"].ToString(),
                                                  cgud_tndcl = dr["cgud_tndcl"].ToString(),
                                                  cgud_calid = dr["cgud_calid"].ToString(),
                                                  cgud_empre = dr["cgud_empre"].ToString(),
                                                  cgud_canal = dr["cgud_canal"].ToString(),
                                                  cgud_caden = dr["cgud_caden"].ToString(),
                                                  cgud_almac = dr["cgud_almac"].ToString(),
                                                  cgud_secci = dr["cgud_secci"].ToString(),
                                                  cgud_estad = dr["cgud_estad"].ToString(),
                                                  cgud_ftnda = dr["cgud_ftnda"].ToString(),
                                                  cgud_nomtc = dr["cgud_nomtc"].ToString(),
                                                  cgud_ructc = dr["cgud_ructc"].ToString(),
                                                  cgud_vorca =Convert.ToDecimal(dr["cgud_vorca"]),
                                                  cgud_vornc =Convert.ToDecimal(dr["cgud_vornc"]),
                                                  cgud_unoca =Convert.ToDecimal(dr["cgud_unoca"]),
                                                  cgud_unonc = Convert.ToDecimal(dr["cgud_unonc"]),
                                                  cgud_uneca = Convert.ToDecimal(dr["cgud_uneca"]),
                                                  cgud_unenc = Convert.ToDecimal(dr["cgud_unenc"]),
                                                  cgud_ftx = dr["cgud_ftx"].ToString(),
                                                  cgud_dspch = dr["cgud_dspch"].ToString(),
                                                  cgud_ssd = dr["cgud_ssd"].ToString(),
                                                  cgud_semre = dr["cgud_semre"].ToString(),
                                                  cgud_anore = dr["cgud_anore"].ToString(),
                                                  cgud_frect =Convert.ToDateTime(dr["cgud_frect"]),
                                                  cgud_fecre = Convert.ToDateTime(dr["cgud_fecre"]),
                                                  cgud_scal = Convert.ToDecimal(dr["cgud_scal"]),
                                                  cgud_scalm = Convert.ToDecimal(dr["cgud_scalm"]),
                                                  cgud_sacc = Convert.ToDecimal(dr["cgud_sacc"]),
                                                  cgud_saccm = Convert.ToDecimal(dr["cgud_saccm"]),
                                                  cgud_ccal = Convert.ToDecimal(dr["cgud_ccal"]),
                                                  cgud_ccalm = Convert.ToDecimal(dr["cgud_ccalm"]),
                                                  cgud_cacc = Convert.ToDecimal(dr["cgud_cacc"]),
                                                  cgud_caccm = Convert.ToDecimal(dr["cgud_caccm"]),
                                                  cgud_caj = Convert.ToDecimal(dr["cgud_caj"]),
                                                  cgud_cajm = Convert.ToDecimal(dr["cgud_cajm"]),
                                                  cgud_ftda = dr["cgud_ftda"].ToString(),
                                                  cgud_subgr = dr["cgud_subgr"].ToString(),
                                                  cgud_ftxtd = dr["cgud_ftxtd"].ToString(),
                                                  cgud_ftxan = dr["cgud_ftxan"].ToString(),
                                                  cgud_ano = dr["cgud_ano"].ToString(),
                                                  cgud_seman = dr["cgud_seman"].ToString(),
                                                  cgud_user = dr["cgud_user"].ToString(),
                                                  cgud_femis = Convert.ToDateTime(dr["cgud_femis"]),
                                                  cgud_hemis = dr["cgud_hemis"].ToString(),
                                                  cgud_conce = dr["cgud_conce"].ToString(),
                                                  cgud_flsf = dr["cgud_flsf"].ToString(),
                                                  cgud_aorig = dr["cgud_aorig"].ToString(),
                                                  cgud_pedid = dr["cgud_pedid"].ToString(),
                                                  cgud_deliv = dr["cgud_deliv"].ToString(),
                                                  log_ultmod = dr["log_ultmod"].ToString(),                                               
                                              }).ToList();
                        }

                        if (_lista_scccgud.Count == 0)
                        {
                            fila_existe = false;
                        }
                        else
                        {
                            fila_existe = true;                           
                        }
                    }

                }

            }
            catch (Exception exc)
            {
                throw;
                //error = exc.Message;
                _lista_scccgud = null;
            }
            return _lista_scccgud;
        }
        #endregion

        private List<BataTransac.Ent_Scdremb> get_scdremb(string _path, ref string error)
        {
            List<BataTransac.Ent_Scdremb> _lista_scdremb = null;

            string sqlquery_scdremb = "SELECT  remb_guiac,remb_artic,remb_calid,remb_medid,remb_corra,remb_canti," +
                                      "remb_almac,remb_cpack,remb_condm,remb_rmed,remb_u_med,remb_categ,remb_subca," +
                                      "remb_clase,remb_prvta,remb_costo,remb_talpr,remb_plaoc,remb_femba,remb_hemba," +
                                      "remb_empre,remb_secci,remb_user,remb_aassd,remb_flag,remb_secue,remb_estad," +
                                      "remb_log,remb_ftx FROM SCDREMB WHERE (LEN(remb_guiac)>0 or  NOT remb_guiac is null) AND !EMPTY(remb_guiac) order by remb_guiac";
            try
            {

                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_scdremb, cn))
                    {
                        cmd.CommandTimeout = 0;

                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            _lista_scdremb = new List<BataTransac.Ent_Scdremb>();
                            _lista_scdremb = (from DataRow dr in dt.Rows
                                              
                                              select new BataTransac.Ent_Scdremb()
                                              {
                                                  remb_guiac = dr["remb_guiac"].ToString(),
                                                  remb_artic = dr["remb_artic"].ToString(),
                                                  remb_calid = dr["remb_calid"].ToString(),
                                                  remb_medid = dr["remb_medid"].ToString(),
                                                  remb_corra = dr["remb_corra"].ToString(),
                                                  remb_canti = Convert.ToDecimal(dr["remb_canti"]),
                                                  remb_almac = dr["remb_almac"].ToString(),
                                                  remb_cpack = dr["remb_cpack"].ToString(),
                                                  remb_condm = dr["remb_condm"].ToString(),
                                                  remb_rmed = dr["remb_rmed"].ToString(),
                                                  remb_u_med = dr["remb_u_med"].ToString(),
                                                  remb_categ = dr["remb_categ"].ToString(),
                                                  remb_subca = dr["remb_subca"].ToString(),
                                                  remb_clase = dr["remb_clase"].ToString(),
                                                  remb_prvta = Convert.ToDecimal(dr["remb_prvta"]),
                                                  remb_costo = Convert.ToDecimal(dr["remb_costo"]),
                                                  remb_talpr = dr["remb_talpr"].ToString(),
                                                  remb_plaoc = dr["remb_plaoc"].ToString(),
                                                  remb_femba = dr["remb_femba"].ToString(),
                                                  remb_hemba = dr["remb_hemba"].ToString(),
                                                  remb_empre = dr["remb_empre"].ToString(),
                                                  remb_secci = dr["remb_secci"].ToString(),
                                                  remb_user = dr["remb_user"].ToString(),
                                                  remb_aassd = dr["remb_aassd"].ToString(),
                                                  remb_flag = dr["remb_flag"].ToString(),
                                                  remb_secue = Convert.ToDecimal(dr["remb_secue"])
                                                  //remb_estad = dr["remb_estad"].ToString(),
                                                  //remb_log = dr["remb_log"].ToString(),
                                                  //remb_ftx = dr["remb_ftx"].ToString(),

                                              }).ToList();
                        }
                    }

                }

            }
            catch (Exception exc)
            {
                error = exc.Message;
                _lista_scdremb = null;
            }
            return _lista_scdremb;
        }

        private List<BataTransac.Ent_Scactco> get_scactco(string _path, ref string error)
        {
            List<BataTransac.Ent_Scactco> _lista_scactco = null;

            string sqlquery_scactco = "SELECT Ctco_talpr,Ctco_plaoc,Ctco_artic,Ctco_calid,Ctco_plpr,Ctco_impr," +
                                         "Ctco_med00,Ctco_med01,Ctco_med02,Ctco_med03,Ctco_med04,Ctco_med05," +
                                         "Ctco_med06,Ctco_med07,Ctco_med08,Ctco_med09,Ctco_med10,Ctco_med11," +
                                         "Ctco_orige,Ctco_fecha,Ctco_usern ,Ctco_empre,Ctco_ftx,Ctco_txpos FROM " +
                                         "SCACTCO order by Ctco_fecha";
           try
            {
            
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_scactco, cn))
                    {
                        cmd.CommandTimeout = 0;
                      
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            _lista_scactco = new List<BataTransac.Ent_Scactco>();
                            _lista_scactco = (from DataRow dr in dt.Rows
                                              select new BataTransac.Ent_Scactco()
                                              {
                                                  ctco_talpr = dr["Ctco_talpr"].ToString(),
                                                  ctco_plaoc = dr["Ctco_plaoc"].ToString(),
                                                  ctco_artic = dr["Ctco_artic"].ToString(),
                                                  ctco_calid = dr["Ctco_calid"].ToString(),
                                                  ctco_plpr = dr["Ctco_plpr"].ToString(),
                                                  ctco_impr = dr["Ctco_impr"].ToString(),
                                                  ctco_med00 = dr["Ctco_med00"].ToString(),
                                                  ctco_med01 = dr["Ctco_med01"].ToString(),
                                                  ctco_med02 = dr["Ctco_med02"].ToString(),
                                                  ctco_med03 = dr["Ctco_med03"].ToString(),
                                                  ctco_med04 = dr["Ctco_med04"].ToString(),
                                                  ctco_med05 = dr["Ctco_med05"].ToString(),
                                                  ctco_med06 = dr["Ctco_med06"].ToString(),
                                                  ctco_med07 = dr["Ctco_med07"].ToString(),
                                                  ctco_med08 = dr["Ctco_med08"].ToString(),
                                                  ctco_med09 = dr["Ctco_med09"].ToString(),
                                                  ctco_med10 = dr["Ctco_med10"].ToString(),
                                                  ctco_med11 = dr["Ctco_med11"].ToString(),
                                                  ctco_orige = dr["Ctco_orige"].ToString(),
                                                  ctco_fecha = DateTime.Parse(dr["Ctco_fecha"].ToString()),
                                                  ctco_usern = dr["Ctco_usern"].ToString(),
                                                  ctco_empre = dr["Ctco_empre"].ToString(),
                                                  ctco_ftx = dr["Ctco_ftx"].ToString(),
                                                  ctco_txpos = dr["Ctco_txpos"].ToString(),
                                              }).ToList();
                        }
                    }

                }

            }
            catch (Exception exc)
            {
                error = exc.Message;
                _lista_scactco = null;
            }
            return _lista_scactco;
        }
        /// <summary>
        /// cebezera de guias para enviar
        /// </summary>
        /// <param name="nroguia"></param>
        /// <returns></returns>
        private List<BataTransac.Ent_Fvdespc> get_fvdespc(string codalm, string nroguia,string _path, ref string error,ref Boolean fila_existe,string ruta_scdddes)
        {
            List<BataTransac.Ent_Fvdespc> fvdespc = null;
            //String sqlquery_fvdespc = "SELECT DESC_ALMAC,DESC_GUDIS,DESC_NDESP,DESC_TDES,DESC_FECHA,DESC_FDESP," +
            //                          "DESC_ESTAD,DESC_TIPO,DESC_TORI,DESC_FEMI,DESC_SEMI,DESC_FTRA,DESC_NUME," +
            //                          "DESC_CONCE,DESC_NMOVC,DESC_EMPRE,DESC_SECCI,DESC_CANAL,DESC_CADEN,DESC_FTX," +
            //                          "DESC_TXPOS,DESC_UNCA,DESC_UNNC,DESC_CAJA,DESC_VACA,DESC_VANC,DESC_VCAJ " +
            //                          "FROM FVDESPC WHERE DESC_GUDIS='" + nroguia + "' AND DESC_ALMAC='" + codalm +"'";
            String sqlquery_fvdespc = "SELECT DESC_ALMAC,DESC_GUDIS,DESC_NDESP,DESC_TDES,DESC_FECHA,DESC_FDESP," +
                          "DESC_ESTAD,DESC_TIPO,DESC_TORI,DESC_FEMI,DESC_SEMI,DESC_FTRA,DESC_NUME," +
                          "DESC_CONCE,DESC_NMOVC,DESC_EMPRE,DESC_SECCI,DESC_CANAL,DESC_CADEN,DESC_FTX," +
                          "DESC_TXPOS,DESC_UNCA,DESC_UNNC,DESC_CAJA,DESC_VACA,DESC_VANC,DESC_VCAJ " +
                          "FROM FVDESPC WHERE   DESC_GUDIS + DESC_ALMAC IN (SELECT DDES_GUIRE + DDES_ALMAC FROM " + ruta_scdddes + "/SCDDDES WHERE DDES_FDESP>=CTOD('" + fecha_despacho.ToString("MM/dd/yy") + "') and DDES_TIPO='DES' and DDES_ESTAD<>'A' AND EMPTY(DDES_FTXTD) AND (NOT EMPTY(DDES_CADEN))" + get_query_alm_ecu() + ")";
            try
            {
                //_path = @"D:\FVT\SISTEMAS";
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_fvdespc, cn))
                    {
                        cmd.CommandTimeout = 0;                       
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            //List<BataTransac.Ent_Fvdespc> fvdespc = new List<BataTransac.Ent_Fvdespc>();
                            List<BataTransac.Ent_Fvdespc> list_fvdespc = (from DataRow dr in dt.Rows
                                       select new BataTransac.Ent_Fvdespc
                                       {
                                           DESC_ALMAC = dr["DESC_ALMAC"].ToString(),
                                           DESC_GUDIS = dr["DESC_GUDIS"].ToString(),
                                           DESC_NDESP = dr["DESC_NDESP"].ToString(),
                                           DESC_TDES = dr["DESC_TDES"].ToString(),
                                          // DESC_FECHA = Convert.ToDateTime(dr["DESC_FECHA"]),se comenta pq viene vacio
                                          // DESC_FDESP = Convert.ToDateTime(dr["DESC_FDESP"]),se supone que es la fecha despacho/*
                                           DESC_ESTAD = dr["DESC_ESTAD"].ToString(),
                                           DESC_TIPO = dr["DESC_TIPO"].ToString(),
                                           DESC_TORI = dr["DESC_TORI"].ToString(),
                                           DESC_FEMI = Convert.ToDateTime(dr["DESC_FEMI"]),
                                           DESC_SEMI = dr["DESC_SEMI"].ToString(),
                                          // DESC_FTRA = Convert.ToDateTime(dr["DESC_FTRA"]),
                                           DESC_NUME = dr["DESC_NUME"].ToString(),
                                           DESC_CONCE = dr["DESC_CONCE"].ToString(),
                                           DESC_NMOVC = dr["DESC_NMOVC"].ToString(),
                                           DESC_EMPRE = dr["DESC_EMPRE"].ToString(),
                                           DESC_SECCI = dr["DESC_SECCI"].ToString(),
                                           DESC_CANAL = dr["DESC_CANAL"].ToString(),
                                           DESC_CADEN = dr["DESC_CADEN"].ToString(),
                                           DESC_FTX = dr["DESC_FTX"].ToString(),
                                           DESC_TXPOS = dr["DESC_TXPOS"].ToString(),
                                           DESC_UNCA =Convert.ToDecimal(dr["DESC_UNCA"]),
                                           DESC_UNNC =Convert.ToDecimal(dr["DESC_UNNC"]),
                                           DESC_CAJA =Convert.ToDecimal(dr["DESC_CAJA"]),
                                           DESC_VACA =Convert.ToDecimal(dr["DESC_VACA"]),
                                           DESC_VANC =Convert.ToDecimal(dr["DESC_VANC"]),
                                           DESC_VCAJ =Convert.ToDecimal(dr["DESC_VCAJ"]),
                                       }).ToList();


                            if (list_fvdespc.Count == 0)
                            {
                                fila_existe = false;
                            }
                            else
                            {
                                fila_existe = true;
                                fvdespc = list_fvdespc;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {

                string _hora = DateTime.Now.ToLongTimeString() + exc.Message;
                TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                error = exc.Message;
                fvdespc=null;
            }
            return fvdespc;
        }
        /// <summary>
        /// Detalle de la guia
        /// </summary>
        /// <param name="nroguia"></param>
        /// <returns></returns>
        private DataTable get_fvdespd(string codalm,string nroguia, string _path, ref string error, string ruta_scdddes)
        {
            DataTable fvdespd = null;
            //string sqlquery_fvdespd = "SELECT DESD_TIPO,DESD_GUDIS,DESD_NDESP,DESD_ALMAC,DESD_ARTIC,DESD_CALID," +
            //                           "DESD_ME00,DESD_ME01,DESD_ME02,DESD_ME03,DESD_ME04,DESD_ME05,DESD_ME06,DESD_ME07,DESD_ME08," + 
            //                           "DESD_ME09,DESD_ME10,DESD_ME11,DESD_CLASE,DESD_MERC,DESD_CATEG,DESD_SUBCA," + 
            //                           "DESD_MARCA,DESD_MERC3,DESD_CATE3,DESD_SUBC3,DESD_MARC3,DESD_CNDME," + 
            //                           "DESD_EMPRE,DESD_SECCI,DESD_CANAL," +
            //                           "DESD_CADEN,DESD_GGUIA,DESD_ESTAD,DESD_PRVTA,DESD_COSTO FROM FVDESPD WHERE DESD_GUDIS='" + nroguia + "'" +
            //                           " AND DESD_ALMAC='" + codalm + "'";
            string sqlquery_fvdespd = "SELECT DESD_TIPO,DESD_GUDIS,DESD_NDESP,DESD_ALMAC,DESD_ARTIC,DESD_CALID," +
                                      "DESD_ME00,DESD_ME01,DESD_ME02,DESD_ME03,DESD_ME04,DESD_ME05,DESD_ME06,DESD_ME07,DESD_ME08," +
                                      "DESD_ME09,DESD_ME10,DESD_ME11,DESD_CLASE,DESD_MERC,DESD_CATEG,DESD_SUBCA," +
                                      "DESD_MARCA,DESD_MERC3,DESD_CATE3,DESD_SUBC3,DESD_MARC3,DESD_CNDME," +
                                      "DESD_EMPRE,DESD_SECCI,DESD_CANAL," +
                                      "DESD_CADEN,DESD_GGUIA,DESD_ESTAD,DESD_PRVTA,DESD_COSTO FROM FVDESPD WHERE  DESD_GUDIS + DESD_ALMAC IN (SELECT DDES_GUIRE + DDES_ALMAC FROM " + ruta_scdddes + "/SCDDDES WHERE DDES_FDESP>=CTOD('" + fecha_despacho.ToString("MM/dd/yy") + "') and DDES_TIPO='DES' and DDES_ESTAD<>'A' AND EMPTY(DDES_FTXTD) AND (NOT EMPTY(DDES_CADEN))" + get_query_alm_ecu() + ")";
            try
            {
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_fvdespd, cn))
                    {
                        cmd.CommandTimeout = 0;                     
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            fvdespd = new DataTable();
                            da.Fill(fvdespd);
                            fvdespd.TableName = "fvdespd";                            
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
                fvdespd = null;                
            }
            return fvdespd;
        }


       
        public string get_query_alm_ecu()
        {
            string sqlquery = "";
            try
            {
                if (valida_file_ecu())
                {
                    BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                    header_user.Username = ConexionWS.user;
                    header_user.Password = ConexionWS.password;

                    BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();

                    var lista_almac = bata_trans.ws_lista_alma_Ecu(header_user);

                    if (lista_almac!=null)
                    {
                        
                        Int32 cur_alm = 1;
                        foreach (var item in lista_almac)
                        {
                            if (cur_alm==1)
                            {
                                sqlquery = " and DDES_ALMAC in ('" + item.alma_ecu+ "'";
                            }
                            else
                            {
                                sqlquery += ",'"+item.alma_ecu + "'" ;
                            }

                            if (lista_almac.Count()==cur_alm)
                            {
                                sqlquery += ")";
                            }
                            

                            cur_alm += 1;
                        }
                    }
                }
            }
            catch 
            {
                sqlquery = "";                
            }
            return sqlquery;
        }
        public Boolean valida_file_ecu()
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

        private void forzar_delete_nopos(string ruta_nopos)
        {
            try
            {
                if (File.Exists(@ruta_nopos))
                {
                    DateTime fcreacion = File.GetCreationTime(@ruta_nopos);

                    DateTime factual = DateTime.Now;

                    TimeSpan ts = factual - fcreacion;

                    Int32 max_num = 3;
                    
                    if (ts.Hours>= max_num)
                    {
                        File.Delete(@ruta_nopos);
                    }
                }
            }
            catch
            {

               
            }
        }

        #region<ENVIO DE STOCK DE ALMACEN>
        public void eje_envio_stk_almacen(ref string _error_ws)
        {
            string _error_transac = "";
            string cod_val = "";
            //List<BataTransac.Ent_Scdddes> _lista_guiasC = null;

            List<BataTransac.Ent_PathDBF> listar_location_dbf = null;
            DataTable dtStock = null;
            TextWriter tw = null;
            try
            {
                /*segundos para ejecutar*/
                //_espera_ejecuta(20);
                /***********************/

                #region<CAPTURAR EL PATH DE LOS DBF>
                Util locationdbf = new Util();
                listar_location_dbf = locationdbf.get_location_dbf(ref _error_ws);
                #endregion

                if (listar_location_dbf == null) return;

                /*VALIDACION DE EJECUCION */
                #region<VALIDA ARCHIVO SI EXISTE PARA NO REALIZAR NINGUNA ACCCION POR SEGURIDAD DE REINDEXACION DEL FOX>
                string name_txt = "NOPOS";
                var _locatio_noservicio = listar_location_dbf.Where(x => x.rutloc_namedbf == name_txt).FirstOrDefault();

                //Boolean valida_exists_txt = false;
                string ruta_validacion = "";
                if (_locatio_noservicio != null)
                {

                    ruta_validacion = _locatio_noservicio.rutloc_location + "\\" + _locatio_noservicio.rutloc_namedbf + ".txt";
                    /* if (File.Exists(@ruta_validacion)) return;*/ //valida_exists_txt = true;

                }
                //ruta_validacion = @"D:\FVT\SISTEMAS\NOPOS.TXT";
                forzar_delete_nopos(ruta_validacion);
                #endregion

                #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                if (valida_file_ecu())
                {
                    NetworkShare.ConnectToShare(_locatio_noservicio.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                }
                #endregion

                //if (valida_exists_txt) return;



                string name_dbf = "SCACSAL";
                var _locatio_scdddes = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();

                //string _error = "";
                /*ya no entra a consultar*/
                if (File.Exists(@ruta_validacion)) return;
                string _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>INGRESO PASO1 ALMACEN";
                tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                /**/


                #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                if (valida_file_ecu())
                {
                    NetworkShare.ConnectToShare(_locatio_noservicio.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                }
                else
                {
                    NetworkShare.ConnectToShare(_locatio_scdddes.rutloc_location, @".\Tareas", "tareas");
                }
                #endregion

                #region<ENVIO DE STOCK DE ALMACEN>
                /*user y password*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
                header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

                BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();
                List<BataTransac.Ent_Stock_Almacen> result = new List<BataTransac.Ent_Stock_Almacen>();
                //get_scdddes
                    
                using (System.Data.OleDb.OleDbConnection dbConn = new System.Data.OleDb.OleDbConnection(ConexionDBF._conexion_oledb(_locatio_scdddes.rutloc_location)))
                {
                    try
                    {

                        //string sql_sem_ano = "SELECT MAX(csal_ano),MAX(csal_seman) FROM SCACSAL";

                        //System.Data.OleDb.OleDbCommand dat_cierre_sem = new System.Data.OleDb.OleDbCommand(sql_sem_ano, dbConn);
                        //System.Data.OleDb.OleDbDataAdapter ada_cierre_sem = new System.Data.OleDb.OleDbDataAdapter(dat_cierre_sem);
                        //DataTable tabla_sem = new DataTable();
                        //if (File.Exists(@ruta_validacion)) return;

                        //_hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>INGRESO CAPTURA MAX ANO Y SEM SCACSAL ";
                        //tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                        //tw.WriteLine(_hora);
                        //tw.Flush();
                        //tw.Close();
                        //tw.Dispose();

                        //ada_cierre_sem.Fill(tabla_sem);

                        //if (tabla_sem != null)
                        //{
                        //    if (tabla_sem.Rows.Count>0)
                        //    {

                        //    }
                        //}

                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>FIN CAPTURA MAX ANO Y SEM SCACSAL ";
                        tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();



                        //-- Obtenemos datos abierto o ultimo cerrado
                        string sql_cierre = "SELECT * FROM SCACSAL WHERE csal_ano= (SELECT MAX(csal_ano) FROM SCACSAL) AND csal_seman= (SELECT MAX(csal_seman) FROM SCACSAL WHERE csal_ano= (SELECT MAX(csal_ano) FROM SCACSAL))";
                        System.Data.OleDb.OleDbCommand dat_cierre = new System.Data.OleDb.OleDbCommand(sql_cierre, dbConn);
                        System.Data.OleDb.OleDbDataAdapter ada_cierre = new System.Data.OleDb.OleDbDataAdapter(dat_cierre);
                        DataTable tabla = new DataTable();
                        if (File.Exists(@ruta_validacion)) return;

                         _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>INGRESO CONSULTA TABLA SCACSAL ";
                        tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();

                        ada_cierre.Fill(tabla);

                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>TERMINANDO DE CONSULTAR TABLA SCACSAL ";
                        tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();

                        dtStock = new DataTable();



                        //---------  ceracion de tabla ----------//
                        dtStock.Columns.Add("Csal_almac", typeof(string));
                        dtStock.Columns.Add("Csal_cd", typeof(string));
                        dtStock.Columns.Add("Csal_artic", typeof(string));
                        dtStock.Columns.Add("Csal_calid", typeof(string));
                        dtStock.Columns.Add("Csal_codRgmd", typeof(string));
                        dtStock.Columns.Add("Csal_MedPer", typeof(string));
                        dtStock.Columns.Add("Csal_MedLat", typeof(string));
                        dtStock.Columns.Add("Csal_cantidad", typeof(string));
                        dtStock.Columns.Add("Csal_secci", typeof(string));
                        dtStock.Columns.Add("Csal_ano", typeof(string));
                        dtStock.Columns.Add("Csal_seman", typeof(string));

                        string centro_dis = "50001";

                        for (Int32 i = 0; i < tabla.Rows.Count; i++)
                        {
                            Int32 Csal_med00 = 0, Csal_med01 = 0, Csal_med02=0, Csal_med03=0, Csal_med04=0, Csal_med05=0, Csal_med06=0,
                            Csal_med07 = 0, Csal_med08 = 0, Csal_med09 = 0, Csal_med10 = 0, Csal_med11 = 0;

                            //Csal_med00
                            /*agregando variables de tipo int32*/
                            Int32.TryParse(tabla.Rows[i]["Csal_med00"].ToString(),out  Csal_med00);
                            Int32.TryParse(tabla.Rows[i]["Csal_med01"].ToString(), out Csal_med01);
                            Int32.TryParse(tabla.Rows[i]["Csal_med02"].ToString(), out Csal_med02);
                            Int32.TryParse(tabla.Rows[i]["Csal_med03"].ToString(), out Csal_med03);
                            Int32.TryParse(tabla.Rows[i]["Csal_med04"].ToString(), out Csal_med04);
                            Int32.TryParse(tabla.Rows[i]["Csal_med05"].ToString(), out Csal_med05);
                            Int32.TryParse(tabla.Rows[i]["Csal_med06"].ToString(), out Csal_med06);
                            Int32.TryParse(tabla.Rows[i]["Csal_med07"].ToString(), out Csal_med07);
                            Int32.TryParse(tabla.Rows[i]["Csal_med08"].ToString(), out Csal_med08);
                            Int32.TryParse(tabla.Rows[i]["Csal_med09"].ToString(), out Csal_med09);
                            Int32.TryParse(tabla.Rows[i]["Csal_med10"].ToString(), out Csal_med10);
                            Int32.TryParse(tabla.Rows[i]["Csal_med11"].ToString(), out Csal_med11);                            


                            // cod_val = tabla.Rows[i]["Csal_artic"].ToString();
                            if (Csal_med00 != 0 || Csal_med01 != 0
                                || Csal_med02 != 0 || Csal_med03 != 0
                                || Csal_med04 != 0 || Csal_med05 != 0
                                || Csal_med06 != 0 || Csal_med07 != 0
                                || Csal_med08 != 0 || Csal_med09 != 0
                                || Csal_med10 != 0 || Csal_med11 != 0)
                            {
                                if (Csal_med00 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med00;//Convert.ToInt32(tabla.Rows[i]["Csal_med00"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "00"; //nombre de columna (posicion)
                                    string Csal_MedLat = "01";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                   // dtStock.Rows.Add(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);

                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);                                    
                                }

                                if (Csal_med01 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med01;//Convert.ToInt32(tabla.Rows[i]["Csal_med01"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "01"; //nombre de columna (posicion)
                                    string Csal_MedLat = "02";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);

                                }

                                if (Csal_med02 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med02; ;//Convert.ToInt32(tabla.Rows[i]["Csal_med02"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "02"; //nombre de columna (posicion)
                                    string Csal_MedLat = "03";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }

                                if (Csal_med03 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med03;//Convert.ToInt32(tabla.Rows[i]["Csal_med03"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "03"; //nombre de columna (posicion)
                                    string Csal_MedLat = "04";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }

                                if (Csal_med04 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med04;//Convert.ToInt32(tabla.Rows[i]["Csal_med04"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "04"; //nombre de columna (posicion)
                                    string Csal_MedLat = "05";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }

                                if (Csal_med05 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med05;//Convert.ToInt32(tabla.Rows[i]["Csal_med05"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "05"; //nombre de columna (posicion)
                                    string Csal_MedLat = "06";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }
                                //dtStock.Rows.Add();

                                if (Csal_med06 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med06;//Convert.ToInt32(tabla.Rows[i]["Csal_med06"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "06"; //nombre de columna (posicion)
                                    string Csal_MedLat = "07";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }
                                if (Csal_med07 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med07;//Convert.ToInt32(tabla.Rows[i]["Csal_med07"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "07"; //nombre de columna (posicion)
                                    string Csal_MedLat = "08";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }

                                if (Csal_med08 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med08;//Convert.ToInt32(tabla.Rows[i]["Csal_med08"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "08"; //nombre de columna (posicion)
                                    string Csal_MedLat = "09";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }

                                if (Csal_med09 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med09;//Convert.ToInt32(tabla.Rows[i]["Csal_med09"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "09"; //nombre de columna (posicion)
                                    string Csal_MedLat = "10";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }

                                if (Csal_med10 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med10;//Convert.ToInt32(tabla.Rows[i]["Csal_med10"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "10"; //nombre de columna (posicion)
                                    string Csal_MedLat = "11";

                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }

                                if (Csal_med11 != 0)
                                {
                                    string Csal_secci = tabla.Rows[i]["Csal_secci"].ToString();
                                    string Csal_ano = tabla.Rows[i]["Csal_ano"].ToString();
                                    string Csal_seman = tabla.Rows[i]["Csal_seman"].ToString();
                                    string Csal_alm = tabla.Rows[i]["Csal_almac"].ToString();
                                    string Csal_artic = tabla.Rows[i]["Csal_artic"].ToString();
                                    string Csal_calid = tabla.Rows[i]["Csal_calid"].ToString();

                                    Int32 Csal_cantidad = Csal_med11;//Convert.ToInt32(tabla.Rows[i]["Csal_med11"]);// cantidad o valor de la posicion
                                    string Csal_codRgmd = tabla.Rows[i]["Csal_rmed"].ToString(); //
                                    string Csal_MedPer = "11"; //nombre de columna (posicion)
                                    string Csal_MedLat = "12";


                                    //FuncionInsertar(Csal_secci, Csal_ano, Csal_seman, Csal_alm, Csal_artic, Csal_calid, Csal_cantidad, Csal_codRgmd, Csal_MedPer, Csal_MedLat);
                                    dtStock.Rows.Add(Csal_alm, centro_dis, Csal_artic, Csal_calid, Csal_codRgmd, Csal_MedPer, Csal_MedLat, Csal_cantidad, Csal_secci, Csal_ano, Csal_seman);
                                }
                            }
                        }
                        
                        if (dtStock!=null)
                        {
                            if (dtStock.Rows.Count>0)
                            {
                                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>CAPTURAR DATOS DE DATATABLE A OBJETO DE WS ";
                                tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                                tw.WriteLine(_hora);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();
                                result = (from DataRow row in dtStock.Rows
                                          select new BataTransac.Ent_Stock_Almacen()
                                          {
                                              cod_tda = row["Csal_almac"].ToString(),
                                              cd= row["Csal_cd"].ToString(),                                              
                                              art_cod = row["Csal_artic"].ToString(),
                                              art_cal = row["Csal_calid"].ToString(),
                                              cod_rgmed= row["Csal_codRgmd"].ToString(),
                                              cod_med_per= row["Csal_MedPer"].ToString(),
                                              cod_med_lat= row["Csal_MedLat"].ToString(),
                                              //art_talla ="",// row["SG_REGL"].ToString(),
                                              art_pares = Convert.ToInt32(row["Csal_cantidad"]),
                                              secci= row["Csal_secci"].ToString(),
                                              ano= row["Csal_ano"].ToString(),
                                              sem= row["Csal_seman"].ToString()
                                          }).ToList();

                                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>DATOS EN COLECCION A OBJETO DE WS ";
                                tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                                tw.WriteLine(_hora);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                if (result.Count > 0)
                                {
                                    var array = new BataTransac.Ent_Lista_Stock_Almacen();
                                    array.lista_stock = result.ToArray();

                                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>ENVIAR STOCK POR WS ";
                                    tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                                    tw.WriteLine(_hora);
                                    tw.Flush();
                                    tw.Close();
                                    tw.Dispose();

                                    BataTransac.Ent_MsgTransac msg = batatran.ws_envia_stock_almacen(header_user, array);

                                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>SALIENDO DE ENVIAR STOCK DE WS ";
                                    tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                                    tw.WriteLine(_hora);
                                    tw.Flush();
                                    tw.Close();
                                    tw.Dispose();


                                    /*Nota*/
                                    //msg.codigo = "0";
                                    //msg.descripcion = "Se actualizo correctamente";

                                    if (msg.codigo == "1")
                                        _error_ws = msg.descripcion;

                                    //msg.codigo = "1";
                                    //msg.descripcion = "descripcion de error";
                                }

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _error_ws = ex.Message;
                        //TextWriter tw1 = new StreamWriter(@"D:\POS\Transmision.net\ERROR.txt", true);
                        //tw1.WriteLine(ex.Message);
                        //tw1.Flush();
                        //tw1.Close();
                        //tw1.Dispose();
                    }

                }

              
                #endregion

              
            }
            catch (Exception exc)
            {
                //dtStock = null;
                _error_ws = exc.Message + " error de metodo " + cod_val.ToString(); ;
                _error_transac = exc.Message + " error de metodo";
                //_envio_guias = "";
            }
            //return _error_transac;
        }

        #endregion
        #region<ENVIO DE PRESCRIPCIONES>
        private void update_list_scccgud(string sqlquery, string _path, ref string _error_ws)
        {
            try
            {
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (OleDbCommand cmd = new OleDbCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception exc)
                    {
                        _error_ws = exc.Message;
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
            }
        }
        public void eje_envio_prescripcion(ref string _error_ws)
        {
            string _error_transac = "";
            List<SCCCGUD> _lista_guiasC = null;

            List<BataTransac.Ent_PathDBF> listar_location_dbf = null;

            //string fecha_hora_actual = DateTime.Now.ToShortTimeString().Substring(0, 5);
            //string fecha_hora_add = DateTime.Now.ToShortTimeString().Substring(0, 5);
            //Int32 sum_horas = 4;

            try
            {
                /*segundos para ejecutar*/
                //_espera_ejecuta(20);
                /***********************/
                //if (fecha_hora_actual == fecha_hora_add)
                //{
                //    fecha_hora_add = DateTime.Now.AddHours(sum_horas).ToShortTimeString().Substring(0, 5);
                //}

                #region<CAPTURAR EL PATH DE LOS DBF>
                Util locationdbf = new Util();
                listar_location_dbf = locationdbf.get_location_dbf(ref _error_ws);
                #endregion

                if (listar_location_dbf == null) return;

                /*VALIDACION DE EJECUCION */
                #region<VALIDA ARCHIVO SI EXISTE PARA NO REALIZAR NINGUNA ACCCION POR SEGURIDAD DE REINDEXACION DEL FOX>
                string name_txt = "NOPOS";
                var _locatio_noservicio = listar_location_dbf.Where(x => x.rutloc_namedbf == name_txt).FirstOrDefault();

                //Boolean valida_exists_txt = false;
                string ruta_validacion = "";
                if (_locatio_noservicio != null)
                {

                    ruta_validacion = _locatio_noservicio.rutloc_location + "\\" + _locatio_noservicio.rutloc_namedbf + ".txt";
                    /* if (File.Exists(@ruta_validacion)) return;*/ //valida_exists_txt = true;

                }
                //ruta_validacion = @"D:\FVT\SISTEMAS\NOPOS.TXT";
                forzar_delete_nopos(ruta_validacion);
                #endregion

                #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                if (valida_file_ecu())
                {
                    NetworkShare.ConnectToShare(_locatio_noservicio.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                }
                #endregion

                //if (valida_exists_txt) return;



                string name_dbf = "SCCCGUD";
                var _locatio_scccgud = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();

                string _error = "";
                /*ya no entra a consultar*/
                if (File.Exists(@ruta_validacion)) return;
                string _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>INGRESO PASO1";
                TextWriter tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                /**/


                #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                if (valida_file_ecu())
                {
                    NetworkShare.ConnectToShare(_locatio_noservicio.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                }
                else
                {
                    NetworkShare.ConnectToShare(_locatio_scccgud.rutloc_location, @".\Tareas", "tareas");
                }
                #endregion

                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==> saliendo del objecto NetworkShare y entrando al metodo get_scccgud";
                tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                Boolean existe_data = false;
                _lista_guiasC = get_scccgud(_locatio_scccgud.rutloc_location, ref _error,ref existe_data);


                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>get_scccgud ==>" + _error + _lista_guiasC.Count().ToString();
                tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();

                /*VERIFICAR SI HAY ERROR*/
                if (_error.Length > 0)
                {
                    _error += " ==>TABLA [scccgud]";
                    Util ws_error_transac = new Util();
                    /*si hay un error entonces 03 error de lectura dbf*/
                    ws_error_transac.control_errores_transac("03", _error, ref _error_ws);
                }
                /**/

                if (_lista_guiasC != null)
                {
                    if (_lista_guiasC.Count == 0)
                    {
                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "ningun registro encontrado,  ";
                        tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                        return;
                    }

                    #region<METODO GRUPO CONSULTA>
                    if (File.Exists(@ruta_validacion)) return;
                    /*en este caso */
                    _error = "";
                    ////name_dbf = "FVDESPC";
                  
                    ////var _location_fvdespc = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();

                    ////_hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "inicio==>acceso dbf ";
                    ////tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    ////tw.WriteLine(_hora);
                    ////tw.Flush();
                    ////tw.Close();
                    ////tw.Dispose();

                    ////#region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                    ////if (valida_file_ecu())
                    ////{
                    ////    NetworkShare.ConnectToShare(_location_fvdespc.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                    ////}
                    ////#endregion

                    ////List<BataTransac.Ent_Fvdespc> fvdespc_lista = get_fvdespc("", "", _location_fvdespc.rutloc_location, ref _error, ref existe_data, _locatio_scdddes.rutloc_location);
                    ////_hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "get_fvdespc==>>" + _error + " " + fvdespc_lista.Count().ToString();
                    ////tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    ////tw.WriteLine(_hora);
                    ////tw.Flush();
                    ////tw.Close();
                    ////tw.Dispose();

                    name_dbf = "SCDDGUD";
                    var _location_scddgud = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                    if (File.Exists(@ruta_validacion)) return;
                    #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                    if (valida_file_ecu())
                    {
                        NetworkShare.ConnectToShare(_location_scddgud.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                    }
                    #endregion

                    DataTable scddgud_lista = get_scddgud("", "", _location_scddgud.rutloc_location, ref _error, _location_scddgud.rutloc_location);
                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "get_scddgud==>>" + _error + " " + scddgud_lista.Rows.Count.ToString();
                    tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();


                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "fin==>acceso dbf ";
                    tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();

                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "fin==>acceso dbf ";
                    tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();
                    #endregion
                    /**/
                    string DESC_ALMACEN = "";
                    string rutloc_location = "";
                    List<string> listGuias = new List<string>();

                    #region<ENVIAMOS GUIAS POR WEBSERVICE>
                    foreach (SCCCGUD filaC in _lista_guiasC)
                    {

                        var fscccgud_tmp = _lista_guiasC.Where(d => d.cgud_gudis== filaC.cgud_gudis).ToList(); 

                        SCCCGUD scccgud = new SCCCGUD();
                        scccgud = fscccgud_tmp[0];


                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>get_scccgud " + filaC.cgud_gudis;
                        tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                        /*si existe data entonces entra a la condicion*/
                        if (existe_data)
                        {
                            /*validando fechas de despacho*/
                            //if (fvdespc != null)
                            //{
                            //    fvdespc.DESC_FDESP = filaC.DDES_FDESP;
                            //    fvdespc.DESC_FECHA = filaC.DDES_FECHA;
                            //    fvdespc.DESC_FTRA = filaC.DDES_FECHA;
                            //}

                            /*VERIFICAR SI HAY ERROR*/
                            if (_error.Length > 0)
                            {
                                _error += " ==>TABLA [SCCCGUD]";
                                Util ws_error_transac = new Util();
                                /*si hay un error entonces 03 error de lectura dbf*/
                                ws_error_transac.control_errores_transac("03", _error, ref _error_ws);
                            }
                            /**/

                            /*captura la cebecera de la guia*/
                            if (scccgud != null)
                            {
                                _error = "";
                                name_dbf = "SCDDGUD";



                                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "inicio==>get_SCDDGUD " + filaC.cgud_gudis;
                                tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                                tw.WriteLine(_hora);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();


                                DataTable scddgud = new DataTable();
                                scddgud = scddgud_lista.Clone();
                                DataRow[] filas_scddgud = null;
                                filas_scddgud = scddgud_lista.Select("DGUD_GUDIS='" + filaC.cgud_gudis + "'"); // get_fvdespd(filaC.DDES_ALMAC, filaC.DDES_GUIRE, _location_fvdespd.rutloc_location, ref _error);

                                foreach (DataRow fila in filas_scddgud)
                                {
                                    scddgud.ImportRow(fila);
                                }

                                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "fin==>get_scccgud " + filaC.cgud_gudis;
                                tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                                tw.WriteLine(_hora);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                /*VERIFICAR SI HAY ERROR*/
                                if (_error.Length > 0)
                                {
                                    _error += " ==>TABLA [SCDDGUD]";
                                    Util ws_error_transac = new Util();
                                    /*si hay un error entonces 03 error de lectura dbf*/
                                    ws_error_transac.control_errores_transac("03", _error, ref _error_ws);
                                }
                                /**/

                                /*verifica que el detalle de la guia tenga filas*/
                                /*si la guias tiene detalle se envia por ws*/

                                if (scddgud != null)
                                {
                                    if (scddgud.Rows.Count > 0)
                                    {
                                        /*enviar el datatable*/
                                        scccgud.dt_SCDDGUD = scddgud;

                                        SCCCGUD  scdddes = new SCCCGUD();
                                        scdddes = filaC;

                                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>inicio de envio web service " + filaC.cgud_gudis;
                                        tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                                        tw.WriteLine(_hora);
                                        tw.Flush();
                                        tw.Close();
                                        tw.Dispose();
                                        /***********************************/                                        
                                        Dat_Prescripcion envio_pres = new Dat_Prescripcion();

                                        string envio_guias_ws = envio_pres.insertar_prescripcion(scdddes);

                                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>envio web service " + filaC.cgud_gudis;
                                        tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                                        tw.WriteLine(_hora);
                                        tw.Flush();
                                        tw.Close();
                                        tw.Dispose();

                                        //si return es true entonces validamos los dbf
                                        if (envio_guias_ws.Length == 0)
                                        {
                                            name_dbf = "SCCCGUD";
                                            var _locatio_scccgud_edit = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                                            /*si es que las guias se grabaron correctamente entonces vamos a setear el valor en el dbf*/

                                            /*ya no entra a consultar*/
                                            if (File.Exists(@ruta_validacion)) return;
                                            /**/
                                            _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>inicio de update scccgud " + filaC.cgud_gudis;
                                            tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                                            tw.WriteLine(_hora);
                                            tw.Flush();
                                            tw.Close();
                                            tw.Dispose();

                                            #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                                            if (valida_file_ecu())
                                            {
                                                NetworkShare.ConnectToShare(_locatio_scccgud_edit.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                                            }
                                            #endregion

                                            listGuias.Add(scdddes.cgud_gudis);
                                            //DESC_ALMACEN = fvdespc.DESC_ALMAC;
                                            rutloc_location = _locatio_scccgud_edit.rutloc_location;

                                            //edit_scdddes(fvdespc.DESC_ALMAC, fvdespc.DESC_GUDIS, _locatio_scdddes_edit.rutloc_location,ref _error_ws);
                                            _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>fin de update scccgud " + _error_ws + "  " + filaC.cgud_gudis;
                                            tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                                            tw.WriteLine(_hora);
                                            tw.Flush();
                                            tw.Close();
                                            tw.Dispose();


                                        }
                                        else
                                        {
                                            Util ws_error_transac = new Util();
                                            _error_ws = envio_guias_ws.ToString();
                                            /*si hay un error entonces 02 error de transaction*/
                                            ws_error_transac.control_errores_transac("02", envio_guias_ws, ref _error_ws);
                                        }
                                    }
                                }

                            }
                        }
                        //}
                    }
                    #endregion

                    if (listGuias.Count > 0)
                    {

                        edit_list_scccgud(DESC_ALMACEN, listGuias, rutloc_location, ref _error_ws);
                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>fin de update list scccgud " + _error_ws;
                        tw = new StreamWriter(@"D:\ALMACEN\log_pres.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }

                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message + " error de metodo";
                _error_transac = exc.Message + " error de metodo";
                //_envio_guias = "";
            }
            //return _error_transac;
        }
        #endregion
        /// <summary>
        ///ejecutar proceso de envios de guias
        /// </summary>
        /// <returns></returns>
        public void  eje_envio_guias(ref string _error_ws)
        {
            string _error_transac ="";
            List<BataTransac.Ent_Scdddes> _lista_guiasC = null;

            List<BataTransac.Ent_PathDBF> listar_location_dbf = null;

            try
            {
                /*segundos para ejecutar*/
                //_espera_ejecuta(20);
                /***********************/

                #region<CAPTURAR EL PATH DE LOS DBF>
                Util locationdbf = new Util();
                listar_location_dbf= locationdbf.get_location_dbf(ref _error_ws);
                #endregion

                if (listar_location_dbf == null) return;

                /*VALIDACION DE EJECUCION */
                #region<VALIDA ARCHIVO SI EXISTE PARA NO REALIZAR NINGUNA ACCCION POR SEGURIDAD DE REINDEXACION DEL FOX>
                string name_txt = "NOPOS";
                var _locatio_noservicio = listar_location_dbf.Where(x => x.rutloc_namedbf == name_txt).FirstOrDefault();

                //Boolean valida_exists_txt = false;
                string ruta_validacion = "";
                if (_locatio_noservicio!=null)
                {
                   
                    ruta_validacion = _locatio_noservicio.rutloc_location + "\\" + _locatio_noservicio.rutloc_namedbf + ".txt";                   
                    /* if (File.Exists(@ruta_validacion)) return;*/ //valida_exists_txt = true;

                }
                //ruta_validacion = @"D:\FVT\SISTEMAS\NOPOS.TXT";
                forzar_delete_nopos(ruta_validacion);
                #endregion

                #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                if (valida_file_ecu())
                {
                    NetworkShare.ConnectToShare(_locatio_noservicio.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                }
                #endregion

                //if (valida_exists_txt) return;



                string name_dbf = "SCDDDES";
                var _locatio_scdddes = listar_location_dbf.Where(x => x.rutloc_namedbf==name_dbf).FirstOrDefault();

                string _error = "";
                /*ya no entra a consultar*/
                if (File.Exists(@ruta_validacion)) return;
                string _hora =DateTime.Today.ToString() + " " +  DateTime.Now.ToLongTimeString() + "==>INGRESO PASO1";
                TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                /**/


                #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                if (valida_file_ecu())
                {
                    NetworkShare.ConnectToShare(_locatio_noservicio.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                }
                else
                {
                    NetworkShare.ConnectToShare(_locatio_scdddes.rutloc_location, @".\Tareas", "tareas");
                }
                #endregion

                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==> saliendo del objecto NetworkShare y entrando al metodo get_scdddes";
                tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();

                _lista_guiasC = get_scdddes(_locatio_scdddes.rutloc_location,ref _error);
               

                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>get_scdddes ==>" + _error + _lista_guiasC.Count().ToString();
                tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();

                /*VERIFICAR SI HAY ERROR*/
                if (_error.Length>0)
                {
                    _error += " ==>TABLA [SCDDDES]";
                    Util ws_error_transac = new Util();
                    /*si hay un error entonces 03 error de lectura dbf*/
                    ws_error_transac.control_errores_transac("03", _error,ref _error_ws);
                }
                /**/
               
                if (_lista_guiasC!=null)
                {
                    if (_lista_guiasC.Count==0)
                    {
                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "ningun registro encontrado,  ";
                        tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                        return;
                    }

                    #region<METODO GRUPO CONSULTA>
                    if (File.Exists(@ruta_validacion)) return;
                    /*en este caso */
                    _error = "";
                    name_dbf = "FVDESPC";
                    Boolean existe_data = false;
                    var _location_fvdespc = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();

                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "inicio==>acceso dbf ";
                    tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();

                    #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                    if (valida_file_ecu())
                    {
                        NetworkShare.ConnectToShare(_location_fvdespc.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                    }
                    #endregion

                    List<BataTransac.Ent_Fvdespc> fvdespc_lista = get_fvdespc("","", _location_fvdespc.rutloc_location, ref _error, ref existe_data, _locatio_scdddes.rutloc_location);
                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "get_fvdespc==>>" + _error + " " + fvdespc_lista.Count().ToString();
                    tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();

                    name_dbf = "FVDESPD";
                    var _location_fvdespd = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                    if (File.Exists(@ruta_validacion)) return;
                    #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                    if (valida_file_ecu())
                    {
                        NetworkShare.ConnectToShare(_location_fvdespd.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                    }
                    #endregion

                    DataTable fvdespd_lista = get_fvdespd("", "", _location_fvdespd.rutloc_location, ref _error, _locatio_scdddes.rutloc_location);
                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "get_fvdespd==>>" + _error + " " + fvdespd_lista.Rows.Count.ToString();
                    tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();


                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "fin==>acceso dbf ";
                    tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();

                    _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "fin==>acceso dbf ";
                    tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    tw.WriteLine(_hora);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();
                    #endregion
                    /**/
                    string DESC_ALMACEN = "";
                    string rutloc_location = "";
                    List<string> listGuias = new List<string>();

                    #region<ENVIAMOS GUIAS POR WEBSERVICE>
                    foreach (BataTransac.Ent_Scdddes filaC in _lista_guiasC)
                    {
                      
                        var fvdespc_tmp = fvdespc_lista.Where(d => d.DESC_ALMAC == filaC.DDES_ALMAC && d.DESC_GUDIS == filaC.DDES_GUIRE).ToList(); // get_fvdespc(filaC.DDES_ALMAC, filaC.DDES_GUIRE, _location_fvdespc.rutloc_location, ref _error, ref existe_data);

                        BataTransac.Ent_Fvdespc fvdespc = new BataTransac.Ent_Fvdespc();
                        fvdespc = fvdespc_tmp[0];
                      

                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>get_fvdespc " + filaC.DDES_GUIRE; 
                        tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                        /*si existe data entonces entra a la condicion*/
                        if (existe_data)
                        {
                            /*validando fechas de despacho*/
                            if (fvdespc != null)
                            {
                                fvdespc.DESC_FDESP = filaC.DDES_FDESP;
                                fvdespc.DESC_FECHA = filaC.DDES_FECHA;
                                fvdespc.DESC_FTRA = filaC.DDES_FECHA;
                            }

                            /*VERIFICAR SI HAY ERROR*/
                            if (_error.Length > 0)
                            {
                                _error += " ==>TABLA [FVDESPC]";
                                Util ws_error_transac = new Util();
                                /*si hay un error entonces 03 error de lectura dbf*/
                                ws_error_transac.control_errores_transac("03", _error,ref _error_ws);
                            }
                            /**/

                            /*captura la cebecera de la guia*/
                            if (fvdespc != null)
                            {
                                _error = "";
                                name_dbf = "FVDESPD";
                              
                         

                                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "inicio==>get_fvdespd " + filaC.DDES_GUIRE;
                                tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                                tw.WriteLine(_hora);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                               
                                DataTable fvdespd = new DataTable();
                                fvdespd = fvdespd_lista.Clone();
                                DataRow[] filas_fvdespd = null;
                                filas_fvdespd = fvdespd_lista.Select("DESD_ALMAC='" + filaC.DDES_ALMAC + "' and DESD_GUDIS='" + filaC.DDES_GUIRE + "'"); // get_fvdespd(filaC.DDES_ALMAC, filaC.DDES_GUIRE, _location_fvdespd.rutloc_location, ref _error);

                                foreach(DataRow fila in filas_fvdespd)
                                {
                                    fvdespd.ImportRow(fila);
                                }

                                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "fin==>get_fvdespd " + filaC.DDES_GUIRE;
                                tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                                tw.WriteLine(_hora);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                /*VERIFICAR SI HAY ERROR*/
                                if (_error.Length > 0)
                                {
                                    _error += " ==>TABLA [FVDESPD]";
                                    Util ws_error_transac = new Util();
                                    /*si hay un error entonces 03 error de lectura dbf*/
                                    ws_error_transac.control_errores_transac("03", _error,ref _error_ws);
                                }
                                /**/

                                /*verifica que el detalle de la guia tenga filas*/
                                /*si la guias tiene detalle se envia por ws*/

                                if (fvdespd != null)
                                {
                                    if (fvdespd.Rows.Count > 0)
                                    {
                                        /*enviar el datatable*/
                                        fvdespc.DT_FVDESPD_TREGMEDIDA = fvdespd;

                                        BataTransac.Ent_Scdddes scdddes = new BataTransac.Ent_Scdddes();
                                        scdddes = filaC;

                                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>inicio de envio web service " + filaC.DDES_GUIRE;
                                        tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                                        tw.WriteLine(_hora);
                                        tw.Flush();
                                        tw.Close();
                                        tw.Dispose();
                                        /***********************************/
                                        Envio_Guias ws_envio = new Envio_Guias();
                                        string envio_guias_ws = ws_envio.envio_ws_guias(fvdespc, scdddes);

                                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>envio web service " + filaC.DDES_GUIRE;
                                        tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                                        tw.WriteLine(_hora);
                                        tw.Flush();
                                        tw.Close();
                                        tw.Dispose();

                                        //si return es true entonces validamos los dbf
                                        if (envio_guias_ws.Length == 0)
                                        {
                                            name_dbf = "SCDDDES";
                                            var _locatio_scdddes_edit = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                                            /*si es que las guias se grabaron correctamente entonces vamos a setear el valor en el dbf*/

                                            /*ya no entra a consultar*/
                                            if (File.Exists(@ruta_validacion)) return;
                                            /**/
                                            _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>inicio de update scddes " + filaC.DDES_GUIRE;
                                            tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                                            tw.WriteLine(_hora);
                                            tw.Flush();
                                            tw.Close();
                                            tw.Dispose();

                                            #region<EN ESTE PASO TRATAMOS DE ENTRAR AL NOVELL PARA TRAERME LA INFO>
                                            if (valida_file_ecu())
                                            {
                                                NetworkShare.ConnectToShare(_locatio_scdddes_edit.rutloc_location, ConexionDBF.user_novell, ConexionDBF.password_novell);
                                            }
                                            #endregion
                                                                                      
                                            listGuias.Add(fvdespc.DESC_ALMAC +  fvdespc.DESC_GUDIS);
                                            DESC_ALMACEN = fvdespc.DESC_ALMAC;
                                            rutloc_location = _locatio_scdddes_edit.rutloc_location;

                                            //edit_scdddes(fvdespc.DESC_ALMAC, fvdespc.DESC_GUDIS, _locatio_scdddes_edit.rutloc_location,ref _error_ws);
                                            _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>fin de update scddes " + _error_ws + "  "  + filaC.DDES_GUIRE;
                                            tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                                            tw.WriteLine(_hora);
                                            tw.Flush();
                                            tw.Close();
                                            tw.Dispose();


                                        }
                                        else
                                        {
                                            Util ws_error_transac = new Util();
                                            _error_ws = envio_guias_ws.ToString();
                                            /*si hay un error entonces 02 error de transaction*/
                                            ws_error_transac.control_errores_transac("02", envio_guias_ws,ref _error_ws);
                                        }
                                    }
                                }

                            }
                        }
                    //}
                    }
                    #endregion

                    if (listGuias.Count>0) {

                        edit_list_scdddes(DESC_ALMACEN, listGuias, rutloc_location, ref _error_ws);
                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==>fin de update list scddes " + _error_ws;
                        tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }

                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message +" error de metodo";
                _error_transac = exc.Message + " error de metodo";
                //_envio_guias = "";
            }
            //return _error_transac;
        }

        public static string retornar()
        {
            return "xxxx";
        }
        private void edit_scdddes(string cod_alm,string nroguia,string _path,ref string _error_ws)
        {
            string sqlquery = "UPDATE SCDDDES SET DDES_FTXTD='X' WHERE DDES_ALMAC='" + cod_alm + "' AND DDES_GUIRE='" + nroguia + "'";
            try
            {
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (OleDbCommand cmd = new OleDbCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception exc)
                    {
                        _error_ws = exc.Message;
                        if (cn!=null)
                        if (cn.State == ConnectionState.Open) cn.Close();                       
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch(Exception exc)
            {
                _error_ws = exc.Message;
            }
        }

        private void edit_list_scdddes(string cod_alm, List<string> listGuia, string _path, ref string _error_ws)
        {

            string strListGuia = "";
            int limite = 20;
            int contador = 0;
            try
            {
                foreach (string strguia in listGuia)
                {
                    contador++;

                    strListGuia += "'" + strguia + "',";

                    if (contador == limite)
                    {

                        strListGuia = strListGuia.TrimEnd(',');

                        string sqlquery = "UPDATE SCDDDES SET DDES_FTXTD='X' WHERE DDES_ALMAC + DDES_GUIRE in (" + strListGuia + ")";

                        strListGuia = "";
                        contador = 0;

                        String error_cursor ="";

                        update_list_scdddes(sqlquery, _path, ref error_cursor);

                        

                        if (error_cursor.Length>0)
                        {

                            string _hora = DateTime.Today.ToString() + " " +DateTime.Now.ToLongTimeString() + " Error de update registros==>" +  error_cursor;
                            TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                            tw.WriteLine(_hora);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();

                         
                        }
                        else
                        {
                            string _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " Update registros correctamente.." ;
                            TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                            tw.WriteLine(_hora);
                            tw.Flush();
                            tw.Close();
                            tw.Dispose();
                        }

                    }
                  

                }

                if (contador > 0) { 
                     strListGuia = strListGuia.TrimEnd(',');

                    //string sqlquery = "UPDATE SCDDDES SET DDES_FTXTD='X' WHERE DDES_ALMAC='" + cod_alm + "' AND DDES_GUIRE in (" + strListGuia + ")";
                    string sqlquery = "UPDATE SCDDDES SET DDES_FTXTD='X' WHERE DDES_ALMAC + DDES_GUIRE in (" + strListGuia + ")";

                    strListGuia = "";
                    contador = 0;

                    String error_cursor = "";

                    update_list_scdddes(sqlquery, _path, ref error_cursor);
                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
            }            
        }

        private void update_list_scdddes(string sqlquery, string _path, ref string _error_ws)
        {  
            try
            {
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (OleDbCommand cmd = new OleDbCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception exc)
                    {
                        _error_ws = exc.Message;
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
            }
        }



        private DataSet ds_fmc_fmd(string cod_tda,string ruta_dbf,ref string error)
        {
            DataSet ds = null;
            DataTable dt_fmc = null;
            DataTable dt_fmd = null;
            string sqlquery_fmc = "SELECT '"  + cod_tda + "' AS V_COD_TDA,V_TFOR,V_PROC,V_CFOR,V_SFOR,V_NFOR,V_FFOR,V_MONE,V_TASA,V_ALMO," + 
	                              "V_ALMD,V_TANE,V_ANEX,V_TDOC,V_SUNA,V_SDOC,V_NDOC,V_FDOC,V_TREF,V_SREF,V_NREF," +       
	                              "V_TIPO,V_ARTI,V_REGL,V_COLO,V_CANT,V_PRES,V_PRED,V_VVTS,V_VVTD,V_AUTO,V_PTOT," +
                                  "V_IMPR,V_CUSE,V_MUSE,V_FCRE,V_FMOD,V_FTRX,V_CTRA,V_MEMO,V_MOTR,V_PAR1,V_PAR2,V_PAR3," + 
                                  "V_LLE1,V_LLE2,V_LLE3 FROM FMC";

            string sqlquery_fmd = "SELECT '" + cod_tda + "' AS I_COD_TDA,I_TFOR,I_PROC,I_CFOR,I_SFOR,I_NFOR,I_TIPO,I_ARTI,I_REGL,I_COLO," +
                                  "I_ITEM,I_UNIC,I_EQU1,I_UNIM,I_CANC,I_CANM,I_PRES,I_PRED,I_VVTS,I_VVTD,I_PLIS," + 
                                  "I_PTOT,I_IMPR,I_CUSE,I_MUSE,I_FCRE,I_FMOD FROM FMD";
            try
            {
                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fmc_fmd_vfpoledb(ruta_dbf)))
                {
                    /*selecccionando el archivo FMC*/
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_fmc, cn))
                    {
                        cmd.CommandTimeout = 0;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            dt_fmc = new DataTable();
                            da.Fill(dt_fmc);
                        }
                        dt_fmc.TableName = "FMC";
                    }
                    /*selecccionando el archivo FMD*/
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_fmd, cn))
                    {
                        cmd.CommandTimeout = 0;
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            dt_fmd = new DataTable();
                            da.Fill(dt_fmd);
                        }
                        dt_fmd.TableName = "FMD";
                    }
                }
                if (dt_fmc!=null && dt_fmd != null)
                {
                    ds = new DataSet();
                    ds.Tables.Add(dt_fmc); ds.Tables.Add(dt_fmd);
                }               

            }
            catch (Exception exc)
            {
                error = exc.Message;
                ds = null;                
            }
            return ds;
        }
       
        public void procesar_dbf_pos(ref string _error_procesos)
        {
            Util datUtil = null;
            Dat_Venta venta_ing = null;
            string _error = "";            
            try
            {
                datUtil = new Util();
                string carpetatienda = datUtil.get_ruta_locationProcesa_dbf("SQL");//@"D:\TiendaPaq"
                string carpetadbf = carpetatienda + "\\DBF";

                //string carpetatienda = @"D:\TiendaPaq";
                //string carpetadbf = carpetatienda + "\\DBF";

                string strCodTienda = "";
                if (!Directory.Exists(@carpetatienda)) Directory.CreateDirectory(@carpetatienda);
                if (!Directory.Exists(@carpetadbf)) Directory.CreateDirectory(@carpetadbf);
             
                string[] filespaquete= Directory.GetFiles(@carpetatienda, "*.*");
               

                for (Int32 i = 0; i < filespaquete.Length; ++i)
                {
                    string[] filesborrar = Directory.GetFiles(@carpetadbf, "*.*");
                    strCodTienda = "";
                    foreach (string filedetele in filesborrar)
                        File.Delete(filedetele);

                    String value = filespaquete[i].ToString();
                    //Char delimiter = '.';
                    //String[] substrings = value.Split(delimiter);
                    strCodTienda =Path.GetExtension(@value).Substring(1,3);
                    strCodTienda = "50" + strCodTienda;// + "==>" + value;

                    //string _hora = DateTime.Now.ToLongTimeString() + strCodTienda;
                    //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    //tw.WriteLine(_hora);
                    //tw.Flush();
                    //tw.Close();
                    //tw.Dispose();

                    _error = descomprimir(filespaquete[i].ToString(), @carpetadbf);
                    if (_error.Length == 0)
                    {
                        //DataSet ds_ventas= datUtil.get_ds_venta(@carpetadbf,ref _error);

                        if (_error.Length==0)
                        {

                            venta_ing = new Dat_Venta();
                            _error =  venta_ing.inserta_venta_dbf(strCodTienda);

                            if (_error.Length==0)
                            {
                                /*en este caso insertamos en el fmc y fmd*/

                                Boolean existe_fmc = File.Exists(@carpetadbf + "\\FMC.DBF");
                                Boolean existe_fmd = File.Exists(@carpetadbf + "\\FMD.DBF");
                                if (existe_fmc && existe_fmd )
                                {
                                    DataSet ds = ds_fmc_fmd(strCodTienda, @carpetadbf,ref  _error);

                                    if (_error.Length==0)
                                    {
                                        _error = venta_ing.insertar_fmc_fmd(strCodTienda, ds);

                                        if (_error.Length==0)
                                        {
                                            /**/
                                            /*borramos paquete*/
                                            if (File.Exists(@value))
                                                File.Delete(@value);
                                        }
                                        else
                                        {
                                            datUtil.control_errores_transac("17", _error, ref _error);
                                        }

                                    }
                                    else
                                    {
                                        datUtil.control_errores_transac("17", _error, ref _error);
                                    }
                                }
                                else
                                {
                                    /**/
                                    /*borramos paquete*/
                                    if (File.Exists(@value))
                                        File.Delete(@value);
                                }
                             
                            }
                            else
                            {
                                /*error de sql*/
                                datUtil.control_errores_transac("06", _error, ref _error);
                            }


                        }
                        else
                        {
                            /*error dbf */
                            datUtil.control_errores_transac("06", _error, ref _error);
                        }

                    }
                    else
                    {
                        /*eror de archivo*/
                        datUtil.control_errores_transac("06", _error,ref _error);
                    }

                }




            }
            catch (Exception EX)
            {

               
            }

            //Dat_Util datUtil = null;
            //try
            //{
            //    datUtil = new Dat_Util();  
            //    string carpetatienda = datUtil.get_ruta_locationProcesa_dbf("VENTA");
            //    string carpetadbf = carpetatienda + "\\DBF";
            //    string strCodTienda = "";

            //        #region <PROCESAMIENTO DBF DE VENTAS DE TIENDA>
            //        if ((Directory.Exists(carpetatienda)))
            //        {
            //            string[] filesborrar;
            //            string verror = "";
            //            filesborrar = System.IO.Directory.GetFiles(@carpetatienda, "*.*");

            //            if (!(Directory.Exists(@carpetadbf)))
            //            {
            //                System.IO.Directory.CreateDirectory(@carpetadbf);
            //            }

            //            string[] filePaths = Directory.GetFiles(@carpetadbf);
            //            foreach (string filePath in filePaths)
            //                File.Delete(filePath);

            //            for (Int32 iborrar = 0; iborrar < filesborrar.Length; ++iborrar)
            //            {

            //                String value = filesborrar[iborrar].ToString();
            //                Char delimiter = '.';
            //                String[] substrings = value.Split(delimiter);
            //                strCodTienda = substrings[1].ToString();

            //                verror = descomprimir(filesborrar[iborrar].ToString(), @carpetadbf);

            //                //if (verror.Length == 0)
            //                //{
            //                //    string strRespuesta = datUtil.LeerDataDBF_TemporalVenta(strCodTienda, @carpetadbf);

            //                //    if (strRespuesta == "S")
            //                //    {
            //                //        System.IO.File.Delete(@filesborrar[iborrar].ToString());
            //                //    }
            //                //    else
            //                //    {
            //                //        string errSw2 = "";
            //                //        util.control_errores_transac("06", strRespuesta, ref errSw2);
            //                //    }

            //                //}
            //                //else
            //                //{

            //                //    string errSw = "";
            //                //    util.control_errores_transac("06", verror + "==>50" + strCodTienda, ref errSw);
            //                //}
            //            }

            //        }

            //        #endregion
               
            //    //****************************************************************************
            //}
            //catch (Exception exc)
            //{
            //    string errSwc = "";
               
            //    //util.control_errores_transac("06", exc.Message, ref errSwc);

            //}



        }


        public void procesar_VentaXstore_pos(ref string _error_procesos)
        {
            Util datUtil = null;
            Dat_Venta venta_ing = null;
            string _error = "";
            DataSet dsListaEnvio = new DataSet();
            try
            {

                venta_ing = new Dat_Venta();
                dsListaEnvio = venta_ing.procesar_listaEnvioXstore();
                DataTable dtLista = new DataTable();
                dtLista = dsListaEnvio.Tables[0];


                for (int i = 0; i < dtLista.Rows.Count; i++)
                {
                   
                    string strTienda = dtLista.Rows[i]["TIENDA"].ToString();
                    DateTime strFecha = DateTime.Parse(dtLista.Rows[i]["FECHA"].ToString());
                    string strEstado = dtLista.Rows[i]["ESTADO"].ToString();
                    string strEnviado = dtLista.Rows[i]["ENVIO"].ToString();
                    string strForzar = dtLista.Rows[i]["FORZAR"].ToString();

                    DataSet dsVenta = new DataSet();
                    dsVenta = venta_ing.GET_OBTENER_VENTA_XSTORE(strTienda, strFecha);


                }            


                

            }
            catch (Exception)
            {


            }          


        }


        //private void tabla_FCIERR(DataTable dt)
        //{
        //    try
        //    {
        //        string _path_envia = basico.ruta_temp_DBF;
        //        DBFNET fcierr = new DBFNET();
        //        fcierr.tabla = "FCIERR";

        //        fcierr.addcol("Ci_csuc", Tipo.Caracter, "3");
        //        fcierr.addcol("Ci_fech", Tipo.Fecha);
        //        fcierr.addcol("Ci_esta", Tipo.Caracter, "1");
        //        fcierr.addcol("Ci_fetr", Tipo.Fecha);
        //        fcierr.addcol("Ci_cuse", Tipo.Caracter, "3");
        //        fcierr.addcol("Ci_muse", Tipo.Caracter, "3");
        //        fcierr.addcol("Ci_fcre", Tipo.Fecha);
        //        fcierr.addcol("Ci_fmod", Tipo.Fecha);
        //        fcierr.addcol("Ci_impz", Tipo.Caracter, "1");
        //        fcierr.addcol("Ci_aper", Tipo.Caracter, "30");
        //        fcierr.addcol("Ci_cier", Tipo.Caracter, "30");

        //        fcierr.creardbf(_path_envia);
        //        fcierr.Insertar_tabla(dt, _path_envia);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //private void tabla_FSTKG(DataTable dtstk)
        //{
        //    try
        //    {
        //        StringBuilder str = null;
        //        string str_cadena = "";


        //        if (dtstk.Rows.Count > 0)
        //        {
        //            str = new StringBuilder();
        //            for (Int32 i = 0; i < dtstk.Rows.Count; ++i)
        //            {
        //                string cad_envio = "\"" + dtstk.Rows[i]["TIENDA"].ToString() + "\"" + ",\"" + dtstk.Rows[i]["ARTICULO"].ToString() + "\"" + "," +
        //                                   dtstk.Rows[i]["TOTAL"].ToString() + "," + dtstk.Rows[i]["00"].ToString() + "," +
        //                                   dtstk.Rows[i]["01"].ToString() + "," + dtstk.Rows[i]["02"].ToString() + "," +
        //                                   dtstk.Rows[i]["03"].ToString() + "," + dtstk.Rows[i]["04"].ToString() + "," +
        //                                   dtstk.Rows[i]["05"].ToString() + "," + dtstk.Rows[i]["06"].ToString() + "," +
        //                                   dtstk.Rows[i]["07"].ToString() + "," + dtstk.Rows[i]["08"].ToString() + "," +
        //                                   dtstk.Rows[i]["09"].ToString() + "," + dtstk.Rows[i]["10"].ToString() + "," +
        //                                   dtstk.Rows[i]["11"].ToString() + "," + dtstk.Rows[i]["FECHA"].ToString();

        //                str.Append(cad_envio);

        //                if (i < dtstk.Rows.Count - 1)
        //                {
        //                    str.Append("\r\n");

        //                }

        //            }
        //            str_cadena = str.ToString();

        //            string _path_envia = basico.ruta_temp_DBF;

        //            if (!Directory.Exists(_path_envia))
        //                Directory.CreateDirectory(_path_envia);
        //            string file_stk = "FSTKG";
        //            string ruta_file_stk = _path_envia + "\\" + file_stk + ".txt";

        //            if (File.Exists(@ruta_file_stk)) File.Delete(@ruta_file_stk);
        //            File.WriteAllText(@ruta_file_stk, str_cadena);
        //        }

        //        //DBFNET venta_det = new DBFNET();
        //        //venta_det.creartxt_stk(_path_envia);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private void tabla_FFACTD(DataTable dt)
        //{
        //    try
        //    {
        //        string _path_envia = basico.ruta_temp_DBF;
        //        DBFNET venta_det = new DBFNET();
        //        venta_det.tabla = "FFACTD";
        //        venta_det.addcol("fd_nint", Tipo.Caracter, "8");
        //        venta_det.addcol("fd_tipo", Tipo.Caracter, "1");
        //        venta_det.addcol("fd_arti", Tipo.Caracter, "12");
        //        venta_det.addcol("fd_regl", Tipo.Caracter, "4");
        //        venta_det.addcol("fd_colo", Tipo.Caracter, "2");
        //        venta_det.addcol("fd_item", Tipo.Caracter, "3");
        //        venta_det.addcol("fd_icmb", Tipo.Caracter, "1");
        //        venta_det.addcol("fd_qfac", Tipo.Numerico, "8,3");
        //        venta_det.addcol("fd_lpre", Tipo.Caracter, "2");
        //        venta_det.addcol("fd_calm", Tipo.Caracter, "4");
        //        venta_det.addcol("fd_pref", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_dref", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_prec", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_brut", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_pimp1", Tipo.Numerico, "6,2");
        //        venta_det.addcol("fd_vimp1", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_subt1", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_pimp2", Tipo.Numerico, "6,2");
        //        venta_det.addcol("fd_vimp2", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_subt2", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_pdct1", Tipo.Numerico, "6,2");
        //        venta_det.addcol("fd_vdct1", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_subt3", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_vdct4", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_vdc23", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_vvta", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_pimp3", Tipo.Numerico, "6,2");
        //        venta_det.addcol("fd_vimp3", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_pimp4", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_vimp4", Tipo.Numerico, "14,4");

        //        venta_det.addcol("fd_total", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_cuse", Tipo.Caracter, "3");
        //        venta_det.addcol("fd_muse", Tipo.Caracter, "3");
        //        venta_det.addcol("fd_fcre", Tipo.Fecha);
        //        venta_det.addcol("fd_fmod", Tipo.Fecha);
        //        venta_det.addcol("fd_auto", Tipo.Caracter, "6");
        //        venta_det.addcol("fd_dre2", Tipo.Numerico, "14,4");
        //        venta_det.addcol("fd_asoc", Tipo.Caracter, "13");

        //        venta_det.creardbf(_path_envia);
        //        venta_det.Insertar_tabla(dt, _path_envia);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //private void tabla_FNOTAA(DataTable dt)
        //{
        //    try
        //    {
        //        string _path_envia = basico.ruta_temp_DBF;
        //        DBFNET venta_pago = new DBFNET();
        //        venta_pago.tabla = "FNOTAA";
        //        venta_pago.addcol("na_nota", Tipo.Caracter, "8");
        //        venta_pago.addcol("na_item", Tipo.Caracter, "3");
        //        venta_pago.addcol("na_mone", Tipo.Caracter, "2");
        //        venta_pago.addcol("na_tpag", Tipo.Caracter, "2");
        //        venta_pago.addcol("na_tasa", Tipo.Numerico, "10,4");
        //        venta_pago.addcol("na_cref", Tipo.Caracter, "2");
        //        venta_pago.addcol("na_sref", Tipo.Caracter, "4");
        //        venta_pago.addcol("na_nref", Tipo.Caracter, "22");
        //        venta_pago.addcol("na_vref", Tipo.Numerico, "14,4");
        //        venta_pago.addcol("na_vpag", Tipo.Numerico, "14,4");
        //        venta_pago.addcol("na_esta", Tipo.Caracter, "1");
        //        venta_pago.addcol("na_cier", Tipo.Caracter, "1");
        //        venta_pago.addcol("na_fcre", Tipo.Caracter, "30");
        //        venta_pago.addcol("na_fmod", Tipo.Caracter, "30");
        //        venta_pago.creardbf(_path_envia);
        //        venta_pago.Insertar_tabla(dt, _path_envia);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //private void tabla_FFACTC(DataTable dt)
        //{
        //    try
        //    {
        //        string _path_envia = basico.ruta_temp_DBF;
        //        DBFNET venta_cab = new DBFNET();
        //        venta_cab.tabla = "FFACTC";
        //        venta_cab.addcol("fc_nint", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_nnot", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_codi", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_suna", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_sfac", Tipo.Caracter, "4");
        //        venta_cab.addcol("fc_nfac", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_ffac", Tipo.Fecha);
        //        venta_cab.addcol("fc_nord", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_cref", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_sref", Tipo.Caracter, "4");
        //        venta_cab.addcol("fc_nref", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_pvta", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_csuc", Tipo.Caracter, "3");
        //        venta_cab.addcol("fc_gvta", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_zona", Tipo.Caracter, "3");
        //        venta_cab.addcol("fc_clie", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_ncli", Tipo.Caracter, "90");
        //        venta_cab.addcol("fc_nomb", Tipo.Caracter, "30");
        //        venta_cab.addcol("fc_apep", Tipo.Caracter, "30");
        //        venta_cab.addcol("fc_apem", Tipo.Caracter, "30");
        //        venta_cab.addcol("fc_dcli", Tipo.Caracter, "50");
        //        venta_cab.addcol("fc_cubi", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_ruc", Tipo.Caracter, "20");
        //        venta_cab.addcol("fc_vuse", Tipo.Caracter, "3");
        //        venta_cab.addcol("fc_vend", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_ipre", Tipo.Caracter, "1");
        //        venta_cab.addcol("fc_tint", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_pint", Tipo.Numerico, "6,2");
        //        venta_cab.addcol("fc_lcsg", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_ncon", Tipo.Caracter, "30");
        //        venta_cab.addcol("fc_dcon", Tipo.Caracter, "30");
        //        venta_cab.addcol("fc_lcon", Tipo.Caracter, "20");
        //        venta_cab.addcol("fc_lruc", Tipo.Caracter, "11");
        //        venta_cab.addcol("fc_agen", Tipo.Caracter, "20");
        //        venta_cab.addcol("fc_mone", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_tasa", Tipo.Numerico, "10,4");
        //        venta_cab.addcol("fc_fpag", Tipo.Caracter, "2");

        //        venta_cab.addcol("fc_nlet", Tipo.Numerico, "2,0");
        //        venta_cab.addcol("fc_qtot", Tipo.Numerico, "8,2");
        //        venta_cab.addcol("fc_pref", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_dref", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_brut", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_vimp1", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_vimp2", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_vdct1", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_vdct4", Tipo.Numerico, "14,4");

        //        venta_cab.addcol("fc_pdc2", Tipo.Numerico, "6,2");
        //        venta_cab.addcol("fc_pdc3", Tipo.Numerico, "6,2");
        //        venta_cab.addcol("fc_vdc23", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_vvta", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_vimp3", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_pimp4", Tipo.Numerico, "6,2");

        //        venta_cab.addcol("fc_vimp4", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_total", Tipo.Numerico, "14,4");
        //        venta_cab.addcol("fc_esta", Tipo.Caracter, "1");
        //        venta_cab.addcol("fc_tdoc", Tipo.Caracter, "1");
        //        venta_cab.addcol("fc_cuse", Tipo.Caracter, "3");
        //        venta_cab.addcol("fc_muse", Tipo.Caracter, "3");

        //        venta_cab.addcol("fc_fcre", Tipo.Fecha);
        //        venta_cab.addcol("fc_fmod", Tipo.Fecha);

        //        venta_cab.addcol("fc_hora", Tipo.Caracter, "8");
        //        venta_cab.addcol("fc_auto", Tipo.Caracter, "3");
        //        venta_cab.addcol("fc_ftx", Tipo.Caracter, "1");
        //        venta_cab.addcol("fc_estc", Tipo.Caracter, "1");
        //        venta_cab.addcol("fc_sexo", Tipo.Caracter, "1");
        //        venta_cab.addcol("fc_mpub", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_edad", Tipo.Caracter, "2");
        //        venta_cab.addcol("fc_regv", Tipo.Caracter, "25");
        //        venta_cab.creardbf(_path_envia);
        //        venta_cab.Insertar_tabla(dt, _path_envia);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}


        public void enviar_scactco(ref string _error_ws)
        {
            //BataTransac.Ent_Fvdespc fvdespc = new BataTransac.Ent_Fvdespc();
            //BataTransac.Ent_Scdddes scdddes = new BataTransac.Ent_Scdddes();
            //Envio_Guias ws_envio = new Envio_Guias();
            BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
            //string envio_guias_ws = ws_envio.envio_ws_guias(fvdespc, scdddes);
            string _error_transac = "";
            List<BataTransac.Ent_Scactco> _lista_scactco = new List<BataTransac.Ent_Scactco>();

            
            
            BataTransac.Ent_List_Scactco listArray = new BataTransac.Ent_List_Scactco();
            string _error = "";
           
            try
            {
                /*segundos para ejecutar*/
             //   _espera_ejecuta(20);
                /***********************/

                #region<CAPTURAR EL PATH DE LOS DBF>

                Util locationdbf = new Util();
                string rutloc_location = locationdbf.get_ruta_locationProcesa_dbf("SCACTCO");
                
                #endregion

                if (rutloc_location == null) return;

                /*VALIDACION DE EJECUCION */
                #region<VALIDA ARCHIVO SI EXISTE PARA NO REALIZAR NINGUNA ACCCION POR SEGURIDAD DE REINDEXACION DEL FOX>

                string name_txt = "SCACTCO";
               
                //Boolean valida_exists_txt = false;
                string ruta_validacion = "";
                if (rutloc_location != null)
                {
                    ruta_validacion = rutloc_location + "\\"+ name_txt + ".txt";
                }

                #endregion
                               
                /*ya no entra a consultar*/
                if (File.Exists(@ruta_validacion)) return;
                /**/

                _lista_scactco = get_scactco(rutloc_location, ref _error);

                /*VERIFICAR SI HAY ERROR*/
                if (_error.Length > 0)
                {
                    _error += " ==>TABLA SCACTCO]";
                    Util ws_error_transac = new Util();
                    /*si hay un error entonces 07 error de lectura dbf*/
                    ws_error_transac.control_errores_transac("07", _error, ref _error_ws);
                }
                /**/

                if (_lista_scactco != null)
                {
                    listArray.lista_scactco = _lista_scactco.ToArray();
                    Envio_Scactco ws_envio = new Envio_Scactco();
                   
                    string envio_scactco_ws = ws_envio.envio_ws_Scactco(listArray);
                 
                    if (envio_scactco_ws.Length > 0)
                    {
                        Util ws_error_transac = new Util();
                        _error_ws = envio_scactco_ws.ToString();
                        /*si hay un error entonces 02 error de transaction*/
                        ws_error_transac.control_errores_transac("07", envio_scactco_ws, ref _error_ws);
                    }                    

                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
                _error_transac = exc.Message;
                //_envio_guias = "";
            }
            //return _error_transac;
        }

        public void enviar_scdremb(ref string _error_ws)
        {
            //BataTransac.Ent_Fvdespc fvdespc = new BataTransac.Ent_Fvdespc();
            //BataTransac.Ent_Scdddes scdddes = new BataTransac.Ent_Scdddes();
            //Envio_Guias ws_envio = new Envio_Guias();
            BataTransac.Bata_TransactionSoapClient bata_trans = new BataTransac.Bata_TransactionSoapClient();
            //string envio_guias_ws = ws_envio.envio_ws_guias(fvdespc, scdddes);
            string _error_transac = "";
            List<BataTransac.Ent_Scdremb> _lista_scdremb = new List<BataTransac.Ent_Scdremb>();



            BataTransac.Ent_List_Scdrem listArray = new BataTransac.Ent_List_Scdrem();
            string _error = "";

            try
            {
                /*segundos para ejecutar*/
                //   _espera_ejecuta(20);
                /***********************/

                #region<CAPTURAR EL PATH DE LOS DBF>

                Util locationdbf = new Util();
                string rutloc_location = locationdbf.get_ruta_locationProcesa_dbf("SCDREMB");

                #endregion

                if (rutloc_location == null) return;

                /*VALIDACION DE EJECUCION */
                #region<VALIDA ARCHIVO SI EXISTE PARA NO REALIZAR NINGUNA ACCCION POR SEGURIDAD DE REINDEXACION DEL FOX>

                string name_txt = "SCDREMB";

                //Boolean valida_exists_txt = false;
                string ruta_validacion = "";
                if (rutloc_location != null)
                {
                    ruta_validacion = rutloc_location + "\\" + name_txt + ".txt";
                }

                #endregion

                /*ya no entra a consultar*/
                if (File.Exists(@ruta_validacion)) return;
                /**/

                _lista_scdremb = get_scdremb(rutloc_location, ref _error);

                /*VERIFICAR SI HAY ERROR*/
                if (_error.Length > 0)
                {
                    _error += " ==>TABLA SCDREMB]";
                    Util ws_error_transac = new Util();
                    /*si hay un error entonces 07 error de lectura dbf*/
                    ws_error_transac.control_errores_transac("07", _error, ref _error_ws);
                }
                /**/

                if (_lista_scdremb != null)
                {
                    listArray.lista_scdremb = _lista_scdremb.ToArray();


                    Envio_Scdremb ws_envio = new Envio_Scdremb();

                    string envio_scdremb_ws = ws_envio.envio_ws_Scdremb(listArray);

                    if (envio_scdremb_ws.Length > 0)
                    {
                        Util ws_error_transac = new Util();
                        _error_ws = envio_scdremb_ws.ToString();
                        /*si hay un error entonces 02 error de transaction*/
                        ws_error_transac.control_errores_transac("07", envio_scdremb_ws, ref _error_ws);
                    }

                }

            }
            catch (Exception exc)
            {
                _error_ws = exc.Message;
                _error_transac = exc.Message;
                //_envio_guias = "";
            }
            //return _error_transac;
        }
        private static string descomprimir(string _rutazip, string _destino)
        {
            string _error = "";
            try
            {
                FastZip fZip = new FastZip();
                fZip.ExtractZip(@_rutazip, @_destino, "");
            }
            catch (Exception exc)
            {
                _error = exc.Message + " ==> El Archivo esta dañado";
            }
            return _error;
        }

        #region<ENVIO POSLOG>
        public void procesar_poslog_pos(ref string _error_procesos)
        {
            Dat_Venta venta_ing = null;
            Util datUtil = null;
            try
            {
                datUtil = new Util();
                venta_ing = new Dat_Venta();
                _error_procesos = venta_ing.procesar_poslog();

                #region<ENVIO DE VENTAS DEL XSTORE A LA BASE DE DATOS>
                if (_error_procesos.Length==0)
                { 
                    _error_procesos=venta_ing.procesar_ventas_xstore();
                }
                #endregion

                #region<actualizacion de la tabla movimiento ventas , guias y caja tienda>

                venta_ing.procesar_ventas_movimiento();
                venta_ing.procesar_guias_movimiento();
                venta_ing.procesar_caja_tienda();
                    
                #endregion


                if (_error_procesos.Length>0)
                {
                    _error_procesos = "error de poslog " + _error_procesos ;
                    datUtil.control_errores_transac("08", _error_procesos, ref _error_procesos);
                }

            }
            catch (Exception exc)
            {
                _error_procesos = exc.Message;                
            }
        }

        #endregion


        #region<ENVIO DE GUIAS SFTP>
        public void envio_Guias_ToxStore(ref string _error_procesos)
        {
            Dat_Venta venta_ing = null;
            DataSet dsListaGuia = new DataSet();

            try
            {

                venta_ing = new Dat_Venta();
                dsListaGuia = venta_ing.procesar_listaGuia_ToXstore();
                DataTable dtLista = new DataTable();
                dtLista = dsListaGuia.Tables[0];


                /*obtenermos los datos del ambiente de produccion del ftp*/
                    //DataSet dsFtp = new DataSet();
                    //dsFtp = venta_ing.get_amb_Xstore("PE", "02");
                    //DataTable dt_ftp = null;
                    //dt_ftp = dsFtp.Tables[0];
                    //gHostName = dt_ftp.Rows[0]["Amb_Ftp_Server"].ToString();
                    //gUserName = dt_ftp.Rows[0]["Amb_Ftp_User"].ToString();
                    //gPassword = dt_ftp.Rows[0]["Amb_Ftp_Pass"].ToString();
                    //gPortNumber = Int32.Parse(dt_ftp.Rows[0]["Amb_Ftp_Port"].ToString());
                    //gftp_ruta_destino = dt_ftp.Rows[0]["Amb_Ftp_Path"].ToString();

                /*obtenermos los datos del ambiente de produccion del ftp*/


                for (int i = 0; i < dtLista.Rows.Count; i++)
                {

                    string strOrigen = dtLista.Rows[i]["DESC_ALMAC"].ToString();
                    string strDocumento = dtLista.Rows[i]["DESC_GUDIS"].ToString();
                    string strDestino = dtLista.Rows[i]["DESC_TDES"].ToString();
                    string pais= dtLista.Rows[i]["PAIS"].ToString();
                    gHostName = dtLista.Rows[i]["FTP_SERVER"].ToString();
                    gUserName = dtLista.Rows[i]["FTP_USER"].ToString();
                    gPassword= dtLista.Rows[i]["FTP_PASSWORD"].ToString();
                    gPortNumber=Convert.ToInt32(dtLista.Rows[i]["FTP_PORT"]);
                    gftp_ruta_destino= dtLista.Rows[i]["FTP_PATH"].ToString();

                    generainter_inv_doc(strOrigen, strDocumento, strDestino, "S", pais);

                }

            }
            catch (Exception)
            {

            }
        }

        private void generainter_inv_doc(string cod_alm, string nro_guia, string cod_tda, string strEnviaFtp,string pais)
        {
            var metroWindow = this;
            string in_inv_doc = "";

            try
            {
                Util locationdbf = new Util();
                string ruta_interface = locationdbf.get_ruta_locationProcesa_dbf("GUIA_TO_XSTORE") + "/" + pais;


                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);

                Dat_Venta venta_ing = new Dat_Venta();
                DataSet ds = new DataSet();
             
                ds = venta_ing.get_inv_doc(cod_alm, nro_guia,pais);
                
                StringBuilder str = null;
                string str_cadena = "";
                if (ds != null)
                {
                    string name_inv_doc = ""; string name_inv_doc_line_item = "";
                    string name_carton = "";

                    DataTable dt_inv = null; DataTable dt_inv_doc_line_item = null; DataTable dt_carton = null;

                    if (ds.Tables.Count > 0)
                    {
                        dt_inv = ds.Tables[0];
                        dt_inv_doc_line_item = ds.Tables[1];
                        dt_carton = ds.Tables[2];
                    }

                    /*INV_DOC*/
                    if (dt_inv.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_inv.Rows.Count; ++i)
                        {
                            str.Append(dt_inv.Rows[i]["INV_DOC"].ToString());

                            if (i < dt_inv.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();



                        name_inv_doc = "INV_DOC_" + cod_tda + "_" + nro_guia + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_inv_doc = ruta_interface + "\\" + name_inv_doc;

                        if (File.Exists(@in_inv_doc)) File.Delete(@in_inv_doc);
                        File.WriteAllText(@in_inv_doc, str_cadena);


                    }

                    /*INV_DOC_LINE_ITEM*/
                    if (dt_inv_doc_line_item.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_inv_doc_line_item.Rows.Count; ++i)
                        {
                            str.Append(dt_inv_doc_line_item.Rows[i]["INV_DOC_LINE_ITEM"].ToString());

                            if (i < dt_inv_doc_line_item.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();



                        name_inv_doc_line_item = "INV_DOC_LINE_ITEM_" + cod_tda + "_" + nro_guia + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_inv_doc = ruta_interface + "\\" + name_inv_doc_line_item;

                        if (File.Exists(@in_inv_doc)) File.Delete(@in_inv_doc);
                        File.WriteAllText(@in_inv_doc, str_cadena);

                    }

                    /*CARTON*/
                    if (dt_carton.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_carton.Rows.Count; ++i)
                        {
                            str.Append(dt_carton.Rows[i]["CARTON"].ToString());

                            if (i < dt_carton.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();



                        name_carton = "CARTON_" + cod_tda + "_" + nro_guia + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_inv_doc = ruta_interface + "\\" + name_carton;

                        if (File.Exists(@in_inv_doc)) File.Delete(@in_inv_doc);
                        File.WriteAllText(@in_inv_doc, str_cadena);

                    }

                }

                Boolean envio = true;
                string mensaje = "";
                if (strEnviaFtp.Equals("S")) {

                    string ftp_ruta_destino = gftp_ruta_destino;//"opt//webxst//BCL//autodeploy//data//org2000";
                    envio = sendftp_file_mnt(ruta_interface, ftp_ruta_destino);

                    if (envio)
                    {
                       string err = venta_ing.Actualizar_Guia_ToXstore(cod_alm, nro_guia, cod_tda);
                    }
                                        
                }
                else
                {
                    mensaje = "Se creo en la ruta : " + in_inv_doc;

                }

            }
            catch (Exception exc)
            {
                TextWriter tw1 = new StreamWriter(@"D:\XSTORE\ERROR.txt", true);
                tw1.WriteLine(exc.Message);
                tw1.Flush();
                tw1.Close();
                tw1.Dispose();
            }
        }

        public Boolean sendftp_file_mnt(string ruta_temp_interface, string ftp_ruta_destino)
        {
            Boolean valida = false;
            try
            {

                // return false;

                string[] _archivos_mnt = Directory.GetFiles(@ruta_temp_interface, "*.MNT");
                               
                for (Int32 a = 0; a < _archivos_mnt.Length; ++a)
                {
                    string _path_archivo_mnt = _archivos_mnt[a].ToString();
                    string _nombrearchivo_mnt = Path.GetFileNameWithoutExtension(@_path_archivo_mnt);
                    string _extension_archivo = Path.GetExtension(@_path_archivo_mnt);
                    string _file_path_destino = ftp_ruta_destino + "/" + _nombrearchivo_mnt + _extension_archivo;
                    Boolean valida_subida = subida_server_ftp(@_path_archivo_mnt, _file_path_destino);
                    if (valida_subida)
                    {
                        if (File.Exists(@_path_archivo_mnt)) File.Delete(@_path_archivo_mnt);
                    }
                }
                valida = true;
            }
            catch (Exception exc)
            {
                valida = false;
                throw;
            }
            return valida;
        }

        private Boolean subida_server_ftp(string file_origen, string file_destino)
        {
            Boolean valida_envio = false;

            try
            {
        
                SessionOptions sessionOptions = new SessionOptions
                {
                    //Protocol = Protocol.Sftp,
                    //HostName = "172.24.20.183",//Ent_Conexion.ftp_server,// "172.24.28.216",
                    //UserName = "retailc",//Ent_Conexion.ftp_user,// "webposintg",
                    //Password = "1wiAwNRa", //Ent_Conexion.ftp_password,// "SubJFpHEN27y",
                    //PortNumber =Ent_Conexion.ftp_puerto,// 22,
                    //GiveUpSecurityAndAcceptAnySshHostKey = true,
                    //SshHostKeyFingerprint = "ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx..."

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
                throw;
            }
            return valida_envio;
        }




        #endregion

    }
}
