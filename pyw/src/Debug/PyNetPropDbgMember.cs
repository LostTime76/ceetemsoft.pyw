using System.Diagnostics;
using System.Reflection;

namespace CeetemSoft.Pyw;

[DebuggerDisplay(PyObj.DbgValueFmt, Name=DbgDispNameAttrVal, Type=DbgDispTypeAttrVal)]
internal sealed class PyNetPropDbgMember : PyDbgMember
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IPyObj _obj;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly PropertyInfo _property;

    internal PyNetPropDbgMember(IPyObj obj, PropertyInfo property) 
        : base(property.Name, property.PropertyType.ToString())
    {
        _obj      = obj;
        _property = property;
    }

    public override string ToString()
    {
        return _property.GetValue(_obj).ToString();
    }
}