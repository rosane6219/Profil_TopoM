using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Profil_TopoM.Classes
{
    public class Courbe
    {
        public List<Point> Points = new List<Point>();
        public List<Ellipse> shownPts = new List<Ellipse>();
        public List<Line> lignes = new List<Line>();
        public double altitude;
        public Point p;
        public Ellipse ell;
        public Line l;

      
        public Point getpoints(int i) { if (i < Points.Count) return this.Points[i]; else { return p; } }
        public double getaltitude() { return altitude; }
        public int nbPoints() { return Points.Count; }
        public int nblignes() { return lignes.Count; }
        public int nbshownPts() { return shownPts.Count; }
        public Ellipse getshownPts(int i)
        {
            if (i < shownPts.Count) return this.shownPts[i];
            else { return ell; }
        }
        public void remplacer(Line l, int i) { if (i < lignes.Count) lignes[i] = l; }
        public Line getlignes(int i) { if (i < lignes.Count) return this.lignes[i]; else return l; }
        public void setpoints(Point p, int i) { if (i < Points.Count) Points[i] = p; else { Points.Add(p); } }
        public void setshownPts(Ellipse ell, int i) { if (i < shownPts.Count) shownPts[i] = ell; else { shownPts.Add(ell); } }
        public void setlignes(Line l, int i) { if (i < lignes.Count) lignes[i] = l; else { lignes.Add(l); } }
        public void removePoint(Point p) { Points.Remove(p); }
        public void removeligne(Line l) { lignes.Remove(l); }
        public void removeshownPts(Ellipse ell) { shownPts.Remove(ell); }
        public void setaltitude(double alt) { this.altitude = alt; }

    }
}