namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyDict
{
    private static class ThrowHelper
    {
        internal static void ObjNotDict(PyObj obj)
        {
            throw new InvalidOperationException(string.Format("Cannot cast object to dictionary. The object: " +
                " '0x{0:X16}' is not a dictionary.", (long)obj.pObj));
        }
    }
}