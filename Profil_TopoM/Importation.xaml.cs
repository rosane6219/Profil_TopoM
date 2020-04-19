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
using Profil_TopoM.Classes;

namespace Profil_TopoM
{
	/// <summary>
	/// Logique d'interaction pour Importation.xaml
	/// </summary>
	public partial class Importation : UserControl
	{


		int Fin = 0;
		double Echelle, Equidistance, altmin, altmax;
		Courbe cou = new Courbe();

		int Ss = 0;
		BitmapImage kak;
		Trace trac= new Trace();
		String nomtr;
		SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fujitsu\Desktop\Profil_topo_MAKER\Profil_TopoM\BDDtopo.mdf;Integrated Security=True");
		public Importation(BitmapImage userImage, Trace trace15)
		{


			InitializeComponent();
			img.Source = userImage;
<<<<<<< HEAD
			kak = userImage;
			trac = trace15;
			nomtr = trace15.nom;


		}

		int k = 0;
=======

		}
		int k = 0; Line segment;

>>>>>>> 9f1a0b9a76b8b8f3123557609c141a9a970e191e
		List<Point> Points = new List<Point>();
		List<Courbe> courbes = new List<Courbe>();
		bool lineStarted = false;
		Point mousePoint1;
		bool lineStarted0 = false;
		Point mousePoint;
		List<Ellipse> shownPts = new List<Ellipse>();
		List<Line> lignes = new List<Line>();
		MouseButtonEventArgs m;
		bool dep = false;
		List<Point> pointIntersection = new List<Point>();
		List<double> altitudee = new List<double>();
		double alt1 = 0;
		double alt2 = 0;
		double pente;

		//------------------------------------------------------------------------------------------------------


		private void cnv_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				int ok = 0; int cpt = 5; int trouve = 0; int l = 0;
				Courbe c1 = courbes[0];
				while ((ok == 0) && (l < courbes.Count))
				{
					trouve = 0;
					while ((ok == 0) && (trouve < courbes[l].nbPoints()))
					{
						if ((courbes[l].getpoints(trouve).X - cpt <= e.GetPosition(this).X) && (e.GetPosition(this).X <= courbes[l].getpoints(trouve).X + cpt) && (courbes[l].getpoints(trouve).Y - cpt <= e.GetPosition(this).Y) && (e.GetPosition(this).Y <= courbes[l].getpoints(trouve).Y + cpt))
						{
							ok = 1;
						}
						else { trouve++; }
					}
					l++;
				}
				l = l - 1;
				if (ok == 1)
				{

					dep = true;
					cnv.Children.Remove(courbes[l].getshownPts(trouve));
					base.OnMouseDoubleClick(e);
					Point p = e.GetPosition(this);
					Ellipse ell = new Ellipse();
					ell.Width = 7;
					ell.Height = 7;
					ell.Fill = Brushes.SkyBlue;
					ell.Stroke = Brushes.Transparent;
					courbes[l].setpoints(p, trouve);
					Canvas.SetLeft(ell, courbes[l].getpoints(trouve).X - 5 / 2);
					Canvas.SetTop(ell, courbes[l].getpoints(trouve).Y - 10 / 2);

					courbes[l].setshownPts(ell, trouve);
					cnv.Children.Add(ell);
					if (trouve > 0) cnv.Children.Remove(courbes[l].getlignes(trouve - 1));
					Line newLine, line;
					if (((trouve - 1) >= 0) && ((trouve) < courbes[l].nbPoints()))
					{
						cnv.Children.Remove(courbes[l].getlignes(trouve - 1));
						newLine = new Line { X1 = courbes[l].getpoints(trouve - 1).X, Y1 = courbes[l].getpoints(trouve - 1).Y, X2 = p.X, Y2 = p.Y };
						courbes[l].setlignes(newLine, trouve - 1);
						if ((courbes[l].getaltitude() <= 0)) { newLine.Stroke = Brushes.Blue; }
						if ((courbes[l].getaltitude() > 0) && (courbes[l].getaltitude() <= 50)) { newLine.Stroke = Brushes.SkyBlue; }
						if ((courbes[l].getaltitude() > 50) && (courbes[l].getaltitude() <= 100)) { newLine.Stroke = Brushes.AliceBlue; }
						if ((courbes[l].getaltitude() > 100) && (courbes[l].getaltitude() <= 200)) { newLine.Stroke = Brushes.Green; }
						if ((courbes[l].getaltitude() > 200) && (courbes[l].getaltitude() <= 400)) { newLine.Stroke = Brushes.GreenYellow; }
						if ((courbes[l].getaltitude() > 400) && (courbes[l].getaltitude() <= 600)) { newLine.Stroke = Brushes.Yellow; }
						if ((courbes[l].getaltitude() > 600) && (courbes[l].getaltitude() <= 800)) { newLine.Stroke = Brushes.Orange; }
						if ((courbes[l].getaltitude() > 800) && (courbes[l].getaltitude() <= 1000)) { newLine.Stroke = Brushes.OrangeRed; }
						if (courbes[l].getaltitude() > 1000) { newLine.Stroke = Brushes.Red; }
						courbes[l].setlignes(newLine, trouve - 1);
						newLine.StrokeThickness = 2;
						cnv.Children.Add(newLine);

					}
					if ((trouve + 1) < courbes[l].nbPoints())
					{
						line = new Line { X1 = courbes[l].getpoints(trouve + 1).X, Y1 = courbes[l].getpoints(trouve + 1).Y, X2 = p.X, Y2 = p.Y };

						cnv.Children.Remove(courbes[l].getlignes(trouve));
						courbes[l].setlignes(line, trouve);
						if ((courbes[l].getaltitude() <= 0)) { line.Stroke = Brushes.Blue; }
						if ((courbes[l].getaltitude() > 0) && (courbes[l].getaltitude() <= 50)) { line.Stroke = Brushes.SkyBlue; }
						if ((courbes[l].getaltitude() > 50) && (courbes[l].getaltitude() <= 100)) { line.Stroke = Brushes.AliceBlue; }
						if ((courbes[l].getaltitude() > 100) && (courbes[l].getaltitude() <= 200)) { line.Stroke = Brushes.Green; }
						if ((courbes[l].getaltitude() > 200) && (courbes[l].getaltitude() <= 400)) { line.Stroke = Brushes.GreenYellow; }
						if ((courbes[l].getaltitude() > 400) && (courbes[l].getaltitude() <= 600)) { line.Stroke = Brushes.Yellow; }
						if ((courbes[l].getaltitude() > 600) && (courbes[l].getaltitude() <= 800)) { line.Stroke = Brushes.Orange; }
						if ((courbes[l].getaltitude() > 800) && (courbes[l].getaltitude() <= 1000)) { line.Stroke = Brushes.OrangeRed; }
						if (courbes[l].getaltitude() > 1000) { line.Stroke = Brushes.Red; }

						line.StrokeThickness = 2;
						cnv.Children.Add(line);

					}
					if (k == 1) { this.lineStarted = false; k = 0; }
				}
				this.InvalidateVisual();
			}
			catch (Exception exp) { MessageBox.Show("Veuillez dessiner au moins une ligne afin de pouvoir modifier des points SVP", "Erreur"); }
		}
		//-----------------------------------------------------------------------------------------------------
		private void cnv_MouseMove(object sender, MouseEventArgs e)
		{
			if (Ss == 0)
			{
				base.OnMouseMove(e);
				if (lineStarted)
					cnv.InvalidateVisual();
			}
			else
			{
				base.OnMouseMove(e);
				if (lineStarted0)
					this.InvalidateVisual();
			}
		}
		//-------------------------------------------------------------------------------------------------------
		private void btn_Click(object sender, RoutedEventArgs e)
		{
			if (courbes[courbes.Count - 1].nbPoints() <= 0) { courbes.Remove(courbes[courbes.Count - 1]); }
			courbes[courbes.Count - 1].removePoint(courbes[courbes.Count - 1].getpoints(courbes[courbes.Count - 1].nbPoints() - 1));
			cnv.Children.Remove(courbes[courbes.Count - 1].getshownPts(courbes[courbes.Count - 1].nbshownPts() - 1));
			courbes.Last().removeshownPts(courbes[courbes.Count - 1].getshownPts(courbes[courbes.Count - 1].nbshownPts() - 1));
			if (courbes[courbes.Count - 1].nblignes() > 0)
			{
				cnv.Children.Remove(courbes[courbes.Count - 1].getlignes(courbes[courbes.Count - 1].nblignes() - 1));
				courbes[courbes.Count - 1].removeligne(courbes[courbes.Count - 1].getlignes(courbes[courbes.Count - 1].nblignes() - 1));
			}
			if (courbes[courbes.Count - 1].nbPoints() > 0)
			{
				lineStarted = true;
				mousePoint1 = courbes[courbes.Count - 1].getpoints(courbes[courbes.Count - 1].nbPoints() - 1);
			}
			else
			{
				lineStarted = false;
				k = 0;
			}
		}
		int suppp = 0; int sup = 0;
		//------------------------------------------------------------------------------------------------------
		private void Arret_Click(object sender, RoutedEventArgs e)
		{
			lineStarted = false;
			k = 0;
		}
		Courbe c; Point mousepoint;
		int s = 0; int u = 0; int v = 0;
		private void cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			mousepoint = Mouse.GetPosition(this);

			if (Ss == 0)
			{
				if (suppp == 1)
				{
					suppp = 0;
					sup = 1;
					int trouve = 0, l = 0;
					int ok = 0;
					int cpt = 10;
					while ((ok == 0) && (l < courbes.Count))
					{
						trouve = 0;
						while ((ok == 0) && (trouve < courbes[l].nbPoints()))
						{
							if ((courbes[l].getpoints(trouve).X - cpt <= e.GetPosition(this).X) &&
								(e.GetPosition(this).X <= courbes[l].getpoints(trouve).X + cpt) &&
								(courbes[l].getpoints(trouve).Y - cpt <= e.GetPosition(this).Y) &&
								(e.GetPosition(this).Y <= courbes[l].getpoints(trouve).Y + cpt))
							{
								ok = 1;
							}
							else { trouve++; }
						}
						l++;
					}
					l = l - 1;
					cnv.Children.Remove(courbes[l].getshownPts(trouve));
					try { shownPts.Remove(shownPts[trouve]); } catch (Exception exp) { }
					courbes[l].removeshownPts(courbes[l].getshownPts(trouve));
					if (trouve > 0) { cnv.Children.Remove(courbes[l].getlignes(trouve - 1)); courbes[l].removeligne(courbes[l].getlignes(trouve - 1)); }
					Line newLine;
					if (trouve == 0)
					{
						cnv.Children.Remove(courbes[l].getlignes(trouve));
						courbes[l].removeligne(courbes[l].getlignes(trouve));
					}
					if (((trouve - 1) >= 0) && ((trouve + 1) < courbes[l].nbPoints()))
					{
						cnv.Children.Remove(courbes[l].getlignes(trouve - 1));
						newLine = new Line
						{
							X1 = courbes[l].getpoints(trouve - 1).X,
							Y1 = courbes[l].getpoints(trouve - 1).Y,
							X2 = courbes[l].getpoints(trouve + 1).X,
							Y2 = courbes[l].getpoints(trouve + 1).Y
						};
						courbes[l].remplacer(newLine, trouve - 1);
						if ((courbes[l].getaltitude() <= 0)) { newLine.Stroke = Brushes.Blue; }
						if ((courbes[l].getaltitude() > 0) && (courbes[l].getaltitude() <= 50)) { newLine.Stroke = Brushes.SkyBlue; }
						if ((courbes[l].getaltitude() > 50) && (courbes[l].getaltitude() <= 100)) { newLine.Stroke = Brushes.AliceBlue; }
						if ((courbes[l].getaltitude() > 100) && (courbes[l].getaltitude() <= 200)) { newLine.Stroke = Brushes.Green; }
						if ((courbes[l].getaltitude() > 200) && (courbes[l].getaltitude() <= 400)) { newLine.Stroke = Brushes.GreenYellow; }
						if ((courbes[l].getaltitude() > 400) && (courbes[l].getaltitude() <= 600)) { newLine.Stroke = Brushes.Yellow; }
						if ((courbes[l].getaltitude() > 600) && (courbes[l].getaltitude() <= 800)) { newLine.Stroke = Brushes.Orange; }
						if ((courbes[l].getaltitude() > 800) && (courbes[l].getaltitude() <= 1000)) { newLine.Stroke = Brushes.OrangeRed; }
						if (courbes[l].getaltitude() > 1000) { newLine.Stroke = Brushes.Red; }

						newLine.StrokeThickness = 2;
						cnv.Children.Add(newLine);


					}
					courbes[l].removePoint(courbes[l].getpoints(trouve));
				}
				else
				{
					base.OnMouseDown(e);
					if (!lineStarted)
					{
						s = 0;
						u = 0;
						v = 0;
						lineStarted = true;
						c = new Courbe();
						courbes.Add(c);
						mousePoint1 = e.GetPosition(this);
						Points.Add(mousePoint1);
						courbes.Last().Addpoints(mousePoint1);
						Ellipse ell = new Ellipse();
						ell.Width = 7;
						ell.Height = 7;
						ell.Fill = Brushes.SkyBlue;
						ell.Stroke = Brushes.Transparent;
						Canvas.SetLeft(ell, mousePoint1.X - 5 / 2);
						Canvas.SetTop(ell, mousePoint1.Y - 10 / 2);
						shownPts.Add(ell);
						courbes.Last().setshownPts(ell, u); u++;
						cnv.Children.Add(ell);
						int ex = 0;
						while (ex == 0)
						{
							try
							{
								Alt alt = new Alt();
								alt.ShowDialog();
								double altit = double.Parse(alt.altitude.Text);
								courbes.Last().setaltitude(altit);
								ex = 1;
							}
							catch (Exception exp)
							{
								MessageBox.Show("Veuillez entrer l'altitude SVP", "Erreur");
								ex = 0;
							}
						}
					}
					else
					{
						Point mousePoint2 = e.GetPosition(this);
						if (sup == 1) { mousePoint1 = courbes.Last().getpoints(courbes.Last().nbPoints() - 1); sup = 0; }
						Line newLine = new Line { X1 = mousePoint1.X, Y1 = mousePoint1.Y, X2 = mousePoint2.X, Y2 = mousePoint2.Y };
						lignes.Add(newLine);
						ToolTip t = new ToolTip();
						t.Content = courbes.Last().getaltitude();
						t.Background = Brushes.AliceBlue;
						newLine.ToolTip = t;

						if ((courbes.Last().getaltitude() <= 0)) { newLine.Stroke = Brushes.Blue; }
						if ((courbes.Last().getaltitude() > 0) && (courbes.Last().getaltitude() <= 50)) { newLine.Stroke = Brushes.SkyBlue; }
						if ((courbes.Last().getaltitude() > 50) && (courbes.Last().getaltitude() <= 100)) { newLine.Stroke = Brushes.AliceBlue; }
						if ((courbes.Last().getaltitude() > 100) && (courbes.Last().getaltitude() <= 200)) { newLine.Stroke = Brushes.Green; }
						if ((courbes.Last().getaltitude() > 200) && (courbes.Last().getaltitude() <= 400)) { newLine.Stroke = Brushes.GreenYellow; }
						if ((courbes.Last().getaltitude() > 400) && (courbes.Last().getaltitude() <= 600)) { newLine.Stroke = Brushes.Yellow; }
						if ((courbes.Last().getaltitude() > 600) && (courbes.Last().getaltitude() <= 800)) { newLine.Stroke = Brushes.Orange; }
						if ((courbes.Last().getaltitude() > 800) && (courbes.Last().getaltitude() <= 1000)) { newLine.Stroke = Brushes.OrangeRed; }
						if (courbes.Last().getaltitude() > 1000) { newLine.Stroke = Brushes.Red; }

						courbes.Last().setlignes(newLine, v); v++;
						newLine.StrokeThickness = 2;
						cnv.Children.Add(newLine);
						mousePoint1 = e.GetPosition(this);
						Points.Add(mousePoint1);
						courbes.Last().Addpoints(mousePoint1);
						Ellipse ell = new Ellipse();
						ell.Width = 7;
						ell.Height = 7;
						ell.Fill = Brushes.SkyBlue;
						ell.Stroke = Brushes.Transparent;
						Canvas.SetLeft(ell, mousePoint1.X - 5 / 2);
						Canvas.SetTop(ell, mousePoint1.Y - 10 / 2);
						shownPts.Add(ell);
						courbes.Last().setshownPts(ell, u); u++;
						cnv.Children.Add(ell);
						if (k == 1)
						{
							this.lineStarted = false; k = 0;
						}
					}
					this.InvalidateVisual();
				}
			}
			else
			{
				if (Fin < 2)
				{
					base.OnMouseDown(e);
					if (!lineStarted0)
					{
						lineStarted0 = true;
						mousePoint = e.GetPosition(this);
					}
					else
					{
						Point mousePoint2 = e.GetPosition(this);
						Line newLine0 = new Line { X1 = mousePoint.X, Y1 = mousePoint.Y, X2 = mousePoint2.X, Y2 = mousePoint2.Y };
						newLine0.Stroke = Brushes.Black;
						newLine0.StrokeThickness = 2;
						cnv.Children.Add(newLine0);
						this.lineStarted0 = false;
					}

					this.InvalidateVisual();
					Fin++;
				}
			}
		}
		//----------------------------------------------------------------------------------------------------------
		private void Supprimer_Click(object sender, RoutedEventArgs e)
		{
			suppp = 1;
		}
		//-------------------------------------------------------------------------------------------------------------
<<<<<<< HEAD

		//-----------------------------------------------------------------------------------------------------------
	
		//----------------------------------------------------------------------------------------------------------
		private void next_Click(object sender, RoutedEventArgs e)
=======
		List <Point> pointIntersection = new List<Point>();
		List<double> altitudee = new List<double>();
		double alt1 = 0;
		double alt2 = 0;
		double pente;
		

		private void prevBtn_Click(object sender, RoutedEventArgs e)
		{
			var parent = (Grid)this.Parent;
			parent.Children.Clear();
			Parametrage parametre = new Parametrage();
			parent.Children.Add(parametre);
		}

		//-----------------------------------------------------------------------------------------------------------

		protected override void OnRender(DrawingContext dc)
>>>>>>> 9f1a0b9a76b8b8f3123557609c141a9a970e191e
		{
			Ss = 1;
			List<Point> Points1 = new List<Point>();
			int cr = 0,jk1=0;
			int ik4=-1,ik3;
			cnx.Open();
			string readString3 = "select * from Trace  where nom ='"+ nomtr +"'";
			
			SqlCommand readCommand3 = new SqlCommand(readString3, cnx);
			int nbs=1000;

			using (SqlDataReader dataRead3 = readCommand3.ExecuteReader())

			{
<<<<<<< HEAD
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
			for (int ik = 0; ik < courbes.Count; ik++)
			{

				int jk = 0;

				for (int ik2 = 0; ik2 < courbes[ik].nbPoints(); ik2++)
				{

					Points1.Add(courbes[ik].getpoints(ik2));

				}
				double ab = courbes[ik].getaltitude();
				int ab1 = (int)ab;
				for ( ik3 = ik4+1; ik3 <( ik4+1+courbes[ik].nbPoints()); ik3++)
				{

					double a;
					double b;
					a = Points1[ik3].X;
					b = Points1[ik3].Y;
					cnx.Open();
					SqlCommand cmd = cnx.CreateCommand();
					cmd.CommandType = CommandType.Text;
					int a1 = (int)a;
					int b1 = (int)b;

					cmd.CommandText = "insert into [Point] (x,y,altitude,critere,Id) values ('" + a1 + "','" + b1 + "','" + ab1 + "','" + cr + "','" + nbs + "')";
					cmd.ExecuteNonQuery();
					cnx.Close();
					jk1 = ik3;
					

				}
				ik4 =jk1;
				cr++;

			}
			Ss = 1;
			cnx.Open();
			

						string readString = "select * from Point  where  Id =" +nbs;

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
						double alts1 = (double)int.Parse(alts);
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
			
			tracesegment imp = new tracesegment(kak, courbes12, nbs);
			var parent = (Grid)this.Parent;
			parent.Children.Clear();
			parent.Children.Add(imp);


=======

				if (lineStarted)
				dc.DrawLine(new Pen(Brushes.Black, 5), this.mousePoint1, Mouse.GetPosition(this));
				double x1 = this.mousePoint.X;
				double y1 = this.mousePoint.Y;
				double x2 = Mouse.GetPosition(this).X;
				double y2 = Mouse.GetPosition(this).Y;
				Point start = new Point(x1, y1);
				Point end = new Point(x2, y2);
				segment = new Line { X1 = start.X, Y1 = start.Y, X2 = end.X, Y2 = end.Y };
				Profil p = new Profil(start, end);
				p.Intersection(courbes, out pointIntersection, out altitudee);
				for (int l = 0; l < pointIntersection.Count; l++)
				{
					Console.WriteLine(pointIntersection[l]);
				}
				int o = 0; Point pp;
				foreach (Courbe item in courbes)
				{
					o++;
					for (int i = 0; i < item.nbPoints() - 1; i++)
					{
						pp = item.getpoints(i);
					}
				}
				if (pointIntersection.Count > 1)
				pente = p.Calcul_P(pointIntersection[0].X, pointIntersection[0].Y, pointIntersection[pointIntersection.Count - 1].X, pointIntersection[pointIntersection.Count - 1].Y, altitudee[0], altitudee[altitudee.Count - 1], 60);
			}
		}
		private void next_Click1(object sender, RoutedEventArgs e)
		{
			trace_profil pr = new trace_profil(Parametrage.altmin, Parametrage.altmax, Parametrage.echell, pente);
			pr.plotData(Parametrage.echell, Parametrage.altmin, Parametrage.altmax, Parametrage.equidis, pointIntersection, altitudee);
			Grids.Children.Clear();
			Grids.Children.Add(pr);
		}
		//----------------------------------------------------------------------------------------------------------
		private void next_Click(object sender, RoutedEventArgs e)
		{
			Ss = 1;
			
		}
	}
}