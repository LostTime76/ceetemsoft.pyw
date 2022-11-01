namespace CeetemSoft.Pyw;

public readonly partial struct PyList
{
    private static class ThrowHelper
    {
        public static void CreationFailure()
        {
            throw new Exception("Could not create a new python list.");
        }

        public static void SetAttrFailure(PyList list, string attr, PyObj value)
        {
            throw new Exception(string.Format("Could not set the value: 0x{0:X} for the attribute: {1} on the " +
                "list: 0x{2:X}.", value.pObj, attr, list.pObj));
        }

        public static void ToStringFailure(PyList list)
        {
            throw new Exception(string.Format("Could not get the string representation of the " +
                "list: 0x{0:X}.", list.pObj));
        }

        public static void ObjNotList(PyObj obj)
        {
            throw new Exception(string.Format("The python object: 0x{0:X} is not a list object.", obj.pObj));
        }

        public static void AddFailure(PyList list, PyObj obj)
        {
            throw new Exception(string.Format("Could not append the item: 0x{0:X} to the end of the " +
                "list: 0x{1:X}.", obj.pObj, list.pObj));
        }

        public static void ClearFailure(PyList list)
        {
            throw new Exception(string.Format("Could not clear the list: 0x{0:X}.", list.pObj));
        }

        public static void InsertFailure(PyList list, int idx, PyObj obj)
        {
            throw new Exception(string.Format("Could not insert the item: 0x{0:X} into the list: 0x{1:X} " +
                "at index: {0}.", obj.pObj, list.pObj, idx));
        }

        public static void RemoveAtFailure(PyList list, int idx)
        {
            throw new Exception(string.Format("Could not remove the item at index: {0} from the " +
                "list: 0x{1:X}.", idx, list.pObj));
        }

        public static void GetItemFailure(PyList list, int idx)
        {
            throw new Exception(string.Format("Could not get the item at index: {0} from the " +
                "list: 0x{1:X}.", idx, list.pObj));
        }

        public static void SetItemFailure(PyList list, int idx, PyObj obj)
        {
            throw new Exception(string.Format("Could not set the item: 0x{0:X} at index: {1} within the " +
                "list: 0x{2:X}.", obj.pObj, idx, list.pObj));
        }
    }
}