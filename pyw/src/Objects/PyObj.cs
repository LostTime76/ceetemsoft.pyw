namespace CeetemSoft.Pyw;

public readonly partial struct PyObj
{
    public const string TypeName = "object";

    internal readonly nint pObj;

    internal PyObj(nint pObj)
    {
        this.pObj = pObj;
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
}