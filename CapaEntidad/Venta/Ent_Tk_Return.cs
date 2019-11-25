using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Venta
{
    public class Ent_Tk_Return
    {
        public string estado_error { get; set; }
        public decimal genera_cupon { get; set; }
        public string cupon_imprimir { get; set; }
        public string text1_cup { get; set; }
        public string text2_cup { get; set; }
        public string text3_cup { get; set; }
        public string text4_cup { get; set; }

    }
    public class Ent_Tk_Set_Parametro
    {
        public string COD_TDA { get; set; }
        public string FC_SUNA { get; set; }
        public string SERIE { get; set; }
        public string NUMERO { get; set; }
        public decimal MONTO { get; set; }
        public DateTime FECHA { get; set; }

        //public Ent_Tk_Return tk_return { get; set; }
    }
    public class Ent_Tk_Get_Valores
    {
        public string CUP_RTN_BARRA         {get;set;}
        public string CUP_RTN_TDA_GEN       {get;set;}
        public string CUP_RTN_FC_SUNA_GEN   {get;set;}
        public string CUP_RTN_SERIE_GEN     {get;set;}
        public string CUP_RTN_NUMERO_GEN    {get;set;}
        public string CUP_RTN_FECHA_GEN     {get;set;}
        public decimal CUP_RTN_MONTO_GEN     {get;set;}
        public string CUP_RTN_TDA_USO       {get;set;}
        public string CUP_RTN_FC_SUNA_USO   {get;set;}
        public string CUP_RTN_SERIE_USO     {get;set;}
        public string CUP_RTN_NUMERO_USO    {get;set;}
        public string CUP_RTN_FECHA_USO     {get;set;}
        public decimal CUP_RTN_TOTAL_USO     {get;set;}
        public string CUP_RTN_FEC_INI_USO   {get;set;}
        public string CUP_RTN_FEC_FIN_USO   {get;set;}
        public decimal CUP_RTN_MONTO_USO     {get;set;}
        public string CUP_RTN_ESTADO        {get;set;}
        public string CUP_RTN_FEC_ING       {get;set;}
        public string CUP_RTN_FEC_ACT       {get;set;}
        public string CUP_RTN_LOG_ING       {get;set;}
        public string CUP_RTN_LOG_UPD       {get;set;}
        public bool CUP_RTN_IMP           {get;set;}
        public string CUP_RTN_IMP_LOG       { get; set; }
        public string estado_error { get; set; }
        public string cupon_imprimir { get; set; }
    }
    public class Ent_Tk_Get_Parametro
    {
        public string COD_CUP { get; set; }
        public string COD_TDA { get; set; }
        public string FC_SUNA { get; set; }
        public string SERIE { get; set; }
        public string NUMERO { get; set; }
        public decimal MONTO { get; set; }
        public DateTime FECHA { get; set; }

        //public Ent_Tk_Return tk_return { get; set; }
    }
}
    