using System.Net;

using CeetemSoft.Pyw;

public static class Test
{
    public static void Main(string[] args)
    {
        PyInterp interp = new PyInterp();
        interp.DebugHost = "localhost";
        interp.DebugPort = 4242;

        interp.Start("tst");
    }
}