using System;
namespace class2_27_4
{
    public interface shape
    {
        double calarea();//计算面积
        bool isvalid();//判断是否合法
    }
    class rectangle:shape
    {
        public double length { get; set; }
        public double width { get; set; }
        public rectangle()
        {
            length = 0;
            width = 0;
        }
        public rectangle(double l,double w)
        {
            length = l;
            width = w;
        }
        public double calarea()
        {
            if (!isvalid())
            {
                Console.WriteLine("矩形不合法");
                return -1;
            }
            return length * width;
        }
        public bool isvalid()
        {
            return length > 0 && width > 0;
        }
    }
    class square : shape
    {
        public double length { get; set; }
        public square()
        {
            length = 0;
        }
        public square(double l)
        {
            length = l;
        }
        public double calarea()
        {
            if (!isvalid())
            {
                Console.WriteLine("正方形不合法");
                return -1;
            }
            return length * length;
        }
        public bool isvalid()
        {
            return length > 0;
        }
    }
    class triangle : shape
    {
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
        public triangle()
        {
            a = 0;
            b = 0;
            c = 0;
        }
        public triangle(double aa,double bb,double cc)
        {
            a = aa;
            b = bb;
            c = cc;
        }
        public double calarea()
        {
            if (!isvalid())
            {
                Console.WriteLine("三角形不合法");
                return -1;
            }
            double s= (a + b + c) / 2;
            return Math.Sqrt(s*(s-a)*(s-b)*(s-c));
        }
        public bool isvalid()
        {
            return a > 0&&b>0&&c>0&&a+b>c&&a+c>b&&b+c>a;
        }
    }
 

    class Program
    {
        static void Main()
        {
            shape[] a = new shape[10];
            a[0] = new rectangle(3, 5);
            a[1] = new rectangle(3, -1);
            a[2] = new rectangle(6, 7);
            a[3]= new square(4);
            a[4]= new square(-7);
            a[5]= new triangle(3,4,5);
            a[6]= new triangle(3, 7, 5);
            a[7]= new triangle(3, -4, 5);
            a[8]= new triangle(3, 7, 11);
            a[9]= new triangle(5, 5, 5);

            double res = 0;
            for(int i =0;i<10;i++)
            {
                res += Math.Max(0, a[i].calarea());
            }
            Console.WriteLine(res);

        }
    }
}
