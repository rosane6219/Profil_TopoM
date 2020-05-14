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
        private SqlConnection cnx;
        Trace trr = new Trace();
        BitmapImage imgg;
        int aym = -1;
        public Accueil()
        {
            InitializeComponent();
            cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");
        }

        private void recherche()
        {
            cnx.Open();
            string readString3 = "select * from Trace  where nom ='" + nomTrace.Text + "'";
            SqlCommand readCommand3 = new SqlCommand(readString3, cnx);
            int nbs = 1000;
            using (SqlDataReader dataRead3 = readCommand3.ExecuteReader())
            {
                if (dataRead3 != null)
                {
                    while (dataRead3.Read())
                    {
                        string xas = dataRead3["Id"].ToString();
                        nbs = int.Parse(xas);
                        string xas2 = dataRead3["echelle"].ToString();
                        double eche = double.Parse(xas2);
                        string xas22 = dataRead3["echellecarte"].ToString();
                        double echecarte = double.Parse(xas22);
                        string xas4 = dataRead3["equidistance"].ToString();
                        int equi = int.Parse(xas4);
                        string xas6 = dataRead3["min"].ToString();
                        double mi = Double.Parse(xas6);
                        string xas8 = dataRead3["max"].ToString();
                        double ma = Double.Parse(xas8);
                        string xas10 = dataRead3["creation"].ToString();
                        string xas105 = dataRead3["modification"].ToString();
                        aym = nbs;
                        string xasa = dataRead3["image"].ToString();
                        Uri ur = new Uri(xasa);
                        imgg = new BitmapImage(ur);
                        Trace trace5 = new Trace(nomTrace.Text, xas10, xas105, mi, ma, eche, echecarte, equi, xasa);
                        trr = trace5;
                    }
                }
            }
            cnx.Close();
            if (aym == -1)
            {
                MessageBox.Show(" Aucun tracé ne correspond au nom saisi.");
            }
            else
            {
                tracecourbes imp = new tracecourbes(imgg, trr);
                var parent = (Grid)this.Parent;
                parent.Children.Clear();
                parent.Children.Add(imp);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            recherche();
        }
    }
}
