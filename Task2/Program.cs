using Task2;

internal class Program
{
    private static void Main(string[] args)
    {

        Shape[] shapes =
        {
            new Circle(5),
            new Rectangle(4,6),
            new Triangle(6,4)
        };

        foreach (Shape shape in shapes)
        {
            shape.Describe();

            ((Idrawable)shape).Draw();

            Console.WriteLine();
        }

        Console.WriteLine("Scaling shapes...\n");

        List<IResizable> resizableShapes = new List<IResizable>
        {
            new Circle(2),
            new Rectangle(3,4)
        };

        ScaleAll(resizableShapes, 2);

        foreach (IResizable item in resizableShapes)
        {
            Console.WriteLine(item.GetType().Name + " scaled.");
        }
    }

    static void ScaleAll(IEnumerable<IResizable> shapes, double factor)
    {
        foreach (IResizable shape in shapes)
        {
            shape.Scale(factor);
        }
    }
}