using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceXstore
{
    [RunInstaller(true)]
    public partial class Install_Xstore : System.Configuration.Install.Installer
    {
        public Install_Xstore()
        {
            InitializeComponent();
        }
    }
}
