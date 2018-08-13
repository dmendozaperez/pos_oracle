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
    public class Dat_Error_Transac
    {
        public void insertar_errores_transac(string tip_error,string tip_des,string cod_tda="",string ambiente_bd="PROD")
        {
            string sqlquery = "USP_INSERTAR_ERRORES_PROCESOS";
            try
            {
                /*ambiente_bd*/
                /*PROD=PRODUCCION*/
                /*DES=DESARROLLO*/
                /*QA=QA*/
                string conexion_sql = "";

                switch(ambiente_bd)
                {
                    case "PROD":
                        conexion_sql = Ent_Conexion.conexion_posperu;
                        break;
                    case "DES":
                        conexion_sql = Ent_Conexion.conexion_posperu_DES;
                        break;
                    case "QA":
                        conexion_sql = Ent_Conexion.conexion_posperu_QA;
                        break;
                }

                using (SqlConnection cn = new SqlConnection(conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ERR_TIP_PR_COD", tip_error);
                            cmd.Parameters.AddWithValue("@ERR_PR_DES", tip_des);
                            cmd.Parameters.AddWithValue("@ERR_COD_TDA", cod_tda);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch 
                    {
                     
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch 
            {

                
            }
        }
    }
}
