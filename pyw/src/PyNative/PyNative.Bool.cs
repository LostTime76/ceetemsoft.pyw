namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyBool_Type;

    [PySymbol]
    private static delegate* unmanaged<long, nint> _PyBool_FromLong;

    internal static nint PyBool_Type()
    {
        return _PyBool_Type;
    }

    internal static bool PyBool_CheckType(nint hObj)
    {
        return false;
        //return (_PyObject_IsInstance(hObj, _PyBool_Type) == PyConst.True);
    }

    internal static nint PyBool_New(bool value)
    {
        return _PyBool_FromLong(value ? PyConst.True : PyConst.False);
    }

    internal static bool PyBool_AsBool(nint hBool)
    {
        return (_PyLong_AsLong(hBool) != 0 ? true : false);
    }
}