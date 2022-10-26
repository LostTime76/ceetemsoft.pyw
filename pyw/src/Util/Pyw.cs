namespace CeetemSoft.Pyw;

using System.Reflection;

public static class Pyw
{
    public static readonly Assembly Assembly    = Assembly.GetExecutingAssembly();
    public static readonly string   AssemblyDir = Path.GetDirectoryName(Assembly.Location);
}