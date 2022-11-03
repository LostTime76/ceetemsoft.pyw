namespace CeetemSoft.Pyw;

public class PyModule : PyClass
{
    private PyModule() : base(0) { }

    public PyModule(nint hModule) : base(hModule)
    {
        if (!PyNative.PyModule_CheckType(hModule))
        {
            ThrowCastFailure();
        }
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object behind the handle is not of module type.");
    }
}