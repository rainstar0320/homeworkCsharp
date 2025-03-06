using System;
using System.Collections.Specialized;
namespace class2_27
{
    class sushuyinzi
    { 
        static void Main()
        {
            string a = Console.ReadLine();
            int number = Convert.ToInt32(a);
            for(int i=2;i<=number;i++)
            {
                while(number%i==0)
                {
                    Console.WriteLine(i);
                    number /= i;
                }
            }
        }
    }

}

