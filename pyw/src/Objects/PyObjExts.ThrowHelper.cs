namespace CeetemSoft.Pyw;

public static partial class PyObjExts
{
    private static class ThrowHelper
    {
        public static void ObjNotStr(PyObj obj)
        {
            throw new Exception(string.Format("The object: 0x{0:X} is not a string.", obj.Ptr));
        }
    }
}