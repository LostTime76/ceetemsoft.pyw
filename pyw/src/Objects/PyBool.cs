using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay(PyObj.DbgDispVal)]
public readonly struct PyBool : IPyDbgObj
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public nint Handle { get; init; }

    public PyBool() : this (PyConst.InvalidHandle) { }

    public PyBool(nint hBool)
    {
        Handle = hBool;
    }

    public override string ToString()
    {
        return Value.ToString().ToLower();
    }

    public static PyBool Create(bool value = false)
    {
        return new PyBool(PyNative.PyBool_New(value));
    }

    public static IPyDbgObj FromHandle(nint hBool)
    {
        return new PyBool(hBool);
    }

    public static implicit operator PyObj(PyBool obj)
    {
        return new PyObj(obj.Handle);
    }

    public static explicit operator PyBool(PyObj obj)
    {
        return new PyBool(obj.Handle);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsValid
    {
        get { return ((Handle != PyConst.InvalidHandle) && PyNative.PyBool_CheckType(Handle)); }
    }

    [PyDbgMember]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool Value
    {
        get { return PyNative.PyBool_AsBool(Handle); }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public PyDbgMember[] DbgMembers
    {
        get { return PyDbg.GetDbgMembers(this); }
    }
}