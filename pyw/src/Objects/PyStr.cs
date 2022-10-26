namespace CeetemSoft.Pyw;

public readonly struct PyStr
{
    public readonly nint pObj;

    public PyStr(string str)
    {
        pObj = PyNative.PyUnicode_FromStr(str);
    }

    public PyStr(nint pObj)
    {
        this.pObj = pObj;
    }

    public override string ToString()
    {
        return PyNative.PyUnicode_AsUtf8(pObj);
    }

    public static implicit operator nint(PyStr obj)
    {
        return obj.pObj;
    }

    public static implicit operator string(PyStr obj)
    {
        return obj.ToString();
    }

    public static implicit operator PyObj(PyStr obj)
    {
        return new PyObj(obj);
    }

    public static explicit operator PyStr(PyObj obj)
    {
        return new PyStr(obj);
    }
}