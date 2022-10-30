namespace CeetemSoft.Pyw;

unsafe internal static partial class PyNative
{
    [AttributeUsage(AttributeTargets.Field)]
    private class PySymbolAttribute : Attribute
    {
        internal readonly string Symbol;

        internal PySymbolAttribute(string symbol = null)
        {
            Symbol = symbol;
        }
    }
}