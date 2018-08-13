using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Transac
{
    public class Dat_PosLog
    {
        public string InsertarTransac_Poslog(string entrada_poslog,string ambiente_bd)
        {
            string sqlquery = "USP_INSERTAR_POS_LOG";
            string _valida = "";
            try
            {
                /*ambiente_bd*/
                /*PROD=PRODUCCION*/
                /*DES=DESARROLLO*/
                /*QA=QA*/
                string conexion_sql = "";

                switch (ambiente_bd)
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
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@data_pos", entrada_poslog);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        _valida = exc.Message;                                         
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                _valida = exc.Message; ;
            }
            return _valida;
        }
    }
}
