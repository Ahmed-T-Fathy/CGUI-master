using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<Point> Py = points.OrderByDescending(p => p.Y).ToList();
            //List<Point> Px = points.OrderByDescending(p => p.X).ToList();
            List<Point> V = new List<Point>();
            int x = 100000;
            int y = (int)Py.Last().Y;
            foreach (Point p in Py)
            {
                if (p.Y == y && p.X < x)
                    x = (int)p.X;
            }


            Point first = new Point(x,y);
            V.Add(first);
            Point last = first;
            Point current=new Point(first.X-1, first.Y);  

            int l = 0;
            if (points.Count < 4)
            {
                outPoints = points;

                return;
            }

            foreach (Point p in points){
                l++;
                double Rangle = 0.0;
                Point piontMakeRangle = null;
                double largestDistance = 0;
                foreach (Point point in points)
                {
                    if (V.Contains(p))
                        continue;
                    if (p != current)
                    {
                        Point v1=new Point(current.X-last.X,current.Y-last.Y);
                        Point v2=new Point(point.X-current.X,point.Y- current.Y);
                        double cross=HelperMethods.CrossProduct(v1, v2);
                        double dot=HelperMethods.DotProduct(v1, v2);
                        double angle= Math.Atan2(cross, dot)*180;
                        //double angle = (double)(Math.Atan2(v2.Y - v1.Y, v2.X - p1.X) * 180 / Math.PI);
                        if (angle < 0)
                        {
                            angle= angle + ( Math.PI)*360;
                        }
                        double distance = Math.Sqrt(Math.Pow(current.X - point.X, 2) + Math.Pow(current.Y - point.Y, 2));
                        if (angle > Rangle)
                        {
                            piontMakeRangle = point;
                            Rangle = angle;
                            largestDistance = distance;
                        }
                        else if (angle == Rangle && distance > largestDistance)
                        {
                            largestDistance = distance;
                            piontMakeRangle = point;
                        }

                    }
                }

                if (piontMakeRangle != null && !V.Contains(piontMakeRangle))
                {
                    if (first== piontMakeRangle)
                    {
                        break;
                    }
                    V.Add(piontMakeRangle);
                    last= current;
                    current= piontMakeRangle;
                    
                }
                
            }

            outPoints = V;
        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}
