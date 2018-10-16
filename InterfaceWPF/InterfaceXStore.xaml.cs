using CapaDato;
using CapaDato.Interfaces;
using CapaDato.Logistica;
using CapaDato.Venta;
using CapaDato.Poslog;
using CapaEntidad;
using CapaEntidad.Logistica;
using CapaEntidad.Poslog;
using CapaEntidad.Util;
using InterfaceWPF.Bll;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ICSharpCode.SharpZipLib.Zip;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace InterfaceWPF
{
    /// <summary>
    /// Lógica de interacción para InterfaceXStore.xaml
    /// </summary>
    public partial class InterfaceXStore : MetroWindow
    {
        public InterfaceXStore()
        {
            InitializeComponent();
        }

        private Dat_Interface dat_interface = null;
        private Basico basico = null;
        private DataTable gdt_Orce = new DataTable();
        private DataTable gdt_Xoficce = new DataTable();
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadIniWPF();
            LoadAmbiente();
        }

        #region<REGION DE INTERFACES RETAIL_LOCATION Y  RETAIL_LOCATION_PROPERTY>

        private void LoadIniWPF()
        {
            basico = new Basico();
            dat_interface = new Dat_Interface();

            Dat_Interface _tienda = new Dat_Interface();
            /*maestros de tienda*/
            dwtienda.ItemsSource = _tienda.get_tienda("PE", true);
            dwtienda.DisplayMember = "des_entid";
            dwtienda.ValueMember = "cod_entid";
            dwtienda.SelectedIndex = 0;
            dwtienda.Focus();

            /*maestros de tienda*/
            dwtienda_M.ItemsSource = _tienda.get_tienda("PE", true);
            dwtienda_M.DisplayMember = "des_entid";
            dwtienda_M.ValueMember = "cod_entid";
            dwtienda_M.SelectedIndex = 0;
            dwtienda_M.Focus();

            /*stock de tienda*/
            dwtiendastk.ItemsSource = _tienda.get_tienda("PE", true);
            dwtiendastk.DisplayMember = "des_entid";
            dwtiendastk.ValueMember = "cod_entid";
            dwtiendastk.SelectedIndex = 0;
            dwtiendastk.Focus();


            /*transferencia de tienda invalid*/
            dwtiendatrans.ItemsSource = _tienda.get_tienda("PE");
            dwtiendatrans.DisplayMember = "des_entid";
            dwtiendatrans.ValueMember = "cod_entid";
            dwtiendatrans.SelectedIndex = -1;
            dwtiendatrans.Focus();

            /*transpaso de almacen a tienda*/
            dwtdaOrigen.ItemsSource = _tienda.get_tienda("PE", true);
            dwtdaOrigen.DisplayMember = "des_entid";
            dwtdaOrigen.ValueMember = "cod_entid";
            dwtdaOrigen.SelectedIndex = 0;
            dwtdaOrigen.Focus();

            dwtiendatrans1.ItemsSource = _tienda.get_tienda("PE",true);
            dwtiendatrans1.DisplayMember = "des_entid";
            dwtiendatrans1.ValueMember = "cod_entid";
            dwtiendatrans1.SelectedIndex = 0;
            dwtiendatrans1.Focus();
            dtpdesde.Text = DateTime.Today.ToString();
            dtphasta.Text = DateTime.Today.ToString();

            dtpfecha.Text = DateTime.Today.ToString();

            /* tiendas BCL*/
            dwtienda_bcl.ItemsSource = _tienda.get_tienda("PE");
            dwtienda_bcl.DisplayMember = "des_entid";
            dwtienda_bcl.ValueMember = "cod_entid";
            dwtienda_bcl.SelectedIndex = -1;
            dwtienda_bcl.Focus();

            /*stock de tienda*/
            dwtienda_dbf.ItemsSource = _tienda.get_tienda("PE");
            dwtienda_dbf.DisplayMember = "des_entid";
            dwtienda_dbf.ValueMember = "cod_entid";
            dwtienda_dbf.SelectedIndex = -1;
            dwtienda_dbf.Focus();

            dwtienda_ven.ItemsSource = _tienda.get_tienda("PE");
            dwtienda_ven.DisplayMember = "des_entid";
            dwtienda_ven.ValueMember = "cod_entid";
            dwtienda_ven.SelectedIndex = -1;
            dwtienda_ven.Focus();
        }


        private void LoadAmbiente()
        {
            basico = new Basico();
            dat_interface = new Dat_Interface();

            Dat_Interface _ambiente= new Dat_Interface();
            /*maestros de tienda*/
            DataTable dt = new DataTable();
            DataTable dtOrce = new DataTable();
            dt = _ambiente.get_ambiente_xoficce("PE");
            dtOrce = _ambiente.get_ambiente_orce("PE");
            gdt_Xoficce = dt;
            gdt_Orce = dtOrce;


            /*maestros de tienda*/
            dwAmbienteT.ItemsSource = dt;
            dwAmbienteT.DisplayMember = "Amb_Des";
            dwAmbienteT.ValueMember = "Amb_Cod";
            dwAmbienteT.SelectedIndex = 0;
            dwAmbienteT.Focus();

            /*maestros de maestros*/
            dwAmbiente_M.ItemsSource = dt;
            dwAmbiente_M.DisplayMember = "Amb_Des";
            dwAmbiente_M.ValueMember = "Amb_Cod";
            dwAmbiente_M.SelectedIndex = 0;
            dwAmbiente_M.Focus();

            /*maestros de stock*/
            dwAmbiente_stk.ItemsSource = dt;
            dwAmbiente_stk.DisplayMember = "Amb_Des";
            dwAmbiente_stk.ValueMember = "Amb_Cod";
            dwAmbiente_stk.SelectedIndex = 0;
            dwAmbiente_stk.Focus();

            /*maestros de bcl*/
            dwAmbiente_bcl.ItemsSource = dt;
            dwAmbiente_bcl.DisplayMember = "Amb_Des";
            dwAmbiente_bcl.ValueMember = "Amb_Cod";
            dwAmbiente_bcl.SelectedIndex = 0;
            dwAmbiente_bcl.Focus();


            /*maestros de shiping*/
            dwAmbiente_trans.ItemsSource = dt;
            dwAmbiente_trans.DisplayMember = "Amb_Des";
            dwAmbiente_trans.ValueMember = "Amb_Cod";
            dwAmbiente_trans.SelectedIndex = 0;
            dwAmbiente_trans.Focus();

            /*maestros de despacho*/
            dwAmbienteDsp.ItemsSource = dt;
            dwAmbienteDsp.DisplayMember = "Amb_Des";
            dwAmbienteDsp.ValueMember = "Amb_Cod";
            dwAmbienteDsp.SelectedIndex = 0;
            dwAmbienteDsp.Focus();

            /*Ambiente Orce */

            dwambiente_orce.ItemsSource = dtOrce;
            dwambiente_orce.DisplayMember = "Amb_Des";
            dwambiente_orce.ValueMember = "Amb_Cod";
            dwambiente_orce.SelectedIndex = 0;
            dwambiente_orce.Focus();

            dwambiente_ven.ItemsSource = dt;
            dwambiente_ven.DisplayMember = "Amb_Des";
            dwambiente_ven.ValueMember = "Amb_Cod";
            dwambiente_ven.SelectedIndex = 0;
            dwambiente_ven.Focus();



        }


        private void btntienda_Click(object sender, RoutedEventArgs e)
        {
            //CapaDato.Xoficce.StockAct d = new CapaDato.Xoficce.StockAct();
            //DataTable dt = d.get_stock_xoffice("50145");
            //Dat_PosLog poslog = new Dat_PosLog();
            //List<Ent_PosLog> lista_poslog = poslog.get_poslog();

            //foreach(var item in lista_poslog)
            //{
            //    var doc = XDocument.Parse(item.pos_log);

            //}

            generainter_retail_location();
        }

        private void llenarTiendaTda(object sender, RoutedEventArgs e)
        {            
            try
            {
                Dat_Interface _tienda = new Dat_Interface();
                string c =e.OriginalSource.ToString();
                string Pais = "";
                
                if (c.Contains("Peru"))
                    Pais = "PE";

                if (c.Contains("Ecuador"))
                    Pais = "EC";

                dwtienda.Items.Clear();

                dwtienda.ItemsSource = _tienda.get_tienda(Pais);
                dwtienda.DisplayMember = "des_entid";
                dwtienda.ValueMember = "cod_entid";
             
                dwtienda.SelectedIndex = -1;
                dwtienda.Focus();
            }
            catch (Exception ex) {
            }
        }

        private void llenarTiendaStk(object sender, RoutedEventArgs e)
        {
            try
            {
                Dat_Interface _tienda = new Dat_Interface();
                string Pais = "";
                string c = e.OriginalSource.ToString();

                if (c.Contains("Peru"))
                    Pais = "PE";

                if (c.Contains("Ecuador"))
                    Pais = "EC";

                dwtiendastk.ItemsSource = _tienda.get_tienda(Pais);
                dwtiendastk.DisplayMember = "des_entid";
                dwtiendastk.ValueMember = "cod_entid";
                dwtiendastk.SelectedIndex = -1;
                dwtiendastk.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        private void llenarTiendaShp(object sender, RoutedEventArgs e)
        {

            try
            {
                Dat_Interface _tienda = new Dat_Interface();
                string Pais = "";
                string c = e.OriginalSource.ToString();

                if (c.Contains("Peru"))
                    Pais = "PE";

                if (c.Contains("Ecuador"))
                    Pais = "EC";

                /*transferencia de tienda invalid*/
                dwtiendatrans.ItemsSource = _tienda.get_tienda(Pais);
                dwtiendatrans.DisplayMember = "des_entid";
                dwtiendatrans.ValueMember = "cod_entid";
                dwtiendatrans.SelectedIndex = -1;
                dwtiendatrans.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        private void llenarTiendaBCL(object sender, RoutedEventArgs e)
        {

            try
            {
                Dat_Interface _tienda = new Dat_Interface();
                string Pais = "";
                string c = e.OriginalSource.ToString();

                if (c.Contains("Peru"))
                    Pais = "PE";

                if (c.Contains("Ecuador"))
                    Pais = "EC";

                /*transferencia de tienda invalid*/
                dwtienda_bcl.ItemsSource = _tienda.get_tienda(Pais);
                dwtienda_bcl.DisplayMember = "des_entid";
                dwtienda_bcl.ValueMember = "cod_entid";
                dwtienda_bcl.SelectedIndex = -1;
                dwtienda_bcl.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        private void llenarTienda_ven(object sender, RoutedEventArgs e)
        {

            try
            {
                Dat_Interface _tienda = new Dat_Interface();
                string Pais = "";
                string c = e.OriginalSource.ToString();

                if (c.Contains("Peru"))
                    Pais = "PE";

                if (c.Contains("Ecuador"))
                    Pais = "EC";

                /*transferencia de tienda invalid*/
                dwtienda_ven.ItemsSource = _tienda.get_tienda(Pais);
                dwtienda_ven.DisplayMember = "des_entid";
                dwtienda_ven.ValueMember = "cod_entid";
                dwtienda_ven.SelectedIndex = -1;
                dwtienda_ven.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        private async void generainter_retail_location()
        {
            var metroWindow = this;
            
           metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {
                Boolean envio = false;
                string pais = "";
                string EnviarFTP = "N";
                string mensaje = "";
                string mensajeEspera = "Generando Interface";

                if (chk_ftp_Tda.IsChecked == true)
                {
                    EnviarFTP = "S";
                    mensajeEspera = "Enviando X FTP Interface: RETAIL LOCATION";
                }

                if (rbtPe_Tda.IsChecked == true)
                    pais = "PE";

                if (rbtEc_Tda.IsChecked == true)
                    pais = "EC";

                if (await valida_retail_location()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, mensajeEspera);  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);
                             

                string codtda = dwtienda.EditValue.ToString();
                string sufijoNombre = "";
                if (codtda != "-1") sufijoNombre = codtda +"_";
                /*DATOS DE INTERFACE RETAIL_LOCATION Y RETAIL_LOCATION_PROMERY*/
                /*Se recorre los datos del dataset y convertir a mnt el final del codigo y envia por un metodo en basico de envio
                 por ftp*/
                DataSet ds = await Task.Run(() => dat_interface.get_retail_location(codtda, pais));
                StringBuilder str = null;
                string str_cadena = "";
                if (ds != null)
                {
                    DataTable dt_retail_location = ds.Tables[0];
                    DataTable dt_retail_location_property = ds.Tables[1];
                    string name_retail_location = ""; string in_retail_location = "";
                    if (dt_retail_location.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_retail_location.Rows.Count; ++i)
                        {
                            str.Append(dt_retail_location.Rows[i]["RETAIL_LOCATION"].ToString());

                            if (i < dt_retail_location.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();



                        name_retail_location = "RETAIL_LOCATION_" + sufijoNombre +  DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_retail_location = ruta_interface + "\\" + name_retail_location;

                        if (File.Exists(@in_retail_location)) File.Delete(@in_retail_location);
                        File.WriteAllText(@in_retail_location, str_cadena);
                    }
                    if (dt_retail_location_property.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_retail_location_property.Rows.Count; ++i)
                        {
                            str.Append(dt_retail_location_property.Rows[i]["RETAIL_LOCATION_PROPERTY"].ToString());

                            if (i < dt_retail_location_property.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();

                        name_retail_location = "RETAIL_LOCATION_PROPERTY_" + sufijoNombre +  DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_retail_location = ruta_interface + "\\" + name_retail_location;

                        if (File.Exists(@in_retail_location)) File.Delete(@in_retail_location);
                        File.WriteAllText(@in_retail_location, str_cadena);
                        envio = true;
                        mensaje = "Se creo en la ruta : " + in_retail_location;
                    }

                }

                if (EnviarFTP.Equals("S")) {
                    string codAmbiente = dwAmbienteT.EditValue.ToString();
                    setearAmbXoficce(codAmbiente);

                    mensaje = "Se enviaron al ftp";
                    envio = await Task.Run(() => basico.sendftp_file_mnt());
                }

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();
            }
            catch(Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }
        private async Task<Boolean> valida_retail_location()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            string _cod_tda_select = "";
            if (dwtienda.EditValue != null)
            {           
               _cod_tda_select = dwtienda.EditValue.ToString();
            }
            if (_cod_tda_select.Length==0)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccion la Tienda.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                dwtienda.Focus();
                valida = true;
                return valida;
            }

            if ((chk_retail_location.IsChecked==false) && (chk_retail_location_property.IsChecked==false))
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccione al menos una interface.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                
                valida = true;
                return valida;
            }

            return valida;
        }
        #endregion
        #region<REGION DE INTERFACES DE MAESTROS>
        private void btnmaestros_Click(object sender, RoutedEventArgs e)
        {
            generainter_maestros();
        }

        private async void generainter_maestros()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {
                string pais = "";
                string EnviarFTP = "N";
                string mensaje = "";
                string mensajeEspera = "Generando Interface";
                Boolean envio = false;

                if (chk_ftp_M.IsChecked == true)
                {
                    EnviarFTP = "S";
                    mensajeEspera = "Enviando X FTP Interface: Seleccionadas";
                }

                if (rbtPe_M.IsChecked == true)
                    pais = "PE";

                if (rbtEcu_M.IsChecked == true)
                    pais = "EC";

                //Boolean envio = await Task.Run(() => basico.sendftp_file_mnt());
                if (await valida_maestros()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, mensajeEspera);  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;
                string codtda = dwtienda_M.EditValue.ToString();
                string sufijoNombre = "";

                if (codtda != "-1") sufijoNombre = codtda + "_";

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);


                StringBuilder str = null;
                string str_cadena = "";
                string name_maestros = ""; string in_maestros = "";
                #region<DIMENSION TYPE>
                if (chk_item_dimension_type.IsChecked==true)
                {
                    DataTable  dt = await Task.Run(() => dat_interface.get_item_dimension_type(pais, codtda));
                    if (dt!=null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["ITEM_DIMENSION_TYPE"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "ITEM_DIMENSION_TYPE_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<DIMENSION VALUE>
                if (chk_item_dimension_value.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_item_dimension_value(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["ITEM_DIMENSION_VALUE"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "ITEM_DIMENSION_VALUE_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<ITEM>
                if (chk_item.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_item(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            Decimal i = 0;
                            foreach(DataRow fila in dt.Rows)
                            {
                              
                                str.Append(fila["ITEM"].ToString());
                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }
                                i += 1;

                            }

                            //for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            //{
                            //    str.Append(dt.Rows[i]["ITEM"].ToString());

                            //    if (i < dt.Rows.Count - 1)
                            //    {
                            //        str.Append("\r\n");

                            //    }

                            //}
                            str_cadena = str.ToString();



                            name_maestros = "ITEM_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<PRICE UPDATE 2>
                if (chk_price_update.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_price_update_2(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["PRICE_UPDATE_2"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "PRICE_UPDATE_2_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<ITEM IMAGES>
                if (chk_item_images.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_item_images(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["ITEM_IMAGES"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "ITEM_IMAGES_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<ORG HIER>
                if (chk_org_hier.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_org_hier(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            Decimal i = 0;
                            foreach (DataRow fila in dt.Rows)
                            {

                                str.Append(fila["ORG_HIER"].ToString());
                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }
                                i += 1;

                            }

                            //for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            //{
                            //    str.Append(dt.Rows[i]["ITEM"].ToString());

                            //    if (i < dt.Rows.Count - 1)
                            //    {
                            //        str.Append("\r\n");

                            //    }

                            //}
                            str_cadena = str.ToString();



                            name_maestros = "ORG_HIER_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<MERCH HIER>
                if (chk_merch_hier.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_merch_hier(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            Decimal i = 0;
                            foreach (DataRow fila in dt.Rows)
                            {

                                str.Append(fila["MERCH_HIER"].ToString());
                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }
                                i += 1;

                            }

                            //for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            //{
                            //    str.Append(dt.Rows[i]["ITEM"].ToString());

                            //    if (i < dt.Rows.Count - 1)
                            //    {
                            //        str.Append("\r\n");

                            //    }

                            //}
                            str_cadena = str.ToString();



                            name_maestros = "MERCH_HIER_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<PARTY>
                if (chk_party_supplier.IsChecked == true || chk_party_employee.IsChecked == true)
                {

                    string strCodEmpl = "N";
                    string strCodSupl = "N";

                    if(chk_party_supplier.IsChecked==true) strCodSupl = "S";
                    if (chk_party_employee.IsChecked == true) strCodEmpl = "S";

                    DataTable dt = await Task.Run(() => dat_interface.get_Party(pais, codtda, strCodSupl, strCodEmpl));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            Decimal i = 0;
                            foreach (DataRow fila in dt.Rows)
                            {

                                str.Append(fila["PARTY"].ToString());
                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }
                                i += 1;

                            }

                            //for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            //{
                            //    str.Append(dt.Rows[i]["ITEM"].ToString());

                            //    if (i < dt.Rows.Count - 1)
                            //    {
                            //        str.Append("\r\n");

                            //    }

                            //}
                            str_cadena = str.ToString();



                            name_maestros = "PARTY_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<lOCATION_PROPERTY>
                if (chk_Location_Property.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_Location_Property(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            Decimal i = 0;
                            foreach (DataRow fila in dt.Rows)
                            {

                                str.Append(fila["INV_LOCATION_PROPERTY"].ToString());
                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }
                                i += 1;

                            }
                            
                            str_cadena = str.ToString();

                            name_maestros = "INV_LOCATION_PROPERTY_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }


                }
                #endregion



                #region<ITEM XREF>
                if (chk_item_xref.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_item_xref(pais, codtda));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["ITEM_XREF"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "ITEM_XREF_" + sufijoNombre + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                envio = true;

                if (EnviarFTP.Equals("S"))
                {
                    string codAmbiente = dwAmbiente_M.EditValue.ToString();
                    setearAmbXoficce(codAmbiente);
                    mensaje = "Se enviaron al ftp";
                    envio = await Task.Run(() => basico.sendftp_file_mnt());
                }
                else {
                    mensaje = "Se creo en la ruta : " + ruta_interface;
                }

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();



            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }
        private async Task<Boolean> valida_maestros()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
           

            if ((chk_item_dimension_type.IsChecked == false) && (chk_item_dimension_value.IsChecked == false)
               && (chk_item.IsChecked == false) && (chk_price_update.IsChecked == false)
               && (chk_item_images.IsChecked == false) && (chk_item_xref.IsChecked == false)
               && (chk_merch_hier.IsChecked == false) && (chk_merch_hier.IsChecked == false) 
               && (chk_org_hier.IsChecked == false) && (chk_party_employee.IsChecked == false) 
               && (chk_party_supplier.IsChecked == false) && (chk_Location_Property.IsChecked == false))
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccione al menos una interface.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                valida = true; 
                return valida;
            }
            return valida;
        }
        #endregion

        #region<REGION OBTENER LAS VENTAS>


        private void btnVtaTienda_Click(object sender, RoutedEventArgs e)
        {
            generar_ventas_tda();
        }     



        private async void generar_ventas_tda()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            DataTable dt1 = new DataTable();
           
            try
            {
                string pais = "PE";
                string EnviarFTP = "N";
                string mensaje = "";
                string mensajeEspera = "Generando Interface";
                Boolean envio = false;

                if (chk_ftp_ven.IsChecked == true)
                {
                    EnviarFTP = "S";
                    mensajeEspera = "Enviando X FTP Interface: Seleccionadas";
                }

                //if (rbtPe_M.IsChecked == true)
                //    pais = "PE";

                //if (rbtEcu_M.IsChecked == true)
                //    pais = "EC";

                //Boolean envio = await Task.Run(() => basico.sendftp_file_mnt());
                if (await valida_Ext_Ventda()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, mensajeEspera);  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                string codtda = dwtienda_ven.EditValue.ToString();
                DateTime fechaIni = Convert.ToDateTime(dtpdesde_ven.Text);
                DateTime fecha_Fin = Convert.ToDateTime(dtphasta_ven.Text);

                string sufijoNombre = "";

                if (codtda != "-1") sufijoNombre = codtda ;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);


                StringBuilder str = null;
                string str_cadena = "";
                string name_maestros = ""; string in_archivos = "";

                Dat_Venta con_venta = new Dat_Venta();
               
                DataSet ds = await Task.Run(() => con_venta.SET_XSTORE_VENTA_EXPORTAR(codtda, fechaIni, fecha_Fin));

                #region<TRANS_LINE_TENDER>
                if (chk_line_tender.IsChecked == true)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["TRANS_LINE_TENDER"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "TRANS_LINE_TENDER_" + sufijoNombre + ".MNT";
                            in_archivos = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                            File.WriteAllText(@in_archivos, str_cadena);
                        }
                    }
                }
                #endregion
                #region<TRANS_TAX>
                if (chk_tax.IsChecked == true)
                {
                    DataTable dt = ds.Tables[1];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["TRANS_TAX"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "TRANS_TAX_" + sufijoNombre + ".MNT";
                            @in_archivos = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                            File.WriteAllText(@in_archivos, str_cadena);
                        }
                    }
                }
                #endregion
                #region<TRANS_LINE_ITEM_TAX>
                if (chk_item_Tax.IsChecked == true)
                {
                    DataTable dt = ds.Tables[2];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            Decimal i = 0;
                            foreach (DataRow fila in dt.Rows)
                            {

                                str.Append(fila["TRANS_LINE_ITEM_TAX"].ToString());
                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }
                                i += 1;

                            }

                      
                            str_cadena = str.ToString();



                            name_maestros = "TRANS_LINE_ITEM_TAX_" + sufijoNombre  + ".MNT";
                            in_archivos = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                            File.WriteAllText(@in_archivos, str_cadena);
                        }
                    }
                }
                #endregion
                #region<TRANS_LINE_ITEM>
                if (chk_line_item.IsChecked == true)
                {
                    DataTable dt = ds.Tables[3];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["TRANS_LINE_ITEM"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "TRANS_LINE_ITEM_" + sufijoNombre  + ".MNT";
                            in_archivos = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                            File.WriteAllText(@in_archivos, str_cadena);
                        }
                    }
                }
                #endregion
                #region<TRANS_HEADER>
                if (chk_header.IsChecked == true)
                {
                    DataTable dt = ds.Tables[4];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["TRANS_HEADER"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "TRANS_HEADER_" + sufijoNombre+ ".MNT";
                            in_archivos = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_archivos)) File.Delete(@in_archivos);
                            File.WriteAllText(@in_archivos, str_cadena);
                        }
                    }
                }
                #endregion

                
                envio = true;

                if (EnviarFTP.Equals("S"))
                {

                    string codAmbiente = dwambiente_ven.EditValue.ToString();
                    setearAmbXoficce(codAmbiente);

                    mensaje = "Se enviaron al ftp";
                    envio = await Task.Run(() => basico.sendftp_file_mnt());
                }
                else
                {
                    mensaje = "Se creo en la ruta : " + ruta_interface;
                }

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();



            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }

        private async Task<Boolean> valida_Ext_Ventda()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;


            if ((chk_tax.IsChecked == false) && (chk_item_Tax.IsChecked == false)
               && (chk_line_item.IsChecked == false) && (chk_header.IsChecked == false)
               && (chk_line_tender.IsChecked == false))
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccione al menos una interface.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                valida = true;
                return valida;
            }
            return valida;
        }

        #endregion

        #region<REGION DE STOCK >
        private void btnstock_Click(object sender, RoutedEventArgs e)
        {
            generainter_stock_ledger();
        }
        private async void generainter_stock_ledger()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {
                Boolean envio = false;
                string pais = "";
                string EnviarFTP = "N";
                string mensaje = "";
                string mensajeEspera = "Generando Interface";

                if (chk_ftp_stk.IsChecked == true)
                {
                    EnviarFTP = "S";
                    mensajeEspera = "Enviando X FTP Interface: STOCK LEDGER";
                }

                if (rbtPe_stk.IsChecked == true)
                    pais = "PE";

                if (rbtEcu_stk.IsChecked == true)
                    pais = "EC";



                if (await valida_stock_ledger()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, mensajeEspera);  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);

                string codtda = dwtiendastk.EditValue.ToString();
                string fecha = Convert.ToDateTime(dtpfecha.ToString()).ToString("yyyyMMdd");

                if (codtda == "-1")
                    codtda = "0";

            /*Se recorre los datos del dataset y convertir a mnt el final del codigo y envia por un metodo en basico de envio
             por ftp*/
            DataTable dt = await Task.Run(() => dat_interface.get_stock_ledger(fecha,codtda,pais));
                StringBuilder str = null;
                string str_cadena = "";
                if (dt != null)
                {                    
                    string name_stock_ledger = ""; string in_stock_ledger = "";
                    if (dt.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                        {
                            str.Append(dt.Rows[i]["STOCK_LEDGER"].ToString());

                            if (i < dt.Rows.Count - 1)
                            {
                                str.Append("\r\n");
                            }
                        }
                        str_cadena = str.ToString();
                        
                        name_stock_ledger = "STOCK_LEDGER_" + codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_stock_ledger = ruta_interface + "\\" + name_stock_ledger;

                        if (File.Exists(@in_stock_ledger)) File.Delete(@in_stock_ledger);
                        File.WriteAllText(@in_stock_ledger, str_cadena);

                        mensaje = "Se creo en la ruta : " + in_stock_ledger;
                    }                 
                }

                if (EnviarFTP.Equals("S"))
                {
                    string codAmbiente = dwAmbiente_stk.EditValue.ToString();
                    setearAmbXoficce(codAmbiente);
                    mensaje = "Se enviaron al ftp";
                    envio = await Task.Run(() => basico.sendftp_file_mnt());
                }

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();
            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }
        private async Task<Boolean> valida_stock_ledger()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            string _cod_tda_select = "";
            if (dwtiendastk.EditValue != null)
            {
                _cod_tda_select = dwtiendastk.EditValue.ToString();
            }
            if (_cod_tda_select.Length == 0)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccion la Tienda.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                dwtiendastk.Focus();
                valida = true;
                return valida;
            }

            if ((chk_stock_ledger.IsChecked == false))
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccione al menos una interface.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                valida = true;
                return valida;
            }

            return valida;
        }
        #endregion
        #region<REGION DE TRANSFERENCIA DE TIENDA>
       
        private async void generainter_inv_valid_destinations()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {

                string pais = "";
                Boolean envio = false;
                string EnviarFTP = "N";
                string mensaje = "";
                string mensajeEspera = "Generando Interface";

                if (chk_ftp_sh.IsChecked == true)
                {
                    EnviarFTP = "S";
                    mensajeEspera = "Enviando X FTP Interface: SHIPPING";
                }

                if (rbtPe_sh.IsChecked == true)
                    pais = "PE";

                if (rbtEcu_sh.IsChecked == true)
                    pais = "EC";

                //Boolean Senvio = await Task.Run(() => basico.sendftp_file_mnt());

                //return;
                if (await valida_inv_valid_destinations()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, mensajeEspera);  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);

                string codtda =dwtiendatrans.EditValue.ToString();
                /*DATOS DE INTERFACE INVALID */
                /*Se recorre los datos del dataset y convertir a mnt el final del codigo y envia por un metodo en basico de envio
                 por ftp*/

                if (chk_inv_valid_destinations.IsChecked == true) {
                    DataTable dt = await Task.Run(() => dat_interface.get_inv_valid_destinations(codtda, pais));
                    StringBuilder str = null;
                    string str_cadena = "";
                    if (dt != null)
                    {                   
                        string name_inv_valid = ""; string in_inv_valid = "";
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["INV_VALID_DESTINATIONS"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_inv_valid = "INV_VALID_DESTINATIONS_" + codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_inv_valid = ruta_interface + "\\" + name_inv_valid;

                            if (File.Exists(@in_inv_valid)) File.Delete(@in_inv_valid);
                            File.WriteAllText(@in_inv_valid, str_cadena);

                            envio = true;
                            mensaje = "Se creo en la ruta : " + ruta_interface + "\\" + name_inv_valid;
                        }                   

                    }
                }

                if (chk_inv_valid_destinations_pro.IsChecked == true)
                {
                    DataTable dtP = await Task.Run(() => dat_interface.get_inv_valid_destinations_property(codtda, pais));
                    StringBuilder strP = null;
                    string str_cadenaP = "";
                    if (dtP != null)
                    {
                        string name_inv_valid = ""; string in_inv_valid = "";
                        if (dtP.Rows.Count > 0)
                        {
                            strP = new StringBuilder();
                            for (Int32 i = 0; i < dtP.Rows.Count; ++i)
                            {
                                strP.Append(dtP.Rows[i]["INV_VALID_DESTINATIONS_PROPERTY"].ToString());

                                if (i < dtP.Rows.Count - 1)
                                {
                                    strP.Append("\r\n");

                                }

                            }
                            str_cadenaP = strP.ToString();



                            name_inv_valid = "INV_VALID_DESTINATIONS_PROPERTY_" + codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_inv_valid = ruta_interface + "\\" + name_inv_valid;

                            if (File.Exists(@in_inv_valid)) File.Delete(@in_inv_valid);
                            File.WriteAllText(@in_inv_valid, str_cadenaP);

                            envio = true;
                            mensaje = "Se creo en la ruta : " + ruta_interface + "\\" + name_inv_valid;
                        }

                    }
                }

                if (EnviarFTP.Equals("S"))
                {
                    string codAmbiente = dwAmbiente_trans.EditValue.ToString();
                    setearAmbXoficce(codAmbiente);
               
                    mensaje = "Se enviaron al ftp";
                    envio = await Task.Run(() => basico.sendftp_file_mnt());
                }

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();
            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }
        private async Task<Boolean> valida_inv_valid_destinations()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            string _cod_tda_select = "";
            if (dwtiendatrans.EditValue != null)
            {
                _cod_tda_select =dwtiendatrans.EditValue.ToString();
            }
            if (_cod_tda_select.Length == 0)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccion la Tienda.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                dwtiendatrans.Focus();
                valida = true;
                return valida;
            }

            if ((chk_inv_valid_destinations.IsChecked == false)&& (chk_inv_valid_destinations_pro.IsChecked == false))
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccione al menos una interface.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                valida = true;
                return valida;
            }

            return valida;
        }
      
        private void btntrans_Click(object sender, RoutedEventArgs e)
        {
            generainter_inv_valid_destinations();
        }
        #endregion
        #region<REGION LOGITICA DE GUIAS DE TRASPASO>
        private async void  consultar_guias()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            Dat_GuiasDespacho con_guia = null;
            try
            {
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Consultando Guias de Transpasos");  //show message
                ProgressAlert.SetIndeterminate();
                string _cod_tda = dwtiendatrans1.EditValue.ToString();
                string _nro_Doc = Docu.Text;
                DateTime _fec_ini =Convert.ToDateTime(dtpdesde.Text);
                DateTime _fec_fin = Convert.ToDateTime(dtphasta.Text);
                con_guia = new Dat_GuiasDespacho();                
                dg_guia.ItemsSource=await Task.Run(()=>con_guia.get_guias_tda_cab(_fec_ini, _fec_fin, _cod_tda, _nro_Doc));
                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();
            }
            catch(Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }

       

        private void btnconsultag_Click(object sender, RoutedEventArgs e)
        {
            consultar_guias();
        }

        private void btngenerarDBF_Click(object sender, RoutedEventArgs e)
        {
            generar_dbf();
        }


        private async void generar_dbf()
        {
            var metroWindow = this;
            if (await valida_generar_dbf()) return;

            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            Dat_Venta con_venta = null;
            
            try
            {             

                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Generando DBF");  //show message
                ProgressAlert.SetIndeterminate();
                string cod_tda = dwtienda_dbf.EditValue.ToString();
                DateTime fecha = Convert.ToDateTime(dtpFecha_DBF.Text);
              
                con_venta = new Dat_Venta();
                DataSet ds  = await Task.Run(() => con_venta.GET_OBTENER_VENTA_XSTORE(cod_tda, fecha));

                tabla_FFACTC(ds.Tables[0]);
                tabla_FFACTD(ds.Tables[1]);
                tabla_FNOTAA(ds.Tables[2]);
                tabla_FSTKG(ds.Tables[3]);
                tabla_FCIERR(ds.Tables[4]);
                tabla_FFLASH(ds.Tables[5]);

                string archivo = "";
                _comprimir_archivo(cod_tda, fecha, ref archivo);

                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();

                string _path_envia = basico.ruta_temp_DBF;
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Se genero el comprimido DBF en la ruta: " + _path_envia, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }

        private void tabla_FFLASH(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET fflash = new DBFNET();
                fflash.tabla = "FFLASH";

                fflash.addcol("fc_ctda", Tipo.Caracter, "3");
                fflash.addcol("fc_fech", Tipo.Fecha);
                fflash.addcol("fc_nsem", Tipo.Numerico, "2");
                fflash.addcol("fc_ndse", Tipo.Numerico, "2");
                fflash.addcol("fc_sica", Tipo.Numerico, "10");
                fflash.addcol("fc_vund", Tipo.Numerico, "4");
                fflash.addcol("fc_vimp", Tipo.Numerico, "10");
                fflash.addcol("fc_gtos", Tipo.Numerico, "10");
                fflash.addcol("fc_rete", Tipo.Numerico, "10");
                fflash.addcol("fc_depo", Tipo.Numerico, "10");
                fflash.addcol("fc_bcod", Tipo.Caracter, "2");
                fflash.addcol("fc_difd", Tipo.Numerico, "10");
                fflash.addcol("fc_motd", Tipo.Caracter, "40");
                fflash.addcol("fc_depd", Tipo.Numerico, "10");
                fflash.addcol("fc_bcdo", Tipo.Caracter, "2");
                fflash.addcol("fc_pdes", Tipo.Numerico, "10");
                fflash.addcol("fc_pbcs", Tipo.Caracter, "2");
                fflash.addcol("fc_pded", Tipo.Numerico, "10");
                fflash.addcol("fc_pbcd", Tipo.Caracter, "2");
                fflash.addcol("fc_ftrx", Tipo.Fecha);
                fflash.addcol("fc_resp", Tipo.Caracter, "3");

                fflash.creardbf(_path_envia);
                fflash.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }

        private void tabla_FCIERR(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET fcierr = new DBFNET();
                fcierr.tabla = "FCIERR";
                               
                fcierr.addcol("Ci_csuc", Tipo.Caracter, "3");
                fcierr.addcol("Ci_fech", Tipo.Fecha);
                fcierr.addcol("Ci_esta", Tipo.Caracter, "1");
                fcierr.addcol("Ci_fetr", Tipo.Fecha);
                fcierr.addcol("Ci_cuse", Tipo.Caracter, "3");
                fcierr.addcol("Ci_muse", Tipo.Caracter, "3");
                fcierr.addcol("Ci_fcre", Tipo.Fecha);
                fcierr.addcol("Ci_fmod", Tipo.Fecha);
                fcierr.addcol("Ci_impz", Tipo.Caracter, "1");
                fcierr.addcol("Ci_aper", Tipo.Caracter, "30");
                fcierr.addcol("Ci_cier", Tipo.Caracter, "30");

                fcierr.creardbf(_path_envia);
                fcierr.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }

        private void tabla_FSTKG(DataTable dtstk)
        {
            try
            {
                StringBuilder str = null;
                string str_cadena = "";
               

                if (dtstk.Rows.Count > 0)
                {
                    str = new StringBuilder();
                    for (Int32 i = 0; i < dtstk.Rows.Count; ++i)
                    {
                        string cad_envio = "\""+ dtstk.Rows[i]["TIENDA"].ToString() + "\"" + ",\"" + dtstk.Rows[i]["ARTICULO"].ToString() + "\"" + "," +
                                           dtstk.Rows[i]["TOTAL"].ToString() + "," + dtstk.Rows[i]["00"].ToString() + "," +
                                           dtstk.Rows[i]["01"].ToString() + "," + dtstk.Rows[i]["02"].ToString() + "," +
                                           dtstk.Rows[i]["03"].ToString() + "," + dtstk.Rows[i]["04"].ToString() + "," +
                                           dtstk.Rows[i]["05"].ToString() + "," + dtstk.Rows[i]["06"].ToString() + "," +
                                           dtstk.Rows[i]["07"].ToString() + "," + dtstk.Rows[i]["08"].ToString() + "," +
                                           dtstk.Rows[i]["09"].ToString() + "," + dtstk.Rows[i]["10"].ToString() + "," +
                                           dtstk.Rows[i]["11"].ToString() + "," + dtstk.Rows[i]["FECHA"].ToString() ;

                        str.Append(cad_envio);

                        if (i < dtstk.Rows.Count - 1)
                        {
                            str.Append("\r\n");

                        }

                    }
                    str_cadena = str.ToString();

                    string _path_envia = basico.ruta_temp_DBF;

                    if (!Directory.Exists(_path_envia))
                        Directory.CreateDirectory(_path_envia);
                    string file_stk = "FSTKG";
                    string ruta_file_stk = _path_envia + "\\" + file_stk + ".txt";                 

                    if (File.Exists(@ruta_file_stk)) File.Delete(@ruta_file_stk);
                    File.WriteAllText(@ruta_file_stk, str_cadena);
                }

                //DBFNET venta_det = new DBFNET();
                //venta_det.creartxt_stk(_path_envia);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private  void tabla_FFACTD(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET venta_det = new DBFNET();
                venta_det.tabla = "FFACTD";
                venta_det.addcol("fd_nint", Tipo.Caracter, "8");
                venta_det.addcol("fd_tipo", Tipo.Caracter, "1");
                venta_det.addcol("fd_arti", Tipo.Caracter, "12");
                venta_det.addcol("fd_regl", Tipo.Caracter, "4");
                venta_det.addcol("fd_colo", Tipo.Caracter, "2");
                venta_det.addcol("fd_item", Tipo.Caracter, "3");
                venta_det.addcol("fd_icmb", Tipo.Caracter, "1");
                venta_det.addcol("fd_qfac", Tipo.Numerico, "8,3");
                venta_det.addcol("fd_lpre", Tipo.Caracter, "2");
                venta_det.addcol("fd_calm", Tipo.Caracter, "4");
                venta_det.addcol("fd_pref", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_dref", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_prec", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_brut", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp1", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vimp1", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_subt1", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp2", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vimp2", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_subt2", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pdct1", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vdct1", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_subt3", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vdct4", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vdc23", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vvta", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp3", Tipo.Numerico, "6,2");
                venta_det.addcol("fd_vimp3", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_pimp4", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_vimp4", Tipo.Numerico, "14,4");

                venta_det.addcol("fd_total", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_cuse", Tipo.Caracter, "3");
                venta_det.addcol("fd_muse", Tipo.Caracter, "3");
                venta_det.addcol("fd_fcre", Tipo.Fecha);
                venta_det.addcol("fd_fmod", Tipo.Fecha);
                venta_det.addcol("fd_auto", Tipo.Caracter, "6");
                venta_det.addcol("fd_dre2", Tipo.Numerico, "14,4");
                venta_det.addcol("fd_asoc", Tipo.Caracter, "13");

                venta_det.creardbf(_path_envia);
                venta_det.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }
        private void tabla_FNOTAA(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET venta_pago = new DBFNET();
                venta_pago.tabla = "FNOTAA";
                venta_pago.addcol("na_nota", Tipo.Caracter, "8");
                venta_pago.addcol("na_item", Tipo.Caracter, "3");
                venta_pago.addcol("na_mone", Tipo.Caracter, "2");
                venta_pago.addcol("na_tpag", Tipo.Caracter, "2");
                venta_pago.addcol("na_tasa", Tipo.Numerico, "10,4");
                venta_pago.addcol("na_cref", Tipo.Caracter, "2");
                venta_pago.addcol("na_sref", Tipo.Caracter, "4");
                venta_pago.addcol("na_nref", Tipo.Caracter, "22");
                venta_pago.addcol("na_vref", Tipo.Numerico, "14,4");
                venta_pago.addcol("na_vpag", Tipo.Numerico, "14,4");
                venta_pago.addcol("na_esta", Tipo.Caracter, "1");
                venta_pago.addcol("na_cier", Tipo.Caracter, "1");
                venta_pago.addcol("na_fcre", Tipo.Caracter, "30");
                venta_pago.addcol("na_fmod", Tipo.Caracter, "30");
                venta_pago.creardbf(_path_envia);
                venta_pago.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }
        private void tabla_FFACTC(DataTable dt)
        {
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
                DBFNET venta_cab = new DBFNET();
                venta_cab.tabla = "FFACTC";
                venta_cab.addcol("fc_nint", Tipo.Caracter, "8");
                venta_cab.addcol("fc_nnot", Tipo.Caracter, "8");
                venta_cab.addcol("fc_codi", Tipo.Caracter, "2");
                venta_cab.addcol("fc_suna", Tipo.Caracter, "2");
                venta_cab.addcol("fc_sfac", Tipo.Caracter, "4");
                venta_cab.addcol("fc_nfac", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ffac", Tipo.Fecha);
                venta_cab.addcol("fc_nord", Tipo.Caracter, "8");
                venta_cab.addcol("fc_cref", Tipo.Caracter, "2");
                venta_cab.addcol("fc_sref", Tipo.Caracter, "4");
                venta_cab.addcol("fc_nref", Tipo.Caracter, "8");
                venta_cab.addcol("fc_pvta", Tipo.Caracter, "2");
                venta_cab.addcol("fc_csuc", Tipo.Caracter, "3");
                venta_cab.addcol("fc_gvta", Tipo.Caracter, "2");
                venta_cab.addcol("fc_zona", Tipo.Caracter, "3");
                venta_cab.addcol("fc_clie", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ncli", Tipo.Caracter, "90");
                venta_cab.addcol("fc_nomb", Tipo.Caracter, "30");
                venta_cab.addcol("fc_apep", Tipo.Caracter, "30");
                venta_cab.addcol("fc_apem", Tipo.Caracter, "30");
                venta_cab.addcol("fc_dcli", Tipo.Caracter, "50");
                venta_cab.addcol("fc_cubi", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ruc", Tipo.Caracter, "20");
                venta_cab.addcol("fc_vuse", Tipo.Caracter, "3");
                venta_cab.addcol("fc_vend", Tipo.Caracter, "8");
                venta_cab.addcol("fc_ipre", Tipo.Caracter, "1");
                venta_cab.addcol("fc_tint", Tipo.Caracter, "2");
                venta_cab.addcol("fc_pint", Tipo.Numerico, "6,2");
                venta_cab.addcol("fc_lcsg", Tipo.Caracter, "2");
                venta_cab.addcol("fc_ncon", Tipo.Caracter, "30");
                venta_cab.addcol("fc_dcon", Tipo.Caracter, "30");
                venta_cab.addcol("fc_lcon", Tipo.Caracter, "20");
                venta_cab.addcol("fc_lruc", Tipo.Caracter, "11");
                venta_cab.addcol("fc_agen", Tipo.Caracter, "20");
                venta_cab.addcol("fc_mone", Tipo.Caracter, "2");
                venta_cab.addcol("fc_tasa", Tipo.Numerico, "10,4");
                venta_cab.addcol("fc_fpag", Tipo.Caracter, "2");

                venta_cab.addcol("fc_nlet", Tipo.Numerico, "2,0");
                venta_cab.addcol("fc_qtot", Tipo.Numerico, "8,2");
                venta_cab.addcol("fc_pref", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_dref", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_brut", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vimp1", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vimp2", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vdct1", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vdct4", Tipo.Numerico, "14,4");

                venta_cab.addcol("fc_pdc2", Tipo.Numerico, "6,2");
                venta_cab.addcol("fc_pdc3", Tipo.Numerico, "6,2");
                venta_cab.addcol("fc_vdc23", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vvta", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_vimp3", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_pimp4", Tipo.Numerico, "6,2");

                venta_cab.addcol("fc_vimp4", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_total", Tipo.Numerico, "14,4");
                venta_cab.addcol("fc_esta", Tipo.Caracter, "1");
                venta_cab.addcol("fc_tdoc", Tipo.Caracter, "1");
                venta_cab.addcol("fc_cuse", Tipo.Caracter, "3");
                venta_cab.addcol("fc_muse", Tipo.Caracter, "3");

                venta_cab.addcol("fc_fcre", Tipo.Fecha);
                venta_cab.addcol("fc_fmod", Tipo.Fecha);

                venta_cab.addcol("fc_hora", Tipo.Caracter, "8");
                venta_cab.addcol("fc_auto", Tipo.Caracter, "3");
                venta_cab.addcol("fc_ftx", Tipo.Caracter, "1");
                venta_cab.addcol("fc_estc", Tipo.Caracter, "1");
                venta_cab.addcol("fc_sexo", Tipo.Caracter, "1");
                venta_cab.addcol("fc_mpub", Tipo.Caracter, "2");
                venta_cab.addcol("fc_edad", Tipo.Caracter, "2");
                venta_cab.addcol("fc_regv", Tipo.Caracter, "25");
                venta_cab.creardbf(_path_envia);
                venta_cab.Insertar_tabla(dt, _path_envia);
            }
            catch
            {
                throw;
            }
        }

        private void _comprimir_archivo(string codTda,DateTime _fecha_proceso, ref string _archivo)
        {           
            ZipOutputStream zipOut = null;
            try
            {
                string _path_envia = basico.ruta_temp_DBF;
               
                string _comprimir = _path_envia + "\\Comp";

                if (!Directory.Exists(@_comprimir))
                    Directory.CreateDirectory(@_comprimir);

                string _ano = _fecha_proceso.ToString("yy");
                string _mes = _fecha_proceso.Month.ToString("D2");
                string _dia = _fecha_proceso.Day.ToString("D2");
                string _fecha = _ano + _mes + _dia;
                String[] filenames = Directory.GetFiles(_path_envia, "*.*");
                _archivo = "TD" + _fecha + "." + codTda.Substring(2, 3);
                string ruta_zip = @_comprimir + "\\TD" + _fecha + "." + codTda.Substring(2, 3);

                string[] _file_cmp = Directory.GetFiles(_comprimir, "*.*");
                //foreach (string f in _file_cmp)
                //{
                //    File.Delete(f.ToString());
                //}

                //if (File.Exists(ruta_zip))
                //{
                //    File.Delete(ruta_zip);
                //}

                //crear archivo zip
                zipOut = new ZipOutputStream(File.Create(@ruta_zip));

                //*********************               

                for (Int32 i = 0; i < filenames.Length; ++i)
                {
                    string _archivo_xml = filenames[i].ToString();
                    FileInfo fi = new FileInfo(_archivo_xml);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fi.Name);
                    FileStream sReader = File.OpenRead(_archivo_xml);
                    byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
                    sReader.Read(buff, 0, (int)sReader.Length);
                    entry.DateTime = fi.LastWriteTime;
                    entry.Size = sReader.Length;
                    sReader.Close();
                    zipOut.PutNextEntry(entry);
                    zipOut.Write(buff, 0, buff.Length);
                }

                zipOut.Finish();
                zipOut.Close();               

                string[] _file = Directory.GetFiles(_path_envia, "*.*");
                foreach (string f in _file)
                {
                    File.Delete(f.ToString());
                }



            }
            catch
            {
                if (zipOut != null)
                {
                    zipOut.Finish();
                    zipOut.Close();
                }
                throw;
            }           
        }

        private async Task<Boolean> valida_generar_dbf()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            string _cod_tda_select = "";
            if (dwtienda_dbf.EditValue != null)
            {
                _cod_tda_select = dwtienda_dbf.EditValue.ToString();
            }
            if (_cod_tda_select.Length == 0)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccion la Tienda.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                dwtienda.Focus();
                valida = true;
                return valida;
            }


            return valida;
        }

        private async void btndetalle_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {
                var button = sender as Button;

                if (button != null)
                {
                    var task = button.DataContext as Ent_GuiasDespacho_Cab;

                    if (task != null)
                    {
                        ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Espere un momento");  //show message
                        ProgressAlert.SetIndeterminate();
                        string cod_alm = task.cod_alm; string nro_guia = task.nro_guia;
                        Dat_GuiasDespacho cons_guia = new Dat_GuiasDespacho();
                        List<Ent_Guias_Articulo> list_guias_art= await Task.Run(() => cons_guia.get_guia_articulo_pares(cod_alm, nro_guia));

                        lbltg.Content=list_guias_art.Sum(s => s.total_pares).ToString();

                        string des_tda = task.des_tda;

                       
                        metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                        metroWindow.LeftWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                        metroWindow.RightWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                        metroWindow.WindowButtonCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                        metroWindow.IconOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;

                        dg_guiadet.ItemsSource = list_guias_art;

                        this.ToggleFlyout(0, "Det. de Guia; Tienda:" + des_tda + ",N° de Guia:" + nro_guia);
                        if (ProgressAlert.IsOpen)
                            await ProgressAlert.CloseAsync();
                    }
                }
            }
            catch (Exception exc)
            {

                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
            
        }
        private void ToggleFlyout(int index, string _header)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }
            flyout.Header = _header;
            flyout.IsOpen = !flyout.IsOpen;
        }
       

        private void btnenviotraspaso_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string strEnvia = "N";

            if (button != null)
            {
                var task = button.DataContext as Ent_GuiasDespacho_Cab;

                if (chk_ftp_Trans.IsChecked ==true) {

                    strEnvia = "S";
                }

                if (task != null)
                {
                  generainter_inv_doc(task.cod_alm, task.nro_guia, task.cod_tda, strEnvia);
                }
            }
        }
        private async void generainter_inv_doc(string cod_alm,string nro_guia,string cod_tda,string strEnviaFtp)
        {
            var metroWindow = this;
            string in_inv_doc = "";
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {
                              
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Enviando X FTP Interface: IN_DOC,INV_DOC_LINE_ITEM,CARTON");  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);

               
                /*DATOS DE INTERFACE INV DOC */
                /*Se recorre los datos del dataset y convertir a mnt el final del codigo y envia por un metodo en basico de envio
                 por ftp*/
                DataSet ds = await Task.Run(() => dat_interface.get_inv_doc(cod_alm,nro_guia));
                StringBuilder str = null;
                string str_cadena = "";
                if (ds != null)
                {
                    string name_inv_doc = ""; string name_inv_doc_line_item = "";
                    string name_carton="" ; 

                    DataTable dt_inv = null;DataTable dt_inv_doc_line_item = null;DataTable dt_carton = null;

                    if (ds.Tables.Count>0)
                    {
                        dt_inv = ds.Tables[0];
                        dt_inv_doc_line_item = ds.Tables[1];
                        dt_carton = ds.Tables[2];
                    }

                    /*INV_DOC*/
                    if (dt_inv.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_inv.Rows.Count; ++i)
                        {
                            str.Append(dt_inv.Rows[i]["INV_DOC"].ToString());

                            if (i < dt_inv.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();



                        name_inv_doc = "INV_DOC_" + cod_tda + "_" + nro_guia + "_" +DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_inv_doc = ruta_interface + "\\" + name_inv_doc;

                        if (File.Exists(@in_inv_doc)) File.Delete(@in_inv_doc);
                        File.WriteAllText(@in_inv_doc, str_cadena);


                    }

                    /*INV_DOC_LINE_ITEM*/
                    if (dt_inv_doc_line_item.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_inv_doc_line_item.Rows.Count; ++i)
                        {
                            str.Append(dt_inv_doc_line_item.Rows[i]["INV_DOC_LINE_ITEM"].ToString());

                            if (i < dt_inv_doc_line_item.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();



                        name_inv_doc_line_item= "INV_DOC_LINE_ITEM_" + cod_tda + "_" + nro_guia + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_inv_doc = ruta_interface + "\\" + name_inv_doc_line_item;

                        if (File.Exists(@in_inv_doc)) File.Delete(@in_inv_doc);
                        File.WriteAllText(@in_inv_doc, str_cadena);

                    }

                    /*CARTON*/
                    if (dt_carton.Rows.Count > 0)
                    {
                        str = new StringBuilder();
                        for (Int32 i = 0; i < dt_carton.Rows.Count; ++i)
                        {
                            str.Append(dt_carton.Rows[i]["CARTON"].ToString());

                            if (i < dt_carton.Rows.Count - 1)
                            {
                                str.Append("\r\n");

                            }

                        }
                        str_cadena = str.ToString();



                        name_carton= "CARTON_" + cod_tda + "_" + nro_guia + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_inv_doc = ruta_interface + "\\" + name_carton;

                        if (File.Exists(@in_inv_doc)) File.Delete(@in_inv_doc);
                        File.WriteAllText(@in_inv_doc, str_cadena);

                    }

                }

                Boolean envio = true;
                string mensaje = "";
                if (strEnviaFtp.Equals("S"))
                {
                    string codAmbiente = dwAmbienteDsp.EditValue.ToString();
                    setearAmbXoficce(codAmbiente);

                    mensaje = "Se enviaron al ftp";
                    envio = await Task.Run(() => basico.sendftp_file_mnt());
                }
                else {
                    mensaje = "Se creo en la ruta : " + in_inv_doc;

                }               




                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();
            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }
        #endregion


        private void btnBCL_Click(object sender, RoutedEventArgs e)
        {
            genera_bcl();
        }
        private async void genera_bcl()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
           
            ProgressDialogController ProgressAlert = null;
            try
            {

                if (await valida_BCL()) return;

                string codtda = dwtienda_bcl.EditValue.ToString();
                string pais = "";
                string EnviarFTP = "N";
                string mensaje = "";
                string mensajeEspera = "Generando Interface";
                Boolean envio = false;

                if (chk_ftp_BCL.IsChecked == true)
                {
                    EnviarFTP = "S";
                    mensajeEspera = "Enviando X FTP Interface: Seleccionadas";
                }

                if (rbtPe_bcl.IsChecked == true)
                    pais = "PE";

                if (rbtEcu_bcl.IsChecked == true)
                    pais = "EC";

              
                //if (await valida_maestros()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, mensajeEspera);  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);


                StringBuilder str = null;
                string str_cadena = "";
                string name_maestros = ""; string in_maestros = "";
                #region<COUNTY_CITY>
                if (chk_bcl_county_city.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_county_city(codtda,pais));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["BCL_COUNTY_CITY"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "BCL_COUNTY_CITY_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<ELECTRONIC_CORRELATIVES>
                if (chk_bcl_electronic.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_electronic_correlatives(codtda,pais));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["BCL_ELECTRONIC_CORRELATIVES"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "BCL_ELECTRONIC_CORRELATIVES_"+ codtda+"_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<MANUAL_CORRELATIVES>
                if (chk_bcl_manual.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_manual_correlatives(codtda,pais));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            Decimal i = 0;
                            foreach (DataRow fila in dt.Rows)
                            {

                                str.Append(fila["BCL_MANUAL_CORRELATIVES"].ToString());
                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }
                                i += 1;

                            }

                            //for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            //{
                            //    str.Append(dt.Rows[i]["ITEM"].ToString());

                            //    if (i < dt.Rows.Count - 1)
                            //    {
                            //        str.Append("\r\n");

                            //    }

                            //}
                            str_cadena = str.ToString();



                            name_maestros = "BCL_MANUAL_CORRELATIVES_" + codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<STATE COUNTY>
                if (chk_bcl_state.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_state_county(codtda,pais));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["BCL_STATE_COUNTY"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "BCL_STATE_COUNTY_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                #region<VARIACION_PRECIOS>
                if (chk_bcl_variacion.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_variacion_precio(codtda,pais));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["BCL_VARIACION_PRECIOS"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "BCL_VARIACION_PRECIOS_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<TENDER_REPOSITORY>
                if (chk_bcl_tender.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_tender_repository(codtda, pais));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["TENDER_REPOSITORY"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "TENDER_REPOSITORY_"+ codtda+"_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<TENDER_REPOSITORY_PROPERTY>
                if (chk_bcl_tender_property.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.get_tender_repository_property(codtda, pais));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["TENDER_REPOSITORY_PROPERTY"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "TENDER_REPOSITORY_PROPERTY_" + codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                envio = true;

                if (EnviarFTP.Equals("S"))
                {
                    string codAmbiente = dwAmbiente_bcl.EditValue.ToString();
                    setearAmbXoficce(codAmbiente);

                    mensaje = "Se enviaron al ftp";
                    envio = await Task.Run(() => basico.sendftp_file_mnt());
                }
                else
                {
                    mensaje = "Se creo en la ruta : " + ruta_interface;
                }

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();



            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }

        private async Task<Boolean> valida_BCL()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            string _cod_tda_select = "";
            if (dwtienda_bcl.EditValue != null)
            {
                _cod_tda_select = dwtienda_bcl.EditValue.ToString();
            }
            if (_cod_tda_select.Length == 0)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccion la Tienda.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                dwtienda_bcl.Focus();
                valida = true;
                return valida;
            }

            if ((chk_bcl_variacion.IsChecked == false) && (chk_bcl_state.IsChecked == false)
               && (chk_bcl_manual.IsChecked == false) && (chk_bcl_electronic.IsChecked == false)
               && (chk_bcl_county_city.IsChecked == false) && (chk_bcl_tender.IsChecked == false) && (chk_bcl_tender_property.IsChecked == false))
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccione al menos una interface.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                valida = true;
                return valida;
            }

            return valida;
        }

        private void btnOrc_Click(object sender, RoutedEventArgs e)
        {
            genera_Orc();
        }

        private async void genera_Orc()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;

            ProgressDialogController ProgressAlert = null;
            try
            {

                if (await valida_ORC()) return;

              
                string pais = "";
                string EnviarFTP = "N";
                string mensaje = "";
                string mensajeEspera = "Generando Interface";
                Boolean envio = false;

                if (chk_ftp_orc.IsChecked == true)
                {
                    EnviarFTP = "S";
                    mensajeEspera = "Enviando X FTP Interface: Seleccionadas";
                }

                //if (rbtPe_bcl.IsChecked == true)
                //    pais = "PE";

                //if (rbtEcu_bcl.IsChecked == true)
                //    pais = "EC";


                //if (await valida_maestros()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, mensajeEspera);  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);


                StringBuilder str = null;
                string str_cadena = "";
                string name_maestros = ""; string in_maestros = "";
                #region<ItemMaintenance>
                if (chk_ItemMaintenance.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.ItemMaintenance());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["ItemMaintenance"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "ItemMaintenance_" + DateTime.Today.ToString("yyyyMMdd") + ".XML";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<MerchandiseHierarch>
                if (chk_MerchandiseHierarch.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.MerchandiseHierarch());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["MerchandiseHierarchyMaintenance"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "MerchandiseHierarchyMaintenance_" + DateTime.Today.ToString("yyyyMMdd") + ".XML";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                #region<ORCE_RetailLocations>
                if (chk_OrcRetailLocations.IsChecked == true)
                {
                    DataTable dt = await Task.Run(() => dat_interface.OrcRetailLocations());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str = new StringBuilder();
                            for (Int32 i = 0; i < dt.Rows.Count; ++i)
                            {
                                str.Append(dt.Rows[i]["strXml"].ToString());

                                if (i < dt.Rows.Count - 1)
                                {
                                    str.Append("\r\n");

                                }

                            }
                            str_cadena = str.ToString();



                            name_maestros = "RetailLocations_" + DateTime.Today.ToString("yyyyMMdd") + ".XML";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion

                envio = true;

                if (EnviarFTP.Equals("S"))
                {
                    mensaje = "Se enviaron al ftp";

                    string codAmbiente = dwambiente_orce.EditValue.ToString();
                    setearAmbOrce(codAmbiente);
                    envio = await Task.Run(() => basico.sendftp_file_orce());
                }
                else
                {
                    mensaje = "Se creo en la ruta : " + ruta_interface;
                }

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                if (ProgressAlert.IsOpen)
                    await ProgressAlert.CloseAsync();

            }
            catch (Exception exc)
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }

        private async Task<Boolean> valida_ORC()
        {
            var metroWindow = this;
            Boolean valida = false;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
         
            if ((chk_ItemMaintenance.IsChecked == false) && (chk_MerchandiseHierarch.IsChecked == false)&& (chk_OrcRetailLocations.IsChecked == false))
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Seleccione al menos una generacion de archivos.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                valida = true;
                return valida;
            }

            return valida;
        }

        private void setearAmbXoficce(string codAmbiente)
        {
            string ftp_server = "";
            string ftp_user = "";
            string ftp_password = "";
            int ftp_puerto = 0;
            string ruta_destino = "";

            for (int i = 0; i < gdt_Xoficce.Rows.Count; i++)
            {
                //aca haces las operaciones con cada fila de la tabla ej:
                if (codAmbiente == gdt_Xoficce.Rows[i]["Amb_Cod"].ToString())
                {
                    ruta_destino = gdt_Xoficce.Rows[i]["Amb_Ftp_Path"].ToString();
                    ftp_server = gdt_Xoficce.Rows[i]["Amb_Ftp_Server"].ToString();
                    ftp_user = gdt_Xoficce.Rows[i]["Amb_Ftp_User"].ToString();
                    ftp_password = gdt_Xoficce.Rows[i]["Amb_Ftp_Pass"].ToString();
                    ftp_puerto = Int32.Parse(gdt_Xoficce.Rows[i]["Amb_Ftp_Port"].ToString());
                }
            }

            basico.ftp_ruta_destino = ruta_destino;
            Ent_Conexion.ftp_server = ftp_server;
            Ent_Conexion.ftp_user = ftp_user;
            Ent_Conexion.ftp_password = ftp_password;
            Ent_Conexion.ftp_puerto = ftp_puerto;

        }

        private void setearAmbOrce(string codAmbiente)
        {

            string ftp_server = "";
            string ftp_user = "";
            string ftp_password = "";
            string ruta_destino = "";
            int ftp_puerto = 0;

            for (int i = 0; i < gdt_Orce.Rows.Count; i++)
            {
                //aca haces las operaciones con cada fila de la tabla ej:
                if (codAmbiente == gdt_Orce.Rows[i]["Amb_Cod"].ToString())
                {
                    ruta_destino = gdt_Orce.Rows[i]["Amb_Ftp_Path"].ToString();
                    ftp_server = gdt_Orce.Rows[i]["Amb_Ftp_Server"].ToString();
                    ftp_user = gdt_Orce.Rows[i]["Amb_Ftp_User"].ToString();
                    ftp_password = gdt_Orce.Rows[i]["Amb_Ftp_Pass"].ToString();
                    ftp_puerto = Int32.Parse(gdt_Orce.Rows[i]["Amb_Ftp_Port"].ToString());
                }
            }

            basico.ftp_ruta_orce = ruta_destino;
            Ent_Conexion.ftp_orce_server = ftp_server;
            Ent_Conexion.ftp_orce_user = ftp_user;
            Ent_Conexion.ftp_orce_password = ftp_password;
            Ent_Conexion.ftp_orce_puerto = ftp_puerto;

        }
    }
}
