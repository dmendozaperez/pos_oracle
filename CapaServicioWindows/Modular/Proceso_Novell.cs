using CapaServicioWindows.CapaDato.Novell;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CapaServicioWindows.Modular
{
    public class Proceso_Novell
    {
        string ruta_temp_DBF= System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//tmpDBF";
        public void procesos_novell(ref string error)
        {
            
            try
            {
                error = envia_paquete_novell();
            }
            catch (Exception exc)
            {
                error = exc.Message;
                
            }
        }
        private string envia_paquete_novell()
        {
            string error = "";
            Dat_Proc_Novell proc_nov = null;
            DataTable dt_tda = null;
            try
            {
                proc_nov = new Dat_Proc_Novell();
                dt_tda = proc_nov.dt_get_envio_novell();
                if (dt_tda!=null)
                {
                    foreach(DataRow fila in dt_tda.Rows)
                    {
                        string cod_tda = fila["tienda"].ToString();
                        DateTime fec_cie =Convert.ToDateTime(fila["fecha"]);



                        DataSet ds = proc_nov.GET_OBTENER_VENTA_XSTORE(cod_tda, fec_cie);

                        if (ds!=null)
                        { 
                            if (ds.Tables[0].Rows.Count>0)
                            {
                                tabla_FFACTC(ds.Tables[0]);
                                tabla_FFACTD(ds.Tables[1]);
                                tabla_FNOTAA(ds.Tables[2]);
                                tabla_FSTKG(ds.Tables[3]);
                                tabla_FCIERR(ds.Tables[4]);
                                tabla_FFLASH(ds.Tables[5]);

                                /*TABLA MOVIMIENTO*/
                                tabla_FMC(ds.Tables[6]);
                                tabla_FMD(ds.Tables[7]);
                                /**/

                                string archivo = "";
                                byte[] file_bytes = null;
                                _comprimir_archivo(cod_tda, fec_cie, ref archivo,ref file_bytes);

                                string envio_paquete = enviar_paquete(cod_tda, file_bytes, archivo);

                                /*si el envio es exitoso entonces modificamos el envio*/
                                if (envio_paquete.Length==0)
                                {
                                    proc_nov.update_system_envio(cod_tda, fec_cie);
                                }    

                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
                
            }
            return error;
        }
        private string enviar_paquete(string cod_tda,byte[] archivo,string nom_archivo)
        {
            string enviar = "";
            try
            {
                BataTrans.Autenticacion conexion = new BataTrans.Autenticacion();
                conexion.user_name = "emcomer";
                conexion.user_password = "Bata2013";

                BataTrans.bata_transaccionSoapClient trans = new BataTrans.bata_transaccionSoapClient();

                String[] _mensaje = trans.ws_transmision_ingreso(conexion, archivo, nom_archivo);

                if (_mensaje[0] == "0") enviar = "error";

            }
            catch(Exception exc)
            {
                enviar = exc.Message;
            }
            return enviar;

        }
        private void _comprimir_archivo(string codTda, DateTime _fecha_proceso, ref string _archivo,ref byte[] file_bytes)
        {
            ZipOutputStream zipOut = null;
            try
            {
                string _path_envia = ruta_temp_DBF;

                string _comprimir = _path_envia + "\\Comp";

                if (!Directory.Exists(@_comprimir))
                    Directory.CreateDirectory(@_comprimir);

                string _ano = _fecha_proceso.ToString("yy");
                string _mes = _fecha_proceso.Month.ToString("D2");
                string _dia = _fecha_proceso.Day.ToString("D2");
                string _fecha = _ano + _mes + _dia;
                String[] filenames = Directory.GetFiles(_path_envia, "*.*");
                _archivo = "TD" + _fecha + "." + codTda.Substring(2, 3);
                string ruta_zip = @_comprimir + "\\TD" + _fecha + "." + codTda.Substring(2, 3);

                string[] _file_cmp = Directory.GetFiles(_comprimir, "*.*");
                //foreach (string f in _file_cmp)
                //{
                //    File.Delete(f.ToString());
                //}

                //if (File.Exists(ruta_zip))
                //{
                //    File.Delete(ruta_zip);
                //}

                //crear archivo zip
                zipOut = new ZipOutputStream(File.Create(@ruta_zip));

                //*********************               

                for (Int32 i = 0; i < filenames.Length; ++i)
                {
                    string _archivo_xml = filenames[i].ToString();
                    FileInfo fi = new FileInfo(_archivo_xml);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fi.Name);
                    FileStream sReader = File.OpenRead(_archivo_xml);
                    byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
                    sReader.Read(buff, 0, (int)sReader.Length);
                    entry.DateTime = fi.LastWriteTime;
                    entry.Size = sReader.Length;
                    sReader.Close();
                    zipOut.PutNextEntry(entry);
                    zipOut.Write(buff, 0, buff.Length);
                }

                zipOut.Finish();
                zipOut.Close();

                file_bytes = File.ReadAllBytes(@ruta_zip);

                string[] _file = Directory.GetFiles(_path_envia, "*.*");
                foreach (string f in _file)
                {
                    File.Delete(f.ToString());
                }



            }
            catch
            {
                if (zipOut != null)
                {
                    zipOut.Finish();
                    zipOut.Close();
                }
                throw;
            }
        }
        private void tabla_FFACTC(DataTable dt)
        {
            try
            {
                string _path_envia = ruta_temp_DBF;
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
        private void tabla_FFACTD(DataTable dt)
        {
            try
            {
                string _path_envia = ruta_temp_DBF;
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
                string _path_envia = ruta_temp_DBF;
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

                    string _path_envia = ruta_temp_DBF;

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
        private void tabla_FCIERR(DataTable dt)
        {
            try
            {
                string _path_envia = ruta_temp_DBF;
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
        private void tabla_FFLASH(DataTable dt)
        {
            try
            {
                string _path_envia = ruta_temp_DBF;
                DBFNET fflash = new DBFNET();
                fflash.tabla = "FFLASH";

                fflash.addcol("fc_ctda", Tipo.Caracter, "3");
                fflash.addcol("fc_fech", Tipo.Fecha);
                fflash.addcol("fc_nsem", Tipo.Numerico, "2,0");
                fflash.addcol("fc_ndse", Tipo.Numerico, "2,0");
                fflash.addcol("fc_sica", Tipo.Numerico, "10,0");
                fflash.addcol("fc_vund", Tipo.Numerico, "4,0");
                fflash.addcol("fc_vimp", Tipo.Numerico, "10,0");
                fflash.addcol("fc_gtos", Tipo.Numerico, "10,0");
                fflash.addcol("fc_rete", Tipo.Numerico, "10,0");
                fflash.addcol("fc_depo", Tipo.Numerico, "10,0");
                fflash.addcol("fc_bcod", Tipo.Caracter, "2");
                fflash.addcol("fc_difd", Tipo.Numerico, "10,0");
                fflash.addcol("fc_motd", Tipo.Caracter, "40");
                fflash.addcol("fc_depd", Tipo.Numerico, "10,0");
                fflash.addcol("fc_bcdo", Tipo.Caracter, "2");
                fflash.addcol("fc_pdes", Tipo.Numerico, "10,0");
                fflash.addcol("fc_pbcs", Tipo.Caracter, "2");
                fflash.addcol("fc_pded", Tipo.Numerico, "10,0");
                fflash.addcol("fc_pbcd", Tipo.Caracter, "2");
                fflash.addcol("fc_ftrx", Tipo.Fecha);
                fflash.addcol("fc_resp", Tipo.Caracter, "3");

                fflash.creardbf(_path_envia);
                fflash.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }
        private void tabla_FMC(DataTable dt)
        {
            try
            {
                string _path_envia = ruta_temp_DBF;
                DBFNET FMC = new DBFNET();
                FMC.tabla = "FMC";

                FMC.addcol("v_proc", Tipo.Caracter, "1");
                FMC.addcol("v_cfor", Tipo.Caracter, "2");
                FMC.addcol("v_sfor", Tipo.Caracter, "4");
                FMC.addcol("v_nfor", Tipo.Caracter, "8");
                FMC.addcol("v_ffor", Tipo.Fecha);
                FMC.addcol("v_mone", Tipo.Caracter, "2");
                FMC.addcol("v_tasa", Tipo.Numerico, "14,4");
                FMC.addcol("v_almo", Tipo.Caracter, "4");
                FMC.addcol("v_almd", Tipo.Caracter, "4");
                FMC.addcol("v_tane", Tipo.Caracter, "2");
                FMC.addcol("v_anex", Tipo.Caracter, "8");
                FMC.addcol("v_tdoc", Tipo.Caracter, "2");
                FMC.addcol("v_tfor", Tipo.Caracter, "1");                                                                                               
                
                FMC.addcol("v_suna", Tipo.Caracter, "2");
                FMC.addcol("v_sdoc", Tipo.Caracter, "4");
                FMC.addcol("v_ndoc", Tipo.Caracter, "8");
                FMC.addcol("v_fdoc", Tipo.Fecha);
                FMC.addcol("v_tref", Tipo.Caracter, "2");
                FMC.addcol("v_sref", Tipo.Caracter, "4");
                FMC.addcol("v_nref", Tipo.Caracter, "8");
                FMC.addcol("v_tipo", Tipo.Caracter, "1");
                FMC.addcol("v_arti", Tipo.Caracter, "12");
                FMC.addcol("v_regl", Tipo.Caracter, "4");
                FMC.addcol("v_colo", Tipo.Caracter, "2");
                FMC.addcol("v_cant", Tipo.Numerico, "14,4");
                FMC.addcol("v_pres", Tipo.Numerico, "14,4");
                FMC.addcol("v_pred", Tipo.Numerico, "14,4");
                FMC.addcol("v_vvts", Tipo.Numerico, "14,4");
                FMC.addcol("v_vvtd", Tipo.Numerico, "14,4");
                FMC.addcol("v_auto", Tipo.Caracter, "10");
                FMC.addcol("v_ptot", Tipo.Numerico, "14,4");
                FMC.addcol("v_impr", Tipo.Caracter, "1");
                FMC.addcol("v_cuse", Tipo.Caracter, "3");
                FMC.addcol("v_muse", Tipo.Caracter, "3");
                FMC.addcol("v_fcre",Tipo.Fecha);
                FMC.addcol("v_fmod", Tipo.Fecha);
                FMC.addcol("v_ftrx", Tipo.Fecha);
                FMC.addcol("v_ctra", Tipo.Caracter, "30");
                FMC.addcol("v_memo", Tipo.Caracter, "10");
                FMC.addcol("v_motr", Tipo.Caracter, "2");
                FMC.addcol("v_par1", Tipo.Caracter, "80");

                FMC.addcol("v_par2", Tipo.Caracter, "80");
                FMC.addcol("v_par3", Tipo.Caracter, "80");
                FMC.addcol("v_lle1", Tipo.Caracter, "80");
                FMC.addcol("v_lle2", Tipo.Caracter, "80");
                FMC.addcol("v_lle3", Tipo.Caracter, "80");
                FMC.addcol("v_tipe", Tipo.Caracter, "2");
                FMC.addcol("v_ruc2", Tipo.Caracter, "15");
                FMC.addcol("v_rzo2", Tipo.Caracter, "40");


                FMC.creardbf(_path_envia);
                FMC.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }
        private void tabla_FMD(DataTable dt)
        {
            try
            {
                string _path_envia = ruta_temp_DBF;
                DBFNET FMD = new DBFNET();
                FMD.tabla = "FMD";

                FMD.addcol("i_tfor", Tipo.Caracter, "1");
                FMD.addcol("i_proc", Tipo.Caracter, "1");
                FMD.addcol("i_cfor", Tipo.Caracter, "2");
                FMD.addcol("i_sfor", Tipo.Caracter, "4");
                FMD.addcol("i_nfor", Tipo.Caracter, "8");
                FMD.addcol("i_tipo", Tipo.Caracter, "1");
                FMD.addcol("i_arti", Tipo.Caracter, "12");
                FMD.addcol("i_regl", Tipo.Caracter, "4");
                FMD.addcol("i_colo", Tipo.Caracter, "2");
                FMD.addcol("i_item", Tipo.Caracter, "3");
                FMD.addcol("i_unic", Tipo.Caracter, "3");
                FMD.addcol("i_equ1", Tipo.Numerico, "10,4");
                FMD.addcol("i_unim", Tipo.Caracter, "3");
                             
                
                FMD.addcol("i_canc", Tipo.Numerico, "14,4");
                FMD.addcol("i_canm", Tipo.Numerico, "14,4");
                FMD.addcol("i_pres", Tipo.Numerico, "14,4");
                FMD.addcol("i_pred", Tipo.Numerico, "14,4");
                FMD.addcol("i_vvts", Tipo.Numerico, "14,4");
                FMD.addcol("i_vvtd", Tipo.Numerico, "14,4");
                FMD.addcol("i_plis", Tipo.Numerico, "14,4");
                FMD.addcol("i_ptot", Tipo.Numerico, "14,4");
                FMD.addcol("i_impr", Tipo.Caracter, "1");
                FMD.addcol("i_cuse", Tipo.Caracter, "3");
                FMD.addcol("i_muse", Tipo.Caracter, "3");
                FMD.addcol("i_fcre", Tipo.Fecha);
                FMD.addcol("i_fmod", Tipo.Fecha);

                FMD.creardbf(_path_envia);
                FMD.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }
    }
}
