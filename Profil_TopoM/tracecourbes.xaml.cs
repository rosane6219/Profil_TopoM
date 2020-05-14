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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Profil_TopoM.Classes;
using System.Data.SqlClient;
using System.Data;

namespace Profil_TopoM
{
	/// <summary>
	/// Logique d'interaction pour Importation.xaml
	/// </summary>
	public partial class tracecourbes : UserControl
	{
		String nomtr;
		BitmapImage kak = new BitmapImage();
		Trace trac = new Trace();
		List<Ellipse> shownPts = new List<Ellipse>();
		List<Line> lignes = new List<Line>();
		List<Point> Points = new List<Point>();
		List<Courbe> courbes = new List<Courbe>();

		SqlConnection cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");

		public tracecourbes(BitmapImage userImage, Trace trace15)
		{
			InitializeComponent();
			img.Source = userImage;
			kak = userImage;
			trac = trace15;
			nomtr = trace15.nom;
		}
		//----------------------------------------------------------------------------------------------------------
		private void next_Click(object sender, RoutedEventArgs e)
		{
			List<Point> Points1 = new List<Point>();
			cnx.Open();
			string readString3 = "select * from Trace  where nom ='" + nomtr + "'";
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
					}
				}
			}
			cnx.Close();
			cnx.Open();
			string readString = "select * from Point  where  Id =" + nbs;
			SqlCommand readCommand = new SqlCommand(readString, cnx);
			List<Courbe> courbes12 = new List<Courbe>();
			double cris1p = 0;
			int ik10 = 0;
			double alts1p = 0;
			Courbe fg = new Courbe();
			using (SqlDataReader dataRead = readCommand.ExecuteReader())
			{
				if (dataRead != null)
				{
					while (dataRead.Read())
					{
						string xs = dataRead["x"].ToString();
						string ys = dataRead["y"].ToString();
						string alts = dataRead["altitude"].ToString();
						string cris = dataRead["critere"].ToString();
						double xs1 = (double)int.Parse(xs);
						double ys1 = (double)int.Parse(ys);
						double alts1 = Double.Parse(alts);
						double cris1 = (double)int.Parse(cris);
						if (cris1 == cris1p)
						{
							Point ab = new Point(xs1, ys1);
							fg.setpoints(ab, ik10);
							ik10++;
						}
						else
						{
							fg.setaltitude(alts1p);
							courbes12.Add(fg);
							fg = new Courbe();
							ik10 = 0;
							Point ab = new Point(xs1, ys1);
							fg.setpoints(ab, ik10);
							ik10++;
						}
						cris1p = cris1;
						alts1p = alts1;
					}
				}
				fg.setaltitude(alts1p);
				courbes12.Add(fg);
			}
			cnx.Close();
			for (int ml = 0; ml < courbes12.Count(); ml++)
			{
				Courbe pap = new Courbe();
				pap = courbes12[ml];
				for (int dml = 0; dml < pap.nbPoints(); dml++)
				{
					double jaj = pap.getpoints(dml).X;
					string dak = jaj.ToString();
				}
			}
			trace_apres_recup imp = new trace_apres_recup(kak, courbes12, nbs);
			var parent = (Grid)this.Parent;
			parent.Children.Clear();
			parent.Children.Add(imp);
		}
	}
}