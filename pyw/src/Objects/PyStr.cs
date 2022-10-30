namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyStr
{
    internal readonly PyObj* pObj;

    public PyStr(string str)
    {
        int   len  = PyUtil.GetUtf8StrLen(str);
        byte* pStr = stackalloc byte[len + 1];
        PyUtil.StrToUtf8Str(str, pStr, len);

        pObj = PyNative.PyUnicode_FromStr(pStr);

        if (pObj == null)
        {
            ThrowHelper.CouldNotCreate(str);
        }
    }

    internal PyStr(PyObj* pObj)
    {
        this.pObj = pObj;
    }

    public override string ToString()
    {
        return PyObj.ToString(this);
    }

    public static bool IsStr(PyObj obj)
    {
        return PyObj.IsType(obj, PyConst.Py_Unicode_Subclass);
    }

    public static implicit operator PyObj(PyStr str)
    {
        return new PyObj(str.pObj);
    }

    public static explicit operator string(PyStr str)
    {
        return str.ToString();
    }

    public static explicit operator PyStr(PyObj obj)
    {
        if (!IsStr(obj))
        {
            ThrowHelper.ObjNotStr(obj);
        }

        return new PyStr(obj.pObj);
    }
}