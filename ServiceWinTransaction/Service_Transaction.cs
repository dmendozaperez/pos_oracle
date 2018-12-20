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
using ICSharpCode.SharpZipLib.Zip;
using CapaServicioWindows.Envio_Ftp_Xstore;

namespace ServiceWinTransaction
{
    public partial class Service_Transaction : ServiceBase
    {
        Timer tmservicio = null;
        Timer tmservicioDBF = null;
        Timer tmservicioVentaXstore = null;

        #region<generacion de interfaces y envio de sftp>
        Timer tmgenera_interface =null;
        private Int32 _valida_genera_interface = 0;

        Timer tmenvia_sftp = null;
        private Int32 _valida_envia_sftp = 0;

        #endregion


        private string file_almace_ecu = @"D:\BataTransaction\ECU.txt";

        Timer tmservicio_GuiaToXstore = null;
        //Timer tmservicioScactcoDBF = null;

        Timer tmservicio_trans = null;
        private Int32 _valida_service_trans = 0;

        Timer tmservicioposlog = null;

        private Int32 _valida_service = 0;
        private Int32 _valida_serviceDBF = 0;

        private Int32 _valida_ven_tmpDBF = 0;

        //private Int32 _valida_serviceScactcoDBF = 0;

        private Int32 _valida_serviceposlog = 0;

        private Int32 _valida_serviceVentaXstore = 0;

        private Int32 _valida_serviceGuiaToXstore = 0;

        public Service_Transaction()
        {
            InitializeComponent();
            //5000=5 segundos
            tmservicio = new Timer(5000);
            tmservicio.Elapsed += new ElapsedEventHandler(tmpServicio_Elapsed);

            tmservicioDBF = new Timer(5000);
            tmservicioDBF.Elapsed += new ElapsedEventHandler(tmpServicioDBF_Elapsed);

            //tmservicioScactcoDBF = new Timer(5000);
            //tmservicioScactcoDBF.Elapsed += new ElapsedEventHandler(tmpServicioScactcoDBF_Elapsed);

            tmservicioposlog = new Timer(5000);
            tmservicioposlog.Elapsed += new ElapsedEventHandler(tmservicioposlog_Elapsed);

            tmservicio_trans = new Timer(5000);
            tmservicio_trans.Elapsed += new ElapsedEventHandler(tmservicio_trans_Elapsed);


            tmservicio_GuiaToXstore = new Timer(5000);
            tmservicio_GuiaToXstore.Elapsed += new ElapsedEventHandler(tmservicio_GuiaToXstore_Elapsed);

            tmgenera_interface = new Timer(5000);
            tmgenera_interface.Elapsed += new ElapsedEventHandler(tmgenera_interface_Elapsed);

            tmenvia_sftp = new Timer(5000);
            tmenvia_sftp.Elapsed += new ElapsedEventHandler(tmenvia_sftp_Elapsed);


        }

        #region<envio de interfaces automatico>
        void tmenvia_sftp_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion

                if (_valida_envia_sftp == 0)
                {
                    _valor = 1;
                    _valida_envia_sftp = 1;
                    string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    Boolean proceso_guiaToXstore = false;

                    if (File.Exists(_valida_proc_guiaToXstore)) proceso_guiaToXstore = true;

                    if (proceso_guiaToXstore)
                    {
                        _valor = 1;
                        string _error = "";
                        _valida_envia_sftp = 1;

                        Ftp_Xstore_Service_Send ejecuta_procesos = null;
                        ejecuta_procesos = new Ftp_Xstore_Service_Send();


                        ejecuta_procesos.proc_envio_ftp();
                        //proc_envio_ftp
                        //ejecuta_procesos.ejecutar_genera_file_xstore_auto(ref _error);

                        //ejecuta_procesos.envio_Guias_ToxStore(ref _error);

                        _valida_envia_sftp = 0;

                    }
                }

            }
            catch (Exception exc)
            {
                //string errSwc = "";
                _valida_envia_sftp = 0;
            }
            if (_valor == 1)
            {
                _valida_envia_sftp = 0;
            }
        }
        void tmgenera_interface_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion

                if (_valida_genera_interface == 0)
                {
                    _valor = 1;
                    _valida_genera_interface = 1;
                    string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    Boolean proceso_guiaToXstore = false;

                    if (File.Exists(_valida_proc_guiaToXstore)) proceso_guiaToXstore = true;

                    if (proceso_guiaToXstore)
                    {
                        _valor = 1;
                        string _error = "";
                        _valida_genera_interface = 1;

                        Ftp_Xstore_Service_Send ejecuta_procesos = null;
                        ejecuta_procesos = new Ftp_Xstore_Service_Send();

                        ejecuta_procesos.ejecutar_genera_file_xstore_auto(ref _error);

                        //ejecuta_procesos.envio_Guias_ToxStore(ref _error);

                        _valida_genera_interface = 0;

                    }
                }

            }
            catch (Exception exc)
            {
                //string errSwc = "";
                _valida_genera_interface = 0;
            }
            if (_valor == 1)
            {
                _valida_genera_interface = 0;
            }
        }
        #endregion

        #region<GUIAS>
        void tmservicio_GuiaToXstore_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion

                if (_valida_serviceGuiaToXstore == 0)
                {
                    _valor = 1;
                    _valida_serviceGuiaToXstore = 1;
                    string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    Boolean proceso_guiaToXstore = false;

                    if (File.Exists(_valida_proc_guiaToXstore)) proceso_guiaToXstore = true;

                    if (proceso_guiaToXstore)
                    {
                        _valor = 1;
                        string _error = "";
                        _valida_serviceGuiaToXstore = 1;
                      
                        Basico ejecuta_procesos = null;
                        ejecuta_procesos = new Basico();
                        ejecuta_procesos.envio_Guias_ToxStore(ref _error);

                        _valida_serviceGuiaToXstore = 0;

                    }
                }

            }
            catch (Exception exc)
            {
                //string errSwc = "";
                _valida_serviceGuiaToXstore = 0;
            }
            if (_valor == 1)
            {
                _valida_serviceGuiaToXstore = 0;
            }
        }
        #endregion

        #region<ENVIO DE PAQUETES>
        void tmservicio_trans_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion
                if (_valida_service_trans == 0)
                {
                    _valor = 1;
                    _valida_service_trans = 1;
                    string _valida_proc_dbf = @"D:\venta.txt";
                    Boolean proceso_insertDBF = false;

                    if (File.Exists(_valida_proc_dbf)) proceso_insertDBF = true;

                    string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    if (File.Exists(_valida_proc_guiaToXstore)) proceso_insertDBF = false;


                    if (proceso_insertDBF)
                    {
                        _valor = 1;
                        string _error = "";
                        _valida_service_trans = 1;
                        Proceso_Novell proc_nov = new Proceso_Novell();
                        
                        proc_nov.procesos_novell(ref _error);
                        //Basico ejecuta_procesos = new Basico();
                        //ejecuta_procesos.procesar_dbf_pos(ref _error);
                        _valida_service_trans = 0;

                    }
                }

            }
            catch (Exception exc)
            {
                //string errSwc = "";
                _valida_service_trans = 0;
            }
            if (_valor == 1)
            {
                _valida_service_trans = 0;
            }
        }
        #endregion

        #region<METODO DE ENVIO DE VENTAS>
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

                string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                if (File.Exists(_valida_proc_guiaToXstore)) return; //proceso_venta = false;

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

                        #region<PROCESAMIENTO DE FCACB Y FDECB>
                        ejecuta_proc_venta.procesar_fcacb_SQL(ref _error_ws);
                        #endregion
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
        #endregion
        //void tmpServicioVentaXstore_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    Int32 _valor = 0;
        //    try
        //    {
        //        if (_valida_serviceVentaXstore == 0)
        //        {
        //            _valor = 1;
        //            _valida_serviceVentaXstore = 1;
        //            string _valida_proc_venXstore = @"D:\venta.txt";
        //            Boolean proceso_ventaXSTORE = false;

        //            if (File.Exists(_valida_proc_venXstore)) proceso_ventaXSTORE = true;

        //            string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
        //            if (File.Exists(_valida_proc_guiaToXstore)) proceso_ventaXSTORE = false;

        //            if (proceso_ventaXSTORE)
        //            {
        //                _valor = 1;
        //                string _error = "";
        //                _valida_serviceVentaXstore = 1;
        //                Basico ejecuta_procesos = new Basico();
        //                ejecuta_procesos.procesar_dbf_pos(ref _error);
        //                _valida_serviceVentaXstore = 0;

        //            }
        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        //string errSwc = "";
        //        _valida_serviceVentaXstore = 0;
        //    }
        //    if (_valor == 1)
        //    {
        //        _valida_serviceVentaXstore = 0;
        //    }
        //}


        void tmpServicioDBF_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion
                if (_valida_ven_tmpDBF == 0)
                {
                    _valor = 1;
                    _valida_ven_tmpDBF = 1;
                    string _valida_proc_dbf = @"D:\venta.txt";
                    Boolean proceso_insertDBF = false;

                    if (File.Exists(_valida_proc_dbf)) proceso_insertDBF = true;


                    string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    if (File.Exists(_valida_proc_guiaToXstore)) proceso_insertDBF = false;

                    if (proceso_insertDBF)
                    {
                        _valor = 1;
                        string _error = "";
                        _valida_ven_tmpDBF = 1;                        
                            Basico ejecuta_procesos =new Basico();                     
                            ejecuta_procesos.procesar_dbf_pos(ref _error);
                        _valida_ven_tmpDBF = 0;
                   
                    }
               }

            }
            catch (Exception exc)
            {
                //string errSwc = "";
                _valida_ven_tmpDBF = 0;
            }
            if (_valor == 1)
            {
                _valida_ven_tmpDBF = 0;
            }
        }
        void tmservicioposlog_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion
                if (_valida_serviceposlog == 0)
                {
                    _valor = 1;
                    _valida_serviceposlog = 1;
                    string _valida_proc_ven = @"D:\venta.txt";
                    Boolean proceso_insert_polog = false;

                    if (File.Exists(_valida_proc_ven)) proceso_insert_polog = true;

                    string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    if (File.Exists(_valida_proc_guiaToXstore)) proceso_insert_polog = false;

                    if (proceso_insert_polog)
                    {
                        _valor = 1;
                        string _error = "";
                        _valida_serviceposlog = 1;
                         Basico ejecuta_procesos = new Basico();
                        ejecuta_procesos.procesar_poslog_pos(ref _error);
                        _valida_serviceposlog = 0;

                    }
                }

            }
            catch (Exception exc)
            {
                //string errSwc = "";
                _valida_serviceposlog = 0;
            }
            if (_valor == 1)
            {
                _valida_serviceposlog = 0;
            }
        }

        //void tmpServicioScactcoDBF_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    Int32 _valor = 0;
        //    try
        //    {
        //        if (_valida_serviceScactcoDBF == 0)
        //        {
        //            _valor = 1;
        //            _valida_serviceScactcoDBF = 1;
        //            string _valida_proc_dbf = @"D:\venta.txt";
        //            Boolean proceso_venta = false;

        //            if (File.Exists(_valida_proc_dbf)) proceso_venta = true;

        //            if (!proceso_venta)
        //            {
        //                _valor = 1;
        //                string _error = "";
        //                _valida_serviceScactcoDBF = 1;
        //                Basico ejecuta_procesos = new Basico();
        //                ejecuta_procesos.enviar_scactco(ref _error);
        //                _valida_serviceScactcoDBF = 0;

        //            }
        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        //string errSwc = "";
        //        _valida_serviceScactcoDBF = 0;
        //    }
        //    if (_valor == 1)
        //    {
        //        _valida_serviceScactcoDBF = 0;
        //    }
        //}

        protected override void OnStart(string[] args)
        {
            tmservicio.Start();
            tmservicioDBF.Start();
            tmservicioposlog.Start();
            tmservicio_trans.Start();
            tmservicio_GuiaToXstore.Start();
            tmgenera_interface.Start();
            tmenvia_sftp.Start();
            //tmservicioScactcoDBF.Start();
        }

        protected override void OnStop()
        {
            tmservicio.Stop();
            tmservicioDBF.Stop();
            tmservicioposlog.Stop();
            tmservicio_trans.Stop();
            tmservicio_GuiaToXstore.Stop();
            tmgenera_interface.Stop();
            tmenvia_sftp.Stop();
            //tmservicioScactcoDBF.Stop();
        }       
              

    }
}
