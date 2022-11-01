namespace CeetemSoft.Pyw;

public static partial class PyObjExts
{
    public static PyStr AsStr(this PyObj obj)
    {
        if (!PyObj.IsType(obj, PyConst.Py_Unicode_Subclass))
        {
            ThrowHelper.ObjNotStr(obj);
        }

        return new PyStr(obj.Ptr);
    }
}