using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SourceDBF_WIN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = DBF.GetFmc(@"D:\DBF\FMC");
            //DataTable DT= SourceDBF.DBF.getTabla(@"D:\DBF\FMC", "FMC09802");
        }
    }
}
