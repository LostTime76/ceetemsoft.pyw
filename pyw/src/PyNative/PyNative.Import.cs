namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyImport_Import;

    internal static nint PyImport_Import(string module)
    {
        return _PyImport_Import(PyUnicode_New(module));
    }
}