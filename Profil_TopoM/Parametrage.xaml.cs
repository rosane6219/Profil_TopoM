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
using System.Data.SqlClient;
using System.Data;

namespace Profil_TopoM
{
    /// <summary>
    /// Logique d'interaction pour Parametrage.xaml
    /// </summary>
    public partial class Parametrage : UserControl
    {

        
        private SqlConnection _con;
        private SqlCommand _command;
        //private SqlDataReader _reader;
        private string _query;
       // private Trace trace;
        private int id=0;

        public Parametrage()
        {
            InitializeComponent();
            _con = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf; Integrated Security = True");
            
        }
       
        private void insertionTrace(Trace trace )
        {
            _query = "INSERT INTO Trace VALUES(@min,@max,@echelle,@equidistance,@image,@nom,@creation,@modification)";
            _con.Open();
            using (_command = new SqlCommand(_query, _con))
            {
                _command.CommandType = CommandType.Text;
                _command.Parameters.AddWithValue("@min", trace.Min);
                _command.Parameters.AddWithValue("@max", trace.Max);
                _command.Parameters.AddWithValue("@echelle", trace.Echelle);
                _command.Parameters.AddWithValue("@equidistance", trace.Equidistance);
                _command.Parameters.AddWithValue("@image", trace.Image);
                _command.Parameters.AddWithValue("@nom", trace.Nom);
                _command.Parameters.AddWithValue("@creation", trace.Datecreation.ToString("dd/mm/yyyy hh:mm:ss"));
                _command.Parameters.AddWithValue("@modification", trace.Datemodification.ToString("dd/mm/yyyy hh:mm:ss"));
                _command.ExecuteNonQuery();
                //id = (int) _command.ExecuteScalar();
            }
            
            _con.Close();
            //list.Items.Add(user);
        }
        BitmapImage img;
        String url;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Selectionner une image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
               url = op.FileName;
               img = new BitmapImage(new Uri(op.FileName));

            }
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            Accueil accueil = new Accueil();
            parent.Children.Add(accueil);
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        { 
            Trace trace = new Trace(nomTrace.Text,DateTime.Now,DateTime.Now,int.Parse(altritude_min.Text), Int32.Parse(altritude_max.Text), Int32.Parse(echelle.Text), Int32.Parse(equidistance.Text), url);
            insertionTrace(trace);
            Importation imp = new Importation(img,id);
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(imp);
        }
    }
}
