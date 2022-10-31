namespace CeetemSoft.Pyw;

public interface IPyObj
{
    public nint Pointer { get; }

    public string GetTypeName();
}