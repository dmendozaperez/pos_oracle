using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Control
{
    public class Dat_Acceso
    {
        /// <summary>
        /// VALIDA EL ACCESO A LA WEB SERVICE
        /// </summary>
        /// <param name="acceso_cod"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Boolean _acceso_ws(string acceso_cod,string user,string password)
        {
            Boolean _valida_acceso = false;
            string sqlquery = "USP_VALIDA_ACCESO_WS";
            try
            {                
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@acceso_cod", acceso_cod);
                            cmd.Parameters.AddWithValue("@user", user);
                            cmd.Parameters.AddWithValue("@password", password);

                            cmd.Parameters.Add("@existe", SqlDbType.Bit);
                            cmd.Parameters["@existe"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            _valida_acceso =(Boolean) cmd.Parameters["@existe"].Value;

                        }
                    }
                    catch (Exception)
                    {
                        _valida_acceso = false;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception)
            {
                _valida_acceso=false;
            }
            return _valida_acceso;
        }
        /// <summary>
        /// Verifica la conexion de tienda hacia la WS NUBE
        /// </summary>
        /// <param name="CODIGO DE TIENDA"></param>
        /// <param name="DESCRIPCION DE LA CONEXION"></param>
        public void _conexion_tda(string con_tda,string con_des)
        {
            string sqlquery = "USP_CONEXION_TDA_WS";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CON_TDA", con_tda);
                            cmd.Parameters.AddWithValue("@CON_DES", con_des);
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
            catch (Exception)
            {
                
            }
        }
    }
}
