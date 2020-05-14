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
    public partial class tracesegment : UserControl
    {
        int mika1;
        double alt1 = 0;
        double alt2 = 0;
        double pente;
        List<Point> Points = new List<Point>();
        List<Courbe> courbes = new List<Courbe>();
        bool lineStarted = false;
        bool lineStarted0 = false;
        bool dep = false;
        bool lineStarted1 = false;
        Point mousePoint1;
        Point mousePoint;
        Point mousePoint11;
        Line segment;
        List<Ellipse> shownPts = new List<Ellipse>();
        List<Line> lignes = new List<Line>();
        MouseButtonEventArgs m;  
        List<Point> pointIntersection = new List<Point>();
        List<double> altitudee = new List<double>();
        public List<Courbe> courbes15 = new List<Courbe>();
        SqlConnection cnx = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\BDDtopo.mdf;Integrated Security=True");
       
        public tracesegment(BitmapImage userImage1, List<Courbe> courbes3, int mika)
        {
            InitializeComponent();
            img1.Source = userImage1;
            courbes15 = courbes3;
            recuperation(courbes3);
            mika1 = mika;
        }    
        //----------------------------------------------------------------------------------------------------------
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (!lineStarted1) 
            {
                lineStarted1 = true;
                mousePoint11 = e.GetPosition(this);
                Ellipse ell = new Ellipse();
                ell.Width = 7;
                ell.Height = 7;
                ell.Fill = Brushes.SkyBlue;
                ell.Stroke = Brushes.Transparent;
                Canvas.SetLeft(ell, mousePoint11.X - 5 / 2);
                Canvas.SetTop(ell, mousePoint11.Y - 10 / 2);               
                cnv2.Children.Add(ell);
            }
            else 
            {
                Point mousePoint22 = e.GetPosition(this);
                Ellipse ell = new Ellipse();
                ell.Width = 7;
                ell.Height = 7;
                ell.Fill = Brushes.SkyBlue;
                ell.Stroke = Brushes.Transparent;
                Canvas.SetLeft(ell, mousePoint22.X - 5 / 2);
                Canvas.SetTop(ell, mousePoint22.Y - 10 / 2);           
                cnv2.Children.Add(ell);
                Line newLine = new Line { X1 = mousePoint11.X, Y1 = mousePoint11.Y, X2 = mousePoint22.X, Y2 = mousePoint22.Y };
                newLine.Stroke = Brushes.Black;
                newLine.StrokeThickness = 2;
                cnv2.Children.Add(newLine);
                double x1 = mousePoint11.X;
                double y1 = mousePoint11.Y;
                double x2 = mousePoint22.X;
                double y2 = mousePoint22.Y;
                Point start = new Point(x1, y1);
                Point end = new Point(x2, y2);
                segment = new Line { X1 = start.X, Y1 = start.Y, X2 = end.X, Y2 = end.Y };
                Profil p = new Profil(start, end);
                p.Intersection(courbes15, out pointIntersection, out altitudee);
                int o = 0; Point pp;
                foreach (Courbe item in courbes15)
                {
                    o++;
                    for (int i = 0; i < item.nbPoints() - 1; i++)
                    {
                        pp = item.getpoints(i);
                    }
                }
                cnx.Open();
                string readString9 = "select * from Trace  where Id =" + mika1;

                SqlCommand readCommand9 = new SqlCommand(readString9, cnx);
                int echellenormal = 0;
                int echellecarte = 0;
                using (SqlDataReader dataRead9 = readCommand9.ExecuteReader())
                {
                    if (dataRead9 != null)
                    {
                        while (dataRead9.Read())
                        {
                            string echnormal = dataRead9["echelle"].ToString();
                            echellenormal = int.Parse(echnormal);
                            string echlcarte = dataRead9["echellecarte"].ToString();
                            echellecarte = int.Parse(echlcarte);
                        }
                    }
                }
                cnx.Close();
                if (pointIntersection.Count > 1)
                    pente = p.Calcul_P(pointIntersection[0].X, pointIntersection[0].Y, pointIntersection[pointIntersection.Count - 1].X, pointIntersection[pointIntersection.Count - 1].Y, altitudee[0], altitudee[altitudee.Count - 1], echellecarte, echellenormal);
                this.lineStarted1 = false;
            }
            this.InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (lineStarted1) this.InvalidateVisual();
        }
        protected override void OnRender(DrawingContext dc)
        {
            if (lineStarted)
                dc.DrawLine(new Pen(Brushes.Black, 5), this.mousePoint11, Mouse.GetPosition(this));
          
        }
        private void next_Click1(object sender, RoutedEventArgs e)
        {
            cnx.Open();
            string readString3 = "select * from Trace  where Id =" + mika1;
            SqlCommand readCommand3 = new SqlCommand(readString3, cnx);
            int nbs = 1000;
            double min1 = 0;
            double max1 = 0;
            double equi1 = 0;
            double ech1 = 0;
            double ech1carte = 0;
            using (SqlDataReader dataRead3 = readCommand3.ExecuteReader())
            {
                if (dataRead3 != null)
                {
                    while (dataRead3.Read())
                    {
                        string mins = dataRead3["min"].ToString();
                        min1 = double.Parse(mins);
                        string maxs = dataRead3["max"].ToString();
                        max1 = double.Parse(maxs);
                        string equi = dataRead3["equidistance"].ToString();
                        equi1 = (double)int.Parse(equi);
                        string ech = dataRead3["echelle"].ToString();
                        ech1 = double.Parse(ech);
                        string echcarte = dataRead3["echellecarte"].ToString();
                        ech1carte = double.Parse(echcarte);
                    }
                }
            }
            cnx.Close();
            trace_profil pr = new trace_profil(min1, max1, ech1carte, ech1, pente);
            pr.plotData(ech1carte, ech1, min1, max1, equi1, pointIntersection, altitudee);
            Grids.Children.Clear();
            Grids.Children.Add(pr);
        }

        public void recuperation(List<Courbe> courbes)
        {
            for (int j = 0; j < courbes.Count(); j++)
            {
                Point mousePoint = new Point();
                Point mousePoint1 = new Point();
                Courbe c = new Courbe();
                c = courbes[j];
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
                cnv2.Children.Add(ell);
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
                    cnv2.Children.Add(newLine);
                    this.InvalidateVisual();
                    mousePoint1 = mousePoint2;
                    nbpoint++;
                    Ellipse ell2 = new Ellipse();
                    ell2.Width = 7;
                    ell2.Height = 7;
                    ell2.Fill = Brushes.SkyBlue;
                    ell2.Stroke = Brushes.Transparent;
                    Canvas.SetLeft(ell2, mousePoint1.X - 5 / 2);
                    Canvas.SetTop(ell2, mousePoint1.Y - 10 / 2);
                    cnv2.Children.Add(ell2);
                }
                this.InvalidateVisual();
            }
        }
    }
}
