namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyList_Type;

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

    internal static nint PyList_Type()
    {
        return _PyList_Type;
    }

    internal static bool PyList_CheckType(nint hObj)
    {
        return PyObj_IsInstance(hObj, _PyList_Type);
    }

    internal static nint PyList_New(int size = 0)
    {
        return _PyList_New(size);
    }

    internal static int PyList_Size(nint hList)
    {
        return _PyList_Size(hList);
    }

    internal static nint PyList_GetItem(nint hList, int idx)
    {
        return _PyList_GetItem(hList, idx);
    }

    internal static nint PyList_GetSlice(nint hList, int low, int high)
    {
        return _PyList_GetSlice(hList, low, high);
    }

    internal static bool PyList_SetItem(nint hList, int idx, nint hValue)
    {
        return (_PyList_SetItem(hList, idx, hValue) == 0);
    }

    internal static bool PyList_Insert(nint hList, int idx, nint hValue)
    {
        return (_PyList_Insert(hList, idx, hValue) == 0);
    }

    internal static bool PyList_Append(nint hList, nint hValue)
    {
        return (_PyList_Append(hList, hValue) == 0);
    }

    internal static bool PyList_SetSlice(nint hList, int low, int high, nint hSlice)
    {
        return (_PyList_SetSlice(hList, low, high, hSlice) == 0);
    }
}