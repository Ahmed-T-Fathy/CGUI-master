using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {
        //List<Point> polygons = new List<Point>();
        bool check = true;
        private void min_max(ref List<Point> points, ref List<Point> outPoints)
        {
            //min x,y max X,Y
            int j = 0;
            Point p = null;
            while (j < 4)
            {
                double check_max = -100000;
                double chck_min = 10000;
                for (int i = 0; i < points.Count; i++)
                {//min x
                    if (j == 0 && points[i].X < chck_min)
                    {
                        chck_min = points[i].X;
                        p = points[i];
                    }
                    //min y
                    else if (j == 1 && points[i].Y < chck_min)
                    {
                        chck_min = points[i].Y;
                        p = points[i];
                    }
                    //MAX X
                    else if (j == 2 && points[i].X > check_max)
                    {
                        check_max = points[i].X;
                        p = points[i];
                    }
                    //MAX Y
                    else if (j == 3 && points[i].Y > check_max)
                    {
                        check_max = points[i].Y;
                        p = points[i];
                    }



                }

                if (!outPoints.Contains(p))
                {
                    outPoints.Add(p);
                }

                if (points.Count == 0)
                    break;
                j++;

            }
            for (int i = 0; i < outPoints.Count; i++)
            {

                points.Remove(outPoints[i]);
            }
        }
        //remove point in first convex
        private void removal(ref List<Point> points, List<Point> outPoints)
        {
            for (int i = 0; i < points.Count; i++)
            {
                double remove = 0;
                int count = 0;
                Point p = points[i];
                for (int j = 0; j < outPoints.Count; j++)
                {
                    if (j == outPoints.Count - 1)
                    {
                        remove = (outPoints[j].X * (outPoints[0].Y - p.Y) - outPoints[0].X * (outPoints[j].Y - p.Y) + p.X * (outPoints[j].Y - outPoints[0].Y));

                        if (remove >= 0)
                        {
                            count++;
                        }

                    }
                    else
                    {
                        remove = (outPoints[j].X * (outPoints[j + 1].Y - p.Y) - outPoints[j + 1].X * (outPoints[j].Y - p.Y) + p.X * (outPoints[j].Y - outPoints[j + 1].Y));

                        if (remove >= 0)
                        {
                            count++;
                        }


                    }

                }

                if (count == outPoints.Count)
                {
                    points.Remove(p);
                    i--;
                }
            }
        }
        // add extreme point 
        private void streme(ref List<Point> outPoints, ref List<Point> points)
        {

            Point p = null;
            List<Point> outPoint = new List<Point>();
            int insert = 0;
            for (int i = 0; i < outPoints.Count; i++)
            {
                outPoint.Add(outPoints[i]);
            }
            for (int i = 0; i < outPoint.Count; i++)
            {
                double directions = 0;
                List<Point> x = new List<Point>(); ;
                for (int j = 0; j < points.Count; j++)
                {

                    p = points[j];
                    if (i == outPoint.Count - 1)
                    {
                        directions = (outPoint[i].X * (outPoint[0].Y - p.Y) - outPoint[0].X * (outPoint[i].Y - p.Y) + p.X * (outPoint[i].Y - outPoint[0].Y));
                        if (directions < 0)
                        {
                            x.Add(p);
                        }

                    }
                    else
                    {
                        directions = (outPoint[i].X * (outPoint[i + 1].Y - p.Y) - outPoint[i + 1].X * (outPoint[i].Y - p.Y) + p.X * (outPoint[i].Y - outPoint[i + 1].Y));
                        if (directions < 0)
                        {
                            x.Add(p);
                        }
                    }
                }
                //distance
                if (x.Count == 1)
                {
                    outPoints.Insert(i + 1 + insert, x[0]);
                    points.Remove(x[0]);
                    insert++;

                }
                else if (x.Count > 1)
                {
                    double test = -1000000;
                    for (int j = 0; j < x.Count; j++)
                    {
                        double square_first = Math.Pow((x[j].X - outPoint[i].X), 2) + Math.Pow((x[j].Y - outPoint[i].Y), 2);
                        double square_second = 0;
                        if (i == outPoint.Count - 1)
                            square_second = Math.Pow((x[j].X - outPoint[0].X), 2) + Math.Pow((x[j].Y - outPoint[0].Y), 2);
                        else
                            square_second = Math.Pow((x[j].X - outPoint[i + 1].X), 2) + Math.Pow((x[j].Y - outPoint[i + 1].Y), 2);

                        double distance = Math.Sqrt(square_first) + Math.Sqrt(square_second);

                        if (distance > test)
                        {
                            p = x[j];
                            test = distance;

                        }
                    }
                    outPoints.Insert(i + 1 + insert, p);
                    insert++;

                    points.Remove(p);

                }
            }

        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {


            if (check == true)
            {

                min_max(ref points, ref outPoints);
                check = false;
            }
            else
                streme(ref outPoints, ref points);

            removal(ref points, outPoints);





            if (points.Count != 0)
            {

                Run(points, lines, polygons, ref outPoints, ref outLines, ref outPolygons);
            }









        }

        public override string ToString()
        {
            return "Convex Hull - Divide & Conquer";
        }

    }
}
