namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyList
{
    internal readonly PyObj* pObj;

    public PyList(int size = 0)
    {
        pObj = PyNative.PyList_New(size);
    }

    internal PyList(PyObj* pObj)
    {
        this.pObj = pObj;
    }

    public void Add(PyObj item)
    {
        if (PyNative.PyList_Append(pObj, item.pObj) != 0)
        {
            ThrowHelper.CouldNotAppend(this, item);
        }
    }

    public static bool IsList(PyObj obj)
    {
        return PyObj.IsType(obj, PyConst.Py_List_Subclass);
    }

    public IEnumerator<PyObj> GetEnumerator()
    {
        for (int idx = 0; idx < Count; idx++)
        {
            yield return this[idx];
        }
    }

    public override string ToString()
    {
        return PyObj.ToString(this);
    }

    public static implicit operator PyObj(PyList list)
    {
        return new PyObj(list.pObj);
    }

    public static explicit operator PyList(PyObj obj)
    {
        if (!IsList(obj))
        {
            ThrowHelper.ObjNotList(obj);
        }

        return new PyList(obj.pObj);
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public int Count
    {
        get { return PyNative.PyList_Size(pObj); }
    }

    public PyObj this[int idx]
    {
        get
        {
            PyObj* pItem = PyNative.PyList_GetItem(pObj, idx);

            if (pItem == null)
            {
                ThrowHelper.CouldNotGetItem(this, idx);
            }

            return new PyObj(pItem);
        }

        set
        {
            if (PyNative.PyList_SetItem(pObj, idx, value.pObj) != 0)
            {
                ThrowHelper.CouldNotSetItem(this, idx, value);
            }
        }
    }
}