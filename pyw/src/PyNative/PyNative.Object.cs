namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<PyObj*, void> _Py_IncRef;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, void> _Py_DecRef;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*, PyObj*> _PyObject_GetAttr;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, byte*, PyObj*> _PyObject_GetAttrString;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*, PyObj*, int> _PyObject_SetAttr;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, byte*, PyObj*, int> _PyObject_SetAttrString;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*> _PyObject_Str;

    internal static void PyObj_IncRef(PyObj* pObj)
    {
        _Py_IncRef(pObj);
    }

    internal static void PyObj_DecRef(PyObj* pObj)
    {
        _Py_DecRef(pObj);
    }

    internal static PyObj* PyObj_GetAttr(PyObj* pObj, PyObj* pAttr)
    {
        return _PyObject_GetAttr(pObj, pAttr);
    }

    internal static PyObj* PyObj_GetAttr(PyObj* pObj, string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return _PyObject_GetAttrString(pObj, pAttr);
    }

    internal static bool PyObj_SetAttr(PyObj* pObj, PyObj* pAttr, PyObj* pValue)
    {
        return (_PyObject_SetAttr(pObj, pAttr, pValue) == 0);
    }

    internal static bool PyObj_SetAttr(PyObj* pObj, string attr, PyObj* pValue)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PyObject_SetAttrString(pObj, pAttr, pValue) == 0);
    }

    internal static bool PyObj_DelAttr(PyObj* pObj, PyObj* pAttr)
    {
        return (_PyObject_SetAttr(pObj, pAttr, null) == 0);
    }

    internal static bool PyObj_DelAttr(PyObj* pObj, string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PyObject_SetAttrString(pObj, pAttr, null) == 0);
    }

    internal static PyObj* PyObj_Str(PyObj* pObj)
    {
        return _PyObject_Str(pObj);
    }

    internal static string PyObj_NetStr(PyObj* pObj)
    {
        // Get the object's string representation
        PyObj* pStr = _PyObject_Str(pObj);

        if (pStr == null)
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