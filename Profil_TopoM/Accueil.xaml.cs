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
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : UserControl
    {
        private SqlConnection _con;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private string _query;

        public Accueil()
        {
            InitializeComponent();
            _con = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");

        }

        private void recherche()
        {
            _query = "SELECT * FROM Trace  where nom = @nom";
            //string readString3 = "select * from Trace  where nom ='" + nomTrace.Text + "'";

            //SqlCommand readCommand3 = new SqlCommand(readString3, _con);
            _con.Open();

            /*using (SqlDataReader dataRead3 = readCommand3.ExecuteReader())

            {
                if (dataRead3 != null)
                {
                    while (dataRead3.Read())
                    {
                        string xas = dataRead3["Id"].ToString();
                        nbs = int.Parse(xas);
                        string xas2 = dataRead3["echelle"].ToString();
                        int eche = int.Parse(xas2);
                        string xas4 = dataRead3["equidistance"].ToString();
                        int equi = int.Parse(xas4);
                        string xas6 = dataRead3["min"].ToString();
                        int mi = int.Parse(xas6);
                        string xas8 = dataRead3["max"].ToString();
                        int ma = int.Parse(xas8);
                        string xas10 = dataRead3["creation"].ToString();
                        string xasa = dataRead3["image"].ToString();
                        Uri ur = new Uri(xasa);
                        BitmapImage imgg = new BitmapImage(ur);
                        Trace trace = new Trace(nomTrace.Text, DateTime.Now, DateTime.Now, mi, ma, eche, equi, xasa);
                        tracecourbes affichage = new tracecourbes(new BitmapImage(new Uri(_reader[4].ToString())), trace);
                        var parent = (Grid)this.Parent;
                        parent.Children.Clear();
                        parent.Children.Add(affichage);
                        _reader.Close();
                    }
                }
            }*/

            using (_command = new SqlCommand(_query, _con))
            {
                _command.CommandType = CommandType.Text;
                _command.Parameters.AddWithValue("@nom", nomTrace.Text);
                _reader = _command.ExecuteReader();
            }
            if (_reader.HasRows)
            {
                _reader.Read();

                double min = double.Parse(_reader[0].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                double max = double.Parse(_reader[1].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                
                    Trace trace = new Trace(nomTrace.Text, DateTime.Parse(_reader[6].ToString()), DateTime.Now,
                        min, max,
                        int.Parse(_reader[2].ToString()), int.Parse(_reader[3].ToString()), _reader[4].ToString(),int.Parse(_reader[9].ToString()));
                    tracecourbes affichage = new tracecourbes(new BitmapImage(new Uri(_reader[4].ToString())), trace);
                    var parent = (Grid)this.Parent;
                    parent.Children.Clear();
                    parent.Children.Add(affichage);
                    _reader.Close();
                
            }
            else
            {
                MessageBox.Show("Nom de tracé introuvable");
                _reader.Close();

            }
            _con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            recherche();
        }
    }
}
