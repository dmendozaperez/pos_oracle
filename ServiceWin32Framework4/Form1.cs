//using CapaServicioWindows.Modular;
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
    }
}
