namespace CeetemSoft.Pyw;

public readonly struct PyObj
{
    public readonly nint pObj;

    public PyObj(nint pObj)
    {
        this.pObj = pObj;
    }

    public override string ToString()
    {
        return PyNative.PyUnicode_AsUtf8(PyNative.PyObj_Str(this));
    }

    public static implicit operator nint(PyObj obj)
    {
        return obj.pObj;
    }

    public static implicit operator PyObj(nint pObj)
    {
        return new PyObj(pObj);
    }
}