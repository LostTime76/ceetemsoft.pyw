using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerTypeProxy(typeof(PyBoolDbgView))]
[DebuggerDisplay(PyObj.DbgDisplayValue, Target=typeof(PyBool))]
public struct PyBool : IPyObj
{
    public const string Typename = "bool";

    private nint _handle;

    public PyBool()           : this(Create(false)) { }
    public PyBool(bool value) : this(Create(value)) { }

    public PyBool(nint hBool)
    {
        _handle = hBool;
    }

    public nint GetHandle()
    {
        return _handle;
    }

    public string GetTypename()
    {
        return Typename;
    }

    public object GetValue()
    {
        return Value;
    }

    public IPyObj NewInst(nint hBool)
    {
        return new PyBool(hBool);
    }

    public override string ToString()
    {
        return PyNative.PyObj_NetStr(_handle);
    }

    public static implicit operator PyObj(PyBool obj)
    {
        return new PyObj(obj._handle);
    }

    public static explicit operator PyBool(PyObj obj)
    {
        return new PyBool(obj.Handle);
    }

    private static nint Create(bool value)
    {
        return PyNative.PyBool_New(value);        
    }

    public bool IsValid
    {
        get { return PyNative.PyBool_CheckType(_handle); }
    }

    public bool Value
    {
        get { return PyNative.PyBool_AsBool(_handle); }
        set
        {
            // Decrement the reference count for the existing object
            PyNative.PyObj_DecRef(_handle);

            // Create a new bool object
            _handle = PyNative.PyBool_New(value);
        }
    }

    public nint Handle
    {
        get { return _handle; }
    }
}