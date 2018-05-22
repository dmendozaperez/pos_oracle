//using CapaServicioWindows.Modular;
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
            tmservicio = new Timer(20000);
            tmservicio.Elapsed += new ElapsedEventHandler(tmpServicio_Elapsed);
        }
        void tmpServicio_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;
            Basico ejecuta_procesos = null;
            try
            {
                //string _error = "ing";
                //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                //tw.WriteLine(_error);
                //tw.Flush();
                //tw.Close();
                //tw.Dispose();

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

                    //_error = CapaServicioWindows.Modular.Basico.retornar();
                    ejecuta_procesos = new Basico();
                    ejecuta_procesos.eje_envio_guias();

                    _valida_service = 0;

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
