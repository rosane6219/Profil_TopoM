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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Profil_TopoM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection _con;
       
        public MainWindow()
        {
            InitializeComponent();
            Accueil home = new Accueil();
            selectionGrid.Children.Add(home);
            _con = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");

        }

       

        private void buttonopenmenu_Click(object sender, RoutedEventArgs e)
        {
            buttonopenmenu.Visibility = Visibility.Collapsed;
            Buttonclosemenu.Visibility = Visibility.Visible;
        }

        private void Buttonclosemenu_Click(object sender, RoutedEventArgs e)
        {
            buttonopenmenu.Visibility = Visibility.Visible;
            Buttonclosemenu.Visibility = Visibility.Collapsed;
        }

        private void paramBtn_Click(object sender, RoutedEventArgs e)
        {
            selectionGrid.Children.Clear();
            Parametrage param = new Parametrage();
            selectionGrid.Children.Add(param);
        }

        private void textBlock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionGrid.Children.Clear();
            Parametrage param = new Parametrage();
            selectionGrid.Children.Add(param);
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            selectionGrid.Children.Clear();
            Accueil accueil = new Accueil();
            selectionGrid.Children.Add(accueil);
        }

        private void textBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionGrid.Children.Clear();
            Accueil accueil = new Accueil();
            selectionGrid.Children.Add(accueil);
        }

        private void dossierBtn_Click(object sender, RoutedEventArgs e)
        {
            selectionGrid.Children.Clear();
            historique param = new historique();
            selectionGrid.Children.Add(param);
        }

        private void textBlock2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionGrid.Children.Clear();
            historique param = new historique();
            selectionGrid.Children.Add(param);
        }

        private void aideBtn_Click(object sender, RoutedEventArgs e)
        {
            selectionGrid.Children.Clear();
            Aide aide = new Aide();
            selectionGrid.Children.Add(aide);
        } 

        private void textBlock3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectionGrid.Children.Clear();
            Aide aide = new Aide();
            selectionGrid.Children.Add(aide);
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textBlock4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

      
    }
}
