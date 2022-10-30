namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PySys
{
    private static class ThrowHelper
    {
        internal static void CannotNew()
        {
            throw new InvalidOperationException("The system module object cannot be created by the user. Use the " +
                "interpreter instance to get a reference to the object.");
        }

        internal static void CannotSetAttr(string attr)
        {
            throw new Exception(string.Format("Could not set the attribute: '{0}' on the system object.", attr));
        }

        internal static void PathIsNotList()
        {
            throw new Exception("The path attribute is not a list. The function expects it to be a list.");
        }
    }
}