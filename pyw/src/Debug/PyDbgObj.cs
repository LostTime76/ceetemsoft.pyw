using System.Diagnostics;
using System.Linq;

namespace CeetemSoft.Pyw;

public class PyDbgObj
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IPyDbgObj _obj;

    public PyDbgObj(IPyDbgObj obj)
    {
        _obj = obj;
    }

    private PyDbgMember[] GetMembers()
    {
        List<PyDbgMember> members = new List<PyDbgMember>();

        // Add built in members
        members.Add(new PyDbgMember(_obj, nameof(IPyDbgObj.Handle), (long)_obj.Handle));

        // Sort the members by name
        return members.OrderBy(member => member.Name).ToArray();
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    internal PyDbgMember[] Members
    {
        get { return GetMembers(); }
    }
}