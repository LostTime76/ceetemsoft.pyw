namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyObj
{
    private static class ThrowHelper
    {
        internal static void CouldNotGetStr(PyObj obj)
        {
            throw new Exception(string.Format("Could not get a string representation of the object: '0x{0:X16}'", (long)obj.pObj));
        }
    }
}