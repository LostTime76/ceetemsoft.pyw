using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay("{GetDebugValue()}", Type=nameof(PyObj))]
[DebuggerTypeProxy(typeof(PyObjDbgView))]
public struct PyObj //: IPyDbgObj
{
    public const string Typename = "object";

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

    public object GetDebugValue()
    {
        return null;
    }
}