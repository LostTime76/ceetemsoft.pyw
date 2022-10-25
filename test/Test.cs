using CeetemSoft.Pyw;

public static class Test
{
    public static void Main(string[] args)
    {
        PyInterp interp = PyInterp.Instance;

        interp.Start("localhost", 4242);

        PySys sys = interp.Sys;
        sys.CacheDir = new PyString("blah blah");
        string s = ((PyString)sys.CacheDir).Value;
    }
}