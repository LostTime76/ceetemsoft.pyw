namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyDict
{
    internal readonly PyObj* pObj;

    public PyDict()
    {
        pObj = PyNative.PyDict_New();
    }

    internal PyDict(PyObj* pObj)
    {
        this.pObj = pObj;
    }

    public override string ToString()
    {
        return PyObj.ToString(this);
    }

    public static bool IsDict(PyObj obj)
    {
        return PyObj.IsType(obj, PyConst.Py_Dict_Subclass);
    }

    public static implicit operator PyObj(PyDict dict)
    {
        return new PyObj(dict.pObj);
    }

    public static explicit operator PyDict(PyObj obj)
    {
        if (!IsDict(obj))
        {
            ThrowHelper.ObjNotDict(obj);
        }

        return new PyDict(obj.pObj);
    }

    public PyObj this[string attr]
    {
        get
        {
            int   len   = PyUtil.GetUtf8StrLen(attr);
            byte* pAttr = stackalloc byte[len + 1];
            PyUtil.StrToUtf8Str(attr, pAttr, len);

            return new PyObj(PyNative.PyDict_GetItemStr(pObj, pAttr));
        }
    }
}