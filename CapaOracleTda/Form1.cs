﻿using CapaDato.Transac;
using CapaEntidad.Util;
using CapaOracleTda.CapaDato;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaOracleTda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Ent_Conexion.conexion = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
            Ent_Conexion.conexion_posperu = ConfigurationManager.ConnectionStrings["SQL_PE"].ConnectionString;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string tienda = txttienda.Text;// "50345";
                Dat_Ora_Conexion dcon = new Dat_Ora_Conexion();
                Ent_Ora_Conexion ora_conexion = dcon.get_conexion_ora(tienda);


                if (ora_conexion==null)
                {
                    MessageBox.Show("No hay datos de conexion", "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cursor.Current = Cursors.Default;
                    return;
                }

                Ent_Acceso_BD.user = ora_conexion.user_ora;
                Ent_Acceso_BD.password = ora_conexion.pas_ora;
                Ent_Acceso_BD.server = ora_conexion.server_ora;
                Ent_Acceso_BD.port = ora_conexion.port_ora;
                Ent_Acceso_BD.sid = ora_conexion.sid_ora;


                //Ent_Acceso_BD.user = "pos";
                //Ent_Acceso_BD.password = "mPx3ahUEV6ez";
                //Ent_Acceso_BD.server = "172.16.100.143";
                //Ent_Acceso_BD.port = 1521;
                //Ent_Acceso_BD.sid = "XSTOREDB";

                Dat_Ora_Data dat_ora = new Dat_Ora_Data();
                DataTable dt= dat_ora.get_documento(txtdoc.Text);

                if (dt!=null)
                {
                    //DataTable dt_poslog= dat_ora.get_poslog(dt.Rows[0]["wkstn_id"].ToString(), dt.Rows[0]["trans_seq"].ToString());
                    DataTable dt_poslog = dat_ora.get_poslog(1.ToString(), 62.ToString());

                    if (dt_poslog!=null)
                    {
                        Dat_PosLog insert_bd = new Dat_PosLog();
                        string data_pos_log = dt_poslog.Rows[0]["poslog_data"].ToString();//.Replace("\n"," ");
                        string[] data_pos_log_array =data_pos_log.Split('\n') ;//data_pos_log.Replace("\n", ""); ;
                        string str_linea_pos_log = "";
                        decimal cur_for = 0;
                        foreach(string str in data_pos_log_array)
                        {
                            str_linea_pos_log +=((cur_for>0)?" ":"") +  str.TrimStart().TrimEnd().Trim();
                            cur_for += 1;
                        }

                        string _valida_error = insert_bd.InsertarTransac_Poslog(str_linea_pos_log, "PROD", "PE");
                        if (_valida_error.Length>0)
                        {
                            MessageBox.Show(_valida_error, "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        { 
                        MessageBox.Show("Se envio al PosPeru", "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para enviar", "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }

                

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            Cursor.Current = Cursors.Default;


        }

        private void btnenviar_Click(object sender, EventArgs e)
        {

        }
    }
}
