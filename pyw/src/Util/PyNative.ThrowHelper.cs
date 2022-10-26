namespace CeetemSoft.Pyw;

unsafe public static partial class PyNative
{
    private static class ThrowHelper
    {
        public static void PyUnicode_FromStr_Fail(string str)
        {
            throw new Exception(string.Format("Could not allocate a new string for: '{0}'", str));
        }

        public static void PyUnicode_AsUtf8_Fail()
        {
            throw new Exception("Could not convert the python string object to a .NET string. Make sure the python object is a " +
                "valid string object.");
        }

        public static void PySys_SetAttr_Fail(string key)
        {
            throw new Exception(string.Format("Could not set system object attr: '{0}'.", key));
        }

        public static void PyObj_Str_Fail()
        {
            throw new Exception("Could not convert the object to its string representation.");
        }

        public static void PyObj_SetAttr_Fail(string key = null)
        {
            string fmt = ((key != null) ? "Could not set the object attribute from the object attr key." :
                "Could not set the object attribute from the object attr: '{0}'.");

            throw new Exception(string.Format(fmt, key));
        }

        public static void PyList_GetElt_Fail(int idx)
        {
            throw new Exception(string.Format("Failed to get list element at idx: '{0}'.", idx));
        }

        public static void PyList_SetElt_Fail(int idx)
        {
            throw new Exception(string.Format("Failed to set list element at idx: '{0}'.", idx));
        }

        public static void PyDict_GetElt_Fail()
        {
            throw new Exception("Failed to get dictionary element.");
        }

        public static void PyDict_SetElt_Fail()
        {
            throw new Exception("Failed to set dictionary element.");
        }
    }
}