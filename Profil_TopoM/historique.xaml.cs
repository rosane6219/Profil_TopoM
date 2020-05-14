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
using System.Data.SqlClient;
using System.Data;

namespace Profil_TopoM
{
    /// <summary>
    /// Logique d'interaction pour historique.xaml
    /// </summary>
    public partial class historique : UserControl
    {

        List<Trace> it = new List<Trace>();
        Trace trr = new Trace();
        BitmapImage imgg;
        int aym = -1;
        SqlConnection cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");
        public historique()
        {
            InitializeComponent();
            cnx.Open();
            string readString3 = "select Id,echelle,min,max,nom,equidistance,image,creation,modification,echellecarte from Trace  ";
            SqlCommand readCommand3 = new SqlCommand(readString3, cnx);
            int nbs;
            using (SqlDataReader dataRead3 = readCommand3.ExecuteReader())
            {
                if (dataRead3 != null)
                {
                    while (dataRead3.Read())
                    {
                        string xas = dataRead3["Id"].ToString();
                        nbs = int.Parse(xas);
                        string no = dataRead3["nom"].ToString();
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
                        string xasa = dataRead3["image"].ToString();
                        Trace trace5 = new Trace(no, xas10, xas105, mi, ma, eche, echecarte, equi, xasa);
                        trr = trace5;
                        it.Add(trr);
                    }
                }
            }
            cnx.Close();
            dg.ItemsSource = it;
        }
        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            cnx.Open();
            string readString3 = "select * from Trace  where nom ='" + nomTracefait.Text + "'";
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
                        Trace trace5 = new Trace(nomTracefait.Text, xas10, xas105, mi, ma, eche, echecarte, equi, xasa);
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



        private void b1_click(object sender, RoutedEventArgs e)
        {
            Trace st = dg.SelectedItem as Trace;
            if (st != null)
            {
                String gag1 = st.nom;
                cnx.Open();
                string readString3 = "select * from Trace  where nom ='" + gag1 + "'";
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
                            Trace trace5 = new Trace(gag1, xas10, xas105, mi, ma, eche, echecarte, equi, xasa);
                            trr = trace5;
                        }
                    }
                }
                cnx.Close();
                tracecourbes imp = new tracecourbes(imgg, trr);
                var parent = (Grid)this.Parent;
                parent.Children.Clear();
                parent.Children.Add(imp);
            }
            else
            {
                MessageBox.Show("Veillez selectionner un tracé.");
            }
        }
    }
}