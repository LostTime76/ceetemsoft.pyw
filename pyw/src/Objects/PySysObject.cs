namespace CeetemSoft.Pyw;

public class PySysObject : PyObject
{
    private const string PathAttr = "path";
    private const string ArgsAttr = "argv";

    public PyObject Args
    {
        get { return new PyObject(PyNative.PySys_GetObject(ArgsAttr)); }
        set { PyNative.PySys_SetObject(ArgsAttr, value._pObj); }
    }

    public PyObject Path
    {
        get { return new PyObject(PyNative.PySys_GetObject(PathAttr)); }
        set { PyNative.PySys_SetObject(PathAttr, value._pObj); }
    }
}