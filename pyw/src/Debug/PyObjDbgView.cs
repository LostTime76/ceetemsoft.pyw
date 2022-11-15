using System.Diagnostics;

namespace CeetemSoft.Pyw;

public sealed class PyObjDbgView
{
    private readonly IPyObj obj;

    internal PyObjDbgView(IPyObj obj)
    {
        this.obj = obj;
    }

    [DebuggerStepThrough]
    [DebuggerHidden]
    [DebuggerNonUserCode]
    [DebuggerStepperBoundary]
    private PyDbgMember[] GetMembers()
    {
        PyDbgMember[] arr = new PyDbgMember[1];

        System.Reflection.PropertyInfo prop = obj.GetType().GetProperty("IsDict");

        arr[0] = new PyNetPropDbgMember(obj, prop);

        return arr;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    internal PyDbgMember[] Members
    {
        get { return GetMembers(); }
    }
}