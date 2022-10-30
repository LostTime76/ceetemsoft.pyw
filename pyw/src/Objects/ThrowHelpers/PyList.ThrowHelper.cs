namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyList
{
    private static class ThrowHelper
    {
        internal static void ObjNotList(PyObj obj)
        {
            throw new InvalidOperationException(string.Format("Cannot cast object to list. The object: " +
                " '0x{0:X16}' is not a list.", (long)obj.pObj));
        }

        internal static void CouldNotGetItem(PyList list, int idx)
        {
            throw new Exception(string.Format("Could not get the item at index: '{0}' within the list: '0x{1:X16}'.", idx, (long)list.pObj));
        }

        internal static void CouldNotAppend(PyList list, PyObj item)
        {
            throw new Exception(string.Format("Could not append the item: '0x{0:X16}' to the list: " +
                "'0x{1:X16}'.", (long)list.pObj, (long)item.pObj));
        }

        internal static void CouldNotSetItem(PyList list, int idx, PyObj item)
        {
            throw new Exception(string.Format("Could not set the item: '0x{0:X16}' at index: '{1}' within the " +
                "list: '0x{2:X16}'.", (long)item.pObj, idx, (long)list.pObj));
        }
    }
}