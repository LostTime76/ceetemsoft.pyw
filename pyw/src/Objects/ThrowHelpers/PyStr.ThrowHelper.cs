namespace CeetemSoft.Pyw;

public readonly partial struct PyStr
{
    private static class ThrowHelper
    {
        public static void CreationFailure(string str)
        {
            throw new Exception(string.Format("Could not create a new python string with the value: '{0}'.", str));
        }

        public static void ToStringFailure(PyStr str)
        {
            throw new Exception(string.Format("Could not get the string representation of the " +
                "string: 0x{0:X}.", str.pObj));
        }

        public static void ObjNotStr(PyObj obj)
        {
            throw new Exception(string.Format("The python object: 0x{0:X} is not a string object.", obj.pObj));
        }
    }
}