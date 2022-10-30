namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyStr
{
    private static class ThrowHelper
    {
        internal static void CouldNotCreate(string str)
        {
            throw new Exception(string.Format("Could not create a new string with value: '{0}'.", str));
        }

        internal static void CouldNotGetStr(PyObj obj)
        {
            throw new Exception(string.Format("Could not get a string representation of the object: '0x{0:X16}'", (long)obj.pObj));
        }

        internal static void ObjNotStr(PyObj obj)
        {
            throw new InvalidOperationException(string.Format("Cannot cast object to string. The object: " +
                " '0x{0:X16}' is not a string.", (long)obj.pObj));
        }
    }
}