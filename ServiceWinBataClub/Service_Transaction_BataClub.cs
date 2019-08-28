﻿using System;
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
using CapaServicioWindows.Bataclub;

namespace ServiceWinBataClub
{
    public partial class Service_Transaction_BataClub : ServiceBase
    {
        #region<REGION PARA BATACLUB VARIABLES>
        Timer tmbataclub = null;
        private Int32 _valida_bataclub = 0;
        private string file_almace_ecu = @"D:\BataTransaction\ECU.txt";
        #endregion
        public Service_Transaction_BataClub()
        {
            InitializeComponent();
            /*PROCESO DE BATACLUB*/
            tmbataclub = new Timer(5000);
            tmbataclub.Elapsed += new ElapsedEventHandler(tmpbataclub_Elapsed);
        }
        #region<REGION DE BATACLUB METODOS>
        void tmpbataclub_Elapsed(object sender, ElapsedEventArgs e)
        {
            Int32 _valor = 0;

            string _ruta_erro_file = @"D:\BataTransaction\log_bataclub.txt";
            string str = "";
            Boolean proceso_venta = false;
            try
            {

                #region<region solo almacen ecuador>
                if (!File.Exists(@file_almace_ecu)) return;
                #endregion


                if (_valida_bataclub == 0)
                {
                    //string _error = "ing";
                    _valor = 1;
                    _valida_bataclub = 1;


                    string _error_ws = "";
                    BataClub batacl = new BataClub();
                    _error_ws = batacl.genera_miembro_bataclub();

                    if (_error_ws.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        tw = new StreamWriter(_ruta_erro_file, true);
                        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==>genera_miembro_bataclub==>" + _error_ws;
                        tw.WriteLine(str);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }
                    _error_ws = batacl.genera_envio_correo_bataclub();
                    if (_error_ws.Length > 0)
                    {
                        TextWriter tw = new StreamWriter(_ruta_erro_file, true);
                        tw = new StreamWriter(_ruta_erro_file, true);
                        str = DateTime.Today.ToString() + " " + DateTime.Now.ToString("HH:mm:ss") + "==> genera_miembro_bataclub==>" + _error_ws;
                        tw.WriteLine(str);
                        tw.Flush();
                        tw.Close();
                        tw.Dispose();
                    }

                    //_error = CapaServicioWindows.Modular.Basico.retornar();

                    _valida_bataclub = 0;

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
                _valida_bataclub = 0;
            }

            if (_valor == 1)
            {
                _valida_bataclub = 0;
            }


        }
        #endregion
        protected override void OnStart(string[] args)
        {
            tmbataclub.Start();
        }

        protected override void OnStop()
        {
            tmbataclub.Stop();
        }
    }
}
