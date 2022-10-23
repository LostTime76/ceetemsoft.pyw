using System.Collections;
using System.Collections.Generic;

namespace CeetemSoft.Pyw;

public class PyList : PyObject
{
    internal PyList(int size = 0) : base(PyNative.PyList_New(size)) { }
    internal PyList(IntPtr pointer) : base(pointer) { }

    public int Append(string item)
    {
        return PyNative.PyList_Append(Pointer, PyNative.PyUnicode_FromString(item));
    }

    public int Clear()
    {
        return Slice(0, Count, PyObject.Null);
    }

    public int Slice(int low, int high, PyObject items)
    {
        return PyNative.PyList_SetSlice(Pointer, low, high, items.Pointer);
    }

    public int SetItem(int idx, string item)
    {
        return PyNative.PyList_SetItem(Pointer, idx, PyNative.PyUnicode_FromString(item));
    }

    public int SetItem(int idx, PyObject item)
    {
        return PyNative.PyList_SetItem(Pointer, idx, item.Pointer);
    }

    public string GetString(int idx)
    {
        return PyObject.AsString(PyNative.PyList_GetItem(Pointer, idx));
    }

    public PyObject GetItem(int idx)
    {
        return new PyObject(PyNative.PyList_GetItem(Pointer, idx));
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public int Count
    {
        get { return PyNative.PyList_Size(Pointer); }
    }
}