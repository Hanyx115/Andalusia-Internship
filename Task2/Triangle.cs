using System;
using System.Collections.Generic;
using System.Text;

namespace Task2;

public class Triangle : Shape, Idrawable
{
    public double BaseLength { get; set; }
    public double Height { get; set; }

    public Triangle(double baseLength, double height)
    {
        BaseLength = baseLength;
        Height = height;
    }

    public override double Area()
    {
        return 0.5 * BaseLength * Height;
    }

    public void Draw()
    {
        Console.WriteLine("  *");
        Console.WriteLine(" ***");
        Console.WriteLine("*****");
    } 
} 

