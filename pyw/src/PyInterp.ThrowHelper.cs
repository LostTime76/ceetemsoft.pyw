namespace CeetemSoft.Pyw;

public sealed partial class PyInterp
{
    private static class ThrowHelper
    {
        public static string DirNotFound()
        {
            throw new ArgumentException("Could not locate a python directory within the path " +
                "environment variable.");
        }
        public static string VerNotFound(string root, string version)
        {
            throw new ArgumentException(string.Format("Could not find a python version: {0} within the " +
                "directory: {1}.", root, version));
        }

        public static string VersionSetWhenStarted()
        {
            throw new InvalidOperationException("The version cannot be set when the interpreter is started.");
        }
    }
}