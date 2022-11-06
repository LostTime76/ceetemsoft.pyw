using System.Diagnostics;

namespace CeetemSoft.Pyw;

public class PyObjDbgView
{
    public readonly long Handle;
    public readonly long RefCount;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public readonly string Name;

    public PyObjDbgView(PyObj obj) : this(obj, null) { }

    public PyObjDbgView(PyObj obj, string name)
    {
        Name     = name;
        Handle   = obj.Handle;
        RefCount = obj.RefCount;
    }
}