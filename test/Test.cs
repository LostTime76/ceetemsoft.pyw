using CeetemSoft.Pyw;

public static class Test
{
    public static void Main(string[] args)
    {
        PyInterp interp = PyInterp.Instance;

        interp.Start();

        PyObj o = new PyObj();
        PyBool b = new PyBool(false);

        long l = b.RefCount;

        o = (PyBool)b;
    }
}