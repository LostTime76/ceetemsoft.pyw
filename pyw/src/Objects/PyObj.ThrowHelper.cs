namespace CeetemSoft.Pyw;

public partial class PyObj
{
    private static class ThrowHelper
    {
        public static void SetAttrFail(PyObj obj, string attr, PyObj value)
        {
            throw new Exception(string.Format("The attribute: {0} could not be set on the object: 0x{1:X} " +
                "with the object value: 0x{2:X}.", obj.Ptr, attr, value.Ptr));
        }

        public static void ToStrFail(PyObj obj)
        {
            throw new Exception(string.Format("The string representation of the object: 0x{0:X} " +
                "could not be obtained.", obj.Ptr));
        }
    }
}