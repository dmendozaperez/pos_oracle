using CapaEntidad.Logistica;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Logistica
{
    public class Dat_Alma_Ecu
    {
        public List<Ent_Alma_Ecu> get_lista_alma_ecu()
        {
            string sqlquery = "USP_GET_ALMACEN_ECU";
            List<Ent_Alma_Ecu> lista = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
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
                                lista = new List<Ent_Alma_Ecu>();
                                while(dr.Read())
                                {
                                    Ent_Alma_Ecu alm = new Ent_Alma_Ecu();
                                    alm.alma_ecu = dr["alma_ecu"].ToString();
                                    lista.Add(alm);
                                }
                            }


                        }

                    }
                    catch (Exception)
                    {
                        lista = null;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch 
            {
                lista = null;
            }
            return lista;
        }
    }
}
