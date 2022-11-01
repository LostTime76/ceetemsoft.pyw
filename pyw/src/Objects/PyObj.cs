namespace CeetemSoft.Pyw;

public readonly partial struct PyObj
{
    internal readonly nint pObj;

    public PyObj()
    {
        ThrowHelper.InvalidCreate();
    }

    internal PyObj(nint pObj)
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

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        return ((PyObj)obj == this);
    }

    public override int GetHashCode()
    {
        return pObj.GetHashCode();
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

    unsafe internal static bool IsType(nint pObj, int flags)
    {
        return ((PyNative.PyType_GetFlags(((PyObjBase*)pObj)->pType) & flags) != 0);
    }

    public static bool operator ==(PyObj a, PyObj b)
    {
        return (a.pObj == b.pObj);
    }

    public static bool operator !=(PyObj a, PyObj b)
    {
        return (a.pObj != b.pObj);
    }

    public bool IsValid
    {
        get { return (pObj != 0); }
    }
}