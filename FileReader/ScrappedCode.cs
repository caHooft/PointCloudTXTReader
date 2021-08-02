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
