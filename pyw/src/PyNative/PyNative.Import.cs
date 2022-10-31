namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*> _PyImport_Import;

    internal static PyObj* PyImport_Import(string module)
    {
        return _PyImport_Import(PyUnicode_New(module));
    }
}