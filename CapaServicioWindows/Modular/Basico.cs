﻿using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using CapaServicioWindows.Conexion;
using CapaServicioWindows.Envio_Ws;

namespace CapaServicioWindows.Modular
{
    public class Basico
    {
        private DateTime fecha_despacho = DateTime.Today.AddDays(-1);
        /// <summary>
        /// verificar las guias cerradas
        /// </summary>
        /// <returns></returns>
        private List<BataTransac.Ent_Scdddes> get_scdddes(string _path,ref string error)
        {
            /*fecha para traer los pedido cerrados desde una fecha*/
            List<BataTransac.Ent_Scdddes> _lista_scdddes = null;
            String sqlquery_scdddes = "SELECT [DDES_TIPO],[DDES_ALMAC],[DDES_GUIRE],[DDES_NDESP],[DDES_MFDES],[DDES_DESTI]," +
                                      "[DDES_N_INI],[DDES_N_FIN],[DDES_CPAGO],[DDES_FEMBA],[DDES_FECHA],[DDES_FDESP],[DDES_ESTAD]," +
                                      "[DDES_GGUIA],[DDES_CCOND],[DDES_CALZ],[DDES_NCALZ],[DDES_TOCAJ],[DDES_IMPRE],[DDES_GVALO]," +
                                      "[DDES_SUBGR],[DDES_RUCTC],[DDES_TRANS],[DDES_TRAN2],[DDES_OBSER],[DDES_NOMTC],[DDES_NGUIA]," +
                                      "[DDES_NRLIQ],[DDES_LIMPR],[DDES_EMPRE],[DDES_CANAL],[DDES_CADEN],[DDES_SECCI],[DDES_FTX],[DDES_FTXTD]" +
                                      "FROM [SCDDDES] WHERE DDES_FDESP>=? and DDES_ESTAD='C' and (DDES_FTXTD IS NULL OR LEN(DDES_FTXTD)=0)";            
            try
            {
                //Util dd = new Util();
                //dd.get_location_dbf();

                using (OleDbConnection cn = new OleDbConnection(ConexionDBF._conexion_fvdes_oledb(_path)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sqlquery_scdddes, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("DATE", OleDbType.Date).Value = fecha_despacho;
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
        /// <summary>
        /// cebezera de guias para enviar
        /// </summary>
        /// <param name="nroguia"></param>
        /// <returns></returns>
        private BataTransac.Ent_Fvdespc get_fvdespc(string codalm, string nroguia,string _path, ref string error)
        {
            BataTransac.Ent_Fvdespc fvdespc = null;
            String sqlquery_fvdespc = "SELECT [DESC_ALMAC],[DESC_GUDIS],[DESC_NDESP],[DESC_TDES],[DESC_FECHA],[DESC_FDESP]," +
                                      "[DESC_ESTAD],[DESC_TIPO],[DESC_TORI],[DESC_FEMI],[DESC_SEMI],[DESC_FTRA],[DESC_NUME]," +
                                      "[DESC_CONCE],[DESC_NMOVC],[DESC_EMPRE],[DESC_SECCI],[DESC_CANAL],[DESC_CADEN],[DESC_FTX]," +
                                      "[DESC_TXPOS] FROM [FVDESPC] WHERE DESC_GUDIS='" + nroguia + "' AND DESC_ALMAC='" + codalm +"'";
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
                                           DESC_FECHA = Convert.ToDateTime(dr["DESC_FECHA"]),
                                           DESC_FDESP = Convert.ToDateTime(dr["DESC_FDESP"]),// se supone que es la fecha despacho
                                           DESC_ESTAD = dr["DESC_ESTAD"].ToString(),
                                           DESC_TIPO = dr["DESC_TIPO"].ToString(),
                                           DESC_TORI = dr["DESC_TORI"].ToString(),
                                           DESC_FEMI = Convert.ToDateTime(dr["DESC_FEMI"]),
                                           DESC_SEMI = dr["DESC_SEMI"].ToString(),
                                           DESC_FTRA = Convert.ToDateTime(dr["DESC_FTRA"]),
                                           DESC_NUME = dr["DESC_NUME"].ToString(),
                                           DESC_CONCE = dr["DESC_CONCE"].ToString(),
                                           DESC_NMOVC = dr["DESC_NMOVC"].ToString(),
                                           DESC_EMPRE = dr["DESC_EMPRE"].ToString(),
                                           DESC_SECCI = dr["DESC_SECCI"].ToString(),
                                           DESC_CANAL = dr["DESC_CANAL"].ToString(),
                                           DESC_CADEN = dr["DESC_CADEN"].ToString(),
                                           DESC_FTX = dr["DESC_FTX"].ToString(),
                                           DESC_TXPOS = dr["DESC_TXPOS"].ToString(),
                                       }).ToList();


                            fvdespc = list_fvdespc[0];
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
            string sqlquery_fvdespd = "SELECT [DESD_TIPO],[DESD_GUDIS],[DESD_NDESP],[DESD_ALMAC],[DESD_ARTIC],[DESD_CALID]," +
                                       "DESD_ME00,DESD_ME01,DESD_ME02,DESD_ME03,DESD_ME04,DESD_ME05,DESD_ME06,DESD_ME07,DESD_ME08," + 
                                       "DESD_ME09,DESD_ME10,DESD_ME11,[DESD_CLASE],[DESD_MERC],[DESD_CATEG],[DESD_SUBCA]," + 
                                       "[DESD_MARCA],[DESD_MERC3],[DESD_CATE3],[DESD_SUBC3],[DESD_MARC3],[DESD_CNDME]," + 
                                       "[DESD_EMPRE],[DESD_SECCI],[DESD_CANAL]," +
                                       "[DESD_CADEN],[DESD_GGUIA],[DESD_ESTAD] FROM [FVDESPD] WHERE DESD_GUDIS='" + nroguia + "'" +
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
        public string eje_envio_guias()
        {
            string _envio_guias ="";
            List<BataTransac.Ent_Scdddes> _lista_guiasC = null;

            List<BataTransac.Ent_PathDBF> listar_location_dbf = null;

            try
            {
                #region<CAPTURAR EL PATH DE LOS DBF>
                Util locationdbf = new Util();
                listar_location_dbf= locationdbf.get_location_dbf();
                #endregion

                string name_dbf = "SCDDDES";
                var _locatio_scdddes = listar_location_dbf.Where(x => x.rutloc_namedbf==name_dbf).FirstOrDefault();

                string _error = "";

                _lista_guiasC = get_scdddes(_locatio_scdddes.rutloc_location,ref _error);

                /*VERIFICAR SI HAY ERROR*/
                if (_error.Length>0)
                {
                    _error += " ==>TABLA [SCDDDES]";
                    Util ws_error_transac = new Util();
                    /*si hay un error entonces 03 error de lectura dbf*/
                    ws_error_transac.control_errores_transac("03", _error);
                }
                /**/
               
                if (_lista_guiasC!=null)
                {
                    foreach(BataTransac.Ent_Scdddes filaC in _lista_guiasC)
                    {
                        _error = "";
                        name_dbf = "FVDESPC";
                        var _location_fvdespc = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                        BataTransac.Ent_Fvdespc fvdespc= get_fvdespc(filaC.DDES_ALMAC,filaC.DDES_GUIRE, _location_fvdespc.rutloc_location, ref _error);

                        /*VERIFICAR SI HAY ERROR*/
                        if (_error.Length > 0)
                        {
                            _error += " ==>TABLA [FVDESPC]";
                            Util ws_error_transac = new Util();
                            /*si hay un error entonces 03 error de lectura dbf*/
                            ws_error_transac.control_errores_transac("03", _error);
                        }
                        /**/

                        /*captura la cebecera de la guia*/
                        if (fvdespc!=null)
                        {
                            _error = "";
                            name_dbf = "FVDESPD";
                            var _location_fvdespd = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                            DataTable fvdespd = get_fvdespd(filaC.DDES_ALMAC,filaC.DDES_GUIRE, _location_fvdespd.rutloc_location,ref _error);

                            /*VERIFICAR SI HAY ERROR*/
                            if (_error.Length > 0)
                            {
                                _error += " ==>TABLA [FVDESPD]";
                                Util ws_error_transac = new Util();
                                /*si hay un error entonces 03 error de lectura dbf*/
                                ws_error_transac.control_errores_transac("03", _error);
                            }
                            /**/

                            /*verifica que el detalle de la guia tenga filas*/
                            /*si la guias tiene detalle se envia por ws*/

                            if (fvdespd!=null)
                            {
                                if (fvdespd.Rows.Count>0)
                                {
                                    /*enviar el datatable*/
                                    fvdespc.DT_FVDESPD_TREGMEDIDA = fvdespd;

                                    BataTransac.Ent_Scdddes scdddes = new BataTransac.Ent_Scdddes();
                                    scdddes = filaC;

                                    /***********************************/
                                    Envio_Guias ws_envio = new Envio_Guias();
                                    string envio_guias_ws = ws_envio.envio_ws_guias(fvdespc, scdddes);
                                    //si return es true entonces validamos los dbf
                                    if (envio_guias_ws.Length==0)
                                    {
                                        name_dbf = "SCDDDES";
                                        var _locatio_scdddes_edit = listar_location_dbf.Where(x => x.rutloc_namedbf == name_dbf).FirstOrDefault();
                                        /*si es que las guias se grabaron correctamente entonces vamos a setear el valor en el dbf*/
                                        edit_scdddes(fvdespc.DESC_ALMAC, fvdespc.DESC_GUDIS, _locatio_scdddes_edit.rutloc_location);                                           
                                    }
                                    else
                                    {
                                        Util ws_error_transac = new Util();
                                        /*si hay un error entonces 02 error de transaction*/
                                        ws_error_transac.control_errores_transac("02", envio_guias_ws);
                                    }
                                }
                            }

                        }

                        
                    }
                }

            }
            catch (Exception)
            {
                _envio_guias = "";
            }
            return _envio_guias;
        }


        public void edit_scdddes(string cod_alm,string nroguia,string _path)
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
                    catch (Exception)
                    {
                        if (cn!=null)
                        if (cn.State == ConnectionState.Open) cn.Close();                       
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch
            {
                               
            }
        }
    }
}
