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
using OxyPlot;
using OxyPlot.Series;
namespace Profil_TopoM
{
    public partial class trace_profil : UserControl
    {
        public trace_profil(double altmin, double altmax, double echelle, double pente)
        {
            InitializeComponent();
            T.Text = Convert.ToString(altmin);
            T1.Text = Convert.ToString(altmax);
            T2.Text = Convert.ToString(echelle);
            T3.Text = Convert.ToString(pente);
        }
        public void plotData(double echelle, double altmin, double altmax, double equidist, List<Point> points_intersections, List<double> altitude)
        {
            var model = new PlotModel();
            LineSeries linePoints = new LineSeries()
            {
                StrokeThickness = 2,
                MarkerSize = 1.5,
                Color = OxyColors.Black,
                MarkerType = MarkerType.Circle,
                MarkerStroke = OxyColors.OrangeRed,
                Background = OxyColors.WhiteSmoke,
                MarkerStrokeThickness = 3,
            };
            int i = 0;
            while (i < points_intersections.Count)
            {
                linePoints.Points.Add(new DataPoint(points_intersections[i].X / echelle, altitude[i]));
                i++;
            }
            var Xaxis = new OxyPlot.Axes.LinearAxis();
            Xaxis.MajorStep = echelle;
            Xaxis.Position = OxyPlot.Axes.AxisPosition.Bottom;
            Xaxis.Title = "Axe des Abscisses";
            model.Axes.Add(Xaxis);
            var Yaxis = new OxyPlot.Axes.LinearAxis();

            Yaxis.Minimum = altmin;
            Yaxis.Maximum = altmax;

            Yaxis.Title = "Axe des altitudes";
            model.Axes.Add(Yaxis);

            // Add Each series to the
            model.Series.Add(linePoints);

            // Add the plot to the window
            plot.Model = model;

        }
        private void precedant_Click(object sender, RoutedEventArgs e)
        {
            /* var parent = (Grid)this.Parent;
             parent.Children.Clear();
             Importation importation = new Importation();
             parent.Children.Add(importation);*/
        }
    }
}
