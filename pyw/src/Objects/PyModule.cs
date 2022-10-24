namespace CeetemSoft.Pyw;

public readonly struct PyModule
{
    public static readonly PyModule None = new PyModule();
    
    internal readonly IntPtr _pObj;

    internal PyModule(IntPtr pObj)
    {
        _pObj = pObj;
    }

    public static PyModule Import(string module)
    {
        return new PyModule(PyNative.PyImport_Import(PyNative.PyUnicode_FromString(module)));
    }
}