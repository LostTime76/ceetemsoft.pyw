using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay(PyObj.DbgValueFmt, Type=nameof(PyDict))]
[DebuggerTypeProxy(typeof(PyObjDbgView))]
public readonly struct PyDict : IPyObj
{
    public const string PyTypename = "dict";

    private const string ToStringFmt = "Size = {0}";

    public readonly nint Handle;

    public PyDict() : this(PyNative.PyDict_New()) { }

    public PyDict(nint hDict)
    {
        Handle = hDict;
    }

    public override bool Equals(object obj)
    {
        return ((obj != null) ? (((PyDict)obj).Handle == Handle) : false);
    }

    public override int GetHashCode()
    {
        return Handle.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format(ToStringFmt, Size);
    }

    public nint GetHandle()
    {
        return Handle;
    }

    public string GetPyTypename()
    {
        return PyTypename;
    }

    public static bool operator ==(nint hDict, PyDict dict)
    {
        return (dict.Handle == hDict);
    }

    public static bool operator ==(PyDict dict, nint hDict)
    {
        return (dict.Handle == hDict);
    }

    public static bool operator !=(nint hDict, PyDict dict)
    {
        return (dict.Handle != hDict);
    }

    public static bool operator !=(PyDict dict, nint hDict)
    {
        return (dict.Handle != hDict);
    }

    public bool IsDict
    {
        get { return ((Handle != PyConst.InvalidHandle) && PyNative.PyDict_CheckType(Handle)); }
    }

    public int Size
    {
        get { return PyNative.PyDict_Size(Handle); }
    }
}