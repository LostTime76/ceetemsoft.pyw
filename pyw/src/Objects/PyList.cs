using System.Collections;

namespace CeetemSoft.Pyw;

public readonly struct PyList : IList<PyObj>
{
    public readonly nint pObj;

    public PyList(nint pObj)
    {
        this.pObj = pObj;
    }
    
    public void Add(PyObj item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(PyObj item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(PyObj[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<PyObj> GetEnumerator()
    {
        for (int idx = 0; idx < Count; idx++)
        {
            yield return this[idx];
        }
    }

    public int IndexOf(PyObj item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, PyObj item)
    {
        throw new NotImplementedException();
    }

    public bool Remove(PyObj item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return PyNative.PyUnicode_AsUtf8(PyNative.PyObj_Str(this));
    }

    public static implicit operator nint(PyList obj)
    {
        return obj.pObj;
    }

    public static implicit operator PyObj(PyList obj)
    {
        return new PyObj(obj);
    }

    public static explicit operator PyList(PyObj obj)
    {
        return new PyList(obj);
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public int Count
    {
        get { return PyNative.PyList_Count(this); }
    }

    public PyObj this[int idx]
    {
        get { return PyNative.PyList_GetElt(this, idx); }
        set { PyNative.PyList_SetElt(this, idx, value); }
    }
}