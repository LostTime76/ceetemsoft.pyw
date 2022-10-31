namespace CeetemSoft.Pyw;

public sealed partial class PyException : Exception
{
    public readonly ArgInfo[] Args;

    public PyException(string message, params ArgInfo[] args) : base(GetMessage(message, args))
    {
        Args = args;
    }

    private static string GetMessage(string message, ReadOnlySpan<ArgInfo> args)
    {
        return null;
    }
}