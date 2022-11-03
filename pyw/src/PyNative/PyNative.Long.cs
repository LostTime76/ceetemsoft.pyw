namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyLong_Type;

    [PySymbol]
    private static delegate* unmanaged<long, nint> _PyLong_FromLong;

    [PySymbol]
    private static delegate* unmanaged<nint, long> _PyLong_AsLong;

    internal static nint PyLong_Type()
    {
        return _PyLong_Type;
    }

    internal static bool PyLong_CheckType(nint hObj)
    {
        return (_PyObject_IsInstance(hObj, _PyLong_Type) == PyConst.True);
    }

    internal static nint PyLong_New(long value)
    {
        return (_PyLong_FromLong(value));
    }

    internal static long PyLong_AsLong(nint hLong)
    {
        return _PyLong_AsLong(hLong);
    }
}