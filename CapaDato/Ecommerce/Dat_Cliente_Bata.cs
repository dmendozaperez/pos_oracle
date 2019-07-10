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
                    try
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
                            cmd.Parameters.AddWithValue("@cod_tda", Cliente.tienda);
                            cmd.Parameters.AddWithValue("@ubigeo_distrito", Cliente.ubigeo_distrito);

                            cmd.ExecuteNonQuery();
                            result.codigo = "0";
                            result.descripcion = "Cliente Actualizado con exito";
                        }
                    }
                    catch (Exception exc)
                    {
                        result.codigo = "-1";
                        result.descripcion = exc.Message;
                    }

                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                   
                }

            }
            catch (Exception exc)
            {
                result.codigo = "-1";
                result.descripcion = exc.Message;

            }
            return result;
        }
        public Ent_MsgTransac Existe_Email_BataClub(Ent_Cliente_BataClub cliente)
        {
            string sqlquery = "USP_BATACLUB_EXISTE_CORREO";
            Ent_MsgTransac result = null;
            try
            {
                result = new Ent_MsgTransac();
                result.codigo = "";
                result.descripcion = "";
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@dni", cliente.dni);
                            cmd.Parameters.AddWithValue("@correo", cliente.correo);
                            cmd.Parameters.Add("@valida", SqlDbType.Bit);
                            cmd.Parameters["@valida"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            Boolean existe =Convert.ToBoolean(cmd.Parameters["@valida"].Value);
                            if (existe)
                            {
                                result.codigo = "-1";
                                result.descripcion = "El correo ya existe en la base de datos";
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
                                
            }
            return result;
        }

        public Ent_Cliente_BataClub Consultar_ClienteBataclub(string dni)
        {
            string sqlquery = "USP_BATACLUB_BUSCAR_CLIENTES";
            Ent_Cliente_BataClub result = null;
            try
            {
                result = new Ent_Cliente_BataClub();
                result.descripcion_error = "";
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DNI", dni);

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                result.existe_cliente = false;
                                while(dr.Read())
                                {
                                    result.dni = dr["DNI"].ToString();
                                    result.primerNombre = dr["PRIMER_NOMBRE"].ToString();
                                    result.segundoNombre = dr["SEGUNDO_NOMBRE"].ToString();
                                    result.apellidoPater = dr["APELLIDO_PAT"].ToString();
                                    result.apellidoMater = dr["APELLIDO_MAT"].ToString();
                                    result.genero = dr["GENERO"].ToString();
                                    result.correo = dr["CORREO"].ToString();
                                    result.fecNac = dr["FEC_NAC"].ToString();
                                    result.telefono = dr["TELEFONO"].ToString();
                                    result.registrado =Convert.ToBoolean(dr["REGISTRADO"]);
                                    result.miembro_bataclub =Convert.ToBoolean(dr["MIEMBRO_BATACLUB"]);
                                    result.ubigeo = dr["UBIGEO"].ToString();
                                    result.ubigeo_distrito = dr["UBIGEO_DISTRITO"].ToString();
                                    result.barra_cliente = dr["DNI_BARRA"].ToString();
                                    result.existe_cliente = true;
                                }
                            }
                           
                        
                        }
                    }
                    catch (Exception exc)
                    {
                        result.existe_cliente = false;
                        result.descripcion_error = exc.Message;
                    }

                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception exc)
            {
                result.existe_cliente = false;
                result.descripcion_error = exc.Message;

            }
            return result;
        }

        public string Insert_Envio_Correo(string email,string dni,string tip_env_cod)
        {
            string sqlquery = "USP_BATACLUB_INSERT_EMAIL_ENVIAR";
            string result = "";
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
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@dni", dni);
                            cmd.Parameters.AddWithValue("@tip_env_cod",tip_env_cod);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        result = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                result = exc.Message;
            }
            return result;
        }
    }
}
