using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUnitTesting
{
    public class Calculations
    {
        public class MathsHelper
        {
            public MathsHelper() { }
            public int Add(int a, int b)
            {
                int x = a + b;
                return x;
            }

            public int Subtract(int a, int b)
            {
                int x = a - b;
                return x;
            }
            public int Multiply(int a, int b)
            {
                int x = a * b;
                return x;
            }
            public int Divide(int a, int b)
            {
                try
                {
                    int x = a / b;
                    return x;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}
