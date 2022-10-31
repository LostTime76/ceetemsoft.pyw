namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<PyTypeObj*, int> _PyType_GetFlags;

    internal static int PyType_GetFlags(PyTypeObj* pType)
    {
        return _PyType_GetFlags(pType);
    }
}