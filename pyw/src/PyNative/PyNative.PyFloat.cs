namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyFloat_Type;

    [PySymbol]
    private static delegate* unmanaged<double, nint> _PyFloat_FromDouble;

    [PySymbol]
    private static delegate* unmanaged<nint, double> _PyFloat_AsDouble;

    internal static nint PyFloat_Type()
    {
        return _PyFloat_Type;
    }

    internal static bool PyFloat_CheckType(nint hObj)
    {
        return (_PyObject_IsInstance(hObj, _PyFloat_Type) == PyConst.True);
    }

    internal static nint PyFloat_New(double value)
    {
        return (_PyFloat_FromDouble(value));
    }

    internal static double PyFloat_AsDouble(nint hDouble)
    {
        return _PyFloat_AsDouble(hDouble);
    }
}