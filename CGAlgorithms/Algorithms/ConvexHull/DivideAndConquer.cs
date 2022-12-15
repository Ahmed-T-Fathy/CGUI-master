using CGUtilities;
using System.Collections.Generic;
using System.Linq;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {

        public List<Point> MergeFunction(List<Point> FirstConvex, List<Point> SecondConvex)
        {
            int counter_FirstConvex = (int)FirstConvex.Count;

            int counter_SecondConvex = (int)SecondConvex.Count;
            List<Point> FirstCONVEX_NEW = new List<Point>();
            List<Point> SecondCONVEX_NEw = new List<Point>();

            int indexx = 0;
            while (indexx < counter_FirstConvex)
            {
                if (!FirstCONVEX_NEW.Contains(FirstConvex[indexx]))
                {

                    FirstCONVEX_NEW.Add(FirstConvex[indexx]);


                }
                ++indexx;


            }

            int index_2 = 0;
            while (index_2 < counter_SecondConvex)
            {
                if (!SecondCONVEX_NEw.Contains(SecondConvex[index_2]))
                {
                    SecondCONVEX_NEw.Add(SecondConvex[index_2]);

                }

                ++index_2;
            }




            int point1 = 0;
            int point2 = 0;


            // point2  leftmost point of b 
            for (int k = 1; k < SecondCONVEX_NEw.Count; k++)
            {
                if (SecondCONVEX_NEw[k].X < SecondCONVEX_NEw[point2].X)
                {

                    point2 = k;


                }

                else if (SecondCONVEX_NEw[k].X == SecondCONVEX_NEw[point2].X)
                {
                    if (SecondCONVEX_NEw[k].Y < SecondCONVEX_NEw[point2].Y)
                    {

                        point2 = k;
                    }

                }

            }
            // point1  rightmost point of firstconvex
            for (int i = 1; i < FirstCONVEX_NEW.Count; i++)
            {
                if (FirstCONVEX_NEW[i].X == FirstCONVEX_NEW[point1].X)
                {
                    if (FirstCONVEX_NEW[i].Y > FirstCONVEX_NEW[point1].Y)
                        point1 = i;
                }
                else if (FirstCONVEX_NEW[i].X > FirstCONVEX_NEW[point1].X)
                {

                    point1 = i;

                }



            }
            //  upper support line  
            int upperpoint1 = point1;
            int upperpoint2 = point2;
            bool flag1 = false;
            int number = 0;

            while (!flag1)
            {
                flag1 = true;
                while (CGUtilities.HelperMethods.CheckTurn(new Line(SecondCONVEX_NEw[upperpoint2].X,
                           SecondCONVEX_NEw[upperpoint2].Y, FirstCONVEX_NEW[upperpoint1].X, FirstCONVEX_NEW[upperpoint1].Y),
                           FirstCONVEX_NEW[(upperpoint1 + 1) % FirstCONVEX_NEW.Count]) == Enums.TurnType.Right)
                {
                    upperpoint1 = (upperpoint1 + 1) % FirstCONVEX_NEW.Count;
                    flag1 = false;
                }
                if (flag1 == true &&
                    (CGUtilities.HelperMethods.CheckTurn(new Line(SecondCONVEX_NEw[upperpoint2].X, SecondCONVEX_NEw[upperpoint2].Y, FirstCONVEX_NEW[upperpoint1].X, FirstCONVEX_NEW[upperpoint1].Y),
                         FirstCONVEX_NEW[(upperpoint1 + 1) % FirstCONVEX_NEW.Count]) == Enums.TurnType.Colinear))
                {

                    upperpoint1 = (upperpoint1 + 1) % FirstCONVEX_NEW.Count;


                }


                while (CGUtilities.HelperMethods.CheckTurn(new Line(FirstCONVEX_NEW[upperpoint1].X, FirstCONVEX_NEW[upperpoint1].Y, SecondCONVEX_NEw[upperpoint2].X, SecondCONVEX_NEw[upperpoint2].Y), SecondCONVEX_NEw[(SecondCONVEX_NEw.Count + upperpoint2 - 1) % SecondCONVEX_NEw.Count]) == Enums.TurnType.Left)
                {
                    upperpoint2 = (SecondCONVEX_NEw.Count + upperpoint2 - 1) % SecondCONVEX_NEw.Count;
                    flag1 = false;

                }
                if (flag1 == true && (CGUtilities.HelperMethods.CheckTurn(new Line(FirstCONVEX_NEW[upperpoint1].X, FirstCONVEX_NEW[upperpoint1].Y, SecondCONVEX_NEw[upperpoint2].X, SecondCONVEX_NEw[upperpoint2].Y), SecondCONVEX_NEw[(upperpoint2 + SecondCONVEX_NEw.Count - 1) % SecondCONVEX_NEw.Count]) == Enums.TurnType.Colinear))
                    upperpoint2 = (upperpoint2 + SecondCONVEX_NEw.Count - 1) % SecondCONVEX_NEw.Count;


            }

            int lowerpoint1 = point1;
            int lowerpoint2 = point2;
            flag1 = false;



            //lower support line 
            while (!flag1)
            {
                flag1 = true;
                while (CGUtilities.HelperMethods.CheckTurn(new Line(SecondCONVEX_NEw[lowerpoint2].X, SecondCONVEX_NEw[lowerpoint2].Y, FirstCONVEX_NEW[lowerpoint1].X, FirstCONVEX_NEW[lowerpoint1].Y), FirstCONVEX_NEW[(lowerpoint1 + FirstCONVEX_NEW.Count - 1) % FirstCONVEX_NEW.Count]) == Enums.TurnType.Left)
                {
                    lowerpoint1 = (lowerpoint1 + FirstCONVEX_NEW.Count - 1) % FirstCONVEX_NEW.Count;
                    flag1 = false;
                }

                if (flag1 == true &&
                    (CGUtilities.HelperMethods.CheckTurn(new Line(SecondCONVEX_NEw[lowerpoint2].X, SecondCONVEX_NEw[lowerpoint2].Y, FirstCONVEX_NEW[lowerpoint1].X, FirstCONVEX_NEW[lowerpoint1].Y),
                         FirstCONVEX_NEW[(lowerpoint1 + FirstCONVEX_NEW.Count - 1) % FirstCONVEX_NEW.Count]) == Enums.TurnType.Colinear))
                {
                    lowerpoint1 = (lowerpoint1 + FirstCONVEX_NEW.Count - 1) % FirstCONVEX_NEW.Count;

                }

                while (CGUtilities.HelperMethods.CheckTurn(new Line(FirstCONVEX_NEW[lowerpoint1].X, FirstCONVEX_NEW[lowerpoint1].Y, SecondCONVEX_NEw[lowerpoint2].X, SecondCONVEX_NEw[lowerpoint2].Y), SecondCONVEX_NEw[(lowerpoint2 + 1) % SecondCONVEX_NEw.Count]) == Enums.TurnType.Right)
                {
                    lowerpoint2 = (lowerpoint2 + 1) % SecondCONVEX_NEw.Count;
                    flag1 = false;

                }
                if (flag1 == true && (CGUtilities.HelperMethods.CheckTurn(new Line(FirstCONVEX_NEW[lowerpoint1].X, FirstCONVEX_NEW[lowerpoint1].Y, SecondCONVEX_NEw[lowerpoint2].X, SecondCONVEX_NEw[lowerpoint2].Y), SecondCONVEX_NEw[(lowerpoint2 + 1) % SecondCONVEX_NEw.Count]) == Enums.TurnType.Colinear))
                    lowerpoint2 = (lowerpoint2 + 1) % SecondCONVEX_NEw.Count;


            }

            List<Point> FINAL_CONVEX = new List<Point>();


            int index_I = upperpoint1;
            if (!FINAL_CONVEX.Contains(FirstCONVEX_NEW[upperpoint1]))
                FINAL_CONVEX.Add(FirstCONVEX_NEW[upperpoint1]);

            while (index_I != lowerpoint1)
            {
                index_I = (index_I + 1) % FirstCONVEX_NEW.Count;


                if (!FINAL_CONVEX.Contains(FirstCONVEX_NEW[index_I]))
                {
                    FINAL_CONVEX.Add(FirstCONVEX_NEW[index_I]);

                }




            }

            index_I = lowerpoint2;
            if (!FINAL_CONVEX.Contains(SecondCONVEX_NEw[lowerpoint2]))
            {

                FINAL_CONVEX.Add(SecondCONVEX_NEw[lowerpoint2]);

            }


            while (index_I != upperpoint2)
            {
                index_I = (index_I + 1) % SecondCONVEX_NEw.Count;

                if (!FINAL_CONVEX.Contains(SecondCONVEX_NEw[index_I]))
                {
                    FINAL_CONVEX.Add(SecondCONVEX_NEw[index_I]);

                }


            }




            return FINAL_CONVEX;
        }

        public List<Point> DivideFunction(List<Point> Givenpoints)
        {

            int RightGivenpoints = (int)Givenpoints.Count / 2;
            int LeftGivenpoints = (int)Givenpoints.Count;
            List<Point> firstCH = new List<Point>();
            List<Point> secondCH = new List<Point>();
            if (Givenpoints.Count == 1)
            {
                return Givenpoints;
            }

            //divide to find minmum convex hull in left
            for (int counter1 = 0; counter1 < RightGivenpoints; counter1++)
            {
                firstCH.Add(Givenpoints[counter1]);
            }
            //divide to find minmum convex hull in right
            for (int counter2 = RightGivenpoints; counter2 < LeftGivenpoints; counter2++)
            {
                secondCH.Add(Givenpoints[counter2]);
            }
            List<Point> firstCH_new = DivideFunction(firstCH);
            List<Point> secondCH_new = DivideFunction(secondCH);
            List<Point> Final_points = MergeFunction(firstCH_new, secondCH_new);
            return Final_points;
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            //sort the points 
            points = points.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
            //sort two subsets
            List<Point> FINAL_POINTS = DivideFunction(points);


            int countt = (int)FINAL_POINTS.Count;

            for (int index = 0; index < countt; ++index)
            {
                //check all points of convex hull in outpoint list 
                if (!outPoints.Contains(FINAL_POINTS[index]))
                    outPoints.Add(FINAL_POINTS[index]);


            }
        }

        public override string ToString()
        {
            return "Convex Hull - Divide & Conquer";
        }

    }
}