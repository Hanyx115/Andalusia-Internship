using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    internal class Rectangle : Shape, Idrawable, IResizable
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
        public override double Area()
        {
            return Width * Height;
        }
        public void Draw()
        {
            Console.WriteLine("******");
            Console.WriteLine("*    *");
            Console.WriteLine("******");
        }
        public void Scale(double factor)
        {
            Width *= factor;
            Height *= factor;
        }
    }
}

