using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay(DbgDispVal)]
public readonly struct PyObj : IPyDbgObj
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public const string DbgDispVal = "{ToString(),nq}";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public nint Handle { get; init; }

    public PyObj(nint hObj)
    {
        Handle = hObj;
    }

    public override string ToString()
    {
        return (Debugger.IsAttached ? PyDbg.DbgToString(this) : PyNative.PyObj_NetStr(Handle));
    }

    public static IPyDbgObj FromHandle(nint hObj)
    {
        return new PyObj(hObj);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsValid
    {
        get { return (Handle != PyConst.InvalidHandle); }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public PyDbgMember[] DbgMembers
    {
        get { return PyDbg.GetDbgMembers(this); }
    }
}