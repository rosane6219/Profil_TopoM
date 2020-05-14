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
        private Trace trace;
        bool isDecimal = false;
        double max, min;
        int equi;
        BitmapImage img;
        String url;

        Regex nameControl = new Regex(@"[A-Za-z0-9]+");
        Regex intControl = new Regex(@"[0-9]+");
        Regex floating = new Regex(@"^[-+]?\d+(.\d+)?$");  
        
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
                            string xsk = dataRead4["nom"].ToString();
                            bac = false;
                            cac++;
                        }
                        if (cac == 0)
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
            int echelle1 = (int)trace.echelle;
            int echelle2 = (int)trace.echellecarte;
            cmd.CommandText = "insert into [Trace] (min,max,echelle,equidistance,image,nom,creation,modification,Id,echellecarte) values ('" + trace.min.ToString() + "','" + trace.max.ToString() + "','" + echelle1 + "','" + trace.equidistance + "','" + trace.image + "','" + trace.nom + "','" + trace.date_creat + "','" + trace.date_modif + "','" + i + "','" + echelle2 + "')";
            cmd.ExecuteNonQuery();
            cnx.Close();
        }

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

        public void CtrShortcut1(Object sender, ExecutedRoutedEventArgs e)
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
                        double ech1 = double.Parse(echelleCM.Text);
                        double ech2 = double.Parse(echelle.Text);
                    if (max < min || ech2==0 || ech1 ==0 || equi % 5 != 0)
                        { 
                            if (max < min) {  MessageBox.Show("L'altitude max doit être supérieure à l'altitude min !"); }
                            if (equi % 5 != 0) { MessageBox.Show("L'équidisatnce doit être multiple de 5"); }
                            else { MessageBox.Show("L'échelle doit être supérieure à zéro"); }
                        }
                        else
                        {
                        trace = new Trace(nomTrace.Text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), min, max, ech1, ech2, Int32.Parse(equidistance.Text), url);
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
                                MessageBox.Show("Ce nom existe déjà , veillez choisir un autre nom ");
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
