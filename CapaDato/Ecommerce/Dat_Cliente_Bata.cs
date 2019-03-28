using CapaEntidad.Ecommerce;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Ecommerce
{
    public class Dat_Cliente_Bata
    {

        public Ent_MsgTransac Resgistrar_ClienteBtaclub(Ent_Cliente_BataClub Cliente, string usuario)
        {
            string sqlquery = "USP_INSERTAR_CLIENTE_BATACLUB";
            Ent_MsgTransac result = null;
          
            try
            {
               
                result = new Ent_MsgTransac();

                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@canal", Cliente.canal);
                        cmd.Parameters.AddWithValue("@dni", Cliente.dni);
                        cmd.Parameters.AddWithValue("@primerNombre", Cliente.primerNombre);
                        cmd.Parameters.AddWithValue("@segundoNombre", Cliente.segundoNombre);
                        cmd.Parameters.AddWithValue("@apellidoPat", Cliente.apellidoPater);
                        cmd.Parameters.AddWithValue("@apellidoMat", Cliente.apellidoMater);
                        cmd.Parameters.AddWithValue("@genero", Cliente.genero);
                        cmd.Parameters.AddWithValue("@correo", Cliente.correo);
                        cmd.Parameters.AddWithValue("@fecNac", Cliente.fecNac);
                        cmd.Parameters.AddWithValue("@telefono", Cliente.telefono);
                        cmd.Parameters.AddWithValue("@ubigeo", Cliente.ubigeo);
                        cmd.Parameters.AddWithValue("@usuario", usuario);



                        cmd.ExecuteNonQuery();
                        result.codigo = "0";
                        result.descripcion = "Cliente registrado Correctamente";
                    }
                }

            }
            catch (Exception exc)
            {
                result.codigo = "-1";
                result.descripcion = exc.Message;

            }
            return result;
        }
    }
}
