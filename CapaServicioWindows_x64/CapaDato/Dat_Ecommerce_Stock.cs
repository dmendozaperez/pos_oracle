using CapaServicioWindows_x64.Conexion;
using CapaServicioWindows_x64.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicioWindows_x64.CapaDato
{
    public class Dat_Ecommerce_Stock
    {
        public List<Ent_Ecommerce_Stock> lista_stock()
        {
            List<Ent_Ecommerce_Stock> lista = null;
            String sqlquery = "USP_ECOMMERCE_GET_STOCK";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                dt = new DataTable();
                                da.Fill(dt);
                                lista = new List<Ent_Ecommerce_Stock>();
                                lista = (from DataRow fila in dt.Rows
                                         select new Ent_Ecommerce_Stock()
                                         {
                                             STK_EC = fila["STK_EC"].ToString(),                                            
                                         }).ToList();
                            }
                        }
                    }
                    catch 
                    {

                        
                    }
                }

            }
            catch
            {

                
            }
            return lista;
        }

        public Ent_Ecommerce_Ruta get_ruta_ec()
        {
            Ent_Ecommerce_Ruta rut = null;
            string sqlquery = "USP_ECOMMERCE_RUTA_UPLOAD";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery,cn))
                        {                           

                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                rut = new Ent_Ecommerce_Ruta();
                                while(dr.Read())
                                {
                                    rut.ruta_ec = dr["ruta_ec"].ToString();
                                }
                            }
                        }
                    }
                    catch
                    {
                        
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {
                rut = null;                
            }
            return rut;
        }
    }
}
