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
        Line segment;
        List<Courbe> courbeaniveau;
        public Profil(Line l, List<Courbe> c) { this.segment = l; this.courbeaniveau = c; }
        //public void DessinerAxes(min , max, echelle) { }
        //public real CalculPente(segment) {}
        public void Intersection(out double alt1, out double alt2, out List<Point> point_Intersection)
        {
            point_Intersection = new List<Point>();
            List<double> altitude = new List<double>();
            alt1 = 0;
            alt2 = 0;
            foreach (Courbe item in courbeaniveau)
            {

                foreach (Line li in item.Lignes())
                {
                    bool intersect = IntersectWithSegementOfLine(segment, li, out Point intersectionPoint);
                    if (intersect)
                    {
                        point_Intersection.Add(intersectionPoint);
                        altitude.Add(item.getaltitude());
                    }
                }
            }
            if (altitude.Count() != 0) { alt1 = altitude[1]; alt2 = altitude.Last(); }


        }

        public bool IntersectWithSegementOfLine(Line line, Line otherLine, out Point intersectionPoint)
        {
            bool hasIntersection = IntersectsWithLine(line, otherLine, out intersectionPoint);
            Point start = new Point(line.X1, line.Y1);
            Point end = new Point(line.X2, line.Y2);
            Point start2 = new Point(otherLine.X1, line.Y1);
            Point end2 = new Point(otherLine.X2, line.Y2);
            if (hasIntersection)

                return IsBetweenTwoPoints(start, end, intersectionPoint) && IsBetweenTwoPoints(start2, end2, intersectionPoint);
            ;

            return false;
        }

        public bool IntersectsWithLine(Line line, Line otherLine, out Point intersectionPoint)
        {
            intersectionPoint = new Point(0, 0);
            if (isVertical(line) && isVertical(otherLine))
                return false;
            if (isVertical(line) || isVertical(otherLine))
            {
                intersectionPoint = GetIntersectionPointIfOneIsVertical(otherLine, line);
                return true;
            }

            double A = (line.Y2 - line.Y1) / (line.X2 - line.X1);
            double Aother = (otherLine.Y2 - otherLine.Y1) / (otherLine.X2 - otherLine.X1);
            double C = line.Y1 - A * line.X1;
            double Cother = otherLine.Y1 - Aother * otherLine.X1;
            double delta = Aother - A;

            bool hasIntersection = Math.Abs(delta) > 0.0001f;
            if (hasIntersection)
            {
                double x = (C - Cother) / delta;
                double y = A * x + C;
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
                }
                else if (start.Y >= end.Y)
                {
                    if ((start.X <= intersect.X) && (intersect.X <= end.X) && (intersect.Y >= end.Y)
                        && (start.Y >= intersect.Y)) return true;
                }
            }
            else if (start.X >= end.X)
            {
                if (start.Y <= end.Y)
                {
                    if ((start.X >= intersect.X) && (intersect.X >= end.X) && (intersect.Y <= end.Y)
                        && (start.Y <= intersect.Y)) return true;
                }
                else if (start.Y >= end.Y)
                {
                    if ((start.X >= intersect.X) && (intersect.X >= end.X) && (intersect.Y >= end.Y)
                       && (start.Y >= intersect.Y)) return true;
                }
            }
            return false;
        }

    }
}
