using System.Diagnostics;

namespace CeetemSoft.Pyw;

internal class PyDbgMember
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected const string DbgDispNameAttrVal = "{DbgDispName}";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected const string DbgDispTypeAttrVal = "";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DbgDispNameFmt = "{0} [{1}]";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal readonly string Name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string _dbgDispName;

    [DebuggerStepThrough]
    [DebuggerHidden]
    [DebuggerNonUserCode]
    [DebuggerStepperBoundary]
    protected PyDbgMember(string name, string typename)
    {
        Name         = name;
        _dbgDispName = string.Format(DbgDispNameFmt, Name, typename);
    }

    internal string DbgDispName
    {
        get { return _dbgDispName; }
    }
}