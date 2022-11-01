namespace CeetemSoft.Pyw;

public partial class PyObj
{
    internal readonly nint Ptr;

    private PyObj() { }

    internal PyObj(nint ptr)
    {
        Ptr = ptr;
    }

    public bool HasAttr(string attr)
    {
        return PyNative.PyObj_HasAttr(Ptr, attr);
    }

    public PyObj GetAttr(string attr)
    {
        if (attr == null)
        {
            return null;
        }

        // Get the attr value
        nint pVal = PyNative.PyObj_GetAttr(Ptr, attr);

        // If the attr does not exist, null should be returned
        return ((pVal != 0) ? new PyObj(pVal) : null);
    }

    public void SetAttr(string attr, PyObj value)
    {
        // If the value is null, delete the attribute
        if (value == null)
        {
            DelAttr(attr);
        }

        // Otherwise set the attr
        else if (!PyNative.PyObj_SetAttr(Ptr, attr, value.Ptr))
        {
            ThrowHelper.SetAttrFail(this, attr, value);
        }
    }

    public bool DelAttr(string attr)
    {
        return ((attr != null) ? PyNative.PyObj_DelAttr(Ptr, attr) : false);
    }

    public override string ToString()
    {
        string str = PyNative.PyObj_NetStr(Ptr);

        if (str == null)
        {
            ThrowHelper.ToStrFail(this);
        }

        return str;
    }

    unsafe internal static bool IsType(PyObj obj, int flags)
    {
        // Python objects are structures with the first element as the reference count and the second
        // element as a pointer to the object's type structure. Both elements are of native size.
        return ((PyNative.PyType_GetFlags(obj.Ptr + sizeof(nint)) & flags) != 0);
    }
}