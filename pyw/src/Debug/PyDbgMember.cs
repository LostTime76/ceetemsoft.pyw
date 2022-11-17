using System.Diagnostics;

namespace CeetemSoft.Pyw;

[DebuggerDisplay(PyObj.DbgDispVal, Name=DbgDispName, Type=DbgDispType)]
internal class PyDbgMember
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected const string DbgDispName = "{DispName,nq}";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected const string DbgDispType = "";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DbgDispNameFmt = "{0} [{1}]";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal readonly string Name;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal readonly string DispName;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal readonly string Value;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal readonly IPyDbgObj Obj;

    internal PyDbgMember(IPyDbgObj obj, string name, object value)
        : this(obj, name, value.GetType().Name, value.ToString()) { }

    internal PyDbgMember(IPyDbgObj obj, string name, string typename, string value = null)
    {
        Name     = name;
        Value    = value;
        Obj      = obj;
        DispName = string.Format(DbgDispNameFmt, name, typename);
    }

    public override string ToString()
    {
        return Value;
    }
}