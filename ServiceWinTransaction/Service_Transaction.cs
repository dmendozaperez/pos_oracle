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
using CapaServicioWindows.CapaDato.Interfaces;
using CapaServicioWindows.Bataclub;
using CapaServicioWindows.CapaDato.WMS;

namespace ServiceWinTransaction
{
    public partial class Service_Transaction : ServiceBase
    {
        Timer tmservicio = null;
        Timer tmservicioDBF = null;
        Timer tmservicioVentaXstore = null;

        #region<REGION PARA BATACLUB VARIABLES>
        //Timer tmbataclub = null;
        //private Int32 _valida_bataclub = 0;
        #endregion

        #region<REGION DE PROCESOS DEL WMS AQ Y EC VARIABLES>
        Timer tmpaq_wms = null;
        private Int32 _valida_aq_wms = 0;

        Timer tmpec_wms = null;
        private Int32 _valida_ec_wms = 0;

        #endregion

        #region<REGION DE RECEPCION DE ALMACEN>
        Timer tmpalm_rece = null;
        private Int32 _valida_alm_rece = 0;
        #endregion

        Timer tmpprescripcion = null;
        private Int32 _valida_PRES = 0;

        Timer tmservicioAQ = null;
        private Int32 _valida_AQ = 0;

        Timer tmservicio_ecu_guia = null;
        private Int32 _valida_service_ecu_guia = 0;

        #region<generacion de interfaces y envio de sftp>
        //Timer tmgenera_interface =null;
        //private Int32 _valida_genera_interface = 0;

        //Timer tmenvia_sftp = null;
        //private Int32 _valida_envia_sftp = 0;

        #endregion


        private string file_almace_ecu = @"D:\BataTransaction\ECU.txt";

        //Timer tmservicio_GuiaToXstore = null;
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

        #region<REGION DE ENVIO DE STOCK DE ALMACEN>
        Timer tmstock_alm = null;
        private Int32 _valida_stk = 0;

        Timer tmvendedor = null;
        private Int32 _valida_ven = 0;

        #endregion

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


            //tmservicio_GuiaToXstore = new Timer(5000);
            //tmservicio_GuiaToXstore.Elapsed += new ElapsedEventHandler(tmservicio_GuiaToXstore_Elapsed);

            //tmgenera_interface = new Timer(5000);
            //tmgenera_interface.Elapsed += new ElapsedEventHandler(tmgenera_interface_Elapsed);

            //tmenvia_sftp = new Timer(5000);
            //tmenvia_sftp.Elapsed += new ElapsedEventHandler(tmenvia_sftp_Elapsed);

            tmservicio_ecu_guia = new Timer(5000);
            tmservicio_ecu_guia.Elapsed += new ElapsedEventHandler(tmservicio_ecu_guia_Elapsed);

            tmservicioAQ = new Timer(5000);
            tmservicioAQ.Elapsed += new ElapsedEventHandler(tmservicioAQ_Elapsed);

            tmpprescripcion = new Timer(5000);
            tmpprescripcion.Elapsed += new ElapsedEventHandler(tmpprescripcion_Elapsed);

            /*PROCESO DE BATACLUB*/
            //tmbataclub = new Timer(5000);
            //tmbataclub.Elapsed += new ElapsedEventHandler(tmpbataclub_Elapsed);

            /*PROCESO DE ENVIO DE STOCK DE ALMACEN*/
            //5 minutos

            tmstock_alm = new Timer(5000);
            tmstock_alm.Elapsed += new ElapsedEventHandler(tmstock_alm_Elapsed);

            /*PROCESO DE VENDEDOR*/
            tmvendedor= new Timer(5000);
            tmvendedor.Elapsed += new ElapsedEventHandler(tmvendedor_Elapsed);

            /*PROCESOS DEL WMS AQUARELLA Y ECCOMERCE*/
            tmpaq_wms = new Timer(5000);
            tmpaq_wms.Elapsed += new ElapsedEventHandler(tmpaq_wms_Elapsed);

            tmpec_wms = new Timer(5000);
            tmpec_wms.Elapsed += new ElapsedEventHandler(tmpec_wms_Elapsed);

            tmpalm_rece = new Timer(5000);
            tmpalm_rece.Elapsed += new ElapsedEventHandler(tmpalm_rece_Elapsed);

        }

        #region<REGION DE RECEPCION DE ALMACEN>
        void tmpalm_rece_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;

            //string _ruta_erro_file = @"D:\ALMACEN\STOCK.txt";
            string _valida_proc_venta = @"D:\venta.txt";
            Boolean proceso_venta = false;
            TextWriter tw = null;
            string _hora = "";
            try
            {
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion

                /*si el archivo existe entonces ejecutar procesos de venta*/
                if (File.Exists(@_valida_proc_venta)) proceso_venta = true;

                string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                if (File.Exists(_valida_proc_guiaToXstore)) return; //proceso_venta = false;

                if (_valida_alm_rece == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_alm_rece = 1;

                    string _error_ws = "";
                    //_error = CapaServicioWindows.Modular.Basico.retornar();

                    #region<SOLO PARA ALMACEN HABILITAR ESTE PROCESO>
                    if (!proceso_venta)
                    {
                        Basico ejecuta_procesos = null;
                        ejecuta_procesos = new Basico();
                        ejecuta_procesos.eje_envio_guias_devolucion(ref _error_ws);
                    }
                    #endregion


                    _valida_alm_rece = 0;

                    if (_error_ws.Length > 0)
                    {
                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==> (servicio windows)" + _error_ws;
                        tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                        //TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        //tw.WriteLine(_error_ws);
                        //tw.Flush();
                        //tw.Close();
                        //tw.Dispose();
                    }
                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                _valida_alm_rece = 0;
                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==> (servicio windows)" + exc.Message;
                tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();

            }

            if (_valor == 1)
            {
                _valida_alm_rece = 0;
            }


        }
        #endregion

        #region<REGION DEL WMS AQUARELLA Y ECOMMERCE PROCESOS>
        void tmpaq_wms_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\BataTransaction\log_WMS_AQ.txt";
            string str = "";
            Boolean proceso_venta = false;
            try
            {

                #region<region solo almacen ecuador>
                if (!File.Exists(@file_almace_ecu)) return;
                #endregion


                if (_valida_aq_wms == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_aq_wms = 1;


                    string _error = "";                 

                    WMS_AQ_EC wms_proc = new WMS_AQ_EC();

                    _error = wms_proc.WMS_Proc_AQ_EC("AQ");
                    if (_error.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        tw = new StreamWriter(_ruta_erro_file, true);
                        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==>WMS_Proc_AQ_EC==>" + _error;
                        tw.WriteLine(str);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }

                    _valida_aq_wms = 0;

                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                tw = new StreamWriter(_ruta_erro_file, true);
                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==> catch ==>" + exc.Message;
                tw.WriteLine(str);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                _valida_aq_wms = 0;
            }

            if (_valor == 1)
            {
                _valida_aq_wms = 0;
            }


        }

        void tmpec_wms_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\BataTransaction\log_WMS_EC.txt";
            string str = "";
            Boolean proceso_venta = false;
            try
            {

                #region<region solo almacen ecuador>
                if (!File.Exists(@file_almace_ecu)) return;
                #endregion


                if (_valida_ec_wms == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_ec_wms = 1;


                    string _error = "";

                    WMS_AQ_EC wms_proc = new WMS_AQ_EC();

                    _error = wms_proc.WMS_Proc_AQ_EC("EC");
                    if (_error.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        tw = new StreamWriter(_ruta_erro_file, true);
                        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==>WMS_Proc_AQ_EC==>" + _error;
                        tw.WriteLine(str);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }

                    _valida_ec_wms = 0;

                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                tw = new StreamWriter(_ruta_erro_file, true);
                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==> catch ==>" + exc.Message;
                tw.WriteLine(str);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                _valida_ec_wms = 0;
            }

            if (_valor == 1)
            {
                _valida_ec_wms = 0;
            }


        }

        #endregion

        #region<REGION DE VENDEDOR>
        void tmvendedor_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\BataTransaction\log_vendedor.txt";
            string str = "";
            Boolean proceso_venta = false;
            try
            {

                #region<region solo almacen ecuador>
                if (!File.Exists(@file_almace_ecu)) return;
                #endregion


                if (_valida_ven == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_ven = 1;


                    string _error = "";
                    Util  act_vendedor = new Util();
                    _error=act_vendedor.update_vendedor();                    
                    if (_error.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        tw = new StreamWriter(_ruta_erro_file, true);
                        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==>genera_miembro_bataclub==>" + _error;
                        tw.WriteLine(str);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }                  

                    _valida_ven = 0;

                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                tw = new StreamWriter(_ruta_erro_file, true);
                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==> catch ==>" + exc.Message;
                tw.WriteLine(str);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                _valida_ven = 0;
            }

            if (_valor == 1)
            {
                _valida_ven = 0;
            }


        }
        #endregion

        #region <REGION DE STOCK DE ALMACEN>
        void tmstock_alm_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;

            //string _ruta_erro_file = @"D:\ALMACEN\STOCK.txt";
            string _valida_proc_venta = @"D:\venta.txt";
            Boolean proceso_venta = false;
            TextWriter tw = null;
            string _hora = "";
            try
            {                
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion

                /*si el archivo existe entonces ejecutar procesos de venta*/
                if (File.Exists(@_valida_proc_venta)) proceso_venta = true;

                string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                if (File.Exists(_valida_proc_guiaToXstore)) return; //proceso_venta = false;

                if (_valida_stk == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_stk = 1;
                   
                    string _error_ws = "";
                    //_error = CapaServicioWindows.Modular.Basico.retornar();

                    #region<SOLO PARA ALMACEN HABILITAR ESTE PROCESO>
                    if (!proceso_venta)
                    {
                        Basico ejecuta_procesos = null;
                        ejecuta_procesos = new Basico();
                        ejecuta_procesos.eje_envio_stk_almacen(ref _error_ws);
                    }
                    #endregion


                    _valida_stk = 0;

                    if (_error_ws.Length > 0)
                    {
                        _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==> (servicio windows)" + _error_ws;
                        tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                        tw.WriteLine(_hora);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                        //TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        //tw.WriteLine(_error_ws);
                        //tw.Flush();
                        //tw.Close();
                        //tw.Dispose();
                    }               
                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                _valida_stk = 0;
                _hora = DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + "==> (servicio windows)" + exc.Message;
                tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
                tw.WriteLine(_hora);
                tw.Flush();
                tw.Close();
                tw.Dispose();

            }

            if (_valor == 1)
            {
                _valida_stk = 0;
            }


        }
        #endregion
        #region<REGION DE BATACLUB METODOS>
        //void tmpbataclub_Elapsed(object sender, ElapsedEventArgs e)
        //{           
        //    Int32 _valor = 0;

        //    string _ruta_erro_file = @"D:\BataTransaction\log_bataclub.txt";
        //    string str = "";
        //    Boolean proceso_venta = false;
        //    try
        //    {                  

        //        #region<region solo almacen ecuador>
        //        if (!File.Exists(@file_almace_ecu)) return;
        //        #endregion


        //        if (_valida_bataclub == 0)
        //        {
        //            //string _error = "ing";
        //            _valor = 1;
        //            _valida_bataclub = 1;


        //            string _error_ws = "";
        //            BataClub batacl = new BataClub();
        //            _error_ws = batacl.genera_miembro_bataclub();
                    
        //            if (_error_ws.Length > 0)
        //            {                        
        //                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
        //                tw = new StreamWriter(_ruta_erro_file, true);
        //                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==>genera_miembro_bataclub==>" + _error_ws;
        //                tw.WriteLine(str);
        //                tw.Flush();
        //                tw.Close();
        //                tw.Dispose();
        //            }
        //            _error_ws = batacl.genera_envio_correo_bataclub();
        //            if (_error_ws.Length > 0)
        //            {
        //                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
        //                tw = new StreamWriter(_ruta_erro_file, true);
        //                str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==> genera_miembro_bataclub==>" + _error_ws;
        //                tw.WriteLine(str);
        //                tw.Flush();
        //                tw.Close();
        //                tw.Dispose();
        //            }

        //            //_error = CapaServicioWindows.Modular.Basico.retornar();

        //            _valida_bataclub = 0;
                   
        //        }
        //        //****************************************************************************
        //    }
        //    catch (Exception exc)
        //    {
        //        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
        //        tw = new StreamWriter(_ruta_erro_file, true);
        //        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==> catch ==>" + exc.Message;
        //        tw.WriteLine(str);
        //        tw.Flush();
        //        tw.Close();
        //        tw.Dispose();
        //        _valida_bataclub = 0;
        //    }

        //    if (_valor == 1)
        //    {
        //        _valida_bataclub = 0;
        //    }


        //}
        #endregion

        #region<PROCESO DE PRESCRIPCION>

        /*variable private para la ejecucion del proceso de prescripcion*/
        private string fecha_hora_actual = DateTime.Now.ToShortTimeString().Substring(0, 5);
        private string fecha_hora_add = DateTime.Now.ToShortTimeString().Substring(0, 5);
        private Int32 sum_horas = 4;
        void tmpprescripcion_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\Almacen\log_pres.txt";
            //string _valida_proc_venta = @"D:\venta.txt";
            Boolean proceso_venta = false;
            try
            {
                fecha_hora_actual = DateTime.Now.ToShortTimeString().Substring(0, 5);
                //Boolean valida_guia_ecu = false;
                //Boolean valida_

                #region<region solo almacen ecuador>
                if (!File.Exists(@file_almace_ecu)) return;
                #endregion


                if (_valida_PRES == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_PRES = 1;


                    string _error_ws = "";
                    //_error = CapaServicioWindows.Modular.Basico.retornar();

                    #region<SOLO PARA PRESCRIPCION>

                    if (fecha_hora_actual != fecha_hora_add)
                    {
                        _valor = 0;
                        _valida_PRES = 0;
                        return;
                    }

                        

                        //if (!proceso_venta)
                        //{
                    Basico envia_pres = new Basico();
                    envia_pres.eje_envio_prescripcion(ref _error_ws);
                    fecha_hora_add = DateTime.Now.AddHours(sum_horas).ToShortTimeString().Substring(0, 5);
                    //CapaServicioWindows.Envio_AQ.Envio_Ventas envia = new CapaServicioWindows.Envio_AQ.Envio_Ventas();
                    //_error_ws = envia.envio_ventas_aq();
                    //Basico ejecuta_procesos = null;
                    //ejecuta_procesos = new Basico();
                    //ejecuta_procesos.eje_envio_guias(ref _error_ws);
                    //}
                    #endregion






                    _valida_PRES = 0;

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
                fecha_hora_add = DateTime.Now.AddHours(sum_horas).ToShortTimeString().Substring(0, 5);
                TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                tw.WriteLine(exc.Message);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                _valida_PRES = 0;
            }

            if (_valor == 1)
            {
                _valida_PRES = 0;
            }


        }
        #endregion

        #region<PROCESOS DE AQUARELLA>
        void tmservicioAQ_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\BataTransaction\ERROR_WS.txt";
            //string _valida_proc_venta = @"D:\venta.txt";
            Boolean proceso_venta = false;
            try
            {

                //Boolean valida_guia_ecu = false;
                //Boolean valida_

                #region<region solo almacen ecuador>
                if (!File.Exists(@file_almace_ecu)) return;
                #endregion


                if (_valida_AQ == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_AQ = 1;


                    string _error_ws = "";
                    //_error = CapaServicioWindows.Modular.Basico.retornar();

                    #region<SOLO PARA AQUARELLA>
                    //if (!proceso_venta)
                    //{



                    CapaServicioWindows.Envio_AQ.Envio_Ventas envia = new CapaServicioWindows.Envio_AQ.Envio_Ventas();
                    envia.actualizar_cliente();

                    _error_ws= envia.envio_ventas_aq();


                    //Basico ejecuta_procesos = null;
                    //ejecuta_procesos = new Basico();
                    //ejecuta_procesos.eje_envio_guias(ref _error_ws);
                    //}
                    #endregion






                    _valida_AQ = 0;

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
                _valida_AQ = 0;
            }

            if (_valor == 1)
            {
                _valida_AQ = 0;
            }


        }
        #endregion

        #region<REGION DE ECUADOR Y ALMACEN LURIN>
        #region<METODO DE ENVIO DE VENTAS>
        void tmservicio_ecu_guia_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\BataTransaction\ERROR_WS.txt";
            //string _valida_proc_venta = @"D:\venta.txt";
            Boolean proceso_venta = false;
            try
            {

                //Boolean valida_guia_ecu = false;
                //Boolean valida_

                #region<region solo almacen ecuador>
                if (!File.Exists(@file_almace_ecu)) return;
                #endregion

             
                if (_valida_service_ecu_guia == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_service_ecu_guia = 1;

                    
                    string _error_ws = "";
                    //_error = CapaServicioWindows.Modular.Basico.retornar();

                    #region<SOLO PARA ALMACEN HABILITAR ESTE PROCESO>
                    //if (!proceso_venta)
                    //{
                        Basico ejecuta_procesos = null;
                        ejecuta_procesos = new Basico();
                        ejecuta_procesos.eje_envio_guias(ref _error_ws);
                    //}
                    #endregion






                    _valida_service_ecu_guia = 0;

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
                _valida_service_ecu_guia = 0;
            }

            if (_valor == 1)
            {
                _valida_service_ecu_guia = 0;
            }


        }
        #endregion
        #endregion

        #region<envio de interfaces automatico>
      
        #endregion

        #region<GUIAS>
       
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
            string file_almacen_lurin = @"D:\BataTransaction\LURIN.txt";
            try
            {
                //string _error = "ing";
                //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
                //tw.WriteLine(_error);
                //tw.Flush();
                //tw.Close();
                //tw.Dispose();
                #region<region solo almacen ecuador>
                if (File.Exists(@file_almace_ecu)) return;
                #endregion
                #region<region solo almacen lurin>
                if (File.Exists(@file_almacen_lurin)) return;
                #endregion

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

                        #region<PROCESAMIENTO DE FMC Y FMD>
                        ejecuta_proc_venta.procesar_fmc_fmd(ref _error_ws);
                        #endregion

                        #region<PROCESAMIENTO DE FMC Y FMD HACIA FVDESPC>
                        ejecuta_proc_venta.procesar_fmc_fmd_fvdespc(ref _error_ws);
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
                _valida_service = 0;
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
               
            }

            if (_valor == 1)
            {
                _valida_service = 0;
            }


        }
        #endregion

       

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

                    //string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    //if (File.Exists(_valida_proc_guiaToXstore)) proceso_insert_polog = false;

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

       
        protected override void OnStart(string[] args)
        {
            tmservicio.Start();
            tmservicioDBF.Start();
            tmservicioposlog.Start();
            tmservicio_trans.Start();
            //tmservicio_GuiaToXstore.Start();
            //tmgenera_interface.Start();4/11
            //tmenvia_sftp.Start();4/11
            tmservicio_ecu_guia.Start();
            tmservicioAQ.Start();
            tmpprescripcion.Start();
            //tmbataclub.Start();
            tmstock_alm.Start();
            tmvendedor.Start();
            tmpaq_wms.Start();
            tmpec_wms.Start();
            tmpalm_rece.Start();
            //tmservicioScactcoDBF.Start();
        }

        protected override void OnStop()
        {
            tmservicio.Stop();
            tmservicioDBF.Stop();
            tmservicioposlog.Stop();
            tmservicio_trans.Stop();
            //tmservicio_GuiaToXstore.Stop();
            //tmgenera_interface.Stop();
            //tmenvia_sftp.Stop();
            tmservicio_ecu_guia.Stop();
            tmservicioAQ.Stop();
            tmpprescripcion.Stop();
            //tmbataclub.Stop();
            tmstock_alm.Stop();
            tmvendedor.Stop();

            tmpaq_wms.Stop();
            tmpec_wms.Stop();

            tmpalm_rece.Stop();

            //tmservicioScactcoDBF.Stop();
        }       
              

    }
}
