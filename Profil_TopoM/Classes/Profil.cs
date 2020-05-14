using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;


namespace Profil_TopoM.Classes
{
    class Profil
    {
        Point start, end;

        public Profil(Point start, Point end)
        {
            this.start = start; 
            this.end = end;
        }
 
        public void Intersection(List<Courbe> courbeaniveau, out List<Point> point_Intersection, out List<double> altitude)
        {
            bool trouv;
            int k, a = 0;
            bool intersect;
            Point intersectionPoint;
            point_Intersection = new List<Point>();
            altitude = new List<double>();
            Point p, p1; int o = 0;
            foreach (Courbe item in courbeaniveau)
            {
                o++;
                for (int i = 0; i < item.nbPoints() - 1; i++)
                {
                    p = item.getpoints(i); p1 = item.getpoints(i + 1);
                    intersect = IntersectWithSegementOfLine(start, end, p, p1, out intersectionPoint);
                    if (intersect)
                    {
                        k = 0; trouv = false;
                        if (point_Intersection.Count == 0)
                        {
                            point_Intersection.Add(intersectionPoint);
                            altitude.Add(item.getaltitude());
                        }
                        else
                        {
                            while ((k < point_Intersection.Count) && (trouv == false))
                            {
                                if (point_Intersection[k].X > intersectionPoint.X) { a = k; trouv = true; }
                                else { k++; }
                            }
                            if (trouv == true)
                            {
                                if ((a >= 0) && (a < point_Intersection.Count) && (point_Intersection.Count > 0))
                                {
                                    point_Intersection.Add(point_Intersection[point_Intersection.Count - 1]);
                                    altitude.Add(altitude[altitude.Count - 1]);
                                    k = point_Intersection.Count - 2;
                                    while (k >= a)
                                    {
                                        point_Intersection[k + 1] = point_Intersection[k];
                                        altitude[k + 1] = altitude[k];
                                        k--;
                                    }
                                    point_Intersection[a] = intersectionPoint;
                                    altitude[a] = item.getaltitude();
                                }
                                else
                                {
                                    while (a < point_Intersection.Count) a++;
                                    point_Intersection.Add(intersectionPoint);
                                    altitude.Add(item.getaltitude());
                                }
                            }
                            else
                            {
                                point_Intersection.Add(intersectionPoint);
                                altitude.Add(item.getaltitude());
                            }
                        }
                    }
                }
            }

        }

        public bool IntersectWithSegementOfLine(Point start, Point end, Point p, Point p1, out Point intersectionPoint)
        {
            Line line = new Line { X1 = start.X, Y1 = start.Y, X2 = end.X, Y2 = end.Y };
            Line otherLine = new Line { X1 = p.X, Y1 = p.Y, X2 = p1.X, Y2 = p1.Y };
            bool hasIntersection = IntersectsWithLine(start, end, p, p1, out intersectionPoint);

            if (hasIntersection)
            { 
                if (IsBetweenTwoPoints(start, end, intersectionPoint) && IsBetweenTwoPoints(p, p1, intersectionPoint)) {  return true; }
                else return false;
            }
            else return false;
        }

        public bool IntersectsWithLine(Point start, Point end, Point p, Point p1, out Point intersectionPoint)
        {
            intersectionPoint = new Point(0, 0);
            double a = (end.Y - start.Y) / (end.X - start.X);
            double a1 = (p1.Y - p.Y) / (p1.X - p.X);
            double b = end.Y - a * end.X;
            double b1 = p1.Y - a1 * p1.X;
            double delta = a - a1;

            bool hasIntersection = Math.Abs(delta) > 0;
            if (hasIntersection)
            {
                double x = (b1 - b) / delta;
                double y = a * x + b;
                intersectionPoint = new Point(x, y);
            }
            return hasIntersection;
        }

        public bool isVertical(Line line) { return Math.Abs(line.X2 - line.X1) < 0.00001f; }


        private Point GetIntersectionPointIfOneIsVertical(Line line1, Line line2)
        {
            Line verticalLine, nonVerticalLine;
            if (isVertical(line2)) { verticalLine = line2; } else { verticalLine = line1; }
            if (isVertical(line2)) { nonVerticalLine = line1; } else { nonVerticalLine = line2; }
            double y = (verticalLine.X1 - nonVerticalLine.X1) *
                       (nonVerticalLine.Y2 - nonVerticalLine.Y1) /
                       ((nonVerticalLine.X2 - nonVerticalLine.X1)) +
                       nonVerticalLine.Y1;
            double x = isVertical(line1) ? line1.X1 : line2.X1;
            return new Point(x, y);
        }


        public bool IsBetweenTwoPoints(Point start, Point end, Point intersect)
        {
            if (start.X <= end.X)
            {
                if (start.Y <= end.Y)
                {
                    if ((start.X <= intersect.X) && (intersect.X <= end.X) && (intersect.Y <= end.Y)
                        && (start.Y <= intersect.Y)) return true;
                    else return false;
                }
                else
                {
                    if ((start.X <= intersect.X) && (intersect.X <= end.X) && (intersect.Y >= end.Y)
                        && (start.Y >= intersect.Y)) return true;
                    else return false;
                }
            }
            else
            {
                if (start.Y <= end.Y)
                {
                    if ((start.X >= intersect.X) && (intersect.X >= end.X) && (intersect.Y <= end.Y)
                        && (start.Y <= intersect.Y)) return true;
                    else return false;
                }
                else
                {
                    if ((start.X >= intersect.X) && (intersect.X >= end.X) && (intersect.Y >= end.Y)
                       && (start.Y >= intersect.Y)) return true;
                    else return false;
                }
            }

        }




        public double Calcul_P(double x1, double y1, double x2, double y2, double alt1, double alt2, int ech1, int ech2)
        {
            double distanceF = ((distance(x1, y1, x2, y2)) * ech2) / ech1;// conversion en metres
            double denivele;
            if (alt1 <= alt2)
            {
                denivele = alt2 - alt1;
            }
            else
            {
                denivele = alt1 - alt2;
            }
            double distanceH = Math.Sqrt(Math.Abs(Math.Pow(distanceF, 2) - Math.Pow(denivele, 2)));
            return (denivele * 100) / distanceH;
        }
        public double distance(double x1, double y1, double x2, double y2)
        {
            double p1 = Math.Pow((x2 - x1), 2);
            double p2 = Math.Pow((y2 - y1), 2);
            double result = Math.Sqrt(p1 + p2);
            return result;
        }
       

    }
}