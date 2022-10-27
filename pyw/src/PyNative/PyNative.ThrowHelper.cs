namespace CeetemSoft.Pyw;

unsafe public static partial class PyNative
{
    private static class ThrowHelper
    {
        public static void PyUnicode_FromStr()
        {
            throw new Exception("Could not create a python string object.");
        }

        public static void PyUnicode_ToStr()
        {
            throw new Exception("Could not retrieve character buffer from python string object.");
        }
    }
}