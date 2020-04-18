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

        private string _user;
        private SqlConnection _con;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private string _query;
        private Trace trace;
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fujitsu\Desktop\Profil_topo_MAKER\Profil_TopoM\BDDtopo.mdf;Integrated Security=True");



        public Parametrage()
        {
            InitializeComponent();
           
        }

        private void insertion(Trace trace)
        {
            cnx.Open();
            bool bac = false;
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            int i = 0;
            i++;
            int cac = 0;
            while (bac == false)
            {
                string readString4 = "select * from Trace  where Id=" + i;
              
            SqlCommand readCommand4 = new SqlCommand(readString4, cnx);
            int nbs = 1000;
             
          
                using (SqlDataReader dataRead4 = readCommand4.ExecuteReader())

                {
                   
                        if (dataRead4 != null)
                        {
                            while (dataRead4.Read())
                            {
                                string xsk = dataRead4["max"].ToString();
                                
                                bac = false;
                            cac++;
                            }

                            if (cac==0)
                        {
                            bac = true;
                        }
                        cac = 0;
                      }
                       
                    
                   
                }
                i++;
             }
            cnx.Close();
            i = i - 1;
            cnx.Open();
            cmd.CommandText    = "insert into [Trace] (min,max,echelle,equidistance,image,nom,creation,modification,Id) values ('" +trace.min + "','" + trace.max + "','" +trace.echelle + "','" + trace.equidistance + "','" + trace.image + "','" + trace.nom + "','" + trace.date_creat.ToString("dd/mm/yyyy hh:mm:ss") + "','" + trace.date_modif.ToString("dd/mm/yyyy hh:mm:ss") + "','" + i + "')";
          cmd.ExecuteNonQuery();
            cnx.Close();
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

            Trace trace = new Trace(nomTrace.Text, DateTime.Now, DateTime.Now, int.Parse(altritude_min.Text), Int32.Parse(altritude_max.Text), Int32.Parse(echelle.Text), Int32.Parse(equidistance.Text), url);
            insertion(trace);
            Importation imp = new Importation(img, trace);
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(imp);
        }
    }
}
