namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyModule_Type;

    internal static bool PyModule_CheckType(nint hObj)
    {
        return PyObj_IsInstance(hObj, _PyModule_Type);
    }

    internal static nint PyModule_Type()
    {
        return _PyModule_Type;
    }
}