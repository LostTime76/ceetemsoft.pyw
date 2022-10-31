namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyObj
{
    internal readonly PyObj* pObj;

    internal PyObj(PyObj* pObj)
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

    internal static bool IsType(PyObj obj, int flags)
    {
        return ((PyNative.PyType_GetFlags(((PyObjBase*)obj.pObj)->pType) & flags) != 0);
    }
}