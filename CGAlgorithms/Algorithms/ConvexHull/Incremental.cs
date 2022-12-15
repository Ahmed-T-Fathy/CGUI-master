using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {
        public List<Point> get_Supporting_lines(Point point, List<Line> lines, List<Point> l, int polygonturn)
        {
            Point point1 = null;
            Point point2 = null;

            foreach (Point p in l)
            {
                // catch after and before points with respect to p 
                Point back = null;
                Point front = null;
                foreach (Line line in lines)
                {
                    if (line.Start == p)
                        front = line.End;
                    else if (line.End == p)
                        back = line.Start;
                }
                // check if p makes support line
                if (HelperMethods.CheckTurn(new Line(back, p), point) == Enums.TurnType.Left && HelperMethods.CheckTurn(new Line(p, front), point) == Enums.TurnType.Right)
                {
                    if (point1 == null)
                        point1 = p;
                    else if (point2 == null)
                    {
                        point2 = p;
                        break;
                    }

                }
                else if (HelperMethods.CheckTurn(new Line(back, p), point) == Enums.TurnType.Right && HelperMethods.CheckTurn(new Line(p, front), point) == Enums.TurnType.Left)
                {
                    if (point1 == null)
                        point1 = p;
                    else if (point2 == null)
                    {
                        point2 = p;
                        break;
                    }

                }
                else if (HelperMethods.CheckTurn(new Line(back, p),point ) == Enums.TurnType.Colinear && HelperMethods.CheckTurn(new Line(p, front), point) == Enums.TurnType.Left)
                {
                    if (point1 == null)
                        point1 = p;
                    else if (point2 == null)
                    {
                        point2 = p;
                        break;
                    }

                }
                else if (HelperMethods.CheckTurn(new Line(back,p), point ) == Enums.TurnType.Left && HelperMethods.CheckTurn(new Line(p, front), point) == Enums.TurnType.Colinear)
                {
                    if (point1 == null)
                        point1 = p;
                    else if (point2 == null)
                    {
                        point2 = p;
                        break;
                    }

                }
                else if (HelperMethods.CheckTurn(new Line(p, point), front) == Enums.TurnType.Colinear && HelperMethods.CheckTurn(new Line(back, point), p) == Enums.TurnType.Colinear)
                {
                    if (point.X == front.X)
                    {
                        if (point.Y < front.Y && point.Y < back.Y)
                        {
                            continue;
                        }
                        if (point.Y > front.Y && point.Y > back.Y)
                        {
                            continue;
                        }
                    }
                    if (point.Y == front.Y)
                    {
                        if (point.X < front.X && point.X < back.X)
                        {
                            continue;
                        }
                        if (point.X > front.X && point.X > back.X)
                        {
                            continue;
                        }
                    }
                    if(HelperMethods.GetDistance(back, point) > HelperMethods.GetDistance(back, front))
                        continue;
                    return new List<Point>();
                    //if (polygonturn == -1)
                    //{
                    //    if (front.X < point.X || front.Y > point.Y)
                    //    {
                    //        if (p.X > point.X || p.Y < point.Y)
                    //        {
                    //            return new List<Point>();
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    if (front.X > point.X || front.Y < point.Y)
                    //    {
                    //        if (p.X < point.X || p.Y > point.Y)
                    //        {
                    //            return new List<Point>();
                    //        }
                    //    }
                    //}
                   
                }
               
            }

            List<Point> supportPoints = new List<Point>();
            if (point1 != null && point2 != null)
            {
                supportPoints.Add(point1);
                supportPoints.Add(point2);
            }

            return supportPoints;
        }
        public void update_l_and_lines(Point point, List<Point> twoPoints, ref List<Point> l, ref List<Line> lines,int polygonturn)
        {

            //which have less y
            int Sindex = 1;


            if (HelperMethods.CheckTurn(new Line(twoPoints[0], point), twoPoints[1]) == Enums.TurnType.Left && polygonturn == -1)
            {
                Sindex =0;
            }
            else if (HelperMethods.CheckTurn(new Line(twoPoints[0], point), twoPoints[1]) == Enums.TurnType.Right && polygonturn == 1)
            {
                Sindex = 0;
            }
            int Eindex = (Sindex == 0) ? 1 : 0;

            //if collinear
            if (point.Y == twoPoints[0].Y && point.Y == twoPoints[1].Y)
            {
                return;
            }
            else if (point.X == twoPoints[0].X && point.X == twoPoints[1].X)
            {
                return;
            }

            // if there is no points between two points
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Start == twoPoints[Sindex] && lines[i].End == twoPoints[Eindex])
                {
                    lines.Remove(lines[i]);
                    lines.Add(new Line(twoPoints[Sindex], point));
                    lines.Add(new Line(point, twoPoints[Eindex]));
                    l.Add(point);
                    return;
                }
            }

            // if there is  points between two points

            Point temp = twoPoints[Sindex];
            List<Point> pointstoremove = new List<Point>();
            List<Line> linestoremove = new List<Line>();
            for (int i = 0; i < lines.Count; i++)
            {
                if (temp == twoPoints[Eindex])
                    break;
                if (lines[i].Start == temp)
                {
                    if (lines[i].End != twoPoints[Eindex])
                    {
                        pointstoremove.Add(lines[i].End);
                        temp = lines[i].End;
                    }

                }
            }
            foreach (Line line in lines)
            {
                foreach (Point p in pointstoremove)
                {
                    if (line.Start == p || line.End == p)
                    {
                        linestoremove.Add(line);
                    }
                }
            }
            foreach (Point p in pointstoremove)
            {
                l.Remove(p);
            }
            foreach (Line line in linestoremove)
            {
                if (lines.Contains(line))
                    lines.Remove(line);
            }

            lines.Add(new Line(twoPoints[Sindex], point));
            lines.Add(new Line(point, twoPoints[Eindex]));
            l.Add(point);
        
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<Point> Py = points.OrderByDescending(p => p.Y).ToList();
            List<Point> Px = Py.OrderByDescending(p => p.X).ToList();
            //List<Point> Px2 = points.OrderByDescending(p => p.X).ToList();
            //Boolean x=true;
            //for(int i=0;i<Px.Count;i++)
            //{
            //    if (Px[i]!=Px2[i])
            //        x=false;
            //}
            List<Point> l = new List<Point>();
            if (points.Count < 3)
            {
                outPoints = points;
                return;
            }
            for (int i = Px.Count - 1; i >= Px.Count - 3; i--)
            {
                l.Add(Px[i]);
            }
            for (int i = 0; i < l.Count; i++)
            {
                if (i == l.Count - 1)
                {
                    lines.Add(new Line(l[i], l[0]));
                    break;
                }
                lines.Add(new Line(l[i], l[i + 1]));
            }

            int polygonturn = -1;

            if (HelperMethods.CheckTurn(lines[0], l[2]) == Enums.TurnType.Right)
                polygonturn = 1;



            //Px.Add(new Point(1,8));
            foreach (Point point in Px)
            {
                if (!l.Contains(point))
                    if (!HelperMethods.InsidePolygon(lines, point))
                    {
                        //get the two points that join to point add them to the "two_points" list
                        List<Point> two_points = new List<Point>();
                        //for (int i = l.Count - 1; i > l.Count ; i--)
                        //{
                        //}
                        two_points = get_Supporting_lines(point, lines, l, polygonturn);
                        if (two_points.Count == 2)
                        {
                            //here update l and lines
                            update_l_and_lines(point, two_points, ref l, ref lines,polygonturn);
                        }


                    }


            }



            List<Line> linestoremove = new List<Line>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines.Count; j++)
                {
                    if (lines[i].End == lines[j].Start)
                    {
                        if (HelperMethods.CheckTurn(lines[i], lines[j].End) == Enums.TurnType.Colinear)
                        {
                            if (l.Contains(lines[i].End))
                                l.Remove(lines[i].End);
                            if (!linestoremove.Contains(lines[i]))
                                linestoremove.Add(lines[i]);
                            if (!linestoremove.Contains(lines[j]))
                                linestoremove.Add(lines[j]);
                            if (!lines.Contains(new Line(lines[i].Start, lines[j].End)))
                                lines.Add(new Line(lines[i].Start, lines[j].End));
                        }
                        break;
                    }

                }
            }
            foreach (Line line in linestoremove)
            {
                if (lines.Contains(line))
                    lines.Remove(line);
            }

            outPoints = l;
            Console.WriteLine(l);
        }

        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }
    }
}
