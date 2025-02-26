using System;

class Calculator
{
    static void Main()
    {
        double num1;
        Console.Write("请输入第一个数字：");
        if (!double.TryParse(Console.ReadLine(), out num1))
        {
            Console.WriteLine("输入无效");
            return;
        }

        double num2;
        Console.Write("请输入第二个数字：");
        if (!double.TryParse(Console.ReadLine(), out num2))
        {
            Console.WriteLine("输入无效");
            return;
        }

        Console.Write("请输入运算符（+、-、*、/）：");
        string op = Console.ReadLine().Trim();
        if (op.Length != 1 || "+-*/".IndexOf(op) == -1)
        {
            Console.WriteLine("运算符无效");
            return;
        }

        if (op == "/" && num2 == 0)
        {
            Console.WriteLine("除数不能为零");
            return;
        }

        double result = 0;
        switch (op)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                result = num1 / num2;
                break;
        }

        Console.WriteLine($"计算结果：{result}");
    }
}
