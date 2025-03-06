using System;
namespace class2_27_3
{
    class sushu
    {
        public int number=0;
        public bool yes =true;
    }
    class aishishai
    {
        static void Main()
        {
            sushu[] a = new sushu[101];
            for (int i = 0; i <=100; i++)
            {
                a[i] = new sushu(); // 要先初始化每个元素!
                a[i].number = i;
            }
            for (int i = 2; i <=100; i++)
            {
                if (a[i].yes == false)
                    continue;
                for (int j = i; j <=100; j++)
                {
                    if (j % i == 0)
                        a[j].yes = false;
                }
                Console.WriteLine(i);
            }
        }
    }
}
