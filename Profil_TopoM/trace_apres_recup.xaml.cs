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
    /// Logique d'interaction pour tracesegment.xaml
    /// </summary>
    public partial class trace_apres_recup : UserControl
    {
		String nomtr;
		int Fin = 0;
		int Ss = 0;
		int k = 0;
		int suppp = 0;
		int sup = 0;
		int mika;
		int s = 0;
		int u = 0; 
		int v = 0;
		double min1 = 0;
		double max1 = 0;
		double equi1 = 0;
		double ech1 = 0;
		bool dep = false;
		bool lineStarted0 = false;
		bool lineStarted = false;
		BitmapImage kak;
		Point mousePoint1;
		Point mousePoint;
		Courbe c;
		Trace trac = new Trace();
		public List<Courbe> courbes33 = new List<Courbe>();
		public List<Courbe> courbes1 = new List<Courbe>();
		List<Point> Points = new List<Point>();
		List<Courbe> courbes = new List<Courbe>();
		List<Ellipse> shownPts = new List<Ellipse>();
		List<Line> lignes = new List<Line>();
		MouseButtonEventArgs m;
		
		SqlConnection cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");

		public trace_apres_recup(BitmapImage userImage1, List<Courbe> courbes3, int nbo)
		{
			InitializeComponent();
			img.Source = userImage1;
			courbes = courbes3;
			recuperation(courbes);
			mika = nbo;
			kak = userImage1;
			cnx.Open();
			string readString37 = "select * from Trace  where Id =" + mika;
			SqlCommand readCommand37 = new SqlCommand(readString37, cnx);
			int nbs = 1000;
			using (SqlDataReader dataRead37 = readCommand37.ExecuteReader())
			{
				if (dataRead37 != null)
				{
					while (dataRead37.Read())
					{
						string mins = dataRead37["min"].ToString();
						min1 = Double.Parse(mins);
						string maxs = dataRead37["max"].ToString();
						max1 = Double.Parse(maxs);
						string equi = dataRead37["equidistance"].ToString();
						equi1 = (double)int.Parse(equi);
					}
				}
			}
			cnx.Close();
		}
		
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
					Console.WriteLine(courbes[l].getpoints(trouve).X + " " + courbes[l].getpoints(trouve).Y);
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
						courbes[l].setlignes(newLine, trouve - 1);
					}
					if ((trouve + 1) < courbes[l].nbPoints())
					{
						line = new Line { X1 = courbes[l].getpoints(trouve + 1).X, Y1 = courbes[l].getpoints(trouve + 1).Y, X2 = p.X, Y2 = p.Y };
						cnv.Children.Remove(courbes[l].getlignes(trouve));
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
						courbes[l].setlignes(line, trouve);
					}
					if (k == 1) { this.lineStarted = false; k = 0; }
				}
				this.InvalidateVisual();
			}
			catch (Exception exp) { MessageBox.Show("Veuillez dessiner au moins une ligne afin de pouvoir modifier des points ", "Erreur"); }
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

		//------------------------------------------------------------------------------------------------------
		private void Arret_Click(object sender, RoutedEventArgs e)
		{
			lineStarted = false;
			k = 0;
		}
		
		private void cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
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

						courbes[l].remplacer(newLine, trouve - 1);
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
						courbes.Last().setpoints(mousePoint1, s); s++;
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
						double altit = 0;
						while (ex == 0)
						{
							try
							{
								do
								{
									Alt alt = new Alt();
									alt.ShowDialog();
									altit = Double.Parse(alt.altitude.Text);
									if (altit >= min1 && altit <= max1)
									{
										if (altit % equi1 == 0 || altit == max1 || altit == min1)
										{
											courbes.Last().setaltitude(altit);
											ex = 1;
										}
										else
										{
											MessageBox.Show($"L'altitude doit être multiplie de l'équidistance :{equi1} ");
										}
									}
									else
									{
										MessageBox.Show($"L'altitude doit être comprise entre {min1} et {max1}");
									}
								} while (altit < min1 || altit > max1);
							}
							catch (Exception exp)
							{
								MessageBox.Show("Veuillez entrer l'altitude de la courbe", "Erreur");
								ex = 0;
							}
						}
					}
					else
					{
						Point mousePoint2 = e.GetPosition(this);
						if (sup == 1) { mousePoint1 = courbes.Last().getpoints(courbes.Last().nbPoints() - 1); sup = 0; }
						Line newLine = new Line { X1 = mousePoint1.X, Y1 = mousePoint1.Y, X2 = mousePoint2.X, Y2 = mousePoint2.Y };
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
						newLine.StrokeThickness = 2;
						lignes.Add(newLine);
						courbes.Last().setlignes(newLine, v); v++;
						cnv.Children.Add(newLine);
						mousePoint1 = e.GetPosition(this);
						Points.Add(mousePoint1);
						courbes.Last().setpoints(mousePoint1, s); s++;
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
		//---------------------------------------------------------------------------------

		public void recuperation(List<Courbe> courbes10)
		{
			for (int j = 0; j < courbes10.Count(); j++)
			{
				List<Ellipse> show = new List<Ellipse>();
				List<Line> lig = new List<Line>();
				Point mousePoint = new Point();
				Point mousePoint1 = new Point();
				Courbe c = new Courbe();
				c = courbes10[j];
				double altit = c.getaltitude();
				int nbpoint = 0;
				mousePoint1 = c.getpoints(nbpoint);
				nbpoint++;
				Ellipse ell = new Ellipse();
				ell.Width = 7;
				ell.Height = 7;
				ell.Fill = Brushes.SkyBlue;
				ell.Stroke = Brushes.Transparent;
				Canvas.SetLeft(ell, mousePoint1.X - 5 / 2);
				Canvas.SetTop(ell, mousePoint1.Y - 10 / 2);
				cnv.Children.Add(ell);
				show.Add(ell);
				this.InvalidateVisual();
				while (nbpoint < c.nbPoints())
				{
					String ag = mousePoint1.X.ToString();
					Point mousePoint2 = c.getpoints(nbpoint);
					Line newLine = new Line { X1 = mousePoint1.X, Y1 = mousePoint1.Y, X2 = mousePoint2.X, Y2 = mousePoint2.Y };
					ToolTip t = new ToolTip();
					t.Content = c.getaltitude();
					t.Background = Brushes.AliceBlue;
					newLine.ToolTip = t;
					if ((altit <= 0)) { newLine.Stroke = Brushes.Blue; }
					if ((altit > 0) && (altit <= 50)) { newLine.Stroke = Brushes.SkyBlue; }
					if ((altit > 50) && (altit <= 100)) { newLine.Stroke = Brushes.AliceBlue; }
					if ((altit > 100) && (altit <= 200)) { newLine.Stroke = Brushes.Green; }
					if ((altit > 200) && (altit <= 400)) { newLine.Stroke = Brushes.GreenYellow; }
					if ((altit > 400) && (altit <= 600)) { newLine.Stroke = Brushes.Yellow; }
					if ((altit > 600) && (altit <= 800)) { newLine.Stroke = Brushes.Orange; }
					if ((altit > 800) && (altit <= 1000)) { newLine.Stroke = Brushes.OrangeRed; }
					if (altit > 1000) { newLine.Stroke = Brushes.Red; }
					newLine.StrokeThickness = 2;
					cnv.Children.Add(newLine);
					lig.Add(newLine);
					courbes10[j].lignes = lig;
					mousePoint1 = mousePoint2;
					nbpoint++;
					Ellipse ell2 = new Ellipse();
					ell2.Width = 7;
					ell2.Height = 7;
					ell2.Fill = Brushes.SkyBlue;
					ell2.Stroke = Brushes.Transparent;
					Canvas.SetLeft(ell2, mousePoint1.X - 5 / 2);
					Canvas.SetTop(ell2, mousePoint1.Y - 10 / 2);
					cnv.Children.Add(ell2);
					show.Add(ell2);
					courbes10[j].shownPts = show;
				}
				this.InvalidateVisual();
			}
		}

		private void next_Click(object sender, RoutedEventArgs e)
		{
			cnx.Open();
			String date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
			string readString35 = "UPDATE Trace SET  modification='" + date + "' where Id =" + mika;
			SqlCommand readCommand35 = new SqlCommand(readString35, cnx);
			readCommand35.ExecuteNonQuery();
			cnx.Close();
			List<Point> Points1 = new List<Point>();
			int cr = 0, jk1 = 0;
			int ik4 = -1, ik3;
			cnx.Open();
			string readString3 = " DELETE FROM Point  WHERE Id =" + mika;
			SqlCommand readCommand3 = new SqlCommand(readString3, cnx);
			readCommand3.ExecuteNonQuery();
			int nbs = 1000;
			cnx.Close();
			for (int ik = 0; ik < courbes.Count; ik++)
			{
				int jk = 0;
				for (int ik2 = 0; ik2 < courbes[ik].nbPoints(); ik2++)
				{
					Points1.Add(courbes[ik].getpoints(ik2));
				}
				double ab = courbes[ik].getaltitude();
				String ab1 = ab.ToString();
				for (ik3 = ik4 + 1; ik3 < (ik4 + 1 + courbes[ik].nbPoints()); ik3++)
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
					cmd.CommandText = "insert into [Point] (x,y,altitude,critere,Id) values ('" + a1 + "','" + b1 + "','" + ab1 + "','" + cr + "','" + mika + "')";
					cmd.ExecuteNonQuery();
					cnx.Close();
					jk1 = ik3;
				}
				ik4 = jk1;
				cr++;
			}
			Ss = 1;
			cnx.Open();
			string readString = "select * from Point  where  Id =" + mika;
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
			tracesegment imp = new tracesegment(kak, courbes12, mika);
			var parent = (Grid)this.Parent;
			parent.Children.Clear();
			parent.Children.Add(imp);
		}
	}
}
