namespace CeetemSoft.Pyw;

public sealed class PySys : PyModule
{
    public const string Name        = "sys";
    public const string ModulesAttr = "modules";

    private PySys(nint hSys) : base(hSys) { }

    internal static PySys Create()
    {
        return new PySys(((PyDict)PyNative.PySys_GetObj(ModulesAttr))[Name]);
    }
}