using System.Runtime.InteropServices;

namespace CeetemSoft.Pyw;

public readonly struct PyString
{
    public readonly IntPtr _pObj;

    public PyString(string input)
    {
        _pObj = PyNative.StringToObject(input);
    }

    public PyString(IntPtr pObj)
    {
        _pObj = pObj;
    }

    public override string ToString()
    {
        return string.Empty;
    }

    public static explicit operator PyString(PyObject obj)
    {
        return new PyString(obj._pObj);
    }

    public string Value
    {
        get { return PyNative.ObjectToString(_pObj); }
    }
}