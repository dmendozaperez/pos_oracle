using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using CapaServicioWindows.Modular;
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

            var dd = batatran.HelloWorld(header_user,"50123");
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
            //string _error = "ing";
            //TextWriter tw = new StreamWriter(@"D:\ALMACEN\ERROR.txt", true);
            //tw.WriteLine(_error);
            //tw.Flush();
            //tw.Close();
            //tw.Dispose();
            //Cursor.Current = Cursors.WaitCursor;
            //string _erro = "";
            //Basico cc = new Basico();
            //cc.procesar_dbf_pos(ref _erro);
            //cc.eje_envio_guias(ref _erro);
            //MessageBox.Show("termino");
            //Cursor.Current = Cursors.Default;
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
            header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";

            List<BataTransac.Ent_File> file = new List<BataTransac.Ent_File>();
         

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

        private void ws_get_time_servicetrans_Click(object sender, EventArgs e)
        {
            try
            { 
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
            header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";
                


            BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();

            var config = batatran.ws_get_time_servicetrans(header_user, "01");
            }
            catch
            {

            }
        }

        private void ws_envia_stock_tda_Click(object sender, EventArgs e)
        {
            try
            {
                /*user y password*/
                BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
                header_user.Username = "3D4F4673-98EB-4EB5-A468-4B7FAEC0C721";
                header_user.Password = "566FDFF1-5311-4FE2-B3FC-0346923FE4B4";


                BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();
                
                /*list*/
                List<BataTransac.Ent_Stock> result = new List<BataTransac.Ent_Stock>();

                //using (StreamReader sr = new StreamReader(@"D:\FSTKG.TXT"))
                //{
                //    string linea;
                //    while ((linea = sr.ReadLine()) != null)
                //    {
                //        BataTransac.Ent_Stock stk = new BataTransac.Ent_Stock();
                //        string[] Array_lista = linea.Split(Convert.ToChar(","));
                //        Array_lista[0]= Array_lista[0].Replace("\"","").Trim().TrimEnd();
                //        Array_lista[1] = Array_lista[1].Replace("\"", "").Trim().TrimEnd();

                //        stk.cod_tda = "50" + Array_lista[0].ToString();
                //        stk.art_cod = Array_lista[1].Substring(0, 7);
                //        stk.art_cal = Array_lista[1].Substring(7, 1);

                //        stk._0 = Array_lista[3].ToString();
                //        stk._1 = Array_lista[4].ToString();
                //        stk._2 = Array_lista[5].ToString();
                //        stk._3 = Array_lista[6].ToString();
                //        stk._4 = Array_lista[7].ToString();
                //        stk._5 = Array_lista[8].ToString();
                //        stk._6 = Array_lista[9].ToString();
                //        stk._7 = Array_lista[10].ToString();
                //        stk._8 = Array_lista[11].ToString();
                //        stk._9 = Array_lista[12].ToString();
                //        stk._10 = Array_lista[13].ToString();
                //        stk._11 = Array_lista[14].ToString();

                //        result.Add(stk);
                //    }

                //}
                BataTransac.Ent_Stock stk = new BataTransac.Ent_Stock();
                stk.cod_tda = "50140";
                stk.art_cod = "0018623";
                stk.art_cal = "1";
                stk.art_talla = "20";
                stk.art_pares = 20;
                result.Add(stk);

                var array = new BataTransac.Ent_Lista_Stock();
                array.lista_stock = result.ToArray();
               
                BataTransac.Ent_MsgTransac msg = batatran.ws_envia_stock_tda(header_user, array);

                /*Nota*/
                //msg.codigo = "0";
                //msg.descripcion = "Se actualizo correctamente";
                
                //msg.codigo = "1";
                //msg.descripcion = "descripcion de error";

                MessageBox.Show(msg.descripcion);

            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
        }

        private void ws_get_stk_tda_Click(object sender, EventArgs e)
        {
            /*user y password*/
            BataEC.ValidateAcceso header_user = new BataEC.ValidateAcceso();
            header_user.Username = "EA646294-11F4-4836-8C6E-F5D9B5F681FC";
            header_user.Password = "DB959DFE-E49A-4F9B-8CD5-97364EE31FBA";


            BataEC.BataEcommerceSoapClient batatran = new BataEC.BataEcommerceSoapClient();

            var result = batatran.ws_get_stk_tda(header_user, "5533806", "40", "150101");

        }

        private void ws_transmision_ingreso_nube_Click(object sender, EventArgs e)
        {
            BataTransac.ValidateAcceso header_user = new BataTransac.ValidateAcceso();
            header_user.Username = "emcomer";
            header_user.Password = "Bata2013";

            BataTransac.Bata_TransactionSoapClient batatran = new BataTransac.Bata_TransactionSoapClient();

            string pathFileIn = @"D:\TD180821.143";



            byte[] _archivo_bytes = File.ReadAllBytes(pathFileIn);

            String[] _mensaje = batatran.ws_transmision_ingreso_nube(header_user, _archivo_bytes, "TD180811.143");



        }
    }
}
