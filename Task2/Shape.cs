using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    public abstract class Shape
    {
        public abstract double Area();

        public void Describe()
        {
            Console.WriteLine($"{GetType().Name}");
            Console.WriteLine($"Area = {Area():F2}");
        }

    }
}
