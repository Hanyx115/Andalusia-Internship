using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    internal class Circle : Shape, Idrawable, IResizable
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double Area()
        {
            return Math.PI * Radius * Radius;
        }

        public void Draw()
        {

            Console.WriteLine(" *** ");
            Console.WriteLine("*   *");
            Console.WriteLine(" *** ");
            /*int r = (int)Radius;

            for (int y = -r; y <= r; y++)
            {
                for (int x = -r; x <= r; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y);

                    if (distance >= r - 0.5 && distance <= r + 0.5)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }

                Console.WriteLine();*/
        }
        public void Scale(double factor)
        {
            Radius *= factor;
        }
    }

    }
