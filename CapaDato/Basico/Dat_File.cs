using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Basico
{
    public class Dat_File
    {
        /// <summary>
        /// filtrar las imagenes que no existe en la bd
        /// </summary>
        /// <returns></returns>
        public List<Ent_File> get_filtrar_file(string tipofile_cod,Ent_Lista_File lista_in)
        {
            string sqlquery = "USP_GET_UPLOAD_FILE";
            List<Ent_File> lista_out = null;
            DataTable dtin = null;
            DataTable dtout = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        dtin = new DataTable();
                        dtin.Columns.Add("file", typeof(string));
                        dtin.Columns.Add("file_cre", typeof(string));
                        dtin.Columns.Add("file_mod", typeof(string));

                        foreach (var item in lista_in.lista_file_name)
                        {
                            dtin.Rows.Add(item.file_name,item.file_creacion,item.file_update);
                        }                        
                        cmd.CommandTimeout = 0;
                        cmd.CommandType =CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TIP_FILE_COD", tipofile_cod);
                        cmd.Parameters.AddWithValue("@TEMP", dtin);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtout = new DataTable();
                            da.Fill(dtout);
                            lista_out = new List<Ent_File>();
                            lista_out = (from DataRow dr in dtout.Rows
                                         select new Ent_File
                                         {
                                             file_name = dr["FILE"].ToString(),
                                             file_creacion= dr["FILE_CREA"].ToString(),
                                             file_update= dr["FILE_MOD"].ToString(),
                                         }).ToList();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                lista_out = null;
            }
            return lista_out;
        } 
        /// <summary>
        /// get de origen y destino de carpeta de archivos
        /// </summary>
        /// <param name="tipo_file_cod"></param>
        /// <returns></returns>
        public Ent_File_Ruta get_ruta_file(string tipo_file_cod)
        {
            Ent_File_Ruta file = null;
            string sqlquery = "USP_GET_RUTA_FILE";
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
                            cmd.Parameters.AddWithValue("@TIPO_COD", tipo_file_cod);
                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                file = new Ent_File_Ruta();
                                while(dr.Read())
                                {
                                    file.file_origen = dr["RUT_FILE_ORIGEN"].ToString();
                                    file.file_destino = dr["RUT_FILE_DESTINO"].ToString();
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                    if (cn!=null)
                    if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {                
                file = null;
                throw exc;
            }
            return file;
        }
        /// <summary>
        /// insertar y mpdificado los archivos subidos por ws
        /// </summary>
        /// <param name="tipo_file_cod"></param>
        /// <param name="nombre"></param>
        public void insert_update_fileBD(string tipo_file_cod,string name_file, string file_creacion, string file_update)
        {
            string sqlquery = "USP_INSERTAR_UPLOAD_FILE";
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
                            cmd.Parameters.AddWithValue("@TIP_CODFILE", tipo_file_cod);
                            cmd.Parameters.AddWithValue("@NOM_FILE", name_file);
                            cmd.Parameters.AddWithValue("@FILE_CREA", file_creacion);
                            cmd.Parameters.AddWithValue("@FILE_MOD", file_update);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                    if (cn!=null)
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                throw exc;                
            }
        }
    }
}
