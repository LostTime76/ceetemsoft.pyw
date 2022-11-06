using System.Diagnostics;

namespace CeetemSoft.Pyw;

public class PyBoolDbgView : PyObjDbgView
{
    public readonly bool Value;
    public readonly bool IsValid;

    public PyBoolDbgView(PyBool obj) : this(obj, null) { }

    public PyBoolDbgView(PyObj obj, string name) : this((PyBool)obj, name) { }

    public PyBoolDbgView(PyBool obj, string name) : base(obj, name)
    {
        Value   = obj.Value;
        IsValid = obj.IsValid;
    }
}