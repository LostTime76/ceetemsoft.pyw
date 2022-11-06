namespace CeetemSoft.Pyw;

public class PyBoolDbgViewProvider: IPyDbgViewProvider
{
    public nint GetPyType()
    {
        return PyNative.PyBool_Type();
    }

    public object CreateView(PyObj obj, string name)
    {
        return new PyBoolDbgView(obj, name);
    }

    public string GetValue(PyObj obj)
    {
        return obj.ToString();
    }
}