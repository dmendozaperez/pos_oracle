using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Modular
{
    public class Tipo
    {
        private Tipo(string value) { Value = value; }

        public string Value { get; set; }

        public static Tipo Caracter { get { return new Tipo("Character"); } }
        public static Tipo Fecha { get { return new Tipo("Date"); } }
        public static Tipo Numerico { get { return new Tipo("Numeric"); } }
    }
    public class DBFNET
    {

        private string _campos_dbf = "";
        private string _param_insert = "";
        private string _query_crear_campos = "";
        public string tabla { set; get; }

        private string _nombre { set; get; }

        private string _campo { set; get; }

        public string _zize { set; get; }

        public DBFNET()
        {
        }
        public void addcol(string nombre, Tipo campo, string zize = "")
        {
            _nombre = nombre;
            _campo = campo.Value;
            _zize = zize;

            if (_campos_dbf.Length == 0)
            {

                _campos_dbf = _nombre;
                _param_insert = "?";
            }
            else
            {
                _campos_dbf += "," + _nombre;
                _param_insert += "," + "?";
            }


            if (_query_crear_campos.Length == 0)
            {
                if (zize.Length > 0)
                {
                    _query_crear_campos += nombre + " " + campo.Value + "(" + zize + ") ";
                }
                else
                {
                    _query_crear_campos += nombre + " " + campo.Value;// + "(" + zize + ") ";
                }
            }
            else
            {
                if (zize.Length > 0)
                {
                    _query_crear_campos += " , " + nombre + " " + campo.Value + "(" + zize + ") ";
                }
                else
                {
                    _query_crear_campos += " , " + nombre + " " + campo.Value; //+ "(" + zize + ") ";
                }
            }

        }

        public void Insertar_tabla(DataTable dt, string strRuta)
        {
            string columna = "";
            OleDbConnection cn = null;
            OleDbCommand cmd = null;
            string sqlquery = "INSERT INTO " + tabla + "(" + _campos_dbf + ")";
            sqlquery += "VALUES(" + _param_insert + ")";
            try
            {
                string cnx =Conexion.ConexionDBF.conexion_DBF_POS;

                cnx = cnx.Replace("XXXX", strRuta);
                cn = new OleDbConnection(cnx);
                if (cn.State == 0) cn.Open();
                for (Int32 fila = 0; fila < dt.Rows.Count; ++fila)
                {
                    cmd = new OleDbCommand(sqlquery, cn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.Text;
                    for (Int32 col = 0; col < dt.Columns.Count; ++col)
                    {
                        columna = dt.Columns[col].ColumnName.ToString();
                        cmd.Parameters.AddWithValue(dt.Columns[col].ColumnName.ToString(), dt.Rows[fila][col]);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception EX)
            {
                string msg = EX.Message + " " + columna;
                if (cn != null)
                    if (cn.State == ConnectionState.Open) cn.Close();
                throw;
            }
            if (cn != null)
                if (cn.State == ConnectionState.Open) cn.Close();
        }

        public void creartxt_stk(string strRuta)
        {
            try
            {
                if (!Directory.Exists(strRuta))
                    Directory.CreateDirectory(strRuta);
                string file_stk = "FSTKG";
                string ruta_file = strRuta + "\\" + file_stk + ".txt";




            }
            catch
            {

                throw;
            }

        }

        public void creardbf(string strRuta)
        {
            OleDbConnection cn = null;
            OleDbCommand cmd = null;
            string _querycrear = "CREATE TABLE " + tabla + "(" + _query_crear_campos + ")";
            try
            {
                if (!Directory.Exists(strRuta))
                    Directory.CreateDirectory(strRuta);

                string file_dbf = strRuta + "\\" + tabla + ".dbf";

                if (File.Exists(@file_dbf)) File.Delete(@file_dbf); 

                string cnx = Conexion.ConexionDBF.conexion_DBF_POS;
                cnx = cnx.Replace("XXXX", strRuta);
                cn = new OleDbConnection(cnx);
                if (cn.State == 0) cn.Open();
                cmd = new OleDbCommand(_querycrear, cn);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();

            }
            catch(Exception exc)
            {
                if (cn != null)
                    if (cn.State == ConnectionState.Open) cn.Close();
                throw;
            }
            if (cn != null)
                if (cn.State == ConnectionState.Open) cn.Close();
        }
    }
}
