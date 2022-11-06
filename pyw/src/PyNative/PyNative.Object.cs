namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, void> _Py_IncRef;

    [PySymbol]
    private static delegate* unmanaged<nint, void> _Py_DecRef;

    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyObject_Type;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, int> _PyObject_IsInstance;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, int> _PyObject_HasAttr;

    [PySymbol]
    private static delegate* unmanaged<nint, byte*, int> _PyObject_HasAttrString;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, nint> _PyObject_GetAttr;

    [PySymbol]
    private static delegate* unmanaged<nint, byte*, nint> _PyObject_GetAttrString;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, nint, int> _PyObject_SetAttr;

    [PySymbol]
    private static delegate* unmanaged<nint, byte*, nint, int> _PyObject_SetAttrString;

    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyObject_Str;

    internal static void PyObj_IncRef(nint hObj)
    {
        _Py_IncRef(hObj);
    }

    internal static void PyObj_DecRef(nint hObj)
    {
        _Py_DecRef(hObj);
    }

    internal static nint PyObj_Type(nint hObj)
    {
        return _PyObject_Type(hObj);
    }

    internal static bool PyObj_IsInstance(nint hObj, nint hTypeObj)
    {
        return (_PyObject_IsInstance(hObj, hTypeObj) != 0);
    }

    internal static bool PyObj_HasAttr(nint hObj, nint hAttr)
    {
        return (_PyObject_HasAttr(hObj, hAttr) != 0);
    }

    internal static bool PyObj_HasAttr(nint hObj, string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PyObject_HasAttrString(hObj, pAttr) != 0);
    }

    internal static nint PyObj_GetAttr(nint hObj, nint hAttr)
    {
        return _PyObject_GetAttr(hObj, hAttr);
    }

    internal static nint PyObj_GetAttr(nint hObj, string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return _PyObject_GetAttrString(hObj, pAttr);
    }

    internal static bool PyObj_SetAttr(nint hObj, nint hAttr, nint hValue)
    {
        return (_PyObject_SetAttr(hObj, hAttr, hValue) == 0);
    }

    internal static bool PyObj_SetAttr(nint hObj, string attr, nint hValue)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PyObject_SetAttrString(hObj, pAttr, hValue) == 0);
    }

    internal static bool PyObj_DelAttr(nint hObj, nint hAttr)
    {
        return (_PyObject_SetAttr(hObj, hAttr, 0) == 0);
    }

    internal static bool PyObj_DelAttr(nint hObj, string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PyObject_SetAttrString(hObj, pAttr, 0) == 0);
    }

    internal static nint PyObj_Str(nint hObj)
    {
        return _PyObject_Str(hObj);
    }

    internal static string PyObj_NetStr(nint hObj)
    {
        // Get the object's string representation
        nint hStr = _PyObject_Str(hObj);

        if (hStr == 0)
        {
            return null;
        }

        // Convert the string object to a NET string
        string str = new string((sbyte*)_PyUnicode_AsUTF8(hStr));

        // Garbage collect the string object as we don't need it
        _Py_DecRef(hStr);

        // Return the NET string
        return str;
    }
}