using CapaServicioWindows.CapaDato.WMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
//using System.Threading;
using System.Timers;

namespace ServiceWinCatalogo
{
    public partial class Service_Transaction_Catalogo : ServiceBase
    {
        /*variables para el WMS*/
        Timer tmpaq_wms = null;
        private Int32 _valida_aq_wms = 0;

        Timer tmpec_wms = null;
        private Int32 _valida_ec_wms = 0;

        /*variable para envio de ventas*/
        Timer tmservicioAQ = null;
        private Int32 _valida_AQ = 0;

        private string con_novel = "";

        public Service_Transaction_Catalogo()
        {
            InitializeComponent();
            /*PROCESOS DEL WMS AQUARELLA Y ECCOMERCE*/
            tmpaq_wms = new Timer(5000);
            tmpaq_wms.Elapsed += new ElapsedEventHandler(tmpaq_wms_Elapsed);

            tmpec_wms = new Timer(5000);
            tmpec_wms.Elapsed += new ElapsedEventHandler(tmpec_wms_Elapsed);

            /*PROCESOS DE ENVIO DE VENTA AQUARELLA*/
            tmservicioAQ = new Timer(5000);
            tmservicioAQ.Elapsed += new ElapsedEventHandler(tmservicioAQ_Elapsed);

        }
        #region<REGION DEL WMS AQUARELLA Y ECOMMERCE PROCESOS>
        void tmpaq_wms_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\Catalogo\log_WMS_Catalogo.txt";
            string str = "";
            try
            {
            
                if (_valida_aq_wms == 0)
                {                 
                    _valor = 1;
                    _valida_aq_wms = 1;
                    string _error = "";
                    WMS_AQ_EC wms_proc = new WMS_AQ_EC();
                    _error = wms_proc.WMS_Proc_AQ_EC("AQ");
                    if (_error.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        //tw = new StreamWriter(_ruta_erro_file, true);
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
                //tw = new StreamWriter(_ruta_erro_file, true);
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

            string _ruta_erro_file = @"D:\Catalogo\log_WMS_EC.txt";
            string str = "";        
            try
            {
              
                if (_valida_ec_wms == 0)
                {                   
                    _valor = 1;
                    _valida_ec_wms = 1;

                    string _error = "";

                    WMS_AQ_EC wms_proc = new WMS_AQ_EC();

                    _error = wms_proc.WMS_Proc_AQ_EC("EC");
                    if (_error.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        //tw = new StreamWriter(_ruta_erro_file, true);
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
                //tw = new StreamWriter(_ruta_erro_file, true);
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
        #region<REGION DE PROCESO DE ENVIO DE VENTA>
        void tmservicioAQ_Elapsed(object sender, ElapsedEventArgs e)
        {        
            Int32 _valor = 0;
            TextWriter tw = null;
            string _ruta_erro_file = @"D:\Catalogo\log_Venta_Catalogo.txt";         
            try
            {
                if (_valida_AQ == 0)
                {             
                    _valor = 1;
                    _valida_AQ = 1;

                    string _error_ws = "";

                    #region<SOLO PARA AQUARELLA>

                    CapaServicioWindows.Envio_AQ.Envio_Ventas envia = new CapaServicioWindows.Envio_AQ.Envio_Ventas();
                    //tw = new StreamWriter(_ruta_erro_file, true);
                    //tw.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==>Actualizando cliente intranet");
                    //tw.Flush();
                    //tw.Close();
                    //tw.Dispose();
                    envia.actualizar_cliente();
                    //tw = new StreamWriter(_ruta_erro_file, true);
                    //tw.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==>Terminando de actualizar cliente intranet");
                    //tw.Flush();
                    //tw.Close();
                    //tw.Dispose();

                    _error_ws = envia.envio_ventas_aq(ref con_novel);

                    #endregion

                    _valida_AQ = 0;

                    if (_error_ws.Length > 0)
                    {
                        tw = new StreamWriter(_ruta_erro_file, true);
                        tw.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") +  "==>" + _error_ws);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }
                }
                //****************************************************************************
            }
            catch (Exception exc)
            {
                tw = new StreamWriter(_ruta_erro_file, true);
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
        protected override void OnStart(string[] args)
        {           
            tmpaq_wms.Start();
            tmpec_wms.Start();
            tmservicioAQ.Start();
        }

        protected override void OnStop()
        {
            tmpaq_wms.Stop();
            tmpec_wms.Stop();
            tmservicioAQ.Stop();
           
        }
    }
}
