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
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Source\Repos\Profil_TopoM\Profil_TopoM\BDDtopo.mdf;Integrated Security=True;Connect Timeout=30");
        public historique()
        {
            InitializeComponent();
<<<<<<< HEAD
=======
            SqlConnection cnx2 = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");
>>>>>>> 8a42412688a4be5586bec1660492f123c7c01da5

            cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Source\Repos\Profil_TopoM\Profil_TopoM\BDDtopo.mdf;Integrated Security=True;Connect Timeout=30");

            cnx.Open();

            string readString3 = "select * from Trace  ";

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
                        

                        string xas4 = dataRead3["equidistance"].ToString();
                        int equi = int.Parse(xas4);
                        string xas6 = dataRead3["min"].ToString();
                        double mi = double.Parse(xas6);
                        string xas8 = dataRead3["max"].ToString();
<<<<<<< HEAD
                        int ma = int.Parse(xas8);
                        string xas10 = dataRead3["creation"].ToString();
                        string xas105 = dataRead3["modification"].ToString();

=======
                        double  ma = double.Parse(xas8);
                        //string xas10 = dataRead3["creation"].ToString();
                        DateTime xas10 = DateTime.Parse(dataRead3["creation"].ToString());
                        string xas21 = dataRead3["echellecarte"].ToString();
                        int echecarte = int.Parse(xas21);
>>>>>>> 8a42412688a4be5586bec1660492f123c7c01da5




                        string xasa = dataRead3["image"].ToString();
                        //Uri ur = new Uri(xasa);
                        //imgg = new BitmapImage(ur);
<<<<<<< HEAD
                        Trace trace5 = new Trace(no, xas10, xas105, mi, ma, eche, equi, xasa);
=======
                        Trace trace5 = new Trace(nomTracefait.Text, xas10, DateTime.Now, mi, ma, eche, equi, xasa,echecarte);
                        //Trace trace5 = new Trace(no, DateTime.Now, DateTime.Now, mi, ma, eche, equi, xasa);
>>>>>>> 8a42412688a4be5586bec1660492f123c7c01da5
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
                        string xas4 = dataRead3["equidistance"].ToString();
                        int equi = int.Parse(xas4);
                        string xas6 = dataRead3["min"].ToString();
                        double mi = double.Parse(xas6);
                        string xas8 = dataRead3["max"].ToString();
<<<<<<< HEAD
                        int ma = int.Parse(xas8);
                        string xas10 = dataRead3["creation"].ToString();
                        string xas105 = dataRead3["modification"].ToString();
=======
                        double ma = double.Parse(xas8);
                        DateTime xas10 = DateTime.Parse(dataRead3["creation"].ToString());
                        string xas21 = dataRead3["echellecarte"].ToString();
                        int echecarte = int.Parse(xas21);
>>>>>>> 8a42412688a4be5586bec1660492f123c7c01da5




                        aym = nbs;
                        string xasa = dataRead3["image"].ToString();
                        Uri ur = new Uri(xasa);
                        imgg = new BitmapImage(ur);
<<<<<<< HEAD
                        Trace trace5 = new Trace(nomTracefait.Text, xas10, xas105, mi, ma, eche, equi, xasa);
=======
                        Trace trace5 = new Trace(nomTracefait.Text, xas10, DateTime.Now, mi, ma, eche, equi, xasa,echecarte);
>>>>>>> 8a42412688a4be5586bec1660492f123c7c01da5
                        trr = trace5;
                    }
                }
            }
            cnx.Close();
            if (aym == -1)
            {
<<<<<<< HEAD
                MessageBox.Show("ce nom n'existe pas !");
=======
                MessageBox.Show("ce nom n'existe pas");
>>>>>>> 8a42412688a4be5586bec1660492f123c7c01da5
            }
            else
            {
                tracecourbes imp = new tracecourbes(imgg, trr);
                var parent = (Grid)this.Parent;
                parent.Children.Clear();
                parent.Children.Add(imp);
            }
        }

       

        private void b1_click (object sender, RoutedEventArgs e)
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
                            string xas4 = dataRead3["equidistance"].ToString();
                            int equi = int.Parse(xas4);
                            string xas6 = dataRead3["min"].ToString();
                            int mi = int.Parse(xas6);
                            string xas8 = dataRead3["max"].ToString();
                            int ma = int.Parse(xas8);
                            string xas10 = dataRead3["creation"].ToString();
                            string xas105 = dataRead3["modification"].ToString();




                            aym = nbs;
                            string xasa = dataRead3["image"].ToString();
                            Uri ur = new Uri(xasa);
                            imgg = new BitmapImage(ur);
                            Trace trace5 = new Trace(gag1, xas10, xas105, mi, ma, eche, equi, xasa);
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
                MessageBox.Show("Veillez cliquer sur un tracé fait svp !");
            }
        }
    }
}