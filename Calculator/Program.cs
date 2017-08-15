using System;
using CSharpFunctionalExtensions;
using SimpleInjector;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.RegisterCalculator();
            var calculator = container.GetInstance<ICalculator>();

            while (true)
            {
                Console.WriteLine("Insert expression like \"3+2*5/6\"");
                var expression = Console.ReadLine();
                calculator.Calculate(expression)
                    .OnSuccess(r => Console.WriteLine($"Result = {r}"))
                    .OnFailure(e => Console.WriteLine(e));
            }
        }
    }
}