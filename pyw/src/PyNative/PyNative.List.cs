namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<int, nint> _PyList_New;

    [PySymbol]
    private static delegate* unmanaged<nint, int> _PyList_Size;

    [PySymbol]
    private static delegate* unmanaged<nint, int, nint> _PyList_GetItem;

    [PySymbol]
    private static delegate* unmanaged<nint, int, int, nint> _PyList_GetSlice;

    [PySymbol]
    private static delegate* unmanaged<nint, int, nint, int> _PyList_SetItem;

    [PySymbol]
    private static delegate* unmanaged<nint, int, nint, int> _PyList_Insert;

    [PySymbol]
    private static delegate* unmanaged<nint, nint, int> _PyList_Append;

    [PySymbol]
    private static delegate* unmanaged<nint, int, int, nint, int> _PyList_SetSlice;

    internal static nint PyList_New(int size = 0)
    {
        return _PyList_New(size);
    }

    internal static int PyList_Size(nint pObj)
    {
        return _PyList_Size(pObj);
    }

    internal static nint PyList_GetItem(nint pObj, int idx)
    {
        return _PyList_GetItem(pObj, idx);
    }

    internal static nint PyList_GetSlice(nint pObj, int low, int high)
    {
        return _PyList_GetSlice(pObj, low, high);
    }

    internal static bool PyList_SetItem(nint pObj, int idx, nint pValue)
    {
        return (_PyList_SetItem(pObj, idx, pValue) == 0);
    }

    internal static bool PyList_Insert(nint pObj, int idx, nint pValue)
    {
        return (_PyList_Insert(pObj, idx, pValue) == 0);
    }

    internal static bool PyList_Append(nint pObj, nint pValue)
    {
        return (_PyList_Append(pObj, pValue) == 0);
    }

    internal static bool PyList_SetSlice(nint pObj, int low, int high, nint pValue)
    {
        return (_PyList_SetSlice(pObj, low, high, pValue) == 0);
    }
}