namespace CeetemSoft.Pyw;

public readonly partial struct PyStr
{
    private static class ThrowHelper
    {
        private const string TypeName = "Object";

        public static void CreationFailure(string str)
        {
            throw new Exception(string.Format("Could not create a new python string with the value: '{0}'.", str));
        }

        public static void ToStringFailure(PyStr str)
        {
            throw new Exception(string.Format("Could not get the string representation of the python string. str: 0x{0:X}", str.pObj));
        }
    }
}