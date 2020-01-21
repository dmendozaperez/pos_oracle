using CapaServicioWindows_x64.Envio_Ftp_Xstore;
using CapaServicioWindows_x64.Modular;
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

namespace ServiceXstore
{
    public partial class Service_Xstore : ServiceBase
    {
        #region<generacion de interfaces y envio de sftp>
        Timer tmgenera_interface = null;
        private Int32 _valida_genera_interface = 0;

        Timer tmenvia_sftp = null;
        private Int32 _valida_envia_sftp = 0;
        #endregion
        #region<generacion para ecommerce>
        Timer tmgenera_stk_ec = null;
        private Int32 _valida_stk_ec = 0;
        #endregion

        #region <guia>
        Timer tmservicio_GuiaToXstore = null;
        private Int32 _valida_serviceGuiaToXstore = 0;
        #endregion

        public Service_Xstore()
        {
            InitializeComponent();

            tmgenera_interface = new Timer(5000);
            tmgenera_interface.Elapsed += new ElapsedEventHandler(tmgenera_interface_Elapsed);

            tmenvia_sftp = new Timer(5000);
            tmenvia_sftp.Elapsed += new ElapsedEventHandler(tmenvia_sftp_Elapsed);

            tmservicio_GuiaToXstore = new Timer(5000);
            tmservicio_GuiaToXstore.Elapsed += new ElapsedEventHandler(tmservicio_GuiaToXstore_Elapsed);

            tmgenera_stk_ec = new Timer(5000);
            tmgenera_stk_ec.Elapsed += new ElapsedEventHandler(tmgenera_stk_ec_Elapsed);
        }

        #region<envio de stock ecommerce>
        Ecommerce_Stock env = new Ecommerce_Stock();
        void tmgenera_stk_ec_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            TextWriter tw1 = null;
            try
            {
                

                if (_valida_stk_ec == 0)
                {
                   
                    _valor = 1;                   
                    _valida_stk_ec = 1;

                    string _enviando = "";
                    env.envio_stock(ref _enviando);


                    if (_enviando.Length > 0)
                    {
                        tw1 = new StreamWriter(@"D:\XSTORE\LOG_EC.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " " + _enviando);
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                    }

                    _valida_stk_ec = 0;

                }

            }
            catch (Exception exc)
            {
                _valida_stk_ec = 0;
            }
            if (_valor == 1)
            {
                _valida_stk_ec = 0;
            }
        }
        #endregion
        #region<envio de interfaces automatico>
        void tmgenera_interface_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            TextWriter tw1 = null;
            try
            {
                #region<region solo almacen ecuador>
                //if (File.Exists(@file_almace_ecu)) return;
                #endregion

                if (_valida_genera_interface == 0)
                {
                    //_valor = 1;
                    //_valida_genera_interface = 1;
                    //string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    //Boolean proceso_guiaToXstore = false;

                    //if (File.Exists(_valida_proc_guiaToXstore)) proceso_guiaToXstore = true;

                    //if (proceso_guiaToXstore)
                    //{
                    _valor = 1;
                    string _error = "";
                    _valida_genera_interface = 1;

                    Ftp_Xstore_Service_Send ejecuta_procesos = null;
                    ejecuta_procesos = new Ftp_Xstore_Service_Send();

                    string pais = "PE";
                    Boolean gen_per_item = false;
                    Boolean gen_ecu_item = false;

                    /*log de ingreso de procesos ejecutar_genera_file_xstore_auto */
                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INGRESANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (ejecutar_genera_file_xstore_auto) PERU");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();
                    /******/

                    ejecuta_procesos.ejecutar_genera_file_xstore_auto(pais, ref _error, ref gen_per_item, ref gen_ecu_item);

                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO DEL SERVICIO DE GENERACION DE INTERFACE METODO (ejecutar_genera_file_xstore_auto) PERU");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();

                    /**/
                    ejecuta_procesos.generar_orce_exclud(ref _error);
                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO DEL SERVICIO DE GENERACION DE INTERFACE METODO (ejecutar_genera_file_xstore_auto) PERU");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();



                    Ftp_Xstore_Service_Send upd_item = new Ftp_Xstore_Service_Send();
                    if (_error.Length == 0)
                    {
                        if (gen_per_item)
                        {
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " INGRESANDO EL SERVICIO DE GENERACION DE INTERFACE METODO (update_articulo_end_xstore) PERU");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                            /*update de articulo de peru*/
                            upd_item.update_articulo_end_xstore(pais);
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO DEL SERVICIO DE GENERACION DE INTERFACE METODO (update_articulo_end_xstore) PERU");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                        }
                    }


                    pais = "EC";

                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO DEL SERVICIO DE GENERACION DE INTERFACE METODO (ejecutar_genera_file_xstore_auto) ECUADOR");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();

                    ejecuta_procesos.ejecutar_genera_file_xstore_auto(pais, ref _error, ref gen_per_item, ref gen_ecu_item);

                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO DEL SERVICIO DE GENERACION DE INTERFACE METODO (ejecutar_genera_file_xstore_auto) ECUADOR");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();

                    /*GENERACION DE INTERFACE*/
                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (ejecutar_genera_interface_xstore)");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();
                    Xstore_Genera_Inter ejecuta_procesos_inter = new Xstore_Genera_Inter();
                    ejecuta_procesos_inter.ejecutar_genera_interface_xstore(ref _error);
                    tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                    tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO AL SERVICIO DE GENERACION DE INTERFACE METODO (ejecutar_genera_interface_xstore)");
                    tw1.Flush();
                    tw1.Close();
                    tw1.Dispose();
                    /********************************/
                    /*UNA VEZ QUE SE HAYAN GENERADO LAS INTERFACES ENTONCES LO QUE VAMOS HACER ES UN UPDATE EN XSTORE*/
                    /*PARA NO VOLVER A ENVIAR EL ARTICULO,  CONTROL PARA ENVIAR SOLO LOS NUEVOS Y MODIFICADOS*/
                    if (_error.Length == 0)
                    {
                        //CapaServicioWindows.Envio_Ftp_Xstore



                        if (gen_ecu_item)
                        {
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " ENTRANDO AL SERVICIO DE GENERACION DE INTERFACE METODO (update_articulo_end_xstore) ECUADOR");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                            /*update de articulo de ecuador*/
                            upd_item.update_articulo_end_xstore(pais);
                            tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                            tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " SALIENDO DEL SERVICIO DE GENERACION DE INTERFACE METODO (update_articulo_end_xstore) ECUADOR");
                            tw1.Flush();
                            tw1.Close();
                            tw1.Dispose();
                        }
                    }

                    /**/

                    if (_error.Length > 0)
                    {
                        tw1 = new StreamWriter(@"D:\XSTORE\ERROR_INTER.txt", true);
                        tw1.WriteLine(DateTime.Today.ToString() + " " + DateTime.Now.ToLongTimeString() + " " + _error);
                        tw1.Flush();
                        tw1.Close();
                        tw1.Dispose();
                    }

                    //ejecuta_procesos.envio_Guias_ToxStore(ref _error);

                    _valida_genera_interface = 0;

                }
                //}

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
        void tmenvia_sftp_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                //if (File.Exists(@file_almace_ecu)) return;
                #endregion

                if (_valida_envia_sftp == 0)
                {
                    _valor = 1;
                    _valida_envia_sftp = 1;
                    //string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    //Boolean proceso_guiaToXstore = false;

                    //if (File.Exists(_valida_proc_guiaToXstore)) proceso_guiaToXstore = true;

                    //if (proceso_guiaToXstore)
                    //{
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

                    //}
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
        #endregion


        #region<GUIAS>
        void tmservicio_GuiaToXstore_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;
            try
            {
                #region<region solo almacen ecuador>
                //if (File.Exists(@file_almace_ecu)) return;
                #endregion

                if (_valida_serviceGuiaToXstore == 0)
                {
                    //_valor = 1;
                    //_valida_serviceGuiaToXstore = 1;
                    //string _valida_proc_guiaToXstore = @"D:\XSTORE\proc_xs.txt";
                    //Boolean proceso_guiaToXstore = false;

                    //if (File.Exists(_valida_proc_guiaToXstore)) proceso_guiaToXstore = true;

                    //if (proceso_guiaToXstore)
                    //{
                        _valor = 1;
                        string _error = "";
                        _valida_serviceGuiaToXstore = 1;

                        Basico ejecuta_procesos = null;
                        ejecuta_procesos = new Basico();
                        ejecuta_procesos.envio_Guias_ToxStore(ref _error);

                        _valida_serviceGuiaToXstore = 0;

                    //}
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

        protected override void OnStart(string[] args)
        {
            tmgenera_interface.Start();
            tmenvia_sftp.Start();

            tmservicio_GuiaToXstore.Start();
            tmgenera_stk_ec.Start();
        }

        protected override void OnStop()
        {
            tmgenera_interface.Stop();
            tmenvia_sftp.Stop();

            tmservicio_GuiaToXstore.Stop();

            tmgenera_stk_ec.Stop();
        }
    }
}
