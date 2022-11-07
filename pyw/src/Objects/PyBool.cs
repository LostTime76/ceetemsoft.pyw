using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay("{Value}", Type=nameof(PyBool))]
[DebuggerTypeProxy(typeof(PyBoolDbgView))]
public struct PyBool : IPyDbgObj
{
    public const string Typename = "bool";

    public nint Handle { get; private set; }

    public PyBool()           : this(Create(false)) { }
    public PyBool(bool value) : this(Create(value)) { }

    public PyBool(nint hBool)
    {
        Handle = hBool;
    }

    public void IncRef()
    {
        PyNative.PyObj_IncRef(Handle);
    }

    public void DecRef()
    {
        PyNative.PyObj_DecRef(Handle);
    }

    public nint GetHandle()
    {
        return Handle;
    }

    public string GetTypename()
    {
        return Typename;
    }

    public object GetDebugValue()
    {
        return Value;
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

    public bool IsValid
    {
        get { return PyNative.PyBool_CheckType(Handle); }
    }

    public bool Value
    {
        get { return PyNative.PyBool_AsBool(Handle); }
        set
        {
            // Decrement reference to the existing handle
            PyNative.PyObj_DecRef(Handle);
            
            // Create a new bool object
            Handle = PyNative.PyBool_New(value);
        }
    }
}