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
        Regex nameControl = new Regex(@"[A-Za-z0-9]+");
        Regex intControl = new Regex(@"[0-9]+");
        SqlConnection cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= {System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");



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
            if (nomTrace.Text == "" || altritude_max.Text == "" || altritude_min.Text == "" || echelle.Text == "" || equidistance.Text == "" || url == "")
            {
                /*SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PrimaryHueMidBrush"];
                var msg = new CustomMaterialMessageBox
                {
                    TxtMessage = { Text = ("Veuillez remplir tous les champs ! "), FontSize = 14, Foreground = Brushes.Black, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center },
                    TxtTitle = { Text = "Erreur", Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Left },
                    BtnOk = { Content = "OK", Background = MyBrush, BorderBrush = Brushes.Transparent },
                    BtnCancel = { Visibility = Visibility.Collapsed, BorderBrush = Brushes.Transparent },
                    BtnCopyMessage = { Visibility = Visibility.Hidden },
                    MainContentControl = { Background = Brushes.White },


                    TitleBackgroundPanel = { Background = MyBrush },
                    Height = 220,
                    Width = 350,
                    BorderBrush = MyBrush
                };
                msg.Show();*/
                MessageBox.Show("Veuillez remplir tous les champs !");
            }
            else
            {
                if (Int32.Parse(altritude_max.Text) < int.Parse(altritude_min.Text)) 
                { MessageBox.Show("L'altitude max doit être superieure à l'altitude min !"); }
                else
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

        private void nomTrace_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!nameControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void equidistance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void echelleCM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void echelle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void altritude_min_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void altritude_max_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) e.Handled = true;
        }
    }
}
