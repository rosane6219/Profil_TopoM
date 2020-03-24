using System;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Profil_TopoM
{
    /// <summary>
    /// Logique d'interaction pour Parametrage.xaml
    /// </summary>
    public partial class Parametrage : UserControl
    {
        public Parametrage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Selectionner une image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Importation imp = new Importation(new BitmapImage(new Uri(op.FileName)));
                var parent = (Grid)this.Parent;
                parent.Children.Clear();
                parent.Children.Add(imp);
            }
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            Accueil accueil = new Accueil();
            parent.Children.Add(accueil);
        }
    }
}
