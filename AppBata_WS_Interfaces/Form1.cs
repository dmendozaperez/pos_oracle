using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaServicioWindows.Modular;
using System.IO;

namespace AppBata_WS_Interfaces
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnhel_Click(object sender, EventArgs e)
        {
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();

            header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
            header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

            var dd = batatran.HelloWorld(header_user);
        }

        private void btn_ws_update_transaction_guias_Click(object sender, EventArgs e)
        {
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
            header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

            BataTransac.Ent_Fvdespc ent = new BataTransac.Ent_Fvdespc();
            ent.DESC_ALMAC = "ok";

            BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();
            
            //batatran.ws_update_transaction_guias(header_user,ent);
        }

        private void btnenvio_Click(object sender, EventArgs e)
        {

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
            Basico cc = new Basico();
            cc.eje_envio_guias();
            MessageBox.Show("termino");
            Cursor.Current = Cursors.Default;
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
            header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

            List<BataTransac.Ent_File> file = new List<BataTransac.Ent_File>();

            // BataTransac.Ent_File ad = new BataTransac.Ent_File();
            //ad.file_name = "xxxx";     
            //file.Add(ad);

            //ad = new BataTransac.Ent_File();
            //ad.file_name = "yyyy";
            //file.Add(ad);

            //var array = new BataTransac.Ent_Lista_File();
            //array.lista_file_name = file.ToArray();

            BataTransac.Bata_TransactionSoapClient get_met = new BataTransac.Bata_TransactionSoapClient();

           
          

            BataTransac.Ent_File_Ruta fileruta = get_met.ws_get_file_path(header_user, "01");


            string ruta_origen = fileruta.file_origen;
            
            if (Directory.Exists(ruta_origen))
            {
                string[] _fotos = Directory.GetFiles(@ruta_origen, "*.jpg");


                var nom = _fotos.Select(Path.GetFileName).ToArray();

                List<BataTransac.Ent_File> result = (from element in nom
                             select new BataTransac.Ent_File
                             { file_name= element,
                             }).ToList();

                /*solo retornar lo que en la base de datos no existe*/
                var array = new BataTransac.Ent_Lista_File();
                array.lista_file_name = result.ToArray();
                var get_fileN = get_met.ws_get_file_upload(header_user, "01", array);


                foreach(var itemname in get_fileN)
                {
                    var file_fil = _fotos.Where(f => f.Contains(itemname.file_name.ToString())).ToList(); 

                    if (file_fil!=null)
                    {
                        if (file_fil.Count>0)
                        {
                            string ruta_file = file_fil[0].ToString();
                            byte[] file_bytes = File.ReadAllBytes(@ruta_file);
                            string nom_file = Path.GetFileName(@ruta_file);

                            get_met.ws_download_file(header_user, file_bytes, nom_file, "01");

                        }
                    }


                }


            }

           //foreach(var item in get_fileN)
           //{


           //} 


        }
    }
}
