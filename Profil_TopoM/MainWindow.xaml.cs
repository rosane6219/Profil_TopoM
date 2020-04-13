using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection _con;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private string _query;
        private List<Trace> list= new List<Trace>();
        public MainWindow()
        {
            InitializeComponent();
            _con = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf; Integrated Security = True");
           // LoadData();

        }
        private void LoadData() 
        {
            String name; DateTime creat; DateTime modif; int mini; int maxi; int echel; int equi; String img;

            _query = "SELECT * FROM Trace";
            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();
                using (_command = new SqlCommand(_query, _con))
                {
                    _reader = _command.ExecuteReader();
                    while (_reader.Read())
                    { //(String name,DateTime creat,DateTime modif,int mini,int maxi,int echel,int equi,String img)
                      //int.Parse(altritude_min.Text)
                        name = _reader[6].ToString();
                        creat = DateTime.Parse(_reader[7].ToString());
                        mini = int.Parse(_reader[1].ToString());
                        list.Add(
                            new Trace(
                                 _reader[6].ToString(),//nom
                                DateTime.Parse(_reader[7].ToString()),//cre
                                DateTime.Parse(_reader[8].ToString()),//modif
                                int.Parse(_reader[1].ToString()),//min
                                int.Parse(_reader[2].ToString()),//max
                                int.Parse(_reader[3].ToString()),//ech
                                int.Parse(_reader[4].ToString()),//equi
                                _reader[5].ToString()//img

                                )
                            );
                    }
                    _reader.Close();

                }
                _con.Close();
            }
            catch (Exception ex){ Console.WriteLine(ex.Message); }
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

        }

        private void textBlock2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void aideBtn_Click(object sender, RoutedEventArgs e)
        {

        } 

        private void textBlock3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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
