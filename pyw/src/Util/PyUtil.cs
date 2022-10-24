using System.Reflection;
using System.Runtime.InteropServices;

namespace CeetemSoft.Pyw;

public static class PyUtil
{
    public const char   PathDelim  = ';';
    public const string ExePathFmt = "{0}.exe";
    public const string DllPathFmt = "{0}.dll";
    public const string PathEnvVar = "Path";

    public static readonly Assembly Assembly    = Assembly.GetExecutingAssembly();
    public static readonly string   AssemblyDir = Path.GetDirectoryName(Assembly.Location);
}