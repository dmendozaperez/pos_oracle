﻿//using CapaServicioWindows.Modular;
using CapaServicioWindows.CapaDato.Venta;
using CapaServicioWindows.Envio_Ftp_Xstore;
using CapaServicioWindows.Modular;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using TaskScheduler;
using Microsoft.Win32.TaskScheduler;

namespace ServiceWin32Framework4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_servicewin_Click(object sender, EventArgs e)
        {
            string _error = "ing";
            TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
            tw.WriteLine(_error);
            tw.Flush();
            tw.Close();
            tw.Dispose();
            Cursor.Current = Cursors.WaitCursor;
            string _erro = "";
            Basico cc = new Basico();
            cc.procesar_dbf_pos(ref _erro);
            //cc.procesar_dbf_pos(ref _erro);
            //cc.eje_envio_guias(ref _erro);
            MessageBox.Show("termino");
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            //string _error = "ing";
            //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
            //tw.WriteLine(_error);
            //tw.Flush();
            //tw.Close();
            //tw.Dispose();
            //Cursor.Current = Cursors.WaitCursor;
            string _erro = "";
            Basico cc = new Basico();
            cc.enviar_scactco(ref _erro);
            //cc.procesar_dbf_pos(ref _erro);
            //cc.eje_envio_guias(ref _erro);
            MessageBox.Show("termino");
            Cursor.Current = Cursors.Default;

        }

        private void btnbarra_Click(object sender, EventArgs e)
        {
            string _error = "";
            Basico ejecuta_procesos = new Basico();
            ejecuta_procesos.enviar_scdremb(ref _error);

            //ejecuta_procesos.enviar_scactco(ref _error);
        }

        private void btnenvio_nov_Click(object sender, EventArgs e)
        {
            Proceso_Novell pr = new Proceso_Novell();
            string error = "";
            pr.procesos_novell(ref error);
        }

        private void btnenviog_Click(object sender, EventArgs e)
        {
            string _error = "ing";
            TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
            tw.WriteLine(_error);
            tw.Flush();
            tw.Close();
            tw.Dispose();
            Cursor.Current = Cursors.WaitCursor;
            string _erro = "";
            Basico cc = new Basico();
           // cc.procesar_dbf_pos(ref _erro);
            cc.eje_envio_guias(ref _erro);
            MessageBox.Show("termino");
            Cursor.Current = Cursors.Default;
        }

        private void btnposlog_Click(object sender, EventArgs e)
        {
            string _error = "";
            Basico ejecuta_procesos = new Basico();
            ejecuta_procesos.procesar_poslog_pos(ref _error);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string _error = "";
            Dat_Venta ejecuta_proc_venta = new Dat_Venta();
           
            ejecuta_proc_venta.procesar_fcacb_SQL(ref _error);

            //string _error = "";
            //Basico ejecuta_procesos = new Basico();
            //ejecuta_procesos.PRO .procesar_fcacb_SQL(ref _error_ws);
            //ejecuta_procesos.envio_Guias_ToxStore(ref _error);

        }

        private void ws_get_xstore_carpeta_upload_Click(object sender, EventArgs e)
        {
            string _error = "";
            Ftp_Xstore_Service_Send envio = new Ftp_Xstore_Service_Send();
            //envio.proc_envio_ftp();
            string pais = "PE";
            Boolean gen_per_item = false;
            Boolean gen_ecu_item = false;

            envio.ejecutar_genera_file_xstore_auto(pais, ref _error,ref gen_per_item,ref gen_ecu_item);
            envio.update_articulo_end_xstore(pais);
            //Dat_Venta ejecuta_proc_venta = new Dat_Venta();
            //CapaServicioWindows.Envio_Ftp_Xstore
            //ejecuta_proc_venta.procesar_fcacb_SQL(ref _error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void envio_Guias_ToxStore_Click(object sender, EventArgs e)
        {
            string _error = "";
            Basico ejecuta_procesos = new Basico();          
            ejecuta_procesos.envio_Guias_ToxStore(ref _error);
        }

        private void ejecutar_genera_interface_xstore_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string _error = "";
            Xstore_Genera_Inter ejecuta_procesos = new Xstore_Genera_Inter();
            ejecuta_procesos.ejecutar_genera_interface_xstore(ref _error);
            MessageBox.Show("Terminado");
            Cursor.Current = Cursors.Default;
        }

        private void procesar_fmc_fmd_Click(object sender, EventArgs e)
        {
            string _error = "";
            Dat_Venta ejecuta_proc_venta = null;
            ejecuta_proc_venta = new Dat_Venta();
            #region<PROCESAMIENTO DE FMC Y FMD>
            ejecuta_proc_venta.procesar_fmc_fmd(ref _error);
            #endregion
        }

        private void get_fmc_insertar_fvdespc_Click(object sender, EventArgs e)
        {
            string _error = "";
            Dat_Venta ejecuta_proc_venta = null;
            ejecuta_proc_venta = new Dat_Venta();
            #region<PROCESAMIENTO DE FVDESPC>
            ejecuta_proc_venta.procesar_fmc_fmd_fvdespc(ref _error);
            #endregion
        }

        private void BTNENVIONOV_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CapaServicioWindows.Envio_AQ.Envio_Ventas env = new CapaServicioWindows.Envio_AQ.Envio_Ventas();
            env.envio_ventas_aq();
            Cursor.Current = Cursors.Default;
        }

        private void btnenvio_prescripciones_Click(object sender, EventArgs e)
        {
            string _error = "ing";
            TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
            tw.WriteLine(_error);
            tw.Flush();
            tw.Close();
            tw.Dispose();
            Cursor.Current = Cursors.WaitCursor;
            string _erro = "";
            Basico cc = new Basico();
            // cc.procesar_dbf_pos(ref _erro);
            cc.eje_envio_prescripcion(ref _erro);
            MessageBox.Show("termino");
            Cursor.Current = Cursors.Default;
        }

        private void btntarea_Click(object sender, EventArgs e)
        {


            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();

            }

            //using (ScheduledTasks Tareas = new ScheduledTasks())

            //{

            //    Task tarea = Tareas.CreateTask("Prueba");

            //    // archivo que vamos a ejecutar, escribimos la ruta completa

            //    tarea.ApplicationName = @"C:\Windows\System32\calc.exe";

            //    tarea.Comment = "Tarea que abre la calculadora";

            //    // configurar la cuenta con la que se ejecutara la tarea

            //    //tarea.SetAccountInformation("usuario", "password");

            //    // limitar la duración de la tarea programada

            //    tarea.MaxRunTime = new TimeSpan(0, 15, 0);

            //    tarea.Creator = "David Mendoza";

            //    // prioridad de la tarea

            //    tarea.Priority = System.Diagnostics.ProcessPriorityClass.High;

            //    // agregamos el disparador, la tarea se ejecutara diariamente a las 6 y 15 pm

            //    //tarea.Triggers.Add(new DailyTrigger(18, 15));

            //    int[] dias = new int[] { 1, 15 };

            //    tarea.Triggers.Add(new MonthlyTrigger(18, 15, dias));

            //    tarea.Save();

            //}
        }
    }
}
