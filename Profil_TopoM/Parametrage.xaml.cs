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
using System.Text.RegularExpressions;

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
        bool isDecimal = false;
        Regex nameControl = new Regex(@"[A-Za-z0-9]+");
        Regex intControl = new Regex(@"[0-9]+");
        Regex floating = new Regex(@"^[-+]?\d+(.\d+)?$");      

        SqlConnection cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= {System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");

        //C:\Users\Fujitsu\Desktop\Profil_topo_MAKER25\Profil_topo_MAKER\Profil_TopoM\BDDtopo.mdf
       // SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= C:\Users\User\source\repos\Profil_TopoM\Profil_TopoM\BDDtopo.mdf;Integrated Security=True");


        public Parametrage()
        {
            InitializeComponent();
        }

        private void insertion(Trace trace)
        {
            cnx.Open();
            bool bac = false;
            SqlCommand cmd;
            //SqlCommand cmd = cnx.CreateCommand();
           // cmd.CommandType = CommandType.Text;
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
            
            i = i - 1;

           /* cmd.CommandText = "insert into [Trace] (min,max,echelle,equidistance,image,nom,creation,modification,Id) values ('" + trace.min + "','" + trace.max + "','" + trace.echelle + "','" + trace.equidistance + "','" + trace.image + "','" + trace.nom + "','" + trace.date_creat.ToString("dd/mm/yyyy hh:mm:ss") + "','" + trace.date_modif.ToString("dd/mm/yyyy hh:mm:ss") + "','" + i + "')";
            cmd.ExecuteNonQuery();
            cnx.Close();*/
            //______________________________________________________________________________________________________________
             _query = "insert into Trace values (@min, @max, @echelle, @equidistance, @image, @nom, @dateCrea, @dateModif, @id,@echellecarte)";
             using (cmd = new SqlCommand(_query, cnx))
             {
                 cmd.Parameters.AddWithValue("@min", trace.min);
                 cmd.Parameters.AddWithValue("@max", trace.max);
                 cmd.Parameters.AddWithValue("@echelle", trace.echelle);
                 cmd.Parameters.AddWithValue("@equidistance", trace.equidistance);
                 cmd.Parameters.AddWithValue("@image", trace.image);
                 cmd.Parameters.AddWithValue("@nom", trace.nom);
                 cmd.Parameters.AddWithValue("@dateCrea", trace.date_creat);
                 cmd.Parameters.AddWithValue("@dateModif", trace.date_modif);
                 cmd.Parameters.AddWithValue("@id", i);
                 cmd.Parameters.AddWithValue("@echellecarte", trace.echellecarte);
                 try
                 {
                     cmd.ExecuteNonQuery();
                 }
                 catch (Exception e)
                 {
                     MessageBox.Show(e.Message);
                 }
             }
             cnx.Close();
            //________________________________________________________________________
            /* _query = "insert into [Trace] (min,max,echelle,equidistance,image,nom,creation,modification,Id) values ('" + trace.min + "','" + trace.max + "','" + trace.echelle + "','" + trace.equidistance + "','" + trace.image + "','" + trace.nom + "','" + trace.date_creat.ToString("dd/MM/yyyy hh:mm:ss") + "','" + trace.date_modif.ToString("dd/MM/yyyy hh:mm:ss") + "','" + i + "')";
             cmd = new SqlCommand(_query, cnx);
             //cmd.CommandText    = "insert into [Trace] (min,max,echelle,equidistance,image,nom,creation,modification,Id) values ('" +trace.min + "','" + trace.max + "','" +trace.echelle + "','" + trace.equidistance + "','" + trace.image + "','" + trace.nom + "','" + trace.date_creat.ToString("dd/MM/yyyy hh:mm:ss") + "','" + trace.date_modif.ToString("dd/MM/yyyy hh:mm:ss") + "','" + i + "')";
             cmd.ExecuteNonQuery();
             cnx.Close();*/
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

        double max,min;
        int equi,ech,echCM;
        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nomTrace.Text == "" || altritude_max.Text == "" || altritude_min.Text == "" || echelle.Text == "" || equidistance.Text == "" || url == null || echelleCM.Text=="")
            {
                MessageBox.Show("Veuillez remplir tous les champs !");
            } 
            else
            { equi = int.Parse(equidistance.Text);
                
                    try
                    {
                        max = double.Parse(altritude_max.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        min = double.Parse(altritude_min.Text, System.Globalization.CultureInfo.InvariantCulture);
                        ech = int.Parse(echelle.Text);
                        echCM = int.Parse(echelleCM.Text);
                        if (max < min || ech==0 || echCM ==0 || equi % 5 != 0)
                        { 
                            if (max < min) {  MessageBox.Show("L'altitude max doit être supérieure à l'altitude min !"); }
                            if (equi % 5 != 0) { MessageBox.Show("L'équidisatnce doit être multiple de 5"); }
                            else { MessageBox.Show("L'échelle doit être supérieure à zéro"); }
                        }
                        else
                        {
                            /*
                              Trace trace = new Trace(nomTrace.Text, DateTime.Now, DateTime.Now, min,
                                   max, Int32.Parse(echelle.Text),
                                   Int32.Parse(equidistance.Text), url, Int32.Parse(echelleCM.Text));
                             insertion(trace);
                            Importation imp = new Importation(img, trace);
                            var parent = (Grid)this.Parent;
                            parent.Children.Clear();
                            parent.Children.Add(imp);*/
                            Trace trace = new Trace(nomTrace.Text, DateTime.Now, DateTime.Now, min,
                                   max, Int32.Parse(echelle.Text),
                                   Int32.Parse(equidistance.Text), url, Int32.Parse(echelleCM.Text));

                            cnx.Open();
                            string readString56 = "select * from Trace  where nom ='" + nomTrace.Text + "'";
                            SqlCommand readCommand56 = new SqlCommand(readString56, cnx);
                            int aym = -1;
                            int gk = -1;
                            using (SqlDataReader dataRead31 = readCommand56.ExecuteReader())
                            {
                                if (dataRead31 != null)
                                {
                                    while (dataRead31.Read())
                                    {
                                        string xas = dataRead31["Id"].ToString();
                                        gk = int.Parse(xas);
                                        aym = gk;
                                    }
                                }
                            }
                            cnx.Close();
                            if (aym == -1)
                            {
                                insertion(trace);
                                Importation imp = new Importation(img, trace);
                                var parent = (Grid)this.Parent;
                                parent.Children.Clear();
                                parent.Children.Add(imp);
                            }

                            else
                            {
                                MessageBox.Show("ce nom existe déja , faut que vous changez du nom !");
                            }

                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                
            }
        }

        private void nomTrace_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!nameControl.IsMatch(e.Text)) { e.Handled = true; MessageBox.Show("Caractère non valide"); }
        }

        private void equidistance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) { e.Handled = true; MessageBox.Show("Caractère non valide"); }
        }

        private void echelleCM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) { e.Handled = true; MessageBox.Show("Caractère non valide"); }
        }

        private void echelle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) { e.Handled = true; MessageBox.Show("Caractère non valide"); }
        }

        private void altritude_min_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "." || e.Text == "-")
            {
                if (altritude_min.Text.Contains(e.Text)) e.Handled = true;
                else isDecimal = (e.Text == ".");
            }

        }

        private void altritude_min_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (altritude_min.Text != "-" && altritude_min.Text != "")
            {
                if (!floating.IsMatch(altritude_min.Text) && !isDecimal)
                {
                    altritude_min.Text = altritude_min.Text.Substring(0, altritude_min.Text.Length - 1);
                    altritude_min.Select(altritude_min.Text.Length, 0);
                }
                else  isDecimal = false;
            }
          
        }

        private void altritude_max_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "." || e.Text == "-")
            {
                if (altritude_max.Text.Contains(e.Text)) e.Handled = true;
                else isDecimal = (e.Text == ".");
            }

        }

        private void altritude_max_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (altritude_max.Text != "-" && altritude_max.Text != "")
            {
                if (!floating.IsMatch(altritude_max.Text) && !isDecimal)
                {
                    altritude_max.Text = altritude_max.Text.Substring(0, altritude_max.Text.Length - 1);
                    altritude_max.Select(altritude_max.Text.Length, 0);
                }
                else  isDecimal = false; 
            }
        }

       
    }
}
