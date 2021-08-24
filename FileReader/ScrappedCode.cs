using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    class ScrappedCode
    {
        public static double FindNearest(double targetNumber, IEnumerable<double> collection)
        {
            var results = collection.ToArray();
            double nearestValue;

            if (results.Any(ab => ab == targetNumber))
            {

                nearestValue = results.FirstOrDefault(i => i == targetNumber);
            }
            else
            {
                double greaterThanTarget = 0;
                double lessThanTarget = 0;

                if (results.Any(ab => ab > targetNumber))
                {
                    greaterThanTarget = results.Where(i => i > targetNumber).Min();
                }

                if (results.Any(ab => ab < targetNumber))
                {
                    lessThanTarget = results.Where(i => i < targetNumber).Max();
                }

                if (lessThanTarget == 0)
                {
                    nearestValue = greaterThanTarget;
                }

                else if (greaterThanTarget == 0)
                {
                    nearestValue = lessThanTarget;
                }

                else if (targetNumber - lessThanTarget < greaterThanTarget - targetNumber)
                {
                    nearestValue = lessThanTarget;
                }

                else
                {
                    nearestValue = greaterThanTarget;
                }
            }
            return nearestValue;
        }
    }


}

//internal static Dictionary<Point, double> MeasureDistanceToRay(Dictionary<Point, double> distanceToRayDictionary, Point cameraPoint, Point[] points, int iterations)
//{
//    double[] ArrayDistToLine = new double[points.Length];
//    Point endOfLine = new Point(131551.964, 398797151, 6.535);

//    for (int i = 0; i < iterations; i++)
//    {

//        ArrayDistToLine[i] = Extension.ShortDistance(cameraPoint, endOfLine, points[i]);
//        distanceToRayDictionary.Add(points[i], ArrayDistToLine[i]);
//    }

//    //Debugging the not sorted Dictionary with distances can be done here
//    //foreach (var value in distanceToCameraDictionary)
//    //{
//    //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
//    //    Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
//    //}

//    return distanceToRayDictionary;
//}

//internal static Dictionary<Point, double> MeasureDistanceToCamera(Dictionary<Point, double> distanceToCameraDictionary, Point cameraPoint, Point[] points, int iterations)
//{
//    double[] ArrayAbs = new double[points.Length];

//    for (int i = 0; i < iterations; i++)
//    {
//        ArrayAbs[i] = (Math.Abs(points[i].X - cameraPoint.X) + Math.Abs(points[i].Y - cameraPoint.Y) + Math.Abs(points[i].Z - cameraPoint.Z));

//        distanceToCameraDictionary.Add(points[i], ArrayAbs[i]);
//    }

//    //Debugging the not sorted Dictionary with distances can be done here
//    //foreach (var value in distanceToCameraDictionary)
//    //{
//    //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
//    //    Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
//    //}

//    return distanceToCameraDictionary;
//}

//var myList = distanceFromCameraDictionary.ToList();

//myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
//foreach (var value in myList)
//{
//    KeyValuePair<Point, double> myVal = (KeyValuePair<Point,double>) value;
//    Console.WriteLine(string.Format("{0}: {1}",myVal.Key.GetPoints(),myVal.Value));
//}

// If distance is less than current lowest distance
// Save point + distance in closest point list or array or dictionary   

//for (int i = 0; i < table2.Rows.Count; i++)
//{
//    xArrayAbs[i] = Math.Abs(points[i].X - cameraPoint.X);
//    yArrayAbs[i] = Math.Abs(points[i].Y - cameraPoint.Y);
//    zArrayAbs[i] = Math.Abs(points[i].Z - cameraPoint.Z);

//    ArrayAbs[i] = (Math.Abs(points[i].X - cameraPoint.X) + Math.Abs(points[i].Y - cameraPoint.Y) + Math.Abs(points[i].Z - cameraPoint.Z));

//    distance.Add(points[i], ArrayAbs[i]);
//}

//double x = 1;
//double y = 1;
//double z = 1;

//distancesFromCamera[i] = new Point(x, y, z);

//calculate distnace
//made the x,y,z into a key
//sort the new dictionary on the value

//distancesFromCamera[i].X = points[i].X - cameraPoint.X;
//distancesFromCamera[i].Y = points[i].Y - cameraPoint.Y;
//distancesFromCamera[i].Z = points[i].Z - cameraPoint.Z;

//xArray[i] = points[i].X - cameraPoint.X;
//yArray[i] = points[i].Y - cameraPoint.Y;
//zArray[i] = points[i].Z - cameraPoint.Z;

/*
double target = 0;
double distance = Math.Abs(distancesFromCamera[0].X - target);
double idx = 0;
for (int c = 1; c < distancesFromCamera.Length; c++)
{
    double cdistance = Math.Abs(distancesFromCamera[c].X - target);
    if (cdistance < distance)
    {
        idx = c;
        distance = cdistance;
    }
}
double theNumber = distancesFromCamera[idx].X;
*/

/*
double target = 0;
double distance = Math.Abs(distancesFromCamera[0].X - target);
double idx = 0;
for (int c = 1; c < distancesFromCamera.Length; c++)
{
    double cdistance = Math.Abs(distancesFromCamera[c].X - target);
    if (cdistance < distance)
    {
        idx = c;
        distance = cdistance;
    }
}
double theNumber = distancesFromCamera[idx].X;
*/

//Func<IEnumerable<int>, Func<IEnumerable<IEnumerable<int>>, IEnumerable<IEnumerable<int>>>> f = B => P => P.OrderBy(p => p.Zip(B, (x, y) => (x - y) * (x - y)).Sum());

//string input;
//while ((input = Console.ReadLine()) != null && input != "\n")
//{
//    var spoints = new List<string>();
//    foreach (Match match in Regex.Matches(input, @"\[[0-9, ]*\]"))
//    {
//        spoints.Add(match.Value);
//    }

//    var Points = spoints.Select(x => x.Trim(new[] { '[', ']' }).Split(',').Select(y => int.Parse(y.Trim())));
//    var ret = f(Points.First())(Points.Skip(1));

//    foreach (var point in ret)
//    {
//        string s = "";
//        foreach (var dim in point)
//            s += dim + ", ";

//        Console.Write("[{0}], ", s.Trim(new[] { ',', ' ' }));
//    }
//    Console.WriteLine();
//}

//double nearestX = FindNearest(1, xArray);
//double nearestY = FindNearest(1, yArray);
//double nearestZ = FindNearest(1, zArray);

//sort on three keys (x, y, and z)

//(defun compare - points(a b)
//(if (equal(car a)(car b) fuzz)
//(if (equal(cadr a)(cadr b) fuzz)
//(> (caddr a) (caddr b))
//(> (cadr a) (cadr b))
//)
//(> (car a) (car b))
//)              

//(defun xyzcompare(a b / x1 y1 z1 x2 y2 z2)
//(mapcar 'set '(x1 y1 z1)(car a))
//(mapcar 'set '(x2 y2 z2)(car b))
//(or
//(< x1 x2)
//(and(= x1 x2)(< y1 y2)) ;< ----you can substitute equal for each = and add fuzz for
//approximate equality
//(and(= x1 x2)(= y1 y2)(< z1 z2))
//) ; end or
//) ; end defun

//double targetNumber = 0;
//var nearest = distancesFromCamera.Aggregate((current, next) => Math.Abs(current.X - targetNumber) < Math.Abs(next.X - targetNumber) ? current : next);

//double targetNumber = 0;                
//var nearestX = distancesFromCamera.Min(a => Math.Abs(a.X - targetNumber));
//var nearestY = distancesFromCamera.Min(a => Math.Abs(a.Y - targetNumber));
//var nearestZ = distancesFromCamera.Min(a => Math.Abs(a.Z - targetNumber));

//    foreach (DataRow row in table2.Rows)
//{

//    string x = row["X"].ToString();
//    string y = row["Y"].ToString();
//    string z = row["Z"].ToString();
//}

//var stringArr = table3.AsEnumerable().Select(r => r.Field<string>("X")).ToArray();

//datatable1.AsEnumerable().Select(r => r.Field<string>("Name")).ToArray();

//double target = 0;

//var nearest = xArray.MinBy(x => Math.Abs(x - target));




// This function does not work beacause it is incomplete
// The example uses 2 functions that are not in the example
// 1 does a constraint (but i dont know what it constains)
// The other one calculates the sqaure distance
// When working this function takes a point and the beginnen and end of a line
// Based on this it calculates the distance from the point to the line
// 
//internal static double DistanceFromLineToPoint(Point point, Point lineStart, Point lineEnd)
//{
//    double line_dist = dist_sq(lineStart.X, lineStart.Y, lineStart.Z, lineEnd.X, lineEnd.Y, lineEnd.Z);

//    if (line_dist != 0)
//    {
//        double t = ((point.X - lineStart.X) * (lineEnd.X - lineStart.X) + (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y) + (point.Z - lineStart.Z) * (lineEnd.Z - lineStart.Z)) / line_dist;

//        t = constrain(t, 0, 1);

//        return dist_sq(point.X, point.Y, point.Z, lineStart.X + t * (lineEnd.X - lineStart.X), lineStart.Y + t * (lineEnd.Y - lineStart.Y), lineStart.Z + t * (lineEnd.Z - lineStart.Z));
//    }

//    else
//    {
//        Console.WriteLine("Line distance = 0 which caused this error");
//        return point.X;
//    }
//}
////function required for DistanceFromLineToPoint
//private static double constrain(double t, int v1, int v2)
//{
//    throw new NotImplementedException();
//}
////function required for DistanceFromLineToPoint
//private static double dist_sq(double x, double y, double z, double v1, double v2, double v3)
//{
//    throw new NotImplementedException();
//}
//// javascript version of above script
////float dist_to_segment_squared(float px, float py, float pz, float lx1, float ly1, float lz1, float lx2, float ly2, float lz2)
////{
////    float line_dist = dist_sq(lx1, ly1, lz1, lx2, ly2, lz2);

////    if (line_dist == 0) return dist_sq(px, py, pz, lx1, ly1, lz1);

////    float t = ((px - lx1) * (lx2 - lx1) + (py - ly1) * (ly2 - ly1) + (pz - lz1) * (lz2 - lz1)) / line_dist;

////    t = constrain(t, 0, 1);

////    return dist_sq(px, py, pz, lx1 + t * (lx2 - lx1), ly1 + t * (ly2 - ly1), lz1 + t * (lz2 - lz1));
////}


/*
                for (int j = 1; j < check; j++)
                {
                    if (Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + j).Value) < tol)
                    {

                    }

                    else
                    {
                        return false;
                    }
                }

                if (Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 1).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 2).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 3).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 4).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 5).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 6).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 7).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 8).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 9).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 10).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 11).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 12).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 13).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 14).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 15).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 16).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 17).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 18).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 19).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 20).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 21).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 22).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 23).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 24).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 25).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 26).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 27).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 28).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 29).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 30).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 30).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 31).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 32).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 33).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 34).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 35).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 36).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 37).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 38).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 39).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 40).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 41).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 42).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 43).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 44).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 45).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 46).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 47).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 48).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 49).Value) < tol
                        && Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + 50).Value) < tol)
                {
                    return true;
                }
                */
