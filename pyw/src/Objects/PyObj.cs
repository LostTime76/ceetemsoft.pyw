using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerTypeProxy(typeof(PyObjDbgView))]
[DebuggerDisplay(PyObj.DbgDisplayValue, Target=typeof(PyObj))]
public readonly struct PyObj : IPyObj
{
    public const string Typename        = "object";
    public const string DbgDisplayValue = "{GetValue()}";

    public readonly nint Handle;

    public PyObj(nint hObj)
    {
        Handle = hObj;
    }

    public nint GetHandle()
    {
        return Handle;
    }

    public string GetTypename()
    {
        return Typename;
    }

    public object GetValue()
    {
        return null;
    }

    public IPyObj NewInst(nint hObj)
    {
        return new PyObj(hObj);
    }

    public override string ToString()
    {
        return PyNative.PyObj_NetStr(Handle);
    }
}