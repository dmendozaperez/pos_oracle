//using CapaServicioWindows.Modular;
using CapaServicioWindows.CapaDato.Venta;
using CapaDato.Basico;
using CapaDato.Tienda;
using CapaServicioWindows.Modular;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ICSharpCode.SharpZipLib.Zip;

namespace ServiceWinTransaction
{
    public partial class Service_Transaction : ServiceBase
    {
        Timer tmservicio = null;
        Timer tmservicioDBF = null;
        private Int32 _valida_service = 0;

        public Service_Transaction()
        {
            InitializeComponent();
            //5000=5 segundos
            tmservicio = new Timer(5000);
            tmservicio.Elapsed += new ElapsedEventHandler(tmpServicio_Elapsed);

            tmservicioDBF = new Timer(5000);
            tmservicioDBF.Elapsed += new ElapsedEventHandler(tmpServicioDBF_Elapsed);
        }
        void tmpServicio_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;
           
            string _ruta_erro_file = @"D:\BataTransaction\ERROR_WS.txt";
            string _valida_proc_venta = @"D:\venta.txt";
            Boolean proceso_venta = false;
            try
            {
                //string _error = "ing";
                //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                //tw.WriteLine(_error);
                //tw.Flush();
                //tw.Close();
                //tw.Dispose();

                /*si el archivo existe entonces ejecutar procesos de venta*/
                if (File.Exists(@_valida_proc_venta)) proceso_venta = true;

                if (_valida_service == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_service = 1;

                    //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    //tw.WriteLine(_error);
                    //tw.Flush();
                    //tw.Close();
                    //tw.Dispose();
                    string _error_ws = "";
                    //_error = CapaServicioWindows.Modular.Basico.retornar();

                    #region<SOLO PARA ALMACEN HABILITAR ESTE PROCESO>
                    if (!proceso_venta)
                    { 
                        Basico ejecuta_procesos = null;
                        ejecuta_procesos = new Basico();
                        ejecuta_procesos.eje_envio_guias(ref _error_ws);
                    }
                    #endregion


                    #region <PROCESAMIENTO DE VENTAS DE TIENDA>
                    if (proceso_venta)
                    { 
                        Dat_Venta ejecuta_proc_venta = null;
                        ejecuta_proc_venta = new Dat_Venta();
                        ejecuta_proc_venta.procesar_ventas_SQL(ref _error_ws);
                    }
                    #endregion

                    _valida_service = 0;

                    if (_error_ws.Length>0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        tw.WriteLine(_error_ws);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }

                    //if (_error.Length > 0)
                    //{
                    //    TextWriter tw1 = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                    //    tw1.WriteLine(_error);
                    //    tw1.Flush();
                    //    tw1.Close();
                    //    tw1.Dispose();
                    //}
                }
                //****************************************************************************
            }
            catch(Exception exc)
            {
                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                tw.WriteLine(exc.Message);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                //tw.WriteLine(exc.Message);
                //tw.Flush();
                //tw.Close();
                //tw.Dispose();
                _valida_service = 0;
            }

            if (_valor == 1)
            {
                _valida_service = 0;
            }


        }

        void tmpServicioDBF_Elapsed(object sender, ElapsedEventArgs e)
        {

            CapaServicioWindows.Modular.Util util = new CapaServicioWindows.Modular.Util();
            Dat_Util datUtil = new Dat_Util();
            string carpetatienda = datUtil.get_ruta_locationProcesa_dbf("VENTA");
            string carpetadbf = carpetatienda + "\\DBF";
            string _valida_proc_dbf = carpetatienda + "\\dbf.txt";
            Boolean proceso_insertDBF = false;

            try
            {

                if (!File.Exists(_valida_proc_dbf)) proceso_insertDBF = true;

                if (proceso_insertDBF)
                {
                    string strCodTienda = "";
                                     
                    #region <PROCESAMIENTO DBF DE VENTAS DE TIENDA>
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
                                    else {
                                        string errSw2 = "";
                                        util.control_errores_transac("06", strRespuesta , ref errSw2);
                                    }

                                }
                                else {
                                    
                                    string errSw = "";
                                    util.control_errores_transac("06", verror+"==>50"+ strCodTienda, ref errSw);                                   
                                }
                            }

                        }

                    #endregion
                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                string errSwc = "";
                util.control_errores_transac("06", exc.Message, ref errSwc);

            }


        }

        protected override void OnStart(string[] args)
        {
            tmservicio.Start();
        }

        protected override void OnStop()
        {
            tmservicio.Stop();
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
