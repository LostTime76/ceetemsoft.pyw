namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, int> _PyType_GetFlags;

    internal static int PyType_GetFlags(nint pType)
    {
        return _PyType_GetFlags(pType);
    }
}