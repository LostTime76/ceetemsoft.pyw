namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyLong_Type;

    [PySymbol]
    private static delegate* unmanaged<long, nint> _PyLong_FromLong;

    [PySymbol]
    private static delegate* unmanaged<ulong, nint> _PyLong_FromUnsignedLong;

    [PySymbol]
    private static delegate* unmanaged<nint, long> _PyLong_AsLong;

    [PySymbol]
    private static delegate* unmanaged<nint, ulong> _PyLong_AsUnsignedLong;

    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyNumber_Long;

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
        return _PyLong_FromLong(value);
    }

    internal static nint PyLong_New(ulong value)
    {
        return _PyLong_FromUnsignedLong(value);
    }

    internal static long PyLong_AsLong(nint hLong)
    {
        return _PyLong_AsLong(hLong);
    }

    internal static ulong PyLong_AsULong(nint hLong)
    {
        return _PyLong_AsUnsignedLong(hLong);
    }

    internal static nint PyLong_Conv(nint hObj)
    {
        return _PyNumber_Long(hObj);
    }
}