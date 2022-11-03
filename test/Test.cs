using CeetemSoft.Pyw;

public static class Test
{
    public static void Main(string[] args)
    {
        PyInterp interp = PyInterp.Instance;

        interp.Start();

        PySys sys = interp.Sys;
    }
}