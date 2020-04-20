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
        Trace trr = new Trace();
        BitmapImage imgg;
        int aym=-1;
        SqlConnection cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");
        public historique()
        {
            InitializeComponent();
           
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
                        int eche = int.Parse(xas2);
                        string xas4 = dataRead3["equidistance"].ToString();
                        int equi = int.Parse(xas4);
                        string xas6 = dataRead3["min"].ToString();
                        int mi = int.Parse(xas6);
                        string xas8 = dataRead3["max"].ToString();
                        int ma = int.Parse(xas8);
                        string xas10 = dataRead3["creation"].ToString();





                        aym = nbs;
                        string xasa = dataRead3["image"].ToString();
                        Uri ur = new Uri(xasa);
                        imgg = new BitmapImage(ur);
                        Trace trace5 = new Trace(nomTracefait.Text, DateTime.Now, DateTime.Now, mi, ma, eche, equi, xasa);
                        trr = trace5;
                    }
                }
            }
            cnx.Close();
            if (aym == -1)
            {
                MessageBox.Show("ce nom n'exuste pas");
            }
            else
            {

                tracecourbes imp = new tracecourbes(imgg, trr);
                var parent = (Grid)this.Parent;
                parent.Children.Clear();
                parent.Children.Add(imp);
            }
        }
    }
}
