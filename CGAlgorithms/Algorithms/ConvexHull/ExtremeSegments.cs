using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            //Special Case - Case 1
            if (points.Count == 1)
            {
                outPoints.Add(points[0]);
                return;
            }

            points.Sort(sortByX);

            bool[] visitedPoints = new bool[points.Count];
            for (int i = 0; i < points.Count; i++)
                visitedPoints[i] = false;


            for (int i = 0; i < points.Count; i++)
            {
                if (i > 0)
                    if (points[i].Equals(points[i - 1]))
                        continue;

                for (int j = i + 1; j < points.Count; j++)
                {
                    if (points[i].Equals(points[j]))
                        continue;

                    Line line = new Line(points[i], points[j]);

                    int noOfRight = 0, noOfLeft = 0;
                    for (int k = 0; k < points.Count; k++)
                    {
                        if (points[k] != points[i] && points[k] != points[j])
                        {
                            Enums.TurnType checkTurn = HelperMethods.CheckTurn(line, points[k]);
                            if (checkTurn == Enums.TurnType.Right)
                                noOfRight += 1;
                            else if (checkTurn == Enums.TurnType.Left)
                                noOfLeft += 1;
                            //Special Cases
                            else if (checkTurn == Enums.TurnType.Colinear)
                            {
                                if (!HelperMethods.PointOnSegment(points[k], points[i], points[j]))
                                {
                                    noOfRight++;
                                    noOfLeft++;
                                    break;
                                }
                            }
                        }
                    }
                    if (noOfLeft == 0 || noOfRight == 0)
                    {
                        if (!visitedPoints[i])
                        {
                            visitedPoints[i] = true;
                            outPoints.Add(points[i]);
                        }
                        if (!visitedPoints[j])
                        {
                            visitedPoints[j] = true;
                            outPoints.Add(points[j]);
                        }
                    }
                }
            }

        }

        public int sortByX(Point a, Point b)
        {
            if (a.X == b.X)
            {
                if (a.Y < b.Y)
                    return -1;
                return 1;
            }
            else if (a.X < b.X)
                return -1;
            return 1;
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}