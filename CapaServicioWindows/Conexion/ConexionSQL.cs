using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.Conexion
{
    public class ConexionSQL
    {
        #region<CONEXION SQL NUBE>
        public static string conexion
        {
            //get { return "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            //get { return "Server=3.16.178.73;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
            //get { return "Server=bd01btp.emcomer.pe;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
            get { return "Server=192.168.2.14;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        }
        public static string conexion_aq
        {
            //get { return "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            //get { return "Server=3.16.178.73;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
            //get { return "Server=bd01btp.emcomer.pe;Database=BdAquarella;User ID=sis_aquarella;Password=Bata2018**;Trusted_Connection=False;"; }
            get { return "Server=192.168.2.14;Database=BdAquarella;User ID=sis_aquarella;Password=Bata2018**;Trusted_Connection=False;"; }
        }
        public static string conexion_ec
        {
            //get { return "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            //get { return "Server=3.16.178.73;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
            get { return "Server=192.168.2.14;Database=BD_ECOMMERCE;User ID=ecommerce;Password=Bata2018.*@=?++;Trusted_Connection=False;"; }
        }
        #endregion
    }
}
