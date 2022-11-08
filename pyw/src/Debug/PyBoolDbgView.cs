using System.Diagnostics;

namespace CeetemSoft.Pyw;

public class PyBoolDbgView : PyObjDbgView
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected PyBool boolObj;

    public PyBoolDbgView(PyBool obj) : base(obj)
    {
        boolObj = obj;
    }

    public bool IsValid
    {
        get { return boolObj.IsValid; }
    }

    public bool Value
    {
        get { return boolObj.Value; }
        set { boolObj.Value = value; }
    }
}