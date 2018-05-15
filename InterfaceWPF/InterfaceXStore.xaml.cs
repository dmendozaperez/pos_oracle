using CapaDato;
using CapaDato.Interfaces;
using CapaDato.Logistica;
using CapaEntidad;
using CapaEntidad.Logistica;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadIniWPF();
        }

        #region<REGION DE INTERFACES RETAIL_LOCATION Y  RETAIL_LOCATION_PROPERTY>

        private void LoadIniWPF()
        {
            basico = new Basico();
            dat_interface = new Dat_Interface();

            Dat_Interface _tienda = new Dat_Interface();
            /*maestros de tienda*/
            dwtienda.ItemsSource = _tienda.get_tienda();
            dwtienda.DisplayMember = "des_entid";
            dwtienda.ValueMember = "cod_entid";
            dwtienda.SelectedIndex = -1;
            dwtienda.Focus();

            /*stock de tienda*/
            dwtiendastk.ItemsSource = _tienda.get_tienda();
            dwtiendastk.DisplayMember = "des_entid";
            dwtiendastk.ValueMember = "cod_entid";
            dwtiendastk.SelectedIndex = -1;
            dwtiendastk.Focus();


            /*transferencia de tienda invalid*/
            dwtiendatrans.ItemsSource = _tienda.get_tienda();
            dwtiendatrans.DisplayMember = "des_entid";
            dwtiendatrans.ValueMember = "cod_entid";
            dwtiendatrans.SelectedIndex = -1;
            dwtiendatrans.Focus();

            /*transpaso de almacen a tienda*/
            dwtiendatrans1.ItemsSource = _tienda.get_tienda(true);
            dwtiendatrans1.DisplayMember = "des_entid";
            dwtiendatrans1.ValueMember = "cod_entid";
            dwtiendatrans1.SelectedIndex = 0;
            dwtiendatrans1.Focus();
            dtpdesde.Text = DateTime.Today.ToString();
            dtphasta.Text = DateTime.Today.ToString();

            dtpfecha.Text = DateTime.Today.ToString();
        }


        private void btntienda_Click(object sender, RoutedEventArgs e)
        {
            generainter_retail_location();
        }
        private async void generainter_retail_location()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {
                if (await valida_retail_location()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Enviando X FTP Interface: RETAIL LOCATION");  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);

                string codtda = dwtienda.EditValue.ToString();
                /*DATOS DE INTERFACE RETAIL_LOCATION Y RETAIL_LOCATION_PROMERY*/
                /*Se recorre los datos del dataset y convertir a mnt el final del codigo y envia por un metodo en basico de envio
                 por ftp*/
                DataSet ds =await Task.Run(()=> dat_interface.get_retail_location(codtda));
                StringBuilder str = null;
                string str_cadena = "";
                if (ds!=null)
                {
                    DataTable dt_retail_location = ds.Tables[0];
                    DataTable dt_retail_location_property = ds.Tables[1];
                    string name_retail_location = ""; string in_retail_location = "";
                    if (dt_retail_location.Rows.Count>0)
                    {
                        str = new StringBuilder();
                        for (Int32 i=0;i<dt_retail_location.Rows.Count;++i)
                        {
                            str.Append(dt_retail_location.Rows[i]["RETAIL_LOCATION"].ToString());

                            if (i<dt_retail_location.Rows.Count - 1)
                            {
                                str.Append("\r\n");
                          
                            }

                        }
                        str_cadena = str.ToString();



                        name_retail_location = "RETAIL_LOCATION_" + codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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

                        name_retail_location = "RETAIL_LOCATION_PROPERTY_" + codtda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_retail_location = ruta_interface + "\\" + name_retail_location;

                        if (File.Exists(@in_retail_location)) File.Delete(@in_retail_location);
                        File.WriteAllText(@in_retail_location, str_cadena);
                     
                    }

                }

                Boolean envio= await Task.Run(() => basico.sendftp_file_mnt());

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Se enviaron al ftp", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

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
                //Boolean envio = await Task.Run(() => basico.sendftp_file_mnt());
                if (await valida_maestros()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Enviando X FTP Interface: Seleccionadas");  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);


                StringBuilder str = null;
                string str_cadena = "";
                string name_maestros = ""; string in_maestros = "";
                #region<DIMENSION TYPE>
                if (chk_item_dimension_type.IsChecked==true)
                {
                    DataTable  dt = await Task.Run(() => dat_interface.get_item_dimension_type());
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



                            name_maestros = "ITEM_DIMENSION_TYPE_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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
                    DataTable dt = await Task.Run(() => dat_interface.get_item_dimension_value());
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



                            name_maestros = "ITEM_DIMENSION_VALUE_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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
                    DataTable dt = await Task.Run(() => dat_interface.get_item());
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



                            name_maestros = "ITEM_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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
                    DataTable dt = await Task.Run(() => dat_interface.get_price_update_2());
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



                            name_maestros = "PRICE_UPDATE_2_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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
                    DataTable dt = await Task.Run(() => dat_interface.get_item_images());
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



                            name_maestros = "ITEM_IMAGES_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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
                    DataTable dt = await Task.Run(() => dat_interface.get_item_xref());
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



                            name_maestros = "ITEM_XREF_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                            in_maestros = ruta_interface + "\\" + name_maestros;

                            if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                            File.WriteAllText(@in_maestros, str_cadena);
                        }
                    }
                }
                #endregion
                Boolean envio = await Task.Run(() => basico.sendftp_file_mnt());

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Se enviaron al ftp", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
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
               && (chk_merch_hier.IsChecked == false))
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
                if (await valida_stock_ledger()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Enviando X FTP Interface: STOCK LEDGER");  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);

                string codtda = dwtiendastk.EditValue.ToString();
                string fecha = Convert.ToDateTime(dtpfecha.ToString()).ToString("yyyyMMdd");
                
                /*Se recorre los datos del dataset y convertir a mnt el final del codigo y envia por un metodo en basico de envio
                 por ftp*/
                DataTable dt = await Task.Run(() => dat_interface.get_stock_ledger(fecha,codtda));
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
                    }                   

                }

                Boolean envio = await Task.Run(() => basico.sendftp_file_mnt());

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Se enviaron al ftp", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

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

                //Boolean Senvio = await Task.Run(() => basico.sendftp_file_mnt());

                //return;
                if (await valida_inv_valid_destinations()) return;
                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Enviando X FTP Interface: RETAIL LOCATION");  //show message
                ProgressAlert.SetIndeterminate();

                string ruta_interface = basico.ruta_temp_interface;

                if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);

                string codtda =dwtiendatrans.EditValue.ToString();
                /*DATOS DE INTERFACE INVALID */
                /*Se recorre los datos del dataset y convertir a mnt el final del codigo y envia por un metodo en basico de envio
                 por ftp*/
                DataTable dt = await Task.Run(() => dat_interface.get_inv_valid_destinations(codtda));
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
                    }                   

                }

                Boolean envio = await Task.Run(() => basico.sendftp_file_mnt());

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Se enviaron al ftp", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

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

            if ((chk_inv_valid_destinations.IsChecked == false))
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
                DateTime _fec_ini =Convert.ToDateTime(dtpdesde.Text);
                DateTime _fec_fin = Convert.ToDateTime(dtphasta.Text);
                con_guia = new Dat_GuiasDespacho();                
                dg_guia.ItemsSource=await Task.Run(()=>con_guia.get_guias_tda_cab(_fec_ini, _fec_fin, _cod_tda));
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

            if (button != null)
            {
                var task = button.DataContext as Ent_GuiasDespacho_Cab;

                if (task != null)
                {
                    generainter_inv_doc(task.cod_alm, task.nro_guia, task.cod_tda);
                }
            }
        }
        private async void generainter_inv_doc(string cod_alm,string nro_guia,string cod_tda)
        {
            var metroWindow = this;
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
                    string name_carton="" ; string in_inv_doc = "";

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



                        name_inv_doc = "INV_DOC_" + cod_tda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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



                        name_inv_doc_line_item= "INV_DOC_LINE_ITEM_" + cod_tda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
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



                        name_carton= "CARTON_" + cod_tda + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                        in_inv_doc = ruta_interface + "\\" + name_carton;

                        if (File.Exists(@in_inv_doc)) File.Delete(@in_inv_doc);
                        File.WriteAllText(@in_inv_doc, str_cadena);

                    }

                }

                Boolean envio = await Task.Run(() => basico.sendftp_file_mnt());

                if (envio) await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Se enviaron al ftp", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

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
    }
}
