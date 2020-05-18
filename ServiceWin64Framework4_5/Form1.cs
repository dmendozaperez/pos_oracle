using CapaServicioWindows_x64.Envio_Ftp_Xstore;
using CapaServicioWindows_x64.Modular;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string pais = "PE";
            Boolean gen_per_item = false;
            Boolean gen_ecu_item = false;
            string _error = "Filanizado correcto";
            Ftp_Xstore_Service_Send ejecuta_procesos = null;

            

            ejecuta_procesos = new Ftp_Xstore_Service_Send();

            //ejecuta_procesos.generar_orce_exclud(ref _error);

            ejecuta_procesos.ejecutar_genera_file_xstore_auto(pais, ref _error, ref gen_per_item, ref gen_ecu_item);

            MessageBox.Show(_error);
            Cursor.Current = Cursors.Default;
        }

        private void btnejecutar_genera_interface_xstore_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string pais = "PE";
            Boolean gen_per_item = false;
            Boolean gen_ecu_item = false;
            string _error = "Filanizado correcto";
            Xstore_Genera_Inter ejecuta_procesos_inter = new Xstore_Genera_Inter();
            ejecuta_procesos_inter.ejecutar_genera_interface_xstore(ref _error);
            MessageBox.Show(_error);
            Cursor.Current = Cursors.Default;
        }

        private void btn_guias_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string pais = "PE";
            Boolean gen_per_item = false;
            Boolean gen_ecu_item = false;
            string _error = "Filanizado correcto";
            Basico ejecuta_procesos = null;
            ejecuta_procesos = new Basico();
            ejecuta_procesos.envio_Guias_ToxStore(ref _error);
            MessageBox.Show(_error);
            Cursor.Current = Cursors.Default;
        }

        private void btn_enviosftp_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string pais = "PE";
            Boolean gen_per_item = false;
            Boolean gen_ecu_item = false;
            string _error = "Filanizado correcto";
            Ftp_Xstore_Service_Send ejecuta_procesos = null;
            ejecuta_procesos = new Ftp_Xstore_Service_Send();
            ejecuta_procesos.proc_envio_ftp();
            MessageBox.Show(_error);
            Cursor.Current = Cursors.Default;
        }
        Ecommerce_Stock env = new Ecommerce_Stock();
        private void btnstk_ec_Click(object sender, EventArgs e)
        {
            string enviando = "";
            env.envio_stock(ref enviando);
        }

        private void btnupdate_bataweb_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string ruta_server = @"\\192.168.2.6\inetpub\wwwroot\BataWeb\bin";
                string ruta_local = @"D:\Fuentes\SL_Web\SL_Web\CapaPresentacion\bin";

                string _CapaDato = "CapaDato.dll";
                string _CapaEntidad = "CapaEntidad.dll";
                string _CapaOraDato = "CapaOraDato.dll";
                string _CapaPresentacion = "CapaPresentacion.dll";

                byte[] _bataweb_dll = null;
                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaDato);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaDato, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaEntidad);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaEntidad, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaOraDato);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaOraDato, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaPresentacion);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaPresentacion, _bataweb_dll);

                MessageBox.Show("Se Actualizo Dll en Produccion BATAWEB", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnact_aq_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string ruta_server = @"\\192.168.2.6\Aquarella\bin";
                string ruta_local = @"D:\Fuentes\Aquarella\www.aquarella.com.pe\www.aquarella.com.pe\bin";

                string _CapaAQ = "www.aquarella.com.pe.dll";
                //string _CapaEntidad = "CapaEntidad.dll";
                //string _CapaOraDato = "CapaOraDato.dll";
                //string _CapaPresentacion = "CapaPresentacion.dll";

                byte[] _aquarella_dll = null;
                _aquarella_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaAQ);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaAQ, _aquarella_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaEntidad);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaEntidad, _bataweb_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaOraDato);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaOraDato, _bataweb_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaPresentacion);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaPresentacion, _bataweb_dll);

                MessageBox.Show("Se Actualizo Dll en Produccion AQUARELLA", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
