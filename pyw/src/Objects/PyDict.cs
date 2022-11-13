namespace CeetemSoft.Pyw;

public readonly struct PyDict : IPyObj
{
    private const string _PyTypename = "dict";

    private readonly nint _handle;

    public PyDict(nint hDict)
    {
        _handle = (PyNative.PyDict_CheckType(hDict) ? hDict : PyConst.InvalidHandle);
    }
}