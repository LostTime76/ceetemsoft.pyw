namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyDict_Type;

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

    internal static bool PyDict_CheckType(nint hObj)
    {
        return PyObj_IsInstance(hObj, _PyDict_Type);
    }

    internal static nint PyDict_Type()
    {
        return _PyDict_Type;
    }

    internal static nint PyDict_New()
    {
        return _PyDict_New();
    }

    internal static int PyDict_Size(nint hDict)
    {
        return _PyDict_Size(hDict);
    }

    internal static void PyDict_Clear(nint hDict)
    {
        _PyDict_Clear(hDict);
    }

    internal static bool PyDict_Contains(nint hDict, nint hKey)
    {
        return (_PyDict_Contains(hDict, hKey) != 0);
    }

    internal static nint PyDict_GetItem(nint hDict, nint hKey)
    {
        return _PyDict_GetItem(hDict, hKey);
    }

    internal static nint PyDict_GetItem(nint hDict, string key)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return _PyDict_GetItemString(hDict, pKey);
    }

    internal static bool PyDict_SetItem(nint hDict, nint hKey, nint hValue)
    {
        return (_PyDict_SetItem(hDict, hKey, hValue) == 0);
    }

    internal static bool PyDict_SetItem(nint hDict, string key, nint hValue)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return (_PyDict_SetItemString(hDict, pKey, hValue) == 0);
    }

    internal static bool PyDict_DelItem(nint hDict, nint hKey)
    {
        return (_PyDict_DelItem(hDict, hKey) == 0);
    }

    internal static bool PyDict_DelItem(nint hDict, string key)
    {
        int   len  = GetUtf8StrLen(key);
        byte* pKey = stackalloc byte[len + 1];
        StrToUtf8Str(key, pKey, len);

        return (_PyDict_DelItemString(hDict, pKey) == 0);
    }

    internal static nint PyDict_Keys(nint hDict)
    {
        return _PyDict_Keys(hDict);
    }

    internal static nint PyDict_Values(nint hDict)
    {
        return _PyDict_Values(hDict);
    }
}