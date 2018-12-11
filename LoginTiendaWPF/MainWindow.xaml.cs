using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginTiendaWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            { 
                      
            string strCambiante  = DateTime.Now.ToString("M/d/yyyy");
            string nombre = strCambiante + "_" + Environment.MachineName;
            string strparam = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(nombre);
            strparam = Convert.ToBase64String(encryted);

            ProcessStartInfo startInfo = new ProcessStartInfo("http://posperu.bgr.pe/BataWeb/LoginIntermedio/Login?variable=" + strparam);
            Process.Start(startInfo);


            InitializeComponent();
            Close();
            }
            catch
            {

            }
        }


    }
}
