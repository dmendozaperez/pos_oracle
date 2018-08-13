using CapaDato.Basico;
using CapaDato.Tienda;
using CapaDato.Venta;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceWSPrueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wsconsulta.validateLogin la = new wsconsulta.validateLogin();
            la.Username = "BataPeru";
            la.Password = "Bata2018**.";

            wsconsulta.Sunat_Reniec_PESoapClient c = new wsconsulta.Sunat_Reniec_PESoapClient();
            var r = c.ws_persona_sunat(la, "20101951872");

            //wsconsulta.Cons_ClienteSoapClient c = new wsconsulta.Cons_ClienteSoapClient();
            //DataTable r = c.ws_persona_reniec("41149120");

            string num = "";

         

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int32 _valor = 0;
            Dat_Util datUtil = new Dat_Util();
            string carpetatienda = datUtil.get_Ruta_locationProcesa_dbf("VENTA");
             string carpetadbf = carpetatienda + "\\DBF";
            string _ruta_erro_file = carpetatienda + "\\ERROR_DBF.txt";
            string _valida_proc_dbf = carpetatienda + "\\dbf.txt";
            Boolean proceso_insertDBF = false;
        
            int _valida_service = 0;

            try
            {

                if (!File.Exists(_valida_proc_dbf)) proceso_insertDBF = true;
             
                if (_valida_service == 0)
                {
                    string strCodTienda = "";
                    _valor = 1;
                    _valida_service = 1;
                    string _error_ws = "";

                    #region <PROCESAMIENTO DBF DE VENTAS DE TIENDA>

                    if (proceso_insertDBF)
                    {
                        if ((Directory.Exists(carpetatienda)))
                        {
                            string[] filesborrar;
                            string verror = "";
                            filesborrar = System.IO.Directory.GetFiles(@carpetatienda, "*.*");

                            if (!(Directory.Exists(@carpetadbf)))
                            {
                                System.IO.Directory.CreateDirectory(@carpetadbf);
                            }

                            string[] filePaths = Directory.GetFiles(@carpetadbf);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);

                            for (Int32 iborrar = 0; iborrar < filesborrar.Length; ++iborrar)
                            {

                                String value = filesborrar[iborrar].ToString();
                                Char delimiter = '.';
                                String[] substrings = value.Split(delimiter);
                                strCodTienda = substrings[1].ToString();
                                
                                verror = descomprimir(filesborrar[iborrar].ToString(), @carpetadbf);

                                if (verror.Length == 0)
                                {
                                    string strRespuesta = datUtil.LeerDataDBF_TemporalVenta(strCodTienda, @carpetadbf);
                                    if (strRespuesta == "S")
                                    {
                                        System.IO.File.Delete(@filesborrar[iborrar].ToString());
                                    }

                                }
                                                                                              
                            }

                        }
                        
                    }
                    #endregion

                    _valida_service = 0;

                    if (_error_ws.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        tw.WriteLine(_error_ws);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }

                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                tw.WriteLine(exc.Message);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                _valida_service = 0;
            }

            if (_valor == 1)
            {
                _valida_service = 0;
            }

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
    }
}
