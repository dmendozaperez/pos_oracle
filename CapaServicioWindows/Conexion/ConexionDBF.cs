﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Conexion
{
    public class ConexionDBF
    {
        
        public static string _SCDDDES { get { return "SCDDDES"; } } 
        public static string _FVDESPC { get { return "FVDESPC"; } }
        public static string _FVDESPD { get { return "FVDESPD"; } }

        private static string _path_default = @"D:\Fuentes\POS\MOV\POS2";

        public static string _conexion_fvdes_vfpoledb
        {           
            get { return "Provider=VFPOLEDB;Data Source=" + _path_default + ";Exclusive=No"; }
        }
        public static string _conexion_fvdes_oledb(string _path)
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _path + ";Extended Properties=dBASE IV;";
        }
    }
}