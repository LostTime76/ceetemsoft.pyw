using System.Collections;
using System.Collections.Generic;

namespace CeetemSoft.Pyw;

public class PyList : PyObject
{
    public PyList(int size = 0) : base(PyNative.PyList_New(size)) { }

    internal PyList(IntPtr pObj) : base(pObj) { }

    public PyList(ReadOnlySpan<string> items) : base(PyNative.PyList_New(0)) { Append(items); }

    public PyList(ReadOnlySpan<PyObject> items) : base(PyNative.PyList_New(0)) { Append(items); }

    public static bool IsList(PyObject obj)
    {
        // Return true until we get functionized type checking in the library...
        return true;
    }

    public bool Append(string item)
    {
        return Append(new PyObject(item));
    }

    public bool Append(PyObject item)
    {
        return (PyNative.PyList_Append(_pObj, item._pObj) == 0);
    }

    public bool Append(ReadOnlySpan<string> items)
    {
        foreach (string item in items)
        {
            if (Append(item) == false)
            {
                return false;
            }
        }

        return true;
    }

    public bool Append(ReadOnlySpan<PyObject> items)
    {
        foreach (PyObject item in items)
        {
            if (Append(item) == false)
            {
                return false;
            }
        }

        return true;
    }

    public bool Clear()
    {
        return SetSlice(0, PyObject.None);
    }

    public bool SetSlice(int start, PyObject items)
    {
        return SetSlice(start, (Size - start), items);
    }

    public bool SetSlice(int start, int length, PyObject items)
    {
        return (PyNative.PyList_SetSlice(_pObj, start, (start + length), items._pObj) == 0);
    }

    public PyObject Slice(int start)
    {
        return Slice(start, (Size - start));
    }

    public PyObject Slice(int start, int length)
    {
        return new PyObject(PyNative.PyList_GetSlice(_pObj, start, (start + length)));
    }

    public string[] AsStrings()
    {
        string[] items = new string[Size];

        for (int idx = 0; idx < items.Length; idx++)
        {
            items[idx] = this[idx].ToString();
        }

        return items;
    }

    public int Size
    {
        get { return PyNative.PyList_Size(_pObj); }
    }

    public PyObject this[int idx]
    {
        get { return new PyObject(PyNative.PyList_GetItem(_pObj, idx)); }
        set { PyNative.PyList_SetItem(_pObj, idx, value._pObj); }
    }
}