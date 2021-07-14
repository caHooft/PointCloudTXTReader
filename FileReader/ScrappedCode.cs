using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    class ScrappedCode
    {
    }
}
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
