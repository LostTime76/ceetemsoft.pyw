using System.Runtime.InteropServices;

namespace CeetemSoft.Pyw;

public class PyObject
{
    public static readonly PyObject None = new PyObject();

    internal readonly IntPtr _pObj;

    public PyObject(long value) : this(PyNative.PyLong_FromLong(value)) { }
    public PyObject(string input) : this(PyNative.PyUnicode_FromString(input)) { }

    internal PyObject() : this(IntPtr.Zero) { }

    internal PyObject(IntPtr pObj)
    {
        _pObj = pObj;
    }

    public override string ToString()
    {
        return Marshal.PtrToStringUTF8(PyNative.PyUnicode_AsUTF8(_pObj));
    }

    public PyList AsList()
    {
        return (PyList.IsList(this) ? new PyList(_pObj) : null);
    }
}