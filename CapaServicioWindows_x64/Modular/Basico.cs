using CapaServicioWindows_x64.CapaDato.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using WinSCP;

namespace CapaServicioWindows_x64.Modular
{
    public class Basico
    {
        private DateTime fecha_despacho = DateTime.Today.AddDays(-25);
        private string gHostName = "172.16.24.216";
        private string gUserName = "webposbpe";
        private string gPassword = "JU737CbDmJvu";
        private int gPortNumber = 22;
        private string gftp_ruta_destino = "";
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
                    string pais = dtLista.Rows[i]["PAIS"].ToString();
                    gHostName = dtLista.Rows[i]["FTP_SERVER"].ToString();
                    gUserName = dtLista.Rows[i]["FTP_USER"].ToString();
                    gPassword = dtLista.Rows[i]["FTP_PASSWORD"].ToString();
                    gPortNumber = Convert.ToInt32(dtLista.Rows[i]["FTP_PORT"]);
                    gftp_ruta_destino = dtLista.Rows[i]["FTP_PATH"].ToString();

                    generainter_inv_doc(strOrigen, strDocumento, strDestino, "S", pais);

                }

            }
            catch (Exception)
            {

            }
        }


        private void generainter_inv_doc(string cod_alm, string nro_guia, string cod_tda, string strEnviaFtp, string pais)
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

                ds = venta_ing.get_inv_doc(cod_alm, nro_guia, pais);

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
                if (strEnviaFtp.Equals("S"))
                {

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

    }
}
