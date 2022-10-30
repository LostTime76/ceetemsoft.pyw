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
        return ToString(this);
    }

    public static string ToString(PyObj obj)
    {
        PyObj* pStrObj = PyNative.PyObj_Str(obj.pObj);
        byte*  pStr    = PyNative.PyUnicode_AsUtf8(pStrObj);

        if ((pStrObj == null) || (pStr == null))
        {
            ThrowHelper.CouldNotGetStr(obj);
        }

        return new string((sbyte*)pStr);
    }

    public static bool IsType(PyObj obj, int flags)
    {
        return ((PyNative.PyType_GetFlags(((PyObjBase*)obj.pObj)->pType) & flags) != 0);
    }
}