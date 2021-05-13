using CapaServicioWindows_x64.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ws_ConsultReniecSunat.Bll
{
    public class Basico
    {
        public static Boolean VerificarPermisos(Autenticacion value)
        {
            Boolean _valida_permiso = false;
            if (value == null)
            {
                _valida_permiso = false;
            }
            else
            {
                /*user=Bata*/
                /*pass=123*/

                if (value.user_name=="Bata" && value.user_password=="123")
                {
                    _valida_permiso = true;
                }
                else
                {
                    _valida_permiso = false;
                }

                //_valida_permiso = VALIDA_USUARIO_WS(value.user_name, value.user_password);
            }
            return _valida_permiso;
        }

        public static DataEntidad buscar_ruc_entidad(string ruc)
        {
            DataEntidad obj = null;
            string sqlquery = "USP_WS_BUSCAR_RUC";
            try
            {

                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@NRO_RUC", ruc);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                obj = new DataEntidad();
                                while (dr.Read())
                                {
                                    obj.ruc = dr["NRO_RUC"].ToString();
                                    obj.razon_social = dr["DES_ENTID"].ToString();
                                }
                            }
                        }
                    }
                    catch 
                    {
                        
                    }
                    if (cn.State == ConnectionState.Open) cn.Close();
                    
                }
            }
            catch (Exception exc)
            {
                obj = null;                
            }
            return obj;
        }
    }
}