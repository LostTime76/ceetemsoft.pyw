using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay("{DbgValue}", Type=nameof(PyObj))]
[DebuggerTypeProxy(typeof(PyObjDbgView))]
public readonly partial struct PyObj
{
    public readonly nint Handle;

    public PyObj() : this(PyConst.InvalidHandle) { }

    public PyObj(nint hObj)
    {
        Handle = hObj;
    }

    public override string ToString()
    {
        return PyNative.PyObj_NetStr(Handle);
    }

    unsafe public static long GetRefCount(PyObj obj)
    {
        return ((obj.Handle != PyConst.InvalidHandle) ? *((nint*)obj.Handle) : PyConst.Error);
    }

    unsafe public long RefCount
    {
        get { return GetRefCount(this); }
    }

    private string DbgValue
    {
        get { return PyDbg.GetValue(this); }
    }
}