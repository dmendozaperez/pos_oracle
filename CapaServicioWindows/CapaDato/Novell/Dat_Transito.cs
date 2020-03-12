using CapaServicioWindows.Conexion;
using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.Novell
{
    public class Dat_Transito
    {
        public string insertar_transito(FPTRANC list)
        {
            string sqlquery = "USP_INSERTAR_TRANSITO";
            string error = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@trac_tipo", list.trac_tipo);
                            cmd.Parameters.AddWithValue("@trac_ndesp", list.trac_ndesp);
                            cmd.Parameters.AddWithValue("@trac_nume", list.trac_nume);
                            cmd.Parameters.AddWithValue("@trac_srem", list.trac_srem);
                            cmd.Parameters.AddWithValue("@trac_nrem", list.trac_nrem);
                            cmd.Parameters.AddWithValue("@trac_tori", list.trac_tori);
                            cmd.Parameters.AddWithValue("@trac_gudis", list.trac_gudis);
                            cmd.Parameters.AddWithValue("@trac_estad", list.trac_estad);
                            cmd.Parameters.AddWithValue("@trac_motiv", list.trac_motiv);
                            cmd.Parameters.AddWithValue("@trac_tdes", list.trac_tdes);
                            cmd.Parameters.AddWithValue("@trac_semi", list.trac_semi);
                            cmd.Parameters.AddWithValue("@trac_srec", list.trac_srec);
                            cmd.Parameters.AddWithValue("@trac_fdoc", list.trac_fdoc);
                            cmd.Parameters.AddWithValue("@trac_ftra", list.trac_ftra);
                            cmd.Parameters.AddWithValue("@trac_fori", list.trac_fori);
                            cmd.Parameters.AddWithValue("@trac_fdes", list.trac_fdes);
                            cmd.Parameters.AddWithValue("@trac_festa", list.trac_festa);
                            cmd.Parameters.AddWithValue("@trac_user", list.trac_user);
                            cmd.Parameters.AddWithValue("@trac_indi", list.trac_indi);
                            cmd.Parameters.AddWithValue("@trac_obs", list.trac_obs);
                            cmd.Parameters.AddWithValue("@trac_log", list.trac_log);
                            cmd.Parameters.AddWithValue("@trac_tralm", list.trac_tralm);
                            cmd.Parameters.AddWithValue("@trac_freco", list.trac_freco);
                            cmd.Parameters.AddWithValue("@trac_creco", list.trac_creco);
                            cmd.Parameters.AddWithValue("@trac_rucdv", list.trac_rucdv);
                            cmd.Parameters.AddWithValue("@trac_tramo", list.trac_tramo);
                            cmd.Parameters.AddWithValue("@trac_trx", list.trac_trx);
                            cmd.Parameters.AddWithValue("@trac_empre", list.trac_empre);
                            cmd.Parameters.AddWithValue("@trac_canal", list.trac_canal);
                            cmd.Parameters.AddWithValue("@trac_caden", list.trac_caden);
                            cmd.Parameters.AddWithValue("@trac_docrf", list.trac_docrf);
                            cmd.Parameters.AddWithValue("@trac_auto", list.trac_auto);
                            cmd.Parameters.AddWithValue("@TMP_DET", list.det_FPTRAND);
                            
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        error = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }
    }
}
