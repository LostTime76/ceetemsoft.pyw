namespace CeetemSoft.Pyw;

public readonly partial struct PyObj
{
    private static class ThrowHelper
    {
        private const string TypeName = "Object";

        public static void ToStringFailure(PyObj obj)
        {
            throw new PyException("Could not get the string representation of the python object.", 
                (TypeName, obj.pObj));
        }
    }
}