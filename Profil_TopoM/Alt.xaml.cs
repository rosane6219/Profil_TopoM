using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Profil_TopoM
{
    /// <summary>
    /// Logique d'interaction pour Alt.xaml
    /// </summary>
    public partial class Alt : Window
    {
    
        public Alt() { InitializeComponent(); }
        public Alt(Button button, TextBox t, TextBox tt)
        {
            sauvegarder = button;
            altitude = t;
            InitializeComponent();
        }
        private void sauvegarder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
