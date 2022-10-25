using System.Reflection;

namespace CeetemSoft.Pyw;

public static class Pyw
{
    public static readonly Assembly Assembly    = Assembly.GetExecutingAssembly();
    public static readonly string   AssemblyDir = Path.GetDirectoryName(Assembly.Location);
}