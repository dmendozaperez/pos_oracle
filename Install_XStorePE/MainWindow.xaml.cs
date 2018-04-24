using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Install_XStorePE
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnejecutar_Click(object sender, RoutedEventArgs e)
        {
            //nombrequipo();
            try
            {
                //bool status = SetComputerName("PCPRUEBA");
                //MessageBox.Show("ok");
                runDBinstaller();
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
         

            
        }

        private void runDBinstaller()
        {
            try
            { 
               System.Diagnostics.Process.Start(@"C:\staging\01.runDBinstaller.bat");
            }
            catch
            {
                throw;
            }
        }

        private void nombrequipo()
        {
            string shostname;
            shostname = System.Net.Dns.GetHostName();
            MessageBox.Show(shostname);
        }
        [DllImport("kernel32.dll")]
        static extern bool SetComputerName(string lpComputerName);
       
    }
}
