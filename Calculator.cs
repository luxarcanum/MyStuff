using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUnitTesting
{
    class Calculator
    {
        //Calculations helping = new Calculations();
        
        static void Main(string[] args)
        {
            Console.WriteLine("Enter two numbers\n");
            int number1;
            int number2;
            number1 = int.Parse(Console.ReadLine());
            number2 = int.Parse(Console.ReadLine());

            Calculations.MathsHelper helper = new Calculations.MathsHelper();
            int a = helper.Add(number1, number2);
            Console.WriteLine("\nThe sum of " + number1 + " and " + number2 + " is " + a);
            int b = helper.Subtract(number1, number2);
            Console.WriteLine("\nThe difference between " + number1 + " and " + number2 + "  is " + b);
            int c = helper.Multiply(number1, number2);
            Console.WriteLine("\nThe product of " + number1 + " and " + number2 + "  is " + c);
            int d = helper.Divide(number1, number2);
            Console.WriteLine("\nThe division of " + number1 + " and " + number2 + "  is " + d);
            Console.ReadKey();
        }
    }
}
