namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<int, PyObj*> _PyList_New;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, int> _PyList_Size;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, int, PyObj*> _PyList_GetItem;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, int, int, PyObj*> _PyList_GetSlice;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, int, PyObj*, int> _PyList_SetItem;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, int, PyObj*, int> _PyList_Insert;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, PyObj*, int> _PyList_Append;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, int, int, PyObj*, int> _PyList_SetSlice;

    internal static PyObj* PyList_New(int size = 0)
    {
        return _PyList_New(size);
    }

    internal static int PyList_Size(PyObj* pObj)
    {
        return _PyList_Size(pObj);
    }

    internal static PyObj* PyList_GetItem(PyObj* pObj, int idx)
    {
        return _PyList_GetItem(pObj, idx);
    }

    internal static PyObj* PyList_GetSlice(PyObj* pObj, int low, int high)
    {
        return _PyList_GetSlice(pObj, low, high);
    }

    internal static bool PyList_SetItem(PyObj* pObj, int idx, PyObj* pValue)
    {
        return (_PyList_SetItem(pObj, idx, pValue) == 0);
    }

    internal static bool PyList_Insert(PyObj* pObj, int idx, PyObj* pValue)
    {
        return (_PyList_Insert(pObj, idx, pValue) == 0);
    }

    internal static bool PyList_Append(PyObj* pObj, PyObj* pValue)
    {
        return (_PyList_Append(pObj, pValue) == 0);
    }

    internal static bool PyList_SetSlice(PyObj* pObj, int low, int high, PyObj* pValue)
    {
        return (_PyList_SetSlice(pObj, low, high, pValue) == 0);
    }
}