using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay(PyObj.DbgDispVal)]
public readonly struct PyBool : IPyDbgObj
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string _PyTypename = "bool";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public nint Handle { get; init; }

    public PyBool()           : this(false) { }
    public PyBool(bool value) : this(PyNative.PyBool_New(value)) { }

    public PyBool(nint hBool)
    {
        Handle = hBool;
    }

    public override string ToString()
    {
        return ((bool)this).ToString().ToLower();
    }

    public static IPyDbgObj FromHandle(nint hBool)
    {
        return new PyBool(hBool);
    }

    public static implicit operator PyBool(bool value)
    {
        return new PyBool(PyNative.PyBool_New(value));
    }

    public static implicit operator bool(PyBool obj)
    {
        return PyNative.PyBool_AsBool(obj.Handle);
    }

    public static implicit operator PyObj(PyBool obj)
    {
        return new PyObj(obj.Handle);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsValid
    {
        get { return ((Handle != PyConst.InvalidHandle) && PyNative.PyBool_CheckType(Handle)); }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string PyTypename
    {
        get { return _PyTypename; }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public PyDbgObj DbgObj
    {
        get { return new PyDbgObj(this); }
    }
}