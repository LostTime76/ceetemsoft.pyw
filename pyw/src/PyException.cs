namespace CeetemSoft.Pyw;

using System.Text;

public sealed class PyException : Exception
{
    private const string ArgFmt = "arg[{0}] - Name: '{1}', Pointer: 0x{2:X16}";

    private static readonly string MsgFmt = "{0}" + Environment.NewLine + Environment.NewLine + "{1}";

    public readonly (string TypeName, nint Pointer)[] Args;

    public PyException(string message, params (string, nint)[] args) 
        : base(GetMessage(message, args))
    {
        Args = args;
    }

    private static string GetMessage(string message, ReadOnlySpan<(string, nint)> args)
    {
        string argsText = GetArgsText(args);

        return ((argsText != null) ? string.Format(MsgFmt, argsText) : message);
    }

    private static string GetArgsText(ReadOnlySpan<(string, nint)> args)
    {
        if (args.Length == 0)
        {
            return null;
        }

        StringBuilder text = new StringBuilder();

        for (int idx = 0; idx < args.Length; idx++)
        {
            if (idx != 0)
            {
                text.Append(Environment.NewLine);
            }

            (string TypeName, nint Pointer) arg = args[idx];

            text.AppendFormat(ArgFmt, arg.TypeName, arg.Pointer);
        }

        return text.ToString();
    }
}