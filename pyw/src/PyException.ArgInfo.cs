namespace CeetemSoft.Pyw;

public sealed partial class PyException : Exception
{
    public readonly struct ArgInfo
    {
        public readonly nint   Ptr;
        public readonly string TypeName;

        public ArgInfo(string typeName, nint ptr = 0)
        {
            Ptr      = ptr;
            TypeName = typeName;
        }
    }
}