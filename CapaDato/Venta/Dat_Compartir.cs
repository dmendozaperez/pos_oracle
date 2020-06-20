using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Venta
{
    public class Dat_Compartir
    {
        public string insertar_enviar_compartir(string cof_cup_cod,string dni,string correo,Decimal total,string cod_tda)
        {
            string valida = "";
            string sqlquery = "USP_BATACLUB_INSERTAR_ENVIAR_COMPARTIR";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@cof_cup_cod", cof_cup_cod);
                            cmd.Parameters.AddWithValue("@dni", dni);
                            cmd.Parameters.AddWithValue("@correo", correo);
                            cmd.Parameters.AddWithValue("@total", total);
                            cmd.Parameters.AddWithValue("@cod_tda", cod_tda);
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                valida = exc.Message;   
            }
            return valida;
        }
    }
}
