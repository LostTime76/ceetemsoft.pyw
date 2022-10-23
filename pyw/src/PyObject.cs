using System.Runtime.InteropServices;

namespace CeetemSoft.Pyw;

public class PyObject
{
    public static readonly PyObject Null = new PyObject(IntPtr.Zero); 
    
    internal IntPtr Pointer { get; init; }

    internal PyObject(IntPtr pointer)
    {
        Pointer = pointer;
    }

    public string AsString()
    {
        return AsString(Pointer);
    }

    public PyList AsList()
    {
        return new PyList(Pointer);
    }

    public static string AsString(IntPtr pointer)
    {
        IntPtr i = PyNative.PyUnicode_AsUTF8(pointer);

        return Marshal.PtrToStringUTF8(PyNative.PyUnicode_AsUTF8(pointer));
    }
}