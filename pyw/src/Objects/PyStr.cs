namespace CeetemSoft.Pyw;

public partial class PyStr : PyObj
{
    public PyStr(string str) : base(PyNative.PyUnicode_New(str))
    {
        if (Ptr == 0)
        {
            ThrowHelper.CreateFail(str);
        }
    }

    internal PyStr(nint ptr) : base(ptr) { }
}