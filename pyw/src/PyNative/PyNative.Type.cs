namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, int> _PyType_GetFlags;

    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyType_GetName;

    internal static int PyType_GetFlags(nint hType)
    {
        return _PyType_GetFlags(hType);
    }

    internal static nint PyType_GetName(nint hType)
    {
        return _PyType_GetName(hType);
    }
}