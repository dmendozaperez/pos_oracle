//using CapaServicioWindows.Modular;
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
            envio.ejecutar_genera_file_xstore_auto(ref _error);
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
    }
}
