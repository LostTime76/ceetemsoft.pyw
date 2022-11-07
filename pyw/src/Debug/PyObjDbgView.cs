namespace CeetemSoft.Pyw;

public class PyObjDbgView
{
    public readonly int field = 42;

    public PyObjDbgView(IPyDbgObj obj)
    {
        System.Diagnostics.Debug.WriteLine(obj.ToString());
    }
}