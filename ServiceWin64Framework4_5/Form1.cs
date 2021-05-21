//using BataClub_Correo;
using BataClub_Correo;
using BataClub_DNI;
using CapaServicioWindows_x64.Bataclub;
using CapaServicioWindows_x64.Envio_Ftp_Xstore;
using CapaServicioWindows_x64.Modular;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
            string pais = "EC";
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
                string ruta_server = @"\\192.168.2.6\BataWeb\bin";
                string ruta_local = @"D:\Fuentes\SL_Web\SL_Web\CapaPresentacion\bin";

                string _CapaDato = "CapaDato.dll";
                string _CapaEntidad = "CapaEntidad.dll";
                //string _CapaOraDato = "CapaOraDato.dll";
                string _CapaPresentacion = "CapaPresentacion.dll";

                byte[] _bataweb_dll = null;
                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaDato);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaDato, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaEntidad);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaEntidad, _bataweb_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaOraDato);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaOraDato, _bataweb_dll);

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

        private void btnaqmvc_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string ruta_server = @"\\192.168.2.6\CatalogoBata\bin";
                string ruta_local = @"D:\Fuentes\Aquarella\AQUARELLA MVC 2018\AquarellaMVC\CapaPresentacion\bin";

                string _CapaDato = "CapaDato.dll";
                string _CapaEntidad = "CapaEntidad.dll";
                //string _CapaOraDato = "CapaOraDato.dll";
                string _CapaPresentacion = "CapaPresentacion.dll";

                byte[] _bataweb_dll = null;
                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaDato);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaDato, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaEntidad);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaEntidad, _bataweb_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaOraDato);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaOraDato, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaPresentacion);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaPresentacion, _bataweb_dll);

                MessageBox.Show("Se Actualizo Dll en Produccion AQ MVC", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnwstransaction_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string ruta_server = @"\\192.168.2.6\inetpub\wwwroot\Ws_BataPOS\bin";
                string ruta_local = @"D:\Fuentes\POS\IntegracionPos_Peru\IntegracionPos_Peru\WS_Bata_Interfaces\bin";

                string _CapaDato = "CapaBasico.dll";
                string _CapaEntidad = "CapaDato.dll";
                string _CapaOraDato = "CapaEntidad.dll";
                string _CapaPresentacion = "WS_Bata_Interfaces.dll";

                byte[] _bataweb_dll = null;
                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaDato);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaDato, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaEntidad);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaEntidad, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaOraDato);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaOraDato, _bataweb_dll);

                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaPresentacion);
                File.WriteAllBytes(@ruta_server + "\\" + _CapaPresentacion, _bataweb_dll);

                MessageBox.Show("Se Actualizo Dll en Produccion WEB SERVICE Ws_BataPOS", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnwsbata_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string ruta_server = @"\\192.168.2.6\inetpub\wwwroot\web_site_tienda\bin";
                string ruta_local = @"D:\Fuentes\Web Service Bata Tienda\WSDL Tienda\WSDL Tienda\WSDL Tienda\bin";

                string _WSDL_Tienda = "WSDL Tienda.dll";
                //string _CapaEntidad = "CapaDato.dll";
                //string _CapaOraDato = "CapaEntidad.dll";
                //string _CapaPresentacion = "WS_Bata_Interfaces.dll";

                byte[] _bataweb_dll = null;
                _bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _WSDL_Tienda);
                File.WriteAllBytes(@ruta_server + "\\" + _WSDL_Tienda, _bataweb_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaEntidad);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaEntidad, _bataweb_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaOraDato);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaOraDato, _bataweb_dll);

                //_bataweb_dll = File.ReadAllBytes(ruta_local + "\\" + _CapaPresentacion);
                //File.WriteAllBytes(@ruta_server + "\\" + _CapaPresentacion, _bataweb_dll);

                MessageBox.Show("Se Actualizo Dll en Produccion WEB SERVICE web_site_tienda", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btncompartir_Click(object sender, EventArgs e)
        {
         
        }

        private void btn_crear_cliente_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            BataClub clientes = new BataClub();
            string error= clientes.creacion_actualizacion_clientes_orce();
            MessageBox.Show(error, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
        }

        private void btn_valida_correo_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ValidacionEmail verifica = new ValidacionEmail();
                Boolean valida= verifica.sendEmail_verificar(txtcorreo.Text);

                MessageBox.Show((valida) ? "El correo " + txtcorreo.Text + " es CORRECTO" : "El correo " + txtcorreo.Text + " es INCORRECTO", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Information);
                Cursor.Current = Cursors.Default;

            }
            catch (Exception exc)
            {

                
            }

            

            //ValidacionEmail verifica = new ValidacionEmail();
            //verifica.sendEmail_verificar("david_mendozap@hotmail.com");
        }

        private void btndni_Click(object sender, EventArgs e)
        {
            ValidaDNI obj_valida = new ValidaDNI();
            Boolean valida= obj_valida.ValidateDocument(txtdni.Text);

            MessageBox.Show((valida) ? "El DNI " + txtdni.Text + " es INCORRECTO" : "El DNI " + txtdni.Text + " es CORRECTO", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btn_tarjeta_cliente_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            BataClub clientes = new BataClub();
            string error = clientes.asociar_tarjeta_cliente();

            MessageBox.Show(error, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
        }
    }
    public class objeto_json
    {
        public VerifyEmailJsonResult VerifyEmailJsonResult { get; set; }
        //public string Message { get; set; }
        //public string MessageDetail { get; set; }
        //public Int32 StatusCode { get; set; }

    }
    public class VerifyEmailJsonResult
    {
        public Data Data { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public Int32 StatusCode { get; set; }
    }
    public class Response_obj
    {
        public string result { get; set; }
        public string reason { get; set; }
        public string role { get; set; }
        public string free { get; set; }
        public string disposable { get; set; }
        public string accept_all { get; set; }
        public string did_you_mean { get; set; }
        public string sendex { get; set; }
        public string email { get; set; }
        public string user { get; set; }
        public string domain { get; set; }
        public string success { get; set; }
        public string message { get; set; }
    }
    public class Data
    {
        public string Response { get; set; }
        public Response_obj Response_obj { get; set; }

    }
}
