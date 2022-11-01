namespace CeetemSoft.Pyw;

public readonly partial struct PyStr
{
    internal readonly nint pObj;

    public PyStr(string str)
    {
        if ((pObj = PyNative.PyUnicode_New(str)) == 0)
        {
            ThrowHelper.CreationFailure(str);
        }
    }

    internal PyStr(nint pObj)
    {
        this.pObj = pObj;
    }

    unsafe public override string ToString()
    {
        byte* pStr = PyNative.PyUnicode_AsUtf8(pObj);

        if (pStr == null)
        {
            ThrowHelper.ToStringFailure(this);
        }

        return new string((sbyte*)pStr);
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
        if (!PyObj.IsType(obj.pObj, PyConst.Py_Unicode_Subclass))
        {
            ThrowHelper.ObjNotStr(obj);
        }

        return new PyStr(obj.pObj);
    }
}