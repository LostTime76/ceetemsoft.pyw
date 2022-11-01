using System.Collections;

namespace CeetemSoft.Pyw;

public readonly partial struct PyList : IList<PyObj>
{
    internal readonly nint pObj;

    public PyList() : this(0) { }

    public PyList(int size)
    {
        if ((pObj = PyNative.PyList_New(size)) == 0)
        {
            ThrowHelper.CreationFailure();
        }
    }

    internal PyList(nint pObj)
    {
        this.pObj = pObj;
    }

    public void SetAttr(string attr, PyObj value)
    {
        if (!PyNative.PyObj_SetAttr(pObj, attr, value.pObj))
        {
            ThrowHelper.SetAttrFailure(this, attr, value);
        }
    }

    public PyObj GetAttr(string attr)
    {
        return new PyObj(PyNative.PyObj_GetAttr(pObj, attr));
    }

    public void Add(PyObj item)
    {
        if (!PyNative.PyList_Append(pObj, item.pObj))
        {
            ThrowHelper.AddFailure(this, item);
        }
    }

    public void Clear()
    {
        if (!PyNative.PyList_SetSlice(pObj, 0, Count, 0))
        {
            ThrowHelper.ClearFailure(this);
        }
    }

    public bool Contains(PyObj item)
    {
        return (IndexOf(item) >= 0);
    }

    public void CopyTo(PyObj[] array, int start)
    {
        for (int idx = 0; idx < Count; idx++)
        {
            array[start++] = this[idx];
        }
    }

    public int IndexOf(PyObj item)
    {
        for (int idx = 0; idx < Count; idx++)
        {
            if (this[idx] == item)
            {
                return idx;
            }
        }

        return PyConst.Error;
    }

    public void Insert(int idx, PyObj item)
    {
        if (!PyNative.PyList_Insert(pObj, idx, item.pObj))
        {
            ThrowHelper.InsertFailure(this, idx, item);
        }
    }

    public bool Remove(PyObj item)
    {
        int idx = IndexOf(item);

        if (idx < 0)
        {
            return false;
        }

        RemoveAt(idx);

        return true;
    }

    public void RemoveAt(int idx)
    {
        if (!PyNative.PyList_SetSlice(pObj, idx, (idx + 1), 0))
        {
            ThrowHelper.RemoveAtFailure(this, idx);
        }
    }

    public IEnumerator<PyObj> GetEnumerator()
    {
        for (int idx = 0; idx < Count; idx++)
        {
            yield return this[idx];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        string str = PyNative.PyObj_NetStr(pObj);

        if (str == null)
        {
            ThrowHelper.ToStringFailure(this);
        }

        return str;
    }

    public static implicit operator PyObj(PyList list)
    {
        return new PyObj(list.pObj);
    }

    public static explicit operator PyList(PyObj obj)
    {
        if (!PyObj.IsType(obj.pObj, PyConst.Py_List_Subclass))
        {
            ThrowHelper.ObjNotList(obj);
        }

        return new PyList(obj.pObj);
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool IsValid
    {
        get { return (pObj != 0); }
    }

    public int Count
    {
        get { return PyNative.PyList_Size(pObj); }
    }

    public PyObj this[int idx]
    {
        get
        {
            nint pItem = PyNative.PyList_GetItem(pObj, idx);

            if (pItem == 0)
            {
                ThrowHelper.GetItemFailure(this, idx);
            }

            return new PyObj(pItem);
        }

        set
        {
            if (!PyNative.PyList_SetItem(pObj, idx, value.pObj))
            {
                ThrowHelper.SetItemFailure(this, idx, value);
            }
        }
    }
}