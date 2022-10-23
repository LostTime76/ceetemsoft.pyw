namespace CeetemSoft.Pyw;

public partial class PyInterp
{
    private static class ThrowHelper
    {
        public static string PythonDirNotFound()
        {
            throw new InvalidOperationException("A python directory could not be located within the path environment variable.");
        }

        public static string PythonVerNotFound(string root, string ver)
        {
            throw new InvalidOperationException(string.Format("Could not find python ver: {0} within the directory: {1}", ver, root));
        }
    }
}