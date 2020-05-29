using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversePolishNotation
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите выражение в постфиксной форме:");
            var line = Console.ReadLine();
            var resultStack = new Stack<double>();
            var data = line.Split(' ');
            double num;
            var isError = false;
            for (int i = 0; i < data.Length; i++)
            {
                if (isError) break;
                var arg = data[i];
                bool isNum = double.TryParse(arg, out num);
                if (isNum)
                    resultStack.Push(num);
                else
                {
                    double op2;
                    switch (arg)
                    {
                        case "+":
                            if (resultStack.Count < 2)
                            {
                                Console.WriteLine("Ошибка: не хватает операнда");
                                isError = true;
                            }
                            else
                                resultStack.Push(resultStack.Pop() + resultStack.Pop());
                            break;
                        case "*":
                            if (resultStack.Count < 2)
                            {
                                Console.WriteLine("Ошибка: не хватает операнда");
                                isError = true;
                            }
                            else
                                resultStack.Push(resultStack.Pop() * resultStack.Pop());
                            break;
                        case "-":
                            if (resultStack.Count < 2)
                            {
                                Console.WriteLine("Ошибка: не хватает операнда");
                                isError = true;
                            }
                            else
                            {
                                op2 = resultStack.Pop();
                                resultStack.Push(resultStack.Pop() - op2);
                            }
                            break;
                        case "/":
                            if (resultStack.Count < 2)
                            {
                                Console.WriteLine("Ошибка: не хватает операнда");
                                isError = true;
                            }
                            else
                            {
                                op2 = resultStack.Pop();
                                if (op2 != 0.0)
                                    resultStack.Push(resultStack.Pop() / op2);
                                else
                                {
                                    Console.WriteLine("Ошибка. Деление на ноль");
                                    isError = true;
                                }
                            }
                            break;
                        case "^":
                            if (resultStack.Count < 2)
                            {
                                Console.WriteLine("Ошибка: не хватает операнда");
                                isError = true;
                            }
                            else
                            {
                                op2 = resultStack.Pop();
                                if (op2 >= 0)
                                    resultStack.Push(Math.Pow(resultStack.Pop(), op2));
                                else
                                {
                                    Console.WriteLine("Бессмысленная операция. Возведение в отрицательную степень");
                                    isError = true;
                                }
                            }
                            break;
                        case "%":
                            if (resultStack.Count < 2)
                            {
                                Console.WriteLine("Ошибка: не хватает операнда");
                                isError = true;
                            }
                            else
                            {
                                op2 = resultStack.Pop();
                                if (op2 != 0.0)
                                    resultStack.Push(resultStack.Pop() % op2);
                                else
                                {
                                    Console.WriteLine("Ошибка. Деление на ноль");
                                    isError = true;
                                }
                            }
                            break;
                        case "$": 
                            if (resultStack.Count == 0)
                            {
                                isError = true;
                                Console.WriteLine("Ошибка: не хватает операнда");
                            }
                            else
                            {
                                op2 = resultStack.Pop();
                                resultStack.Push(op2 * -1);
                            }
                            break;
                        case "!":
                            if (resultStack.Count == 0)
                            {
                                isError = true;
                                Console.WriteLine("Ошибка: не хватает операнда");
                            }
                            else
                            {
                                op2 = resultStack.Pop();
                                if (op2 >= 0)
                                    resultStack.Push(Factorial(op2));
                                else
                                {
                                    Console.WriteLine("Бессмысленная операция. Факториал отрицательного числа");
                                    isError = true;
                                }
                            }
                            break;
                    }
                }
            }

            if (resultStack.Count == 1)
                Console.WriteLine($"{line} = {resultStack.Pop()}");
            else
                Console.WriteLine("Ошибка: значение выражения не определено");
            Console.ReadLine();
        }

        private static double Factorial(double op2)
        {
            var result = 1;
            for (int i = 2; i <= op2; i++)
                result *= i;
            return result;
        }
    }
}
