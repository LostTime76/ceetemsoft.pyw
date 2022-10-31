namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint> _PyDict_New;

    [PySymbol]
    private static delegate* unmanaged<nint, int> _PyDict_Size;

    [PySymbol]
    private static delegate* unmanaged<nint, void> _PyDict_Clear;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, int> _PyDict_Contains;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, nint> _PyDict_GetItem;

    [PySymbol]
    private static delegate* unmanaged<nint, byte*, nint> _PyDict_GetItemString;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, nint, int> _PyDict_SetItem;

    [PySymbol]
    private static delegate* unmanaged<nint, byte*, nint, int> _PyDict_SetItemString;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, int> _PyDict_DelItem;

    [PySymbol]
    private static delegate* unmanaged<nint, byte*, int> _PyDict_DelItemString;

    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyDict_Keys;

    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyDict_Values;

    internal static nint PyDict_New()
    {
        return _PyDict_New();
    }

    internal static int PyDict_Size(nint pObj)
    {
        return _PyDict_Size(pObj);
    }

    internal static void PyDict_Clear(nint pObj)
    {
        _PyDict_Clear(pObj);
    }

    internal static bool PyDict_Contains(nint pObj, nint pKey)
    {
        return (_PyDict_Contains(pObj, pKey) != 0);
    }

    internal static nint PyDict_GetItem(nint pObj, nint pKey)
    {
        return _PyDict_GetItem(pObj, pKey);
    }

    internal static nint PyDict_GetItem(nint pObj, string key)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return _PyDict_GetItemString(pObj, pKey);
    }

    internal static bool PyDict_SetItem(nint pObj, nint pKey, nint pValue)
    {
        return (_PyDict_SetItem(pObj, pKey, pValue) == 0);
    }

    internal static bool PyDict_SetItem(nint pObj, string key, nint pValue)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return (_PyDict_SetItemString(pObj, pKey, pValue) == 0);
    }

    internal static bool PyDict_DelItem(nint pObj, nint pKey)
    {
        return (_PyDict_DelItem(pObj, pKey) == 0);
    }

    internal static bool PyDict_DelItem(nint pObj, string key)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return (_PyDict_DelItemString(pObj, pKey) == 0);
    }

    internal static nint PyDict_Keys(nint pObj)
    {
        return _PyDict_Keys(pObj);
    }

    internal static nint PyDict_Values(nint pObj)
    {
        return _PyDict_Values(pObj);
    }
}