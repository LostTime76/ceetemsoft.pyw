using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay("{Value}", Type=nameof(PyBool))]
[DebuggerTypeProxy(typeof(PyBoolDbgView))]
public readonly struct PyBool
{
    public readonly nint Handle;

    public PyBool()           : this(Create(false)) { }
    public PyBool(bool value) : this(Create(value)) { }

    public PyBool(nint hBool)
    {
        Handle = hBool;
    }

    public override string ToString()
    {
        return PyNative.PyObj_NetStr(Handle);
    }

    private static nint Create(bool value)
    {
        return PyNative.PyBool_New(value);
    }

    public static implicit operator PyObj(PyBool obj)
    {
        return new PyObj(obj.Handle);
    }

    public static explicit operator PyBool(PyObj obj)
    {
        return new PyBool(obj.Handle);
    }

    public bool Value
    {
        get { return PyNative.PyBool_AsBool(Handle); }
    }

    public bool IsValid
    {
        get { return PyNative.PyBool_CheckType(Handle); }
    }

    public long RefCount
    {
        get { return PyObj.GetRefCount(this); }
    }
}