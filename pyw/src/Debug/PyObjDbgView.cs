using System.Diagnostics;

namespace CeetemSoft.Pyw;

public class PyObjDbgView
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected PyObj obj;

    public PyObjDbgView(PyObj obj)
    {
        this.obj = obj;    
    }

    public nint Handle
    {
        get { return obj.Handle; }
    }
}