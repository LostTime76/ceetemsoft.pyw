namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<void> _Py_Initialize;

    [PySymbol]
    private static delegate* unmanaged<void> _Py_Finalize;

    internal static void Py_Initialize()
    {
        _Py_Initialize();
    }

    internal static void Py_Finalize()
    {
        _Py_Finalize();
    }
}