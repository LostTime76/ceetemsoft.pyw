namespace CeetemSoft.Pyw;

public readonly partial struct PyObj
{
    private static class ThrowHelper
    {
        public static void InvalidCreate()
        {
            throw new Exception("Objects cannot be created this way.");
        }

        public static void SetAttrFailure(PyObj obj, string attr, PyObj value)
        {
            throw new Exception(string.Format("Could not set the value: 0x{0:X} for the attribute: {1} on the " +
                "object: 0x{2:X}.", value.pObj, attr, obj.pObj));
        }

        public static void ToStringFailure(PyObj obj)
        {
            throw new Exception(string.Format("Could not get the string representation of the " +
                "object: 0x{0:X}", obj.pObj));
        }
    }
}