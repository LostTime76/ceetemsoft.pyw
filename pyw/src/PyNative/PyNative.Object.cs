namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, void> _Py_IncRef;

    [PySymbol]
    private static delegate* unmanaged<nint, void> _Py_DecRef;

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

    internal static void PyObj_IncRef(nint pObj)
    {
        _Py_IncRef(pObj);
    }

    internal static void PyObj_DecRef(nint pObj)
    {
        _Py_DecRef(pObj);
    }

    internal static nint PyObj_GetAttr(nint pObj, nint pAttr)
    {
        return _PyObject_GetAttr(pObj, pAttr);
    }

    internal static nint PyObj_GetAttr(nint pObj, string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return _PyObject_GetAttrString(pObj, pAttr);
    }

    internal static bool PyObj_SetAttr(nint pObj, nint pAttr, nint pValue)
    {
        return (_PyObject_SetAttr(pObj, pAttr, pValue) == 0);
    }

    internal static bool PyObj_SetAttr(nint pObj, string attr, nint pValue)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PyObject_SetAttrString(pObj, pAttr, pValue) == 0);
    }

    internal static bool PyObj_DelAttr(nint pObj, nint pAttr)
    {
        return (_PyObject_SetAttr(pObj, pAttr, 0) == 0);
    }

    internal static bool PyObj_DelAttr(nint pObj, string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PyObject_SetAttrString(pObj, pAttr, 0) == 0);
    }

    internal static nint PyObj_Str(nint pObj)
    {
        return _PyObject_Str(pObj);
    }

    internal static string PyObj_NetStr(nint pObj)
    {
        // Get the object's string representation
        nint pStr = _PyObject_Str(pObj);

        if (pStr == 0)
        {
            return null;
        }

        // Convert the string object to a NET string
        string str  = new string((sbyte*)_PyUnicode_AsUTF8(pStr));

        // Garbage collect the string object as we don't need it
        _Py_DecRef(pStr);

        // Return the NET string
        return str;
    }
}