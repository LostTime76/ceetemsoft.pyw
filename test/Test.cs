using CeetemSoft.Pyw;

public static class Test
{
    public static void Main(string[] args)
    {
        PyInterp interp = PyInterp.Instance;

        interp.Start();

        PyBool b = new PyBool(true);
        PyObj o = b;

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
    }
}