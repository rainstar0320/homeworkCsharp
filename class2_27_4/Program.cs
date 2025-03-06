using System;
namespace class2_27_4
{
    class tuopulici
    {
        static void Main()
        {
            int[,] a = new int[3, 4] { { 1, 2, 3, 4 }, { 5, 1, 2, 3 }, { 9, 5, 1, 2 } };
            string ans = "True";
            for (int i = 0; i <3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i!=2&&j!=3&& a[i,j] != a[i + 1,j+1])//注意矩形数组表示格式的区别
                    {
                        ans = "False";
                    }
                }
            }
            Console.WriteLine(ans);
        }
    }
}
