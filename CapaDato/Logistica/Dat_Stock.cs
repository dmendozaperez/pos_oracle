﻿using CapaEntidad.Logistica;
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
    public class Dat_Stock
    {
        /// <summary>
        /// actualizar stock desde tienda
        /// </summary>
        /// <param name="lista_stk"></param>
        /// <returns></returns>
        public Ent_MsgTransac insertar_stock_tda(Ent_Lista_Stock lista_stk)
        {
            string sqlquery = "[USP_INSERTAR_STOCK_TIENDA]";
            Ent_MsgTransac msg = null;
            DataTable dt_stock = null;
            
            try
            {
                msg = new Ent_MsgTransac();
                dt_stock = ConvertListToDataTable(lista_stk);
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {                        
                        if (cn.State == 0) cn.Open();                       
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TEMP_STK", dt_stock);
                            cmd.ExecuteNonQuery();
                            msg.codigo = "0";
                            msg.descripcion = "Se actualizo correctamente";
                        }
                       
                    }
                    catch (Exception exc)
                    {
                        msg.codigo = "1";
                        msg.descripcion = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {

                msg.codigo = "1";
                msg.descripcion = exc.Message;
            }
            return msg;
        }
        /// <summary>
        /// convertir list a un datatable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// 
        public Ent_MsgTransac insertar_stock_tda_almacen(Ent_Lista_Stock_Almacen lista_stk)
        {
            string sqlquery = "[USP_INSERTAR_STOCK_ALMACEN]";
            Ent_MsgTransac msg = null;
            DataTable dt_stock = null;

            try
            {
                msg = new Ent_MsgTransac();
                dt_stock = ConvertListToDataTable_Almacen(lista_stk);
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TEMP_STK", dt_stock);
                            cmd.ExecuteNonQuery();
                            msg.codigo = "0";
                            msg.descripcion = "Se actualizo correctamente";
                        }

                    }
                    catch (Exception exc)
                    {
                        msg.codigo = "1";
                        msg.descripcion = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {

                msg.codigo = "1";
                msg.descripcion = exc.Message;
            }
            return msg;
        }

        private DataTable ConvertListToDataTable_Almacen(Ent_Lista_Stock_Almacen list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable();
                table.Columns.Add("cod_tda", typeof(string));
                table.Columns.Add("stk_cd", typeof(string));
                table.Columns.Add("cod_art", typeof(string));                
                table.Columns.Add("calidad", typeof(string));
                table.Columns.Add("stk_cod_rgmed", typeof(string));
                table.Columns.Add("stk_med_per", typeof(string));
                table.Columns.Add("stk_med_lat", typeof(string));
                table.Columns.Add("pares", typeof(Int32));
                table.Columns.Add("stk_secci", typeof(string));
                table.Columns.Add("stk_ano", typeof(string));
                table.Columns.Add("stk_sem", typeof(string));
                                     

                foreach (var item in list.lista_stock)
                {
                    //table.Rows.Add(item.cod_tda, item.art_cod, item.art_cal,
                    //               item._0, item._1, item._2, item._3, item._4, item._5,
                    //               item._6, item._7, item._8, item._9, item._10, item._11);

                    table.Rows.Add(item.cod_tda,item.cd,  item.art_cod, item.art_cal,
                                   item.cod_rgmed, item.cod_med_per,item.cod_med_lat,
                                   item.art_pares,item.secci,item.ano,item.sem);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return table;
        }

        private DataTable ConvertListToDataTable(Ent_Lista_Stock list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable();
                table.Columns.Add("cod_tda", typeof(string));
                table.Columns.Add("cod_art", typeof(string));
                table.Columns.Add("calidad", typeof(string));
                table.Columns.Add("talla", typeof(string));
                table.Columns.Add("pares", typeof(Int32));
                //table.Columns.Add("02", typeof(string));
                //table.Columns.Add("03", typeof(string));
                //table.Columns.Add("04", typeof(string));
                //table.Columns.Add("05", typeof(string));
                //table.Columns.Add("06", typeof(string));
                //table.Columns.Add("07", typeof(string));
                //table.Columns.Add("08", typeof(string));
                //table.Columns.Add("09", typeof(string));
                //table.Columns.Add("10", typeof(string));
                //table.Columns.Add("11", typeof(string));                                

                foreach(var item in list.lista_stock)
                {
                    //table.Rows.Add(item.cod_tda, item.art_cod, item.art_cal,
                    //               item._0, item._1, item._2, item._3, item._4, item._5,
                    //               item._6, item._7, item._8, item._9, item._10, item._11);

                    table.Rows.Add(item.cod_tda, item.art_cod, item.art_cal,
                                   item.art_talla, item.art_pares);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return table;
        }
    }
}
