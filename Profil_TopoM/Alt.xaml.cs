using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Regex floating = new Regex(@"^[-+]?\d+(.\d+)?$");
        bool isDecimal = false;
        public Alt()
        {
            InitializeComponent();
        }

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

        private void altitude_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "." || e.Text == "-")
            {
                if (altitude.Text.Contains(e.Text)) e.Handled = true;
                else isDecimal = (e.Text == ".");
            }
        }

        private void altitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (altitude.Text != "-" && altitude.Text != "")
            {
                if (!floating.IsMatch(altitude.Text) && !isDecimal)
                {
                    altitude.Text = altitude.Text.Substring(0, altitude.Text.Length - 1);
                    altitude.Select(altitude.Text.Length, 0);
                }
                else isDecimal = false;
            }
        }




    }
}
