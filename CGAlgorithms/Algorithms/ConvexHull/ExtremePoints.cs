using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            // Check Daplicate
            List<Point> uniquePoints = new List<Point>();
            bool isDuplicated = false;
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (points[j].Equals(points[i]))
                    {
                        isDuplicated = true;
                        break;
                    }
                }
                if (!isDuplicated)
                    uniquePoints.Add(points[i]);
            }
            points.Clear();
            points = uniquePoints;

            bool[] inSide = new bool[points.Count];
            for (int i = 0; i < points.Count; i++)
                inSide[i] = false;

            for (int j = 0; j < points.Count; j++)
            {
                if (inSide[j])
                    continue;
                for (int k = 0; k < points.Count; k++)
                {
                    if (inSide[k] || k == j)
                        continue;
                    for (int l = 0; l < points.Count; l++)
                    {
                        if (inSide[l] || k == l || l == j || HelperMethods.CheckTurn(new Line(points[j], points[k]), points[l]) == Enums.TurnType.Colinear)
                            continue;
                        for (int index = 0; index < points.Count; index++)
                        {

                            if (inSide[index] || k == index || index == j || index == l)
                                continue;
                            if (HelperMethods.PointInTriangle(points[index], points[j], points[k], points[l]) != Enums.PointInPolygon.Outside)
                            {
                                inSide[index] = true;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < points.Count; i++)
            {
                if (!inSide[i])
                    outPoints.Add(points[i]);
            }

        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}
