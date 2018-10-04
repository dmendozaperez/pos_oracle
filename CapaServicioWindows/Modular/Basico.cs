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

namespace CapaServicioWindows.Modular
{
    public class Basico
    {
        private DateTime fecha_despacho = DateTime.Today.AddDays(-25);

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
                                      "FROM SCDDDES WHERE DDES_FDESP>=CTOD('" + fecha_despacho.ToString("MM/dd/yy")  + "') and DDES_TIPO='DES' and DDES_ESTAD<>'A' AND EMPTY(DDES_FTXTD) AND (NOT EMPTY(DDES_CADEN)) ";            
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
                error = exc.Message;
                _lista_scdddes = null;
            }
            return _lista_scdddes;
        }

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
        private BataTransac.Ent_Fvdespc get_fvdespc(string codalm, string nroguia,string _path, ref string error,ref Boolean fila_existe)
        {
            BataTransac.Ent_Fvdespc fvdespc = null;
            String sqlquery_fvdespc = "SELECT DESC_ALMAC,DESC_GUDIS,DESC_NDESP,DESC_TDES,DESC_FECHA,DESC_FDESP," +
                                      "DESC_ESTAD,DESC_TIPO,DESC_TORI,DESC_FEMI,DESC_SEMI,DESC_FTRA,DESC_NUME," +
                                      "DESC_CONCE,DESC_NMOVC,DESC_EMPRE,DESC_SECCI,DESC_CANAL,DESC_CADEN,DESC_FTX," +
                                      "DESC_TXPOS,DESC_UNCA,DESC_UNNC,DESC_CAJA,DESC_VACA,DESC_VANC,DESC_VCAJ " +
                                      "FROM FVDESPC WHERE DESC_GUDIS='" + nroguia + "' AND DESC_ALMAC='" + codalm +"'";
            try
            {
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
                                fvdespc = list_fvdespc[0];
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
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
        private DataTable get_fvdespd(string codalm,string nroguia, string _path, ref string error)
        {
            DataTable fvdespd = null;
            string sqlquery_fvdespd = "SELECT DESD_TIPO,DESD_GUDIS,DESD_NDESP,DESD_ALMAC,DESD_ARTIC,DESD_CALID," +
                                       "DESD_ME00,DESD_ME01,DESD_ME02,DESD_ME03,DESD_ME04,DESD_ME05,DESD_ME06,DESD_ME07,DESD_ME08," + 
                                       "DESD_ME09,DESD_ME10,DESD_ME11,DESD_CLASE,DESD_MERC,DESD_CATEG,DESD_SUBCA," + 
                                       "DESD_MARCA,DESD_MERC3,DESD_CATE3,DESD_SUBC3,DESD_MARC3,DESD_CNDME," + 
                                       "DESD_EMPRE,DESD_SECCI,DESD_CANAL," +
                                       "DESD_CADEN,DESD_GGUIA,DESD_ESTAD,DESD_PRVTA,DESD_COSTO FROM FVDESPD WHERE DESD_GUDIS='" + nroguia + "'" +
                                       " AND DESD_ALMAC='" + codalm + "'";
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

                #endregion

                //if (valida_exists_txt) return;


                string name_dbf = "SCDDDES";
                var _locatio_scdddes = listar_location_dbf.Where(x => x.rutloc_namedbf==name_dbf).FirstOrDefault();

                string _error = "";
                /*ya no entra a consultar*/
               // if (File.Exists(@ruta_validacion)) return;
                /**/
                _lista_guiasC = get_scdddes(_locatio_scdddes.rutloc_location,ref _error);

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
                    foreach (BataTransac.Ent_Scdddes filaC in _lista_guiasC)
                    {
                        //if (filaC.DDES_GUIRE == "1914315")
                        //{

                     

                        _error = "";
                        name_dbf = "FVDESPC";

                        /*verificar si  existe la guias en la tabla cab fv*/
                        Boolean existe_data = false;

                        var _location_fvdespc = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();

                        /*ya no entra a consultar*/
                       // if (File.Exists(@ruta_validacion)) return;
                        /**/

                        BataTransac.Ent_Fvdespc fvdespc = get_fvdespc(filaC.DDES_ALMAC, filaC.DDES_GUIRE, _location_fvdespc.rutloc_location, ref _error, ref existe_data);

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
                                var _location_fvdespd = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();

                                /*ya no entra a consultar*/
                             //   if (File.Exists(@ruta_validacion)) return;
                                /**/

                                DataTable fvdespd = get_fvdespd(filaC.DDES_ALMAC, filaC.DDES_GUIRE, _location_fvdespd.rutloc_location, ref _error);

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

                                        /***********************************/
                                        Envio_Guias ws_envio = new Envio_Guias();
                                        string envio_guias_ws = ws_envio.envio_ws_guias(fvdespc, scdddes);
                                        //si return es true entonces validamos los dbf
                                        if (envio_guias_ws.Length == 0)
                                        {
                                            name_dbf = "SCDDDES";
                                            var _locatio_scdddes_edit = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                                            /*si es que las guias se grabaron correctamente entonces vamos a setear el valor en el dbf*/

                                            /*ya no entra a consultar*/
                                           // if (File.Exists(@ruta_validacion)) return;
                                            /**/

                                            edit_scdddes(fvdespc.DESC_ALMAC, fvdespc.DESC_GUDIS, _locatio_scdddes_edit.rutloc_location,ref _error_ws);
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

        public void procesar_dbf_pos(ref string _error_procesos)
        {
            Util datUtil = null;
            Dat_Venta venta_ing = null;
            string _error = "";            
            try
            {
                datUtil = new Util();
                string carpetatienda = datUtil.get_ruta_locationProcesa_dbf("SQL");
                string carpetadbf = carpetatienda + "\\DBF";
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
                    Char delimiter = '.';
                    String[] substrings = value.Split(delimiter);
                    strCodTienda = substrings[1].ToString();
                    strCodTienda = "50" + strCodTienda;
                    _error = descomprimir(filespaquete[i].ToString(), @carpetadbf);
                    if (_error.Length == 0)
                    {
                        //DataSet ds_ventas= datUtil.get_ds_venta(@carpetadbf,ref _error);

                        if (_error.Length==0)
                        {

                            venta_ing = new Dat_Venta();
                            _error= venta_ing.inserta_venta_dbf(strCodTienda);

                            if (_error.Length==0)
                            {
                                /*borramos paquete*/
                                if (File.Exists(@value))
                                    File.Delete(@value);
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
            catch (Exception)
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


        private void tabla_FCIERR(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET fcierr = new DBFNET();
                fcierr.tabla = "FCIERR";

                fcierr.addcol("Ci_csuc", Tipo.Caracter, "3");
                fcierr.addcol("Ci_fech", Tipo.Fecha);
                fcierr.addcol("Ci_esta", Tipo.Caracter, "1");
                fcierr.addcol("Ci_fetr", Tipo.Fecha);
                fcierr.addcol("Ci_cuse", Tipo.Caracter, "3");
                fcierr.addcol("Ci_muse", Tipo.Caracter, "3");
                fcierr.addcol("Ci_fcre", Tipo.Fecha);
                fcierr.addcol("Ci_fmod", Tipo.Fecha);
                fcierr.addcol("Ci_impz", Tipo.Caracter, "1");
                fcierr.addcol("Ci_aper", Tipo.Caracter, "30");
                fcierr.addcol("Ci_cier", Tipo.Caracter, "30");

                fcierr.creardbf(_path_envia);
                fcierr.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }

        private void tabla_FSTKG(DataTable dtstk)
        {
            try
            {
                StringBuilder str = null;
                string str_cadena = "";


                if (dtstk.Rows.Count > 0)
                {
                    str = new StringBuilder();
                    for (Int32 i = 0; i < dtstk.Rows.Count; ++i)
                    {
                        string cad_envio = "\"" + dtstk.Rows[i]["TIENDA"].ToString() + "\"" + ",\"" + dtstk.Rows[i]["ARTICULO"].ToString() + "\"" + "," +
                                           dtstk.Rows[i]["TOTAL"].ToString() + "," + dtstk.Rows[i]["00"].ToString() + "," +
                                           dtstk.Rows[i]["01"].ToString() + "," + dtstk.Rows[i]["02"].ToString() + "," +
                                           dtstk.Rows[i]["03"].ToString() + "," + dtstk.Rows[i]["04"].ToString() + "," +
                                           dtstk.Rows[i]["05"].ToString() + "," + dtstk.Rows[i]["06"].ToString() + "," +
                                           dtstk.Rows[i]["07"].ToString() + "," + dtstk.Rows[i]["08"].ToString() + "," +
                                           dtstk.Rows[i]["09"].ToString() + "," + dtstk.Rows[i]["10"].ToString() + "," +
                                           dtstk.Rows[i]["11"].ToString() + "," + dtstk.Rows[i]["FECHA"].ToString();

                        str.Append(cad_envio);

                        if (i < dtstk.Rows.Count - 1)
                        {
                            str.Append("\r\n");

                        }

                    }
                    str_cadena = str.ToString();

                    string _path_envia = basico.ruta_temp_DBF;

                    if (!Directory.Exists(_path_envia))
                        Directory.CreateDirectory(_path_envia);
                    string file_stk = "FSTKG";
                    string ruta_file_stk = _path_envia + "\\" + file_stk + ".txt";

                    if (File.Exists(@ruta_file_stk)) File.Delete(@ruta_file_stk);
                    File.WriteAllText(@ruta_file_stk, str_cadena);
                }

                //DBFNET venta_det = new DBFNET();
                //venta_det.creartxt_stk(_path_envia);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tabla_FFACTD(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET venta_det = new DBFNET();
                venta_det.tabla = "FFACTD";
                venta_det.addcol("fd_nint", Tipo.Caracter, "8");
                venta_det.addcol("fd_tipo", Tipo.Caracter, "1");
                venta_det.addcol("fd_arti", Tipo.Caracter, "12");
                venta_det.addcol("fd_regl", Tipo.Caracter, "4");
                venta_det.addcol("fd_colo", Tipo.Caracter, "2");
                venta_det.addcol("fd_item", Tipo.Caracter, "3");
                venta_det.addcol("fd_icmb", Tipo.Caracter, "1");
                venta_det.addcol("fd_qfac", Tipo.Numerico, "8,3");
                venta_det.addcol("fd_lpre", Tipo.Caracter, "2");
                venta_det.addcol("fd_calm", Tipo.Caracter, "4");
                venta_det.addcol("fd_pref", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_dref", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_prec", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_brut", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp1", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vimp1", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_subt1", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp2", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vimp2", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_subt2", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pdct1", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vdct1", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_subt3", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vdct4", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vdc23", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vvta", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp3", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vimp3", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp4", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vimp4", Tipo.Numerico, "14,4");

                venta_det.addcol("fd_total", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_cuse", Tipo.Caracter, "3");
                venta_det.addcol("fd_muse", Tipo.Caracter, "3");
                venta_det.addcol("fd_fcre", Tipo.Fecha);
                venta_det.addcol("fd_fmod", Tipo.Fecha);
                venta_det.addcol("fd_auto", Tipo.Caracter, "6");
                venta_det.addcol("fd_dre2", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_asoc", Tipo.Caracter, "13");

                venta_det.creardbf(_path_envia);
                venta_det.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }
        private void tabla_FNOTAA(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET venta_pago = new DBFNET();
                venta_pago.tabla = "FNOTAA";
                venta_pago.addcol("na_nota", Tipo.Caracter, "8");
                venta_pago.addcol("na_item", Tipo.Caracter, "3");
                venta_pago.addcol("na_mone", Tipo.Caracter, "2");
                venta_pago.addcol("na_tpag", Tipo.Caracter, "2");
                venta_pago.addcol("na_tasa", Tipo.Numerico, "10,4");
                venta_pago.addcol("na_cref", Tipo.Caracter, "2");
                venta_pago.addcol("na_sref", Tipo.Caracter, "4");
                venta_pago.addcol("na_nref", Tipo.Caracter, "22");
                venta_pago.addcol("na_vref", Tipo.Numerico, "14,4");
                venta_pago.addcol("na_vpag", Tipo.Numerico, "14,4");
                venta_pago.addcol("na_esta", Tipo.Caracter, "1");
                venta_pago.addcol("na_cier", Tipo.Caracter, "1");
                venta_pago.addcol("na_fcre", Tipo.Caracter, "30");
                venta_pago.addcol("na_fmod", Tipo.Caracter, "30");
                venta_pago.creardbf(_path_envia);
                venta_pago.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }
        private void tabla_FFACTC(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET venta_cab = new DBFNET();
                venta_cab.tabla = "FFACTC";
                venta_cab.addcol("fc_nint", Tipo.Caracter, "8");
                venta_cab.addcol("fc_nnot", Tipo.Caracter, "8");
                venta_cab.addcol("fc_codi", Tipo.Caracter, "2");
                venta_cab.addcol("fc_suna", Tipo.Caracter, "2");
                venta_cab.addcol("fc_sfac", Tipo.Caracter, "4");
                venta_cab.addcol("fc_nfac", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ffac", Tipo.Fecha);
                venta_cab.addcol("fc_nord", Tipo.Caracter, "8");
                venta_cab.addcol("fc_cref", Tipo.Caracter, "2");
                venta_cab.addcol("fc_sref", Tipo.Caracter, "4");
                venta_cab.addcol("fc_nref", Tipo.Caracter, "8");
                venta_cab.addcol("fc_pvta", Tipo.Caracter, "2");
                venta_cab.addcol("fc_csuc", Tipo.Caracter, "3");
                venta_cab.addcol("fc_gvta", Tipo.Caracter, "2");
                venta_cab.addcol("fc_zona", Tipo.Caracter, "3");
                venta_cab.addcol("fc_clie", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ncli", Tipo.Caracter, "90");
                venta_cab.addcol("fc_nomb", Tipo.Caracter, "30");
                venta_cab.addcol("fc_apep", Tipo.Caracter, "30");
                venta_cab.addcol("fc_apem", Tipo.Caracter, "30");
                venta_cab.addcol("fc_dcli", Tipo.Caracter, "50");
                venta_cab.addcol("fc_cubi", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ruc", Tipo.Caracter, "20");
                venta_cab.addcol("fc_vuse", Tipo.Caracter, "3");
                venta_cab.addcol("fc_vend", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ipre", Tipo.Caracter, "1");
                venta_cab.addcol("fc_tint", Tipo.Caracter, "2");
                venta_cab.addcol("fc_pint", Tipo.Numerico, "6,2");
                venta_cab.addcol("fc_lcsg", Tipo.Caracter, "2");
                venta_cab.addcol("fc_ncon", Tipo.Caracter, "30");
                venta_cab.addcol("fc_dcon", Tipo.Caracter, "30");
                venta_cab.addcol("fc_lcon", Tipo.Caracter, "20");
                venta_cab.addcol("fc_lruc", Tipo.Caracter, "11");
                venta_cab.addcol("fc_agen", Tipo.Caracter, "20");
                venta_cab.addcol("fc_mone", Tipo.Caracter, "2");
                venta_cab.addcol("fc_tasa", Tipo.Numerico, "10,4");
                venta_cab.addcol("fc_fpag", Tipo.Caracter, "2");

                venta_cab.addcol("fc_nlet", Tipo.Numerico, "2,0");
                venta_cab.addcol("fc_qtot", Tipo.Numerico, "8,2");
                venta_cab.addcol("fc_pref", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_dref", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_brut", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vimp1", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vimp2", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vdct1", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vdct4", Tipo.Numerico, "14,4");

                venta_cab.addcol("fc_pdc2", Tipo.Numerico, "6,2");
                venta_cab.addcol("fc_pdc3", Tipo.Numerico, "6,2");
                venta_cab.addcol("fc_vdc23", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vvta", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vimp3", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_pimp4", Tipo.Numerico, "6,2");

                venta_cab.addcol("fc_vimp4", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_total", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_esta", Tipo.Caracter, "1");
                venta_cab.addcol("fc_tdoc", Tipo.Caracter, "1");
                venta_cab.addcol("fc_cuse", Tipo.Caracter, "3");
                venta_cab.addcol("fc_muse", Tipo.Caracter, "3");

                venta_cab.addcol("fc_fcre", Tipo.Fecha);
                venta_cab.addcol("fc_fmod", Tipo.Fecha);

                venta_cab.addcol("fc_hora", Tipo.Caracter, "8");
                venta_cab.addcol("fc_auto", Tipo.Caracter, "3");
                venta_cab.addcol("fc_ftx", Tipo.Caracter, "1");
                venta_cab.addcol("fc_estc", Tipo.Caracter, "1");
                venta_cab.addcol("fc_sexo", Tipo.Caracter, "1");
                venta_cab.addcol("fc_mpub", Tipo.Caracter, "2");
                venta_cab.addcol("fc_edad", Tipo.Caracter, "2");
                venta_cab.addcol("fc_regv", Tipo.Caracter, "25");
                venta_cab.creardbf(_path_envia);
                venta_cab.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }


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

                #region<actualizacion de la tabla movimiento ventas y guias>

                venta_ing.procesar_ventas_movimiento();
                venta_ing.procesar_guias_movimiento();
                    
                #endregion


                if (_error_procesos.Length>0)
                {
                    datUtil.control_errores_transac("08", _error_procesos, ref _error_procesos);
                }

            }
            catch (Exception exc)
            {
                _error_procesos = exc.Message;                
            }
        }

        #endregion
    }
}
