using System;
namespace class2_27_2
{
    class maxminbasum
    { 
        static void Main()
        {
            int[] a = { 1, 7, 6, 4, 2, 6, 7, 99 };
            Array.Sort(a);
            int maxnumber = a[a.GetUpperBound(0)];
            string max = Convert.ToString(maxnumber);
            Console.WriteLine(max);
            int minnumber = a[a.GetLowerBound(0)];
            string min = Convert.ToString(minnumber);
            Console.WriteLine(min);
            double ans = 0;
            foreach(int i in a)
            {
                ans += i;
            }
            Console.WriteLine(ans);
            double ba = ans / a.GetLength(0);
            Console.WriteLine(ba);
        }
    }
}

