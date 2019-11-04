using CapaServicioWindows_x64.Envio_Ftp_Xstore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceWin64Framework4_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ftp_Xstore_Service_Send ejecuta_procesos = null;
            ejecuta_procesos = new Ftp_Xstore_Service_Send();
            ejecuta_procesos.ejecutar_genera_file_xstore_auto(pais, ref _error, ref gen_per_item, ref gen_ecu_item);
        }
    }
}
