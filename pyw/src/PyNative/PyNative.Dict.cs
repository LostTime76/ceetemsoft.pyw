namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<PyObj*> _PyDict_New;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, int> _PyDict_Size;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, void> _PyDict_Clear;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*, int> _PyDict_Contains;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*, PyObj*> _PyDict_GetItem;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, byte*, PyObj*> _PyDict_GetItemString;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*, PyObj*, int> _PyDict_SetItem;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, byte*, PyObj*, int> _PyDict_SetItemString;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*, int> _PyDict_DelItem;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, byte*, int> _PyDict_DelItemString;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*> _PyDict_Keys;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*> _PyDict_Values;

    internal static PyObj* PyDict_New()
    {
        return _PyDict_New();
    }

    internal static int PyDict_Size(PyObj* pObj)
    {
        return _PyDict_Size(pObj);
    }

    internal static void PyDict_Clear(PyObj* pObj)
    {
        _PyDict_Clear(pObj);
    }

    internal static bool PyDict_Contains(PyObj* pObj, PyObj* pKey)
    {
        return (_PyDict_Contains(pObj, pKey) != 0);
    }

    internal static PyObj* PyDict_GetItem(PyObj* pObj, PyObj* pKey)
    {
        return _PyDict_GetItem(pObj, pKey);
    }

    internal static PyObj* PyDict_GetItem(PyObj* pObj, string key)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return _PyDict_GetItemString(pObj, pKey);
    }

    internal static bool PyDict_SetItem(PyObj* pObj, PyObj* pKey, PyObj* pValue)
    {
        return (_PyDict_SetItem(pObj, pKey, pValue) == 0);
    }

    internal static bool PyDict_SetItem(PyObj* pObj, string key, PyObj* pValue)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return (_PyDict_SetItemString(pObj, pKey, pValue) == 0);
    }

    internal static bool PyDict_DelItem(PyObj* pObj, PyObj* pKey)
    {
        return (_PyDict_DelItem(pObj, pKey) == 0);
    }

    internal static bool PyDict_DelItem(PyObj* pObj, string key)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return (_PyDict_DelItemString(pObj, pKey) == 0);
    }

    internal static PyObj* PyDict_Keys(PyObj* pObj)
    {
        return _PyDict_Keys(pObj);
    }

    internal static PyObj* PyDict_Values(PyObj* pObj)
    {
        return _PyDict_Values(pObj);
    }
}