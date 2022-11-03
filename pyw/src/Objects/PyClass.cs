namespace CeetemSoft.Pyw;

public abstract class PyClass : PyObj
{
    protected PyClass(nint hObj) : base(hObj) { }
}