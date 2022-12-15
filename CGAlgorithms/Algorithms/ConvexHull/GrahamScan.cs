using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {
        public Point FirstPoint;
        public bool pointsCompare(Point first_point, Point second_point)
        {
            Point VECTOR1 = FirstPoint.Vector(first_point);
            Point VECTOR2 = FirstPoint.Vector(second_point);
            //crosproduct between two points
            double limit = HelperMethods.CrossProduct(VECTOR1, VECTOR2);
            if (limit > +1e-6) return true;
            if (limit < -1e-6) return false;
            //find minimum y or minimum x
            bool flag = false;
            if (first_point.Y < second_point.Y || (first_point.Y == second_point.Y && first_point.X < second_point.X))
                flag = true;
            return flag;


        }
        public void Merge_sort(ref List<Point> points)
        {
            List<Point> leftpoints = new List<Point>();
            List<Point> rightpoints = new List<Point>();
            if (points.Count <= 1)
                return;
            int middle = (int)(points.Count / 2);
            for (int counter1 = 0; counter1 < points.Count; counter1++)
            {
                if (counter1 < middle)
                    //first half of points
                    leftpoints.Add(points[counter1]);
                else
                    //second half of points
                    rightpoints.Add(points[counter1]);
            }
            Merge_sort(ref leftpoints);
            Merge_sort(ref rightpoints);
            points.Clear();
            int pointrlt = 0;
            int pointrrt = 0;
            while (pointrlt < leftpoints.Count && pointrrt < rightpoints.Count)
            {
                if (pointsCompare(leftpoints[pointrlt], rightpoints[pointrrt]))
                    points.Add(leftpoints[pointrlt++]);
                else
                    points.Add(rightpoints[pointrrt++]);
            }
            //add sort points in original list of points
            while (pointrlt < leftpoints.Count)
            {
                points.Add(leftpoints[pointrlt]);
                pointrlt++;

            }

            while (pointrrt < rightpoints.Count)
            {

                points.Add(rightpoints[pointrrt]);
                pointrrt++;
            }

        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        { //trtib mn kbir -> so8ir
          //elmfrod anii 3wza as8r Y -> last point int points 
          // points = points.OrderByDescending(p => p.Y).ToList();
          //Point minimumYPOINT = points.Last();
          //  bottom left point
            int COUNTER = (int)points.Count;
            for (int index = 1; index < COUNTER; index++)
            {  //using swap method 3adeee
                if (points[index].Y < points[0].Y || (points[index].Y == points[0].Y && points[index].X < points[0].X))
                {
                    Point temp = points[0];
                    points[0] = points[index];
                    points[index] = temp;
                }
            }
            // sorting angles with FirstPoint counter clock wise
            FirstPoint = points[0];
            Merge_sort(ref points);
            //  removed colinear points
            List<Point> listPoint = new List<Point>();
            listPoint.Add(points[0]);
            for (int index2 = 1; index2 < COUNTER; index2++)
            {


                // for (int index3= index2 + 1; index3 < points.Count; index3++)
                // {
                //  Line linee = new Line(points[0], points[index3]);
                //if (HelperMethods.CheckTurn(FirstPoint.Vector(points[index3]), FirstPoint.Vector(points[index3 + 1])) != Enums.TurnType.Colinear)
                //  break;
                //}
                while (index2 + 1 < COUNTER)
                {

                    if (HelperMethods.CheckTurn(FirstPoint.Vector(points[index2]), FirstPoint.Vector(points[index2 + 1])) != Enums.TurnType.Colinear)
                        break;
                    index2++;
                }


                listPoint.Add(points[index2]);
            }
            // graham scan
            Stack<Point> stackPoints = new Stack<Point>();
            if (listPoint.Count > 3)
            {
                stackPoints.Push(listPoint[0]);
                stackPoints.Push(listPoint[1]);
                stackPoints.Push(listPoint[2]);
                for (int k = 3; k < listPoint.Count; k++)
                {
                    while (true)
                    {
                        Point toppoint = stackPoints.Peek();
                        stackPoints.Pop();
                        Point pretoppoint = stackPoints.Peek();
                        stackPoints.Push(toppoint);

                        if (HelperMethods.CheckTurn(pretoppoint.Vector(toppoint), pretoppoint.Vector(listPoint[k])) == Enums.TurnType.Left)
                            break;
                        stackPoints.Pop();
                    }
                    stackPoints.Push(listPoint[k]);
                }
                while (stackPoints.Count != 0)
                {
                    outPoints.Add(stackPoints.Peek());
                    stackPoints.Pop();
                }
            }
            else
                outPoints = listPoint;
        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}
