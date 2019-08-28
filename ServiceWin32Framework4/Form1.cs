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
//using TaskScheduler;
using Microsoft.Win32.TaskScheduler;
using System.Data.SqlClient;
using System.Reflection;

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
        public String ruta_temp_interface = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//tmpinterface";

        private DataTable dt_replace_tda(DataTable dt, string cod_tda)
        {
            DataTable dt_replace = null;
            try
            {

                if (dt != null)
                {
                    dt_replace = dt;
                    string file_cab = dt.Rows[0][0].ToString();

                    string str_tda_ant = file_cab.Substring(file_cab.IndexOf(':') - 6, 13);

                    string str_tda_new = "\"" + str_tda_ant.Replace(str_tda_ant, "STORE:" + cod_tda) + "\"";

                    file_cab = file_cab.Replace(str_tda_ant, str_tda_new);
                    dt_replace.Rows[0][0] = file_cab.ToString();

                }
            }
            catch
            {
                dt_replace = null;
            }
            return dt_replace;
        }
        private void btn_item_deal_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string sqlquery = "USP_XSTORE_GET_ITEM_DEAL_PROPERTY";
            DataTable dt = null;
            try
            {
                DataTable dttienda=  get_tienda();

                foreach (DataRow fila in dttienda.Rows)
                {
                    using (SqlConnection cn = new SqlConnection(conexion))
                    {

                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CODTIENDA", fila["cod_entid"].ToString());
                            cmd.Parameters.AddWithValue("@OUTLET", Convert.ToBoolean(fila["OUTLET"]));

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                if (dt==null)
                                { 
                                    dt = new DataTable();
                                    da.Fill(dt);
                                }
                                else
                                {
                                    dt = dt_replace_tda(dt, fila["cod_entid"].ToString());
                                }
                                #region<GET_ITEM_DEAL_PROPERTY>
                                StringBuilder str = null;
                                string str_cadena = "";
                                string name_maestros = ""; string in_maestros = "";
                                if (dt!=null)
                                {
                                    string ruta_interface = ruta_temp_interface;
                                    if (!Directory.Exists(@ruta_interface)) Directory.CreateDirectory(@ruta_interface);
                                    if (dt.Rows.Count>0)
                                    {
                                                                                                                                                                                                                     
                                                    str = new StringBuilder();
                                                    for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                                    {
                                                        str.Append(dt.Rows[i]["ITEM_DEAL_PROPERTY"].ToString());

                                                        if (i < dt.Rows.Count - 1)
                                                        {
                                                            str.Append("\r\n");

                                                        }

                                                    }
                                                    str_cadena = str.ToString();



                                                    name_maestros = "ITEM_DEAL_PROPERTY_" + fila["cod_entid"].ToString() + "_" + DateTime.Today.ToString("yyyyMMdd") + ".MNT";
                                                    in_maestros = ruta_interface + "\\" + name_maestros;

                                                    if (File.Exists(@in_maestros)) File.Delete(@in_maestros);
                                                    File.WriteAllText(@in_maestros, str_cadena);
                                                                                                                             
                                        
                                    }
                                }
                                #endregion


                            }
                        }
                    }

                }
                MessageBox.Show("se generaron las interfaces", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            
            Cursor.Current = Cursors.Default;
        }
        string conexion = "Server=172.28.7.14;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;";
        private DataTable get_tienda()
        {
            DataTable dt = null;
            string sqlquery = "select cod_entid,OUTLET=dbo.FTIENDA_OUTLET(cod_entid) from tentidad_tienda where xstore=1 and cod_pais='PE' and cod_cadena='BA' ";
            //string sqlquery = "select cod_entid,OUTLET=dbo.FTIENDA_OUTLET(cod_entid) from tentidad_tienda where cod_pais='PE' and cod_entid='50102'";
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch 
            {                
            }
            return dt;
        }

        private DataTable get_tienda_orce()
        {
            DataTable dt = null;
            string sqlquery = "SELECT ORC_DET_TDA FROM ORCE_INTERFACE_DET_TDA  WHERE ORC_DET_TDA_COD = 3";
            //string sqlquery = "select cod_entid,OUTLET=dbo.FTIENDA_OUTLET(cod_entid) from tentidad_tienda where cod_pais='PE' and cod_entid='50102'";
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch
            {
            }
            return dt;
        }


        private void btnsk_almacen_Click(object sender, EventArgs e)
        {
            string _error = "ing";
            TextWriter tw = new StreamWriter(@"D:\ALMACEN\STOCK.txt", true);
            tw.WriteLine(_error);
            tw.Flush();
            tw.Close();
            tw.Dispose();
            Cursor.Current = Cursors.WaitCursor;
            string _erro = "";
            Basico cc = new Basico();
            // cc.procesar_dbf_pos(ref _erro);
            cc.eje_envio_stk_almacen(ref _erro);
            MessageBox.Show("termino");
            Cursor.Current = Cursors.Default;
        }

        private void btnpaperless_Click(object sender, EventArgs e)
        {
            
        }

        private void BTNVENDE_Click(object sender, EventArgs e)
        {
            string _error = "";
            Util act_vendedor = new Util();
            _error = act_vendedor.update_vendedor();
        }

        private void orce_exclud_interface_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string _error = "";
            //Xstore_Genera_Inter ejecuta_procesos = new Xstore_Genera_Inter();

            Ftp_Xstore_Service_Send ejecuta_procesos = null;
            ejecuta_procesos = new Ftp_Xstore_Service_Send();

            ejecuta_procesos.generar_orce_exclud(ref _error);
            MessageBox.Show(_error +  Environment.NewLine + "Terminado");
            Cursor.Current = Cursors.Default;
        }
    }
}
