//using CapaServicioWindows.Modular;
using CapaServicioWindows.CapaDato.Venta;
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

namespace ServiceWinTransaction
{
    public partial class Service_Transaction : ServiceBase
    {
        Timer tmservicio = null;
        private Int32 _valida_service = 0;
        public Service_Transaction()
        {
            InitializeComponent();
            //5000=5 segundos
            tmservicio = new Timer(5000);
            tmservicio.Elapsed += new ElapsedEventHandler(tmpServicio_Elapsed);
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
        protected override void OnStart(string[] args)
        {
            tmservicio.Start();
        }

        protected override void OnStop()
        {
            tmservicio.Stop();
        }
    }
}
